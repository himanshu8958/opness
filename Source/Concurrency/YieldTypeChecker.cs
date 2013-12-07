﻿#if QED

using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using Microsoft.Boogie;
using Microsoft.Automata;

using System.Diagnostics.Contracts;
using Microsoft.Boogie.AbstractInterpretation;
using Microsoft.Boogie.GraphUtil;

namespace Microsoft.Boogie
{
    /*
     Summary:
     * 
     */
    class YieldTypeChecker
    {
        
          CharSetSolver yieldTypeCheckerAutomatonSolver;
          string yieldTypeCheckerRegex = @"^((1|2)+(3|4))*((D)+(((5|6))+((7|8))+((1|2))+((3|4)))*[A]((9)+(7)+(3))*)*$";
          Char[][] yieldTypeCheckerVocabulary;
          BvSet yielTypeCheckerBvSet;
          Automaton<BvSet> yieldTypeCheckerAutomaton;
          

        private YieldTypeChecker()
        {
            
            yieldTypeCheckerAutomatonSolver = new CharSetSolver(BitWidth.BV7);
            yieldTypeCheckerVocabulary = new char[][] {
                new char[]{'1','2'},
                new char[]{'3','4'},
                new char[]{'5','6'},
                new char[]{'7','8'},
                new char[]{'9','A'},
                new char[]{'B','C'},
                new char[]{'D','E'}
            };
            yielTypeCheckerBvSet = yieldTypeCheckerAutomatonSolver.MkRangesConstraint(false, yieldTypeCheckerVocabulary);
            yieldTypeCheckerAutomaton = yieldTypeCheckerAutomatonSolver.Convert(yieldTypeCheckerRegex); //accepts strings that match the regex r

            
        }
        public bool IsYieldTypeSafe(Automaton<BvSet> implTypeCheckAutomaton){

            Automaton<BvSet> complYieldTypeSafeProp = this.yieldTypeCheckerAutomaton.Complement(this.yieldTypeCheckerAutomatonSolver);
            
            return true;
        }

        public static void AddChecker(LinearTypeChecker linearTypeChecker)
        {
            Program yielTypeCheckedProgram = linearTypeChecker.program;
            YieldTypeChecker regExprToAuto = new YieldTypeChecker();
            foreach (var decl in yielTypeCheckedProgram.TopLevelDeclarations)
            {
                Implementation impl = decl as Implementation;
                if (impl != null)
                {
                    if (impl.Blocks.Count > 0)
                    {
                        int phaseNumSpecImpl = 0;
                        foreach (Ensures e in impl.Proc.Ensures)
                        {
                            phaseNumSpecImpl = QKeyValue.FindIntAttribute(e.Attributes, "phase", int.MaxValue);

                        }
                        YieldTypeCheckerCore.TypeCheck(impl, phaseNumSpecImpl);


                    }
                }
            }
        }
    }


    class YieldTypeCheckerCore : StandardVisitor
    {

        CheckingContext checkingContext;
        int errorCount;


        Implementation ytypeChecked;
        // int numCallInEncImpl; // number of callcmds in impl
        int specPhaseNumImpl; // impl.proc's spec num
        int yTypeCheckCurrentPhaseNum;// current phase of yield type checking
        int programCounter; // initial state of the impl
        int concreteInitialState; // First seen initial state in an implementation

        HashSet<CallCmd> callCmdsInImpl; //  callCmdsInImpl[Implementation] ==> Set = { call1, call2, call3 ... }
        int numCallCmdInEnclosingProc; // Number of CallCmds in       

        Dictionary<Tuple<int, int>, string> verticesToEdges; // Tuple<int,int> --> "Y" : State(k) --Y--> State(k+1)
        HashSet<Tuple<int, int>> yieldTypeCheckGraph; // (-inf ph0 ] (ph0 ph1] (ph1 ph2] ..... (phk phk+1] where phk+1 == atcSpecPhsNumTypeCheckedProc
        // Dictionary<Implementation, Automaton<YieldTypeSet>> yieldTypeCheckAutomaton; // To be generated with input yieldTypeCheckGraph

        List<Tuple<int, int>> phaseIntervals; // (-inf ph0 ] (ph0 ph1] (ph1 ph2] ..... (phk phk+1] intervals

        
        private YieldTypeCheckerCore(Implementation toTypeCheck, int specPhaseNum)
        {
            ytypeChecked = toTypeCheck;
            this.errorCount = 0;
            this.checkingContext = new CheckingContext(null);
            // numCallInEncImpl = 0;
            specPhaseNumImpl = specPhaseNum;
            yTypeCheckCurrentPhaseNum = 0;
            programCounter = Math.Abs(Guid.NewGuid().GetHashCode());
            concreteInitialState = programCounter;

            /*Utility Maps*/
            phaseIntervals = new List<Tuple<int, int>>();
            callCmdsInImpl = new HashSet<CallCmd>();
            numCallCmdInEnclosingProc = 0;

            // Graph and Automaton
            yieldTypeCheckGraph = new HashSet<Tuple<int, int>>();
            verticesToEdges = new Dictionary<Tuple<int, int>, string>();
        }


        // return phase intervals
        private void CalculateCallCmds(Implementation impl)
        {
            List<CallCmd> callCmdInBodyEncImplList = new List<CallCmd>();
            
            foreach (Block b in impl.Blocks)
            {
                List<Cmd> cmds = new List<Cmd>();
                for (int i = 0; i < b.Cmds.Count; i++)
                {
                    Cmd cmd = b.Cmds[i];
                    if (cmd is CallCmd)
                    {
                        CallCmd callCmd = b.Cmds[i] as CallCmd;
                        numCallCmdInEnclosingProc = numCallCmdInEnclosingProc + 1;



                        if (!(callCmdsInImpl.Contains(callCmd)))
                        {
                            callCmdsInImpl.Add(callCmd);
                            callCmdInBodyEncImplList.Add(callCmd);// 

                        }

                    }
                }

            }

            //Sort all PhaseNum of callCmds inside Implementation 
            List<int> callCmdPhaseNumIndexList = new List<int>();
            for (int i = 0; i < callCmdInBodyEncImplList.Count; i++)
            {
                int tmpPhaseNum = QKeyValue.FindIntAttribute(callCmdInBodyEncImplList[i].Proc.Attributes, "phase", 0);
                callCmdPhaseNumIndexList.Add(tmpPhaseNum);
            }

            callCmdPhaseNumIndexList.Sort();

            //Create Phase Intervals
            for (int i = 0; i < callCmdPhaseNumIndexList.Count; i++)
            {
                //set the initial 
                if (i == 0)
                {
                    Tuple<int, int> initTuple = new Tuple<int, int>(int.MinValue, callCmdPhaseNumIndexList[i]);
                    phaseIntervals.Add(initTuple);
                }
                else
                {
                    Tuple<int, int> intervalToInsert = new Tuple<int, int>(callCmdPhaseNumIndexList[i - 1] + 1, callCmdPhaseNumIndexList[i]);
                    phaseIntervals.Add(intervalToInsert);
                }
            }
            
        }

        public Automaton<YieldTypeSet> YieldTypeCheckAutomata(YieldTypeCheckerCore yieldTypeCheckerPerImpl)
        {              
            
            for (int i = 0; i<this.phaseIntervals.Count ; i++) {
                yTypeCheckCurrentPhaseNum =  this.phaseIntervals[i].Item2 - 1;
            }

            yieldTypeCheckerPerImpl.BuildAutomatonGraph();
            //Console.Write(yieldTypeCheckerPerImpl.PrintGraph(yieldTypeCheckerPerImpl.GetTuplesForGraph()));
            yieldTypeCheckerPerImpl.PostProcessGraph(yieldTypeCheckerPerImpl.GetTuplesForGraph());
            return yieldTypeCheckerPerImpl.BuildAutomaton(yieldTypeCheckerPerImpl.GetTuplesForGraph());
            
        }

        public static Automaton<YieldTypeSet> TypeCheck(Implementation impl, int specPhsNum)
        {
            YieldTypeCheckerCore yieldTypeCheckerPerImpl = new YieldTypeCheckerCore(impl, specPhsNum);
            return yieldTypeCheckerPerImpl.YieldTypeCheckAutomata(yieldTypeCheckerPerImpl);
        }

      


        private void BuildAutomatonGraph()
        {
            foreach (Block block in ytypeChecked.Blocks)
            {

                for (int i = 0; i < block.Cmds.Count; i++)
                {
                    AddNodeToGraph(block.Cmds[i]);
                }

                if (block.TransferCmd is GotoCmd)
                {
                    GotoCmd gt = block.TransferCmd as GotoCmd;
                    AddNodeToGraphForGotoCmd(gt);
                }
                else if (block.TransferCmd is ReturnCmd)
                {
                    ReturnCmd rt = block.TransferCmd as ReturnCmd;
                    AddNodeToGraphForReturnCmd(rt);
                }
            }


        }

        private Graph<int> GetTuplesForGraph()
        {
            Graph<int> graphFromTuples = new Graph<int>(yieldTypeCheckGraph);
            return graphFromTuples;

        }

        private void AddNodeToGraph(Cmd cmd)
        {
            if (cmd is AssignCmd)
            {
                AssignCmd assignCmd = cmd as AssignCmd;
                AddNodeToGraphForAssignCmd(assignCmd);
            }
            else if (cmd is HavocCmd)
            {
                HavocCmd havocCmd = cmd as HavocCmd;
                AddNodeToGraphForHavocCmd(havocCmd);
            }
            else if (cmd is AssumeCmd)
            {
                AssumeCmd assumeCmd = cmd as AssumeCmd;
                AddNodeToGraphForAssumeCmd(assumeCmd);
            }
            else if (cmd is AssertCmd)
            {
                AssertCmd assertCmd = cmd as AssertCmd;
                AddNodeToGraphForAssertCmd(assertCmd);
            }
            else if (cmd is YieldCmd)
            {
                YieldCmd yieldCmd = cmd as YieldCmd;
                AddNodeToGraphForYieldCmd(yieldCmd);

            }
            else if (cmd is CallCmd)
            {

                CallCmd callCmd = cmd as CallCmd;
                AddNodeToGraphForCallCmd(callCmd, yTypeCheckCurrentPhaseNum);

            }
        }

        private void AddNodeToGraphForGotoCmd(GotoCmd cmd)
        {
            int source = programCounter;
            int dest = programCounter;//Math.Abs(Guid.NewGuid().GetHashCode());
            programCounter = dest;

            // Create a Epsilon transition between them
            Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
            yieldTypeCheckGraph.Add(srcdst);
            verticesToEdges[srcdst] = "N";
        }

        private void AddNodeToGraphForReturnCmd(ReturnCmd cmd)
        {

            // Do nothing !

        }

        private void AddNodeToGraphForYieldCmd(YieldCmd cmd)
        {

            int source = programCounter;
            int dest = Math.Abs(Guid.NewGuid().GetHashCode());
            programCounter = dest;

            Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
            yieldTypeCheckGraph.Add(srcdst);
            verticesToEdges[srcdst] = "Y";

        }

        private void AddNodeToGraphForAssignCmd(AssignCmd cmd)
        {

            int source = programCounter;
            int dest = Math.Abs(Guid.NewGuid().GetHashCode());
            programCounter = dest;

            Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
            yieldTypeCheckGraph.Add(srcdst);
            verticesToEdges[srcdst] = "Q";
        }

        private void AddNodeToGraphForHavocCmd(HavocCmd cmd)
        {

            int source = programCounter;
            int dest = Math.Abs(Guid.NewGuid().GetHashCode());
            programCounter = dest;

            Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
            yieldTypeCheckGraph.Add(srcdst);
            verticesToEdges[srcdst] = "Q";

        }

        private void AddNodeToGraphForAssumeCmd(AssumeCmd cmd)
        {

            int source = programCounter;
            int dest = Math.Abs(Guid.NewGuid().GetHashCode());
            programCounter = dest;

            Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
            yieldTypeCheckGraph.Add(srcdst);
            verticesToEdges[srcdst] = "P";

        }

        private void AddNodeToGraphForAssertCmd(AssertCmd cmd)
        {
            int source = programCounter;
            int dest = Math.Abs(Guid.NewGuid().GetHashCode());
            // Create a Epsilon transition between them
            programCounter = dest;

            Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
            yieldTypeCheckGraph.Add(srcdst);
            verticesToEdges[srcdst] = "N";

        }


        private void AddNodeToGraphForCallCmd(CallCmd cmd, int currentCheckPhsNum)
        {
            int callePhaseNum = 0;
            
            foreach (Ensures e in cmd.Proc.Ensures)
            {
                callePhaseNum = QKeyValue.FindIntAttribute(e.Attributes, "phase", int.MaxValue);
            }

            if (callePhaseNum > currentCheckPhsNum)
            {
                int source = Math.Abs(Guid.NewGuid().GetHashCode());
                programCounter = source;
            }
            else
            {
          

                foreach (Ensures e in cmd.Proc.Ensures)
                {
                    if (QKeyValue.FindBoolAttribute(e.Attributes, "atomic"))
                    {
                        int source = programCounter;
                        int dest = Math.Abs(Guid.NewGuid().GetHashCode());
                        programCounter = dest;
                        Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
                        yieldTypeCheckGraph.Add(srcdst);
                        verticesToEdges[srcdst] = "A";

                    }
                    else if (QKeyValue.FindBoolAttribute(e.Attributes, "right"))
                    {
                        int source = programCounter;
                        int dest = Math.Abs(Guid.NewGuid().GetHashCode());
                        programCounter = dest;
                        Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
                        yieldTypeCheckGraph.Add(srcdst);
                        verticesToEdges[srcdst] = "R";
                    }
                    else if (QKeyValue.FindBoolAttribute(e.Attributes, "left"))
                    {
                        int source = programCounter;
                        int dest = Math.Abs(Guid.NewGuid().GetHashCode());
                        programCounter = dest;
                        Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
                        yieldTypeCheckGraph.Add(srcdst);
                        verticesToEdges[srcdst] = "L";
                    }
                    else if (QKeyValue.FindBoolAttribute(e.Attributes, "both"))
                    {

                        int source = programCounter;
                        int dest = Math.Abs(Guid.NewGuid().GetHashCode());
                        programCounter = dest;
                        Tuple<int, int> srcdst = new Tuple<int, int>(source, dest);
                        yieldTypeCheckGraph.Add(srcdst);
                        verticesToEdges[srcdst] = "B";
                    }

                }

            }

        }


        private Graph<int> CreateGrapFromHashSet(HashSet<Tuple<int, int>> grph)
        {

            Graph<int> graphForAutomaton = new Graph<int>(grph);

            // Do Some transformation on graph here // 

            return graphForAutomaton;

        }
        public string PrintGraph(Graph<int> graph)
        {
            var s = new StringBuilder();
            s.AppendLine("\nImplementation " + ytypeChecked.Proc.Name + " digraph G {");
            foreach (var e in graph.Edges)
                s.AppendLine("  \"" + e.Item1.ToString() + "\" -- " + verticesToEdges[e] + " --> " + "  \"" + e.Item2.ToString() + "\";");
            s.AppendLine("}");

            return s.ToString();

        }
        
        private HashSet<Tuple<int,int>> CollectBackEdges(Graph<int> g, Tuple<int,int> backEdge)
        {           
            int n = backEdge.Item1;
            int d = backEdge.Item2;

            HashSet<int> loop = new HashSet<int>();
            Stack<int> stack = new Stack<int>();
            HashSet<Tuple<int,int>> yieldReachingEdges = new HashSet<Tuple<int,int>>();
            loop.Add(d);
            if (!n.Equals(d)) // then n is not in loop
            {
                loop.Add(n);
                stack.Push(n); // push n onto stack
            }
            while (stack.Count > 0) // not empty
            {
                int m = stack.Peek();
              //  Console.Write("\n Back is : " + m.ToString()+ "\n");
                stack.Pop(); // pop stack
                foreach (int p in g.Predecessors(m))
                {
                   // Contract.Assert(p!= null);
                    if (!(loop.Contains(p)))
                    {
                        Tuple<int , int > yieldReaching = new Tuple<int,int>(p,m);
                        yieldReachingEdges.Add(yieldReaching);
                        //Console.Write("\n "+ p.ToString()+ " --> " + m.ToString() + "\n"); 
                        loop.Add(p);
                        stack.Push(p); // push p onto stack
                    }
                }
            }
            return yieldReachingEdges;
        }
       
        private HashSet<Tuple<int,int>> BackwardEdgesOfYields(Graph<int> graph)
        {
            HashSet<Tuple<int, int>> yieldTrueEdges = new HashSet<Tuple<int, int>>();
            
            foreach(var e in graph.Edges){
               
               
                if(verticesToEdges[e] == "Y"){
                   HashSet<Tuple<int,int>> yieldReachingEdges= CollectBackEdges(graph, e);
                   foreach(Tuple<int,int> yldrch in yieldReachingEdges){
                       // Console.Write("\n" + " From :"+yldrch.Item1.ToString() + " To :"+yldrch.Item2.ToString()+"\n");
                        yieldTrueEdges.Add(yldrch);
                    }
                }
            }
            return yieldTrueEdges;
        }

        private void PostProcessGraph(Graph<int> graph)
        {
            HashSet<Tuple<int, int>> yieldTrueEdges = BackwardEdgesOfYields(graph);


            foreach (Tuple<int, int> yldrch in yieldTrueEdges)
            {

                if (verticesToEdges[yldrch] == "Q")
                {
                    verticesToEdges[yldrch] = "3";
                }
                else if (verticesToEdges[yldrch] == "P")
                {
                    verticesToEdges[yldrch] = "1";
                }
                else if (verticesToEdges[yldrch] == "B")
                {
                    verticesToEdges[yldrch] = "7";
                }
                else if (verticesToEdges[yldrch] == "R")
                {
                    verticesToEdges[yldrch] = "5";
                }
                else if (verticesToEdges[yldrch] == "L")
                {
                    verticesToEdges[yldrch] = "9";
                }
                else if (verticesToEdges[yldrch] == "A")
                {
                    verticesToEdges[yldrch] = "A";
                }
                else if (verticesToEdges[yldrch] == "Y")
                {
                    verticesToEdges[yldrch] = "D";
                }



            }
            foreach (Tuple<int, int> nyldrch in yieldTypeCheckGraph)
            {

                if (!yieldTrueEdges.Contains(nyldrch))
                {

                    if (verticesToEdges[nyldrch] == "Q")
                    {
                        verticesToEdges[nyldrch] = "4";
                    }
                    else if (verticesToEdges[nyldrch] == "P")
                    {
                        verticesToEdges[nyldrch] = "2";
                    }
                    else if (verticesToEdges[nyldrch] == "B")
                    {
                        verticesToEdges[nyldrch] = "8";
                    }
                    else if (verticesToEdges[nyldrch] == "R")
                    {
                        verticesToEdges[nyldrch] = "6";
                    }
                    else if (verticesToEdges[nyldrch] == "L")
                    {
                        verticesToEdges[nyldrch] = "C";
                    }
                    else if (verticesToEdges[nyldrch] == "A")
                    {
                        verticesToEdges[nyldrch] = "B";
                    }                   
                    
                }

            }
        }

        private int[] ComputeFinalStates(Graph<int> graph)
        {

            List<int> finalStatesList = new List<int>();
            
            foreach (int fe in graph.Nodes)
            {
                foreach (Tuple<int, int> e in graph.Edges)
                {
                    if (!fe.Equals(e.Item1))
                    {
                        finalStatesList.Add(fe);
                    }
                }
            }
            int[] finalStates = new int[finalStatesList.Count];

            for (int i = 0; i < finalStatesList.Count; i++) {
                finalStates[i] = finalStatesList[i];            
            }

            return finalStates;
        }

        private List<int> ComputeInitalStates(Graph<int> graph) {

            List<int> initialStates = new List<int>();
            foreach (int fe in graph.Nodes)
            {
                foreach (Tuple<int, int> e in graph.Edges)
                {
                    if (!fe.Equals(e.Item2))
                    {
                        initialStates.Add(fe);
                    }
                }
            }
            return initialStates;

        }
        
        private Automaton<YieldTypeSet> BuildAutomaton(Graph<int> graph) {
            
            List<Move<YieldTypeSet>> transitions = new List<Move<YieldTypeSet>>();
         //   var solver = new YieldTypeSetSolver();

            
            foreach (Tuple<int, int> e in graph.Edges) {
                
                if (verticesToEdges[e] == "3")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.Q)));
                }
                else if (verticesToEdges[e] == "1")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.P)));
                    
                }
                else if (verticesToEdges[e] == "7")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.B)));
                }
                else if (verticesToEdges[e] == "5")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.R)));
                }
                else if (verticesToEdges[e] == "9")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.L)));   
                }
                else if (verticesToEdges[e] == "A")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.A)));
                }
                else if (verticesToEdges[e] == "D")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.Y)));
                }
                else if (verticesToEdges[e] == "4")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.QF)));
                }
                else if (verticesToEdges[e] == "2")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.PF)));
                }
                else if (verticesToEdges[e] == "8")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.BF)));
                }
                else if (verticesToEdges[e] == "6")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.RF)));
                }
                else if (verticesToEdges[e] == "C")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.LF)));
                }
                else if (verticesToEdges[e] == "B")
                {
                    transitions.Add(Move<YieldTypeSet>.M(e.Item1, e.Item2, new YieldTypeSet(Transitions.AF)));    
               
                }
                else if (verticesToEdges[e] == "N") { 
                      transitions.Add(Move<YieldTypeSet>.Epsilon(e.Item1,e.Item2));  
                
                }
            
            }

            // get initial states 
            List<int> initialStates = ComputeInitalStates(graph);
            // get final states
            int[] finalStates = ComputeFinalStates(graph);
            // put Epslion from first initial state seen to other initial states created

            foreach (int s in initialStates) {
                // Put every one epsilon to itself no problem
                /*if (!s.Equals(concreteInitialState)) { }*/
                transitions.Add(Move<YieldTypeSet>.Epsilon(concreteInitialState,s));
            }


            //create Automaton
            Automaton<YieldTypeSet> yieldTypeCheckAutomaton = Automaton<YieldTypeSet>.Create(concreteInitialState, finalStates, transitions); ;
            
            return yieldTypeCheckAutomaton;

        }


        
        private void Error(Absy node, string message)
        {
            checkingContext.Error(node, message);
            errorCount++;
        }
 


    }

    static class Transitions
    {

        public static uint P = 0x1;
        public static uint Q = 0x4;
        public static uint Y = 0x1000;
        public static uint R = 0x10;
        public static uint B = 0x40;
        public static uint A = 0x200;
        public static uint L = 0x100;
        public static uint PF = 0x2;
        public static uint QF = 0x8;
        public static uint BF = 0x80;
        public static uint RF = 0x20;
        public static uint LF = 0x800;
        public static uint AF = 0x400;


    }

    public class YieldTypeSet
    {
        uint elems;

        static internal YieldTypeSet Empty = new YieldTypeSet(0);
        static internal YieldTypeSet Full = new YieldTypeSet(uint.MaxValue);

        public YieldTypeSet(uint elems)
        {
            this.elems = elems;
        }

        public override bool Equals(object obj)
        {
            YieldTypeSet s = obj as YieldTypeSet;
            if (s == null)
                return false;
            return elems == s.elems;
        }

        public override int GetHashCode()
        {
            return (int)elems;
        }

        public YieldTypeSet Union(YieldTypeSet s)
        {
            return new YieldTypeSet(elems | s.elems);
        }

        public YieldTypeSet Intersect(YieldTypeSet s)
        {
            return new YieldTypeSet(elems & s.elems);
        }

        public YieldTypeSet Complement()
        {
            return new YieldTypeSet(~elems);
        }

        public override string ToString()
        {
            return string.Format("YieldTypeSet(0x{0})", elems.ToString("X"));
        }
    }


    public class YieldTypeSetSolver : IBoolAlgMinterm<YieldTypeSet>
    {

        MintermGenerator<YieldTypeSet> mtg; //use the default built-in minterm generator
        YieldTypeSetSolver()
        {
            //create the minterm generator for this solver
            mtg = new MintermGenerator<YieldTypeSet>(this);
        }

        public bool AreEquivalent(YieldTypeSet predicate1, YieldTypeSet predicate2)
        {
            return predicate1.Equals(predicate2);
        }

        public YieldTypeSet False
        {
            get { return YieldTypeSet.Empty; }
        }

        public YieldTypeSet MkAnd(IEnumerable<YieldTypeSet> predicates)
        {
            YieldTypeSet res = YieldTypeSet.Full;
            foreach (var s in predicates)
                res = res.Intersect(s);
            return res;
        }

        public YieldTypeSet MkNot(YieldTypeSet predicate)
        {
            return predicate.Complement();
        }

        public YieldTypeSet MkOr(IEnumerable<YieldTypeSet> predicates)
        {
            YieldTypeSet res = YieldTypeSet.Empty;
            foreach (var s in predicates)
                res = res.Union(s);
            return res;
        }

        public YieldTypeSet True
        {
            get { return YieldTypeSet.Full; }
        }

        public bool IsSatisfiable(YieldTypeSet predicate)
        {
            return !predicate.Equals(YieldTypeSet.Empty);
        }

        public YieldTypeSet MkAnd(YieldTypeSet predicate1, YieldTypeSet predicate2)
        {
            return predicate1.Intersect(predicate2);
        }

        public YieldTypeSet MkOr(YieldTypeSet predicate1, YieldTypeSet predicate2)
        {
            return predicate1.Union(predicate2);
        }

        public IEnumerable<Pair<bool[], YieldTypeSet>> GenerateMinterms(params YieldTypeSet[] constraints)
        {
            return mtg.GenerateMinterms(constraints);
        }
        public YieldTypeSet Simplify(YieldTypeSet s)
        {
            return s;

        }
    }
}

#endif