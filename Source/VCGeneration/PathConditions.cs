using System;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.IO;
using Microsoft.Boogie;
using Microsoft.Boogie.GraphUtil;
using System.Diagnostics.Contracts;
using Microsoft.Basetypes;
using Microsoft.Boogie.VCExprAST;
//using Microsoft.Boogie.SMTLib;
using VC;

namespace VCGeneration {
    using Bpl = Microsoft.Boogie;
    class InferInvariant {
        private HashSet<VCExpr> pathConditions; // path conditions of all the paths in foo
        public void setPathConditions ( HashSet<VCExpr> pc ) {
            this.pathConditions = pc;
        }

        //public VCExpr getPost ( VCExpr inState ) {
        //    return null;
        //
        /**
         * Returns the state that holds after executing the path whose path condition is pc
         * when the input state is inState.
         */
        private VCExpr getPost ( VCExpr inState,VCExpr pc ) {
            //check if inState and pc is  SAT
            return null;
        }
    }
    class PathConditionsVC {
        private static Program prog;
        private static bool PrintPath = false;
        private static bool printPC = true;
        public static ProverInterface prover;



        internal static void printPathConditions ( HashSet<VCExpr> PCs ) {
            //PCHandler.printPathConditions(PCs);
            foreach (VCExpr expr in PCs) {
                
                //Microsoft.Boogie.SMTLib.SMTLibLineariser.
                //Contract.Requires(e != null);
                //Contract.Requires(namer != null);
                //Contract.Ensures(Contract.Result<string>() != null);

                //StringWriter sw = new StringWriter();
                //SMTLibExprLineariser lin = new SMTLibExprLineariser(sw,namer,opts);
                //Contract.Assert(lin != null);
                //lin.Linearise(e,LineariserOptions.Default);
                
            }

        }

        /// <summary>
        /// This method generates the path conditions with the invariants adapted for the new version of the global vairable after each methdo call.
        ///
        /// known issues :
        /// 1. if g0 is the latest version of g before method call. after the method call inv[g/g1] is placed. But after that if
        /// there is write to g. then it is to g2. but this should not cause problems.
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="source"></param>
        /// <param name="invariant"></param>
        /// <returns></returns>
        internal static HashSet<VCExpr> getPathConditions ( Implementation impl,Block source,VCExpr invariant ) {
            Graph<Block> g = Program.GraphFromImpl(impl);
            HashSet<VCExpr> ret = new HashSet<VCExpr>();
            g.ComputeLoops();
            HashSet<List<Block>> paths = getPaths(g,source);
            paths = unbraidPaths(paths);
            foreach (List<Block> path in paths) {
                VCExpr pc = getPathCondition(path,impl,invariant);
                ret.Add(pc);
            }
            return ret;
        }
		// Represets the mathematical function assumed to be equivalent to the callee
        static Function calledFunctionInCode = null;
		/// Represents the mathematical function assumed to be equivalent to the caller
        static Function callerFunctionInCode = null;
		// given a path, the program and the invariant, returns a path condition for the program
        public static VCExpr getPathCondition ( List<Block> path,Implementation impl,VCExpr invariant) {
            var outputVariables = new List<Variable>();
            HashSet<Variable> modified = new HashSet<Variable>();
            VCExpr ret = invariant;
            VCExpressionGenerator gen = prover.VCExprGen;
            var exprGen = prover.Context.ExprGen;
            var translator = prover.Context.BoogieExprTranslator;
            for (int bCtr = 0,blockCount = path.Count; bCtr < blockCount; bCtr++) {
                Block curBlock = path.ElementAt(bCtr);
                Boolean skip = false;
                for (int sCtr = 0,stmtCount = curBlock.cmds.Count; sCtr < stmtCount; sCtr++) {
                    if (skip) {
                        skip = false;
                        continue;
                    }
                    VCExpr expr;
                    var cmd = curBlock.cmds.ElementAt(sCtr);
                    var acmd = cmd as AssumeCmd;

                    if (acmd == null) {
                        if (cmd is AssertCmd) {
                            expr = translator.Translate((cmd as AssertCmd).Expr);
                            expr = gen.Not(expr);
                        } else
                            continue;
                    } else {
                        expr = translator.Translate(acmd.Expr);
                        NAryExpr naryExpr = acmd.Expr as NAryExpr;
                        if (naryExpr != null && naryExpr.Fun is FunctionCall) {
                            FunctionCall call = naryExpr.Fun as FunctionCall;
                            Function function = call.Func;
                            List<Variable> newInparams = new List<Variable>();
                            List<Expr> newArgs = new List<Expr>(naryExpr.Args);
                            newInparams.AddRange(function.InParams);
							//remove the global variables at the beginning
							for(int i = 0 ;i < prog.GlobalVariables.Count; i++){
								newInparams.RemoveRange(0,1);
								newArgs.RemoveRange(0,1);
							}
							//remove the global variabel modified from the end
							for(int i = 0; i < impl.Proc.Modifies.Count; i++) {
								newInparams.RemoveRange(newInparams.Count -1, 1);
								newArgs.RemoveRange(newArgs.Count -1,1);
							}
							
							Variable retVar = newInparams.ElementAt(newInparams.Count -1);							
							Expr retVarExpr = newArgs.ElementAt(newArgs.Count() - 1);
                            //remove the return variable
							newInparams.RemoveRange(newInparams.Count -1 , 1);
							newArgs.RemoveRange(newArgs.Count -1, 1);
                            VCExpr retVarVCExpr = translator.Translate(retVarExpr);                            
							FunctionCall myCall = new FunctionCall(calledFunctionInCode);
                            NAryExpr myExpr = new NAryExpr(Token.NoToken,myCall,newArgs);
                            myExpr.Resolve(new ResolutionContext(null));
                            myExpr.Typecheck(new TypecheckingContext(null));
                            prover.Context.DeclareFunction(calledFunctionInCode,"");
                            //Console.WriteLine("my expr type : " + myExpr.Type);
                            expr = translator.Translate(myExpr);
                            expr = gen.Eq(expr,retVarVCExpr);

                            // get the substituted invarinat
                            VCExprSubstitution subst = new VCExprSubstitution();
                            SubstitutingVCExprVisitor substituter = new SubstitutingVCExprVisitor(gen);
                            List<Expr> l = new List<Expr>();
                            l.AddRange(impl.Proc.Modifies);
                            int gCount = impl.Proc.Modifies.Count();
                            List<Expr> incarExpr = new List<Expr>();
                            incarExpr.AddRange(naryExpr.Args);
                            incarExpr.RemoveRange(0,incarExpr.Count() - gCount);
                            List<VCExpr> orig = translator.Translate(l);
                            for (int ctr = 0; ctr < impl.Proc.Modifies.Count(); ctr++) {
                                if (orig.ElementAt(ctr) is VCExprVar) {
                                    VCExprVar v = orig.ElementAt(ctr) as VCExprVar;
                                    VCExpr k = translator.Translate(incarExpr.ElementAt(ctr));
                                    subst[v] = k;
                                    //Console.WriteLine(v + " is replaced with " + k);
                                } else {
                                    Console.WriteLine("error : 3");
                                    Environment.Exit(0);
                                }
                            }
                            ret = gen.AndSimp(ret,expr);
                            expr = substituter.Mutate(invariant,subst);
                            skip = true;        // skip the next statement
                        }
                    }
                    ret = gen.AndSimp(ret,expr);
                }
            }
            return ret;
        }

        /*
         * variable path has the invariant encoded into it already
        */
        private static VCExpr post ( VCExpr path,Implementation impl ) {
            VCExprSubstitution subst = new VCExprSubstitution();
            VCExpressionGenerator gen = prover.VCExprGen;
            SubstitutingVCExprVisitor substituter = new SubstitutingVCExprVisitor(gen);
            var translator = prover.Context.BoogieExprTranslator;

            // substitute variables named as global varables
            for (int ctr = 0; ctr < impl.Proc.Modifies.Count; ctr++) {
                VCExprVar v = translator.Translate(impl.Proc.Modifies.ElementAt(ctr)) as VCExprVar;
                VCExprVar nuVar = gen.Variable(v.Name + "_my",v.Type);
                subst[v] = nuVar;
            }
            // conjunt foo_g = g for all global variables g
            VCExpr substitutedPath = substituter.Mutate(path,subst);
            for (int ctr = 0; ctr < impl.Proc.Modifies.Count; ctr++) {
                VCExprVar g = translator.Translate(impl.Proc.Modifies.ElementAt(ctr)) as VCExprVar;
                VCExprVar foo_g = gen.Variable(impl.Proc.Name + "_" + g.Name,g.Type);
                VCExpr eq = gen.Eq(g,foo_g);
                substitutedPath = gen.Add(substitutedPath,eq);
            }
            return substitutedPath;
        }
        private static bool isInvariant ( List<Block> path,VCExpr inv,Implementation impl ) {
            VCExpr pathConditionWithInvariant = getPathCondition(path,impl,inv);
            VCExpressionGenerator gen = prover.VCExprGen;
            gen.Implies(pathConditionWithInvariant,inv);

            // to continue form here
            // now check if inv implies pathConditionWithInvariant

            return false;
        }

        private static HashSet<List<Block>> unbraidPaths ( HashSet<List<Block>> paths ) {
            HashSet<List<Block>> newPaths = new HashSet<List<Block>>();
            foreach (var path in paths) {
                List<Block> newPath = new List<Block>();
                for (int ctr = 0,len = path.Count; ctr < len; ctr++) {
                    Block b = path.ElementAt(ctr);
                    Block newBlock = new Block();
                    newBlock.cmds = b.cmds;
                    newBlock.tok = b.tok;
                    newBlock.Label = b.Label;

                    newPath.Add(newBlock);
                }
                newPaths.Add(newPath);
            }
            foreach (var path in newPaths) {
                for (int ctr = 0; ctr < path.Count - 1; ctr++) {
                    Block cur = path.ElementAt(ctr);
                    Block nextBlock = path.ElementAt(ctr + 1);
                    List<Block> nextBlocks = new List<Block>();
                    nextBlocks.Add(nextBlock);
                    cur.TransferCmd = new GotoCmd(Token.NoToken,nextBlocks);
                }
            }
            return newPaths;
        }

        private static void printPath ( List<Block> list ) {
            if (!PrintPath) return;
            for (int b = 0,bSize = list.Count(); b < bSize; b++) {
                Block cur = list.ElementAt(b);
                Console.WriteLine("Block :" + cur.Label + " next : " + cur.TransferCmd);
                foreach (Cmd c in cur.cmds) {
                    Console.Write(c);
                }
            }
        }

        private static HashSet<List<Block>> getPaths ( Graph<Block> g,Block source ) {
            HashSet<List<Block>> ans = new HashSet<List<Block>>();
            List<Block> init = new List<Block>();
            init.Add(source);
            ans.Add(init);
            Boolean done = false;

            while (!done) {
                HashSet<List<Block>> toRemove = new HashSet<List<Block>>();
                HashSet<List<Block>> toAdd = new HashSet<List<Block>>();
                foreach (var path in ans) {
                    ReturnCmd ret = path.Last().TransferCmd as ReturnCmd;
                    if (ret == null) {
                        var nextNodes = g.Successors(path.Last());
                        //Console.WriteLine("for node :" + path.Last() + " next nodes are :" + path.Last().TransferCmd);
                        //Console.WriteLine("next node count : " + nextNodes.Count());
                        switch (nextNodes.Count()) {
                            case 0:
                                throw (new Exception("node with 0 successors"));
                                break;
                            case 1:
                                toRemove.Add(path);
                                List<Block> newPath = new List<Block>(path);
                                newPath.Insert(path.Count,nextNodes.ElementAt(0));
                                toAdd.Add(newPath);
                                //path.Insert(path.Count,nextNodes.ElementAt(0));
                                break;
                            default:
                                var prePath = path;
                                //ans.Remove(prePath);
                                toRemove.Add(path);
                                foreach (var node in nextNodes) {
                                    List<Block> nuPath = new List<Block>(prePath);
                                    nuPath.Insert(nuPath.Count,node);
                                    toAdd.Add(nuPath);
                                }
                                break;
                        }
                    }
                }
                foreach (var path in toRemove)
                    ans.Remove(path);
                foreach (var path in toAdd)
                    ans.Add(path);
                done = true;
                foreach (var path in ans) {
                    ReturnCmd ret = path.Last().TransferCmd as ReturnCmd;
                    if (ret == null) done = false;
                }
            }
            return ans;
        }
    /* internal static bool doAnalysis( Implementation implementation, Block src, Program prog){ return false;} */
        internal static bool doAnalysis ( Implementation impl,Block source,Program program ) {
            /* Console.WriteLine();
            Console.WriteLine("**********hello*************");
            Console.WriteLine(impl.Name);
             */
            if(program.Implementations.Count() > 1) {
                foreach(var v in program.Implementations) {
                    if( v.Name.Contains("mcm")){
                        impl = v;
                    }
                }
            }
            VCExpressionGenerator gen = new VCExpressionGenerator();
            CommandLineOptions.Clo.PrintInstrumented = true;
            CommandLineOptions.Clo.ExtractLoops = true;
            prog = program;
            var a = program.ExtractLoops();
            /* TokenTextWriter t = new TokenTextWriter("out.bpl", true, false); */
            //program.Emit(t);
            //Console.WriteLine("output from extract loops");
            //for(int i = 0; i < a.ElementAt(0).Value.Count(); i++) {
            //    Console.WriteLine(a.ElementAt(0).Value.ElementAt(i));
            //}
            //Console.WriteLine("***************");
            //Console.WriteLine("block count: " + impl.Blocks.Count());
            //for(int i = 0; i < impl.Blocks.Count(); i++) {
            //    Console.WriteLine(impl.Blocks.ElementAt(i).Label + " :");
            //    var block = impl.Blocks.ElementAt(i);
            //    for(int j = 0; j < block.cmds.Count(); j++) {
            //        Console.WriteLine(j +" " +block.cmds.ElementAt(j));
            //    }
            //}
            //Console.WriteLine(a.ElementAt(0).Value.ElementAt(1));
            //for (int i = 0; i < program.Procedures.Count(); i++)
            //    Console.WriteLine(program.Procedures.ElementAt(i));
            //Console.WriteLine(program.Procedures.Count());
            //return false;
            //Console.WriteLine("source block is : " + source);
            prover = ProverInterface.CreateProver(program,"errorLog",false,CommandLineOptions.Clo.ProverKillTime);
            setupCalledInterpretedFunction(impl,program);
            /* Console.WriteLine("called function : "+ calledFunctionInCode + " , " + calledFunctionInCode.InParams.Count); */
            setupCallerInterpretedFunction(impl,program);            
            /* Console.WriteLine("caller function : "+ callerFunctionInCode+ " , " + callerFunctionInCode.InParams.Count); */
            
            CommandLineOptions.Clo.SIBoolControlVC = true;
            CommandLineOptions.Clo.UseLabels = false;

	        VCExpr invariant = getInvariant(impl, program);
            

            Console.WriteLine("invariant : " + invariant);
            HashSet<VCExpr> pathConditions = getPathConditions(impl,source,invariant);
            /* foreach( var v in pathConditions )
                Console.WriteLine(prover.VCExpressionToString(v));
             */
            if (!isInductive(invariant, impl, pathConditions)) {
                Console.WriteLine("invariant not inductive");
                return false;
            }
            printPathConditions(pathConditions);
            
            VCExpr f = forAllOPCheck(impl, pathConditions, invariant);
			Console.WriteLine("for all opcheck :::::::::::::::::::::::::::");
            
			Console.WriteLine(prover.VCExpressionToString(f));
            VCExpr f1 = forSomeOPCheck(impl, pathConditions, invariant, callerFunctionInCode);
            // Console.WriteLine("\n\nfor some opcheck :::::::::::::::::::::::::::");
			// Console.WriteLine(f1);            
            prover.Push();
		
            prover.Assert(f, true); 
            /* prover.Assert(gen.Not(f), true); */
            // //Console.WriteLine(f1 == null);
            // //return false;
            // /* prover.Assert(f1, true); */
            // //Console.WriteLine(gen.Not(f));
            EmptyErrorHandler h = new EmptyErrorHandler();
            prover.Check();
            
            var forall = prover.CheckOutcomeCore(h);
            prover.Pop();
	    
	    var outcome = prover.CheckOutcomeCore(h);
            Console.WriteLine("outcome is " + outcome);
            Console.WriteLine("Invalid outcome implies the given procedure is OP (impurity witness)");
            // //if (forAllOPCheck(impl,pathConditions,invariant)) {
            // //    return true;
            // //}
            return false;
        }

        private static void setupCallerInterpretedFunction(Implementation imple, Program program)
        {
            String functionName = imple.Name;
            /* foreach(Implementation v in program.Implementations){
                if(v.Name = functionName)
            } */


            Function functionFromCode = null;
			var translator = prover.Context.BoogieExprTranslator;
        
            NAryExpr naryexpr = null;
            Implementation foundImpl = null;
            foreach(Implementation impl in program.Implementations){
            /*     Console.WriteLine("*****************");
                Console.WriteLine("checking impl : "+ impl.Name);
             */    foreach (Block b in impl.Blocks) {                    
                    //if (functionfromcode != null) break;
                    foreach (Cmd c in b.cmds) {
                        /* Console.WriteLine(c + " , " + c.GetType().Name + "\n");
                        if( c.GetType().Name.Contains("CallCmd")){
                            Console.WriteLine( "hahahaaha***************");
                        } */
                        AssumeCmd acmd = c as AssumeCmd;
                        if (acmd != null) {
                            naryexpr = acmd.Expr as NAryExpr;
                            if (naryexpr != null) {
                                FunctionCall call = naryexpr.Fun as FunctionCall;
                                /* if (call != null && call.FunctionName.Contains(functionName)) { */
                                    if (call != null) {
                                        /* Console.WriteLine("call.FunctinName : " + call.FunctionName);
                                        Console.WriteLine("functionName : " + functionName);
                                         */
                                         if(call.FunctionName.Equals(functionName)){
                                            /* Console.WriteLine("call : "+ call.FunctionName);
                                            Console.WriteLine("call1 : "+ functionName);
                                            Console.WriteLine("Found");
                                             */functionFromCode = call.Func;                                            
                                            foundImpl = impl;
                                        }
                                }
                            }
                        }
                    }
                }
            }
            if(functionFromCode == null){
                foreach(Implementation impl in program.Implementations){
                    /* Console.WriteLine("*****************");
                    Console.WriteLine("checking impl : "+ impl.Name); */
                    foreach (Block b in impl.Blocks) {                    
                        //if (functionfromcode != null) break;
                        foreach (Cmd c in b.cmds) {
                            /* Console.WriteLine(c + " , " + c.GetType().Name + "\n"); */
                            if( c.GetType().Name.Contains("CallCmd")){
                                CallCmd cal = (CallCmd) c;
                                if(cal.Proc.Name.Equals(functionName)) {
                                    /* c.Proc.Name                                 */
                                    /* foreach( var v in cal.Proc.InParams)
                                    Console.WriteLine(v);
                                    Console.WriteLine(); */
                                    
                                    callerFunctionInCode = new Function(Token.NoToken, 
                                    "my_" + functionName, cal.Proc.InParams, cal.Proc.OutParams.ElementAt(0));
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            List<Variable> newinparams = new List<Variable>();
            /* Console.WriteLine("reached"); */
            newinparams.AddRange(functionFromCode.InParams);
            /* Console.WriteLine("Reached 1"); */
			for (int i = 0; i < prog.GlobalVariables.Count; i++) {
                newinparams.RemoveRange(0, 1);
                /* Console.WriteLine("hello"); */
            }
            // remove the global variables modified from the end
            for(int i = 0; i < foundImpl.Proc.Modifies.Count; i++) {
                newinparams.RemoveRange(newinparams.Count() -1, 1);
                /* Console.WriteLine("bello"); */
            }
            
            //remove the return variable
            Variable retvar = newinparams.ElementAt(newinparams.Count() - 1);
            newinparams.RemoveRange(newinparams.Count() -1, 1);
/*             calledFunctionInCode = new Function(Token.NoToken, "my_" + functionFromCode.Name, newinparams, retvar); */
            callerFunctionInCode = new Function(Token.NoToken, "my_" + functionFromCode.Name, newinparams, retvar);
        }

        /// <summary>
        /// checks if the invariant is inductive
        /// </summary>
        /// <param name="invariant"></param>
        /// <param name="impl"></param>
        /// <param name="pathConditions"></param>
        /// <returns></returns>
        private static bool isInductive(VCExpr invariant, Implementation impl, HashSet<VCExpr> pathConditions) {
            /* foreach( var v in impl.Proc.OutParams) {
                Console.WriteLine(v);
            } */
            /* Console.WriteLine("+++++++++++++++"); */
            foreach(var pc in pathConditions) {
                //first find the final variables of the globals
                // Console.WriteLine(pc);
            }
            return true;
        }

        /// <summary>
        /// Returns a forula which if UNSAT implies the procedure impl is OP
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="pathConditions"></param>
        /// <param name="invariant"></param>
        /// <returns></returns>
        private static VCExpr forAllOPCheck ( Implementation impl,HashSet<VCExpr> pathConditions,VCExpr invariant ) {
            VCExpr formula = null;
            VCExpressionGenerator gen = new VCExpressionGenerator();

            for (int i = 0; i < pathConditions.Count(); i++) {
                for (int j = i; j < pathConditions.Count(); j++) {
                    VCExpr p = getPairConstraint(impl,pathConditions.ElementAt(i),pathConditions.ElementAt(j));
                    List<VCExpr> argsToMyFunction = new List<VCExpr>();
                    
                                        /* Console.WriteLine(p); */
                    /* Console.WriteLine(prover.VCExpressionToString(p)); */
                    //VCExpr[] lis = new VCExpr[1];
                    //lis[0] = gen.Integer(BigNum.ONE);

                    if (formula == null) {
                        formula = p;
                    } else {
                        formula = gen.And(formula,p);
                    }
                }
            }
            return formula;
        }

        /// <summary>
        /// returns a formula that if satisfiable implies the method is not op
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="expr1"></param>
        /// <param name="expr2"></param>
        /// <returns></returns>
        public static VCExpr getPairConstraint ( Implementation impl,VCExpr expr1,VCExpr expr2 ) {
            //Console.WriteLine("Pc1 : " + expr1 + "\n\n");
            //Console.WriteLine("Pc2 : " + expr2 + "\n\n");
            VCExpressionGenerator gen = new VCExpressionGenerator();
            VCExpr ret = null;
            VCExprSubstitution subst = new VCExprSubstitution();
            SubstitutingVCExprVisitor substituter = new SubstitutingVCExprVisitor(gen);
            HashSet<VCExprVar> var1 = getVariables(expr1);
            HashSet<VCExprVar> var2 = getVariables(expr2);
            //find the return variable
            String retVarName = impl.Proc.Name + "_" + impl.Proc.OutParams[0].Name;
            VCExprVar retVar2 = null;
            VCExprVar retVar1 = null;
            HashSet<VCExprVar> inParamsVCExpr = new HashSet<VCExprVar>();
            foreach (var variable in var2) {            // initilize retVar1,2; inParmamsVCExpr, subst
                VCExprVar t = gen.Variable("p2_" + variable.Name,variable.Type);
                subst[variable] = t;
                if (variable.Name.Contains(retVarName)) {
                    retVar2 = t;
                    retVar1 = variable;
                }
                foreach (var a in impl.Proc.InParams) {
                    if (a.Name.Equals(variable.Name))
                        inParamsVCExpr.Add(variable);
                }
            }
            VCExpr an = substituter.Mutate(expr2,subst);
            //Console.WriteLine(an);
            VCExpr pc = gen.And(an,expr1);
            VCExpr retEqual = gen.Eq(retVar2,retVar1);  // equate the return variables
            VCExpr retNotEqual = gen.Not(gen.Eq(retVar2,retVar1));  // unequal return variables
            VCExprVar z = null;
            VCExpr equalParams = null;
            foreach (var v in impl.Proc.InParams) {     // equate all the parameters
                foreach (var n in inParamsVCExpr) {
                    if (n.Name.Equals(v.Name))
                        z = n;
                }
                VCExpr p = gen.Eq(z,subst[z]);
                if (equalParams == null) {
                    equalParams = p;
                } else {
                    equalParams = gen.And(equalParams,p);
                }
            }
            //ret = gen.Not(gen.Implies(gen.And(pc,equalParams),retEqual));
            //ret = gen.Implies(gen.And(pc, equalParams), retEqual);
            ret = gen.And(gen.And(pc,equalParams),retNotEqual);
            List<VCExprVar> varList = new List<VCExprVar>();
            foreach (var v in var1) {
                varList.Add(v);
            }
            foreach (var v in var2) {
                varList.Add(subst[v] as VCExprVar);
            }
            List<VCExpr> k = new List<VCExpr>();
            k.Add(ret);
            VCTrigger s = new VCTrigger(true,k);
            List<VCTrigger> trig = new List<VCTrigger>();
            //ret = gen.Forall(varList,trig,ret);
            ret = gen.Exists(varList,trig,ret);
            //Console.WriteLine("\n" + ret);
            //ret = gen.Not(ret);
            return ret;
        }

        private static HashSet<VCExprVar> getVariables ( VCExpr expr1 ) {
            HashSet<VCExprVar> ret = new HashSet<VCExprVar>();
            getVariables(expr1,ret);
            return ret;
        }

        private static void getVariables ( VCExpr expr,HashSet<VCExprVar> ret ) {
            if (expr is VCExprConstant || expr is VCExprContracts || expr is VCExprOp || expr is VCExprLiteral)
                return;
            if (expr is VCExprLet)
                return;
            if (expr is VCExprNAry) {
                VCExprNAry nExp = expr as VCExprNAry;
                for (int ctr = 0; ctr < nExp.Arity; ctr++) {
                    VCExpr arg = nExp[ctr];
                    VCExprVar argVar = arg as VCExprVar;
                    if (argVar != null) {
                        ret.Add(argVar);
                    } else {
                        getVariables(arg,ret);
                    }
                }
            }
        }

        private static void setupCalledInterpretedFunction ( Implementation impl,Program prog ) {
			Function functionFromCode = null;
			var translator = prover.Context.BoogieExprTranslator;
            NAryExpr naryexpr = null;
            foreach (Block b in impl.Blocks) {
                //if (functionfromcode != null) break;
                foreach (var c in b.cmds) {
                    AssumeCmd acmd = c as AssumeCmd;
                    if (acmd != null) {
                        naryexpr = acmd.Expr as NAryExpr;
                        if (naryexpr != null) {
                            FunctionCall call = naryexpr.Fun as FunctionCall;
                            if (call != null) {
                                functionFromCode = call.Func;
                            }
                        }
                    }
                }
            }
             
             List<Variable> newinparams = new List<Variable>();
               newinparams.AddRange(functionFromCode.InParams);
			
            for (int i = 0; i < prog.GlobalVariables.Count; i++) {
                newinparams.RemoveRange(0, 1);
                /* newinparams.RemoveRange(newinparams.Count() - 1, 1); */
            }
            // remove the global variables modified from the end
            for(int i = 0; i < impl.Proc.Modifies.Count; i++) {
                newinparams.RemoveRange(newinparams.Count() -1, 1);
            }
            //remove the return variable
            Variable retvar = newinparams.ElementAt(newinparams.Count() - 1);
            newinparams.RemoveRange(newinparams.Count() -1, 1);
            calledFunctionInCode = new Function(Token.NoToken, "my_" + functionFromCode.Name, newinparams, retvar);
        }


        /// <summary>
        /// Returns a formula that if satisfiable imlies impl is OP
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="paths"></param>
        /// <param name="invariant"></param>
        /// <returns></returns>
        private static VCExpr forSomeOPCheck ( Implementation impl,HashSet<VCExpr> paths,VCExpr invariant, Function proposedF ) {
            VCExpressionGenerator gen = new VCExpressionGenerator();
            VCExpr ret = null;
            for (int i = 0; i < paths.Count(); i++) {
                var ee = equateToFunction(impl,paths.ElementAt(i), proposedF);
                if(ret == null) {
                    ret = ee;
                } else {
                    ret = gen.And(ret, ee);
                }
            }
            //Console.WriteLine(ret);
            return ret;
        }

        /// <summary>
        /// takes vcExpr which is the path constraint for a path, and adds the following constraint
        /// equates the return variable to f(param)
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="vcExpr"></param>
        /// <returns></returns>
        private static VCExpr equateToFunction(Implementation impl, VCExpr vcExpr, Function proposedF) {
            VCExpressionGenerator gen = new VCExpressionGenerator();
            var retVarName = impl.Proc.OutParams[0].Name;
            VCExprVar retVar = gen.Variable(retVarName, new BasicType(SimpleType.Int)); ;
            HashSet<VCExprVar> vars = getVariables(vcExpr);
            foreach (var v in vars) {
                if (v.Name.Contains(retVarName)) {
                    retVar = v;
                }
            }
            FunctionCall myCall = new FunctionCall(proposedF);
            NAryExpr call = new NAryExpr(Token.NoToken, myCall, getParameterList(impl, vcExpr));
            var translator = prover.Context.BoogieExprTranslator;
            call.Resolve(new ResolutionContext(null));
            call.Typecheck(new TypecheckingContext(null));
            VCExpr fExpr = translator.Translate(call);
            VCExpr toRet = gen.And(vcExpr, gen.Eq(fExpr, retVar));
            return toRet;
        }

        private static List<Expr> getParameterList(Implementation impl, VCExpr expr) {
            //impl.Proc.InParams;
            List<Expr> ret = new List<Expr>(impl.Proc.InParams.Count);
            HashSet<Expr> retSet = new HashSet<Expr>();
            Graph<Block> graph = Program.GraphFromImpl(impl);
            foreach(var node in graph.Nodes) {
                foreach (var cmd in node.cmds) {
                    var acmd = cmd as AssumeCmd;
                    if(acmd != null) {
                        NAryExpr naryExpr = acmd.Expr as NAryExpr;
                        if (naryExpr != null) { // cmd is an assume command
                            if (!(naryExpr.Fun is FunctionCall)) {
                                //Console.WriteLine(naryExpr);
                                foreach(var exp in naryExpr.Args) {
                                    //if( exp is NAryExpr)
                                    //Console.WriteLine(exp + "  " + exp.GetType().Name);
                                    if(exp is IdentifierExpr) {
                                        retSet.Add(exp);
                                    } else if(exp is NAryExpr) {
                                        NAryExpr nexp = exp as NAryExpr;
                                        foreach(var exp1 in nexp.Args) {
                                            if(exp1 is IdentifierExpr) {
                                                retSet.Add(exp1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            for(int i = 0; i < impl.Proc.InParams.Count; i++) {
                foreach(var v in retSet) {
                    if(impl.Proc.InParams.ElementAt(i).Name.Equals(v.ToString())) {
                        //Console.WriteLine(i);
                        ret.Add(v);
                    }
                }
            }
            return ret;
        }

        private static VCExpr getInvariant ( Implementation impl, Program program) {
            /* Console.WriteLine("entered"); */
            VCExpr ret = null;
            var translator = prover.Context.BoogieExprTranslator;
            BasicType b = new BasicType(SimpleType.Int);
            VCExpressionGenerator gen = new VCExpressionGenerator();
            if( impl.Name.Contains("FactArray")) {
                #region disjunct 1
                VCExprVar kIndex = gen.Variable("k", new BasicType(SimpleType.Int) );
                VCExprVar gArray = gen.Variable("g", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int}, BasicType.Int ));
                // \forall i. g[i] = 0  OR g[i] = F9 (n-1) * n
                VCExpr m1 = gen.Integer(BigNum.FromInt(0));
                List<VCExpr> argsForSelect = new List <VCExpr>();
                argsForSelect.Add(gArray);
                argsForSelect.Add(kIndex);
                VCExpr arrayAccess = gen.Select(argsForSelect.ToArray());
                Console.WriteLine(arrayAccess);
                VCExpr disjunct1 = gen.Eq(arrayAccess, m1);
                 Console.WriteLine(disjunct1); 
                #endregion

                List<Expr> arg= new List<Expr>();
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(0)));
                FunctionCall myCall = new FunctionCall(calledFunctionInCode);
                Expr call = new NAryExpr(Token.NoToken, myCall, arg);
                call.Resolve(new ResolutionContext(null));
                call.Typecheck(new TypecheckingContext(null));
                List<VCExpr> arg1 = new List<VCExpr>();

                 List<VCExpr> aaa = new List<VCExpr>();
                 aaa.Add(kIndex); aaa.Add( gen.Integer(BigNum.FromInt(1)));
                 arg1.Add(gen.Function(VCExpressionGenerator.SubIOp, aaa ));
                VCExpr a = gen.Function(calledFunctionInCode, arg1);

                List<VCExpr> bbb = new List<VCExpr>();
                 bbb.Add(kIndex); bbb.Add(a);
                a = gen.Function(VCExpressionGenerator.MulIOp,new List<VCExpr>(bbb));
                VCExpr disjunct2 = gen.Eq(a, arrayAccess);
                VCExpr unQuant = gen.Or(disjunct1, disjunct2);
                List<VCExprVar> vars = new List<VCExprVar>();
                vars.Add(kIndex);
                List<VCTrigger> triggers = new List<VCTrigger>();				
                VCExpr quant =  gen.Forall(vars,triggers,unQuant);
				return quant;                

             } else if (impl.Name.Contains("Fact")) {
                VCExprVar g = gen.Variable("nineteen",new BasicType(SimpleType.Int));
                g = (VCExprVar)translator.Translate(impl.Proc.Modifies.ElementAt(0));

                VCExpr m1 = gen.Integer(BigNum.FromInt(-1));
                ret = gen.Eq(g,m1);
                Expr eighteen = new LiteralExpr(Token.NoToken,BigNum.FromInt(18));
                Expr nineteen = new LiteralExpr(Token.NoToken,BigNum.FromInt(19));
                List<Expr> arg = new List<Expr>();
                arg.Add(eighteen);
                FunctionCall myCall = new FunctionCall(callerFunctionInCode);
                Expr call = new NAryExpr(Token.NoToken,myCall,arg);
                call.Resolve(new ResolutionContext(null));
                call.Typecheck(new TypecheckingContext(null));
                List<VCExpr> arg1 = new List<VCExpr>();
                arg1.Add(translator.Translate(call));
                arg1.Add(gen.Integer(BigNum.FromInt(19)));
                VCExpr ret2 = gen.Function(VCExpressionGenerator.MulIOp,arg1);
                ret2 = gen.Eq(g,ret2);
                ret = gen.Or(ret,ret2);
                return ret;
            } else if (impl.Name.Contains("recent_fact")) {
                VCExprVar lastN = (VCExprVar)translator.Translate(impl.Proc.Modifies.ElementAt(0));
                VCExprVar lastAns = (VCExprVar)translator.Translate(impl.Proc.Modifies.ElementAt(1));
                VCExpr m1 = gen.Integer(BigNum.FromInt(-1));
                ret = gen.Eq(lastAns,m1);
                List<VCExpr> lastNMinus1 = new List<VCExpr>();
                lastNMinus1.Add(m1);
                lastNMinus1.Add(lastN);

                VCExpr ret2 = gen.Function(callerFunctionInCode, gen.Function(VCExpressionGenerator.AddIOp, lastNMinus1));
                VCExpr ret3 = gen.Function(VCExpressionGenerator.MulIOp, ret2, lastN);
                ret2 = gen.Eq(lastAns,ret3);
                ret = gen.Or(ret,ret2);
                return ret;
            } else if( impl.Name.Contains("mcm")) {                
                #region disjunct 1
                VCExprVar iIndex = gen.Variable("i", new BasicType(SimpleType.Int) );
                VCExprVar jIndex = gen.Variable("j", new BasicType(SimpleType.Int) );
                VCExprVar mArray = gen.Variable("m", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int, BasicType.Int}, BasicType.Int ));
                // m[i,j] = -1 OR m[i,j] = mcm(i, j)
                VCExpr m1 = gen.Integer(BigNum.FromInt(-1));
                List<VCExpr> argsForSelect = new List <VCExpr>();
                argsForSelect.Add(mArray);
                argsForSelect.Add(iIndex);
                argsForSelect.Add(jIndex);
                VCExpr arrayAccess = gen.Select(argsForSelect.ToArray());
                VCExpr disjunct1 = gen.Eq(arrayAccess, m1);
                
                #endregion
                
                List<Expr> arg= new List<Expr>();
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(0)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(1)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(2)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(3)));
                // arg.Add(Expr.Ident(callerFunctionInCode.InParams.ElementAt(1)));
                FunctionCall myCall = new FunctionCall(calledFunctionInCode);
                Expr call = new NAryExpr(Token.NoToken, myCall, arg);
                call.Resolve(new ResolutionContext(null));
                call.Typecheck(new TypecheckingContext(null));
                List<VCExpr> arg1 = new List<VCExpr>();
                /* arg1.Add(translator.Translate(call)); */
                arg1.Add(iIndex);
                arg1.Add(jIndex);
                arg1.Add(iIndex);
                arg1.Add(gen.Integer(BigNum.FromInt(-1)));
                VCExpr a = gen.Function(calledFunctionInCode, arg1);
                VCExpr disjunct2 = gen.Eq(a, arrayAccess);
                VCExpr unQuant = gen.Or(disjunct1, disjunct2);
                List<VCExprVar> vars = new List<VCExprVar>();
                vars.Add(iIndex);
                vars.Add(jIndex);
				List<VCTrigger> triggers = new List<VCTrigger>();				
                VCExpr quant =  gen.Forall(vars,triggers,unQuant);
                // Console.WriteLine(quant);
				return quant;

            } else if( impl.Name.Contains("makeChange")) {
                #region disjunct 1
                VCExprVar kIndex = gen.Variable("k", new BasicType(SimpleType.Int) );
                VCExprVar gArray = gen.Variable("g", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int, BasicType.Int}, BasicType.Int ));
                // \forall i. g[i] = 0  OR g[i] = F9 (n-1) * n
                // m[i,j] = -1 OR m[i,j] = mcm(i, j)
                VCExpr m1 = gen.Integer(BigNum.FromInt(0));
                List<VCExpr> argsForSelect = new List <VCExpr>();
                argsForSelect.Add(gArray);
                argsForSelect.Add(kIndex);
                VCExpr arrayAccess = gen.Select(argsForSelect.ToArray());
                Console.WriteLine(arrayAccess);
                VCExpr disjunct1 = gen.Eq(arrayAccess, m1);
                 Console.WriteLine(disjunct1); 
                #endregion

                List<Expr> arg= new List<Expr>();
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(0)));
                /* arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(1)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(2)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(3))); */
                // arg.Add(Expr.Ident(callerFunctionInCode.InParams.ElementAt(1)));
                FunctionCall myCall = new FunctionCall(calledFunctionInCode);
                Expr call = new NAryExpr(Token.NoToken, myCall, arg);
                call.Resolve(new ResolutionContext(null));
                call.Typecheck(new TypecheckingContext(null));
                List<VCExpr> arg1 = new List<VCExpr>();
                /* arg1.Add(translator.Translate(call)); */
                /* arg1.Add(iIndex);
                arg1.Add(jIndex);
                arg1.Add(iIndex);
                 */arg1.Add(gen.Integer(BigNum.FromInt(-1)));
                VCExpr a = gen.Function(calledFunctionInCode, arg1);
                VCExpr disjunct2 = gen.Eq(a, arrayAccess);
                VCExpr unQuant = gen.Or(disjunct1, disjunct2);
                List<VCExprVar> vars = new List<VCExprVar>();
                /* vars.Add(iIndex);
                vars.Add(jIndex);
				 */List<VCTrigger> triggers = new List<VCTrigger>();				
                VCExpr quant =  gen.Forall(vars,triggers,unQuant);
                // Console.WriteLine(quant);
				return quant;                

             } else if( impl.Name.Contains("hamming")) {
                #region disjunct 1
                VCExprVar kIndex = gen.Variable("k", new BasicType(SimpleType.Int) );
                VCExprVar gArray = gen.Variable("cache", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int, BasicType.Int}, BasicType.Int ));
                // \forall i. g[i] = 0  OR g[i] = F9 (n-1) * n
                // m[i,j] = -1 OR m[i,j] = mcm(i, j)
                VCExpr m1 = gen.Integer(BigNum.FromInt(0));
                List<VCExpr> argsForSelect = new List <VCExpr>();
                argsForSelect.Add(gArray);
                argsForSelect.Add(kIndex);
                VCExpr arrayAccess = gen.Select(argsForSelect.ToArray());
                Console.WriteLine(arrayAccess);
                VCExpr disjunct1 = gen.Eq(arrayAccess, m1);
                 Console.WriteLine(disjunct1); 
                #endregion

                List<Expr> arg= new List<Expr>();
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(0)));
                /* arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(1)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(2)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(3))); */
                // arg.Add(Expr.Ident(callerFunctionInCode.InParams.ElementAt(1)));
                FunctionCall myCall = new FunctionCall(calledFunctionInCode);
                Expr call = new NAryExpr(Token.NoToken, myCall, arg);
                call.Resolve(new ResolutionContext(null));
                call.Typecheck(new TypecheckingContext(null));
                List<VCExpr> arg1 = new List<VCExpr>();
                /* arg1.Add(translator.Translate(call)); */
                /* arg1.Add(iIndex);
                arg1.Add(jIndex);
                arg1.Add(iIndex);
                 */arg1.Add(gen.Integer(BigNum.FromInt(-1)));
                VCExpr a = gen.Function(calledFunctionInCode, arg1);
                VCExpr disjunct2 = gen.Eq(a, arrayAccess);
                VCExpr unQuant = gen.Or(disjunct1, disjunct2);
                List<VCExprVar> vars = new List<VCExprVar>();
                /* vars.Add(iIndex);
                vars.Add(jIndex); */
                vars.Add(kIndex);
				List<VCTrigger> triggers = new List<VCTrigger>();				
                VCExpr quant =  gen.Forall(vars,triggers,unQuant);
                // Console.WriteLine(quant);
				return quant;                

             } else if( impl.Name.Contains("fib")) {
                #region disjunct 1
                VCExprVar kIndex = gen.Variable("k", new BasicType(SimpleType.Int) );
                VCExprVar gArray = gen.Variable("cache", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int, BasicType.Int}, BasicType.Int ));
                // \forall i. g[i] = 0  OR g[i] = F9 (n-1) * n
                // m[i,j] = -1 OR m[i,j] = mcm(i, j)
                VCExpr m1 = gen.Integer(BigNum.FromInt(0));
                List<VCExpr> argsForSelect = new List <VCExpr>();
                argsForSelect.Add(gArray);
                argsForSelect.Add(kIndex);  
                VCExpr arrayAccess = gen.Select(argsForSelect.ToArray());
                Console.WriteLine(arrayAccess);
                VCExpr disjunct1 = gen.Eq(arrayAccess, m1);
                 Console.WriteLine(disjunct1); 
                #endregion

                List<Expr> arg= new List<Expr>();
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(0)));
                /* arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(1)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(2)));
                arg.Add(Expr.Ident(calledFunctionInCode.InParams.ElementAt(3))); */
                // arg.Add(Expr.Ident(callerFunctionInCode.InParams.ElementAt(1)));
                FunctionCall myCall = new FunctionCall(calledFunctionInCode);
                Expr call = new NAryExpr(Token.NoToken, myCall, arg);
                call.Resolve(new ResolutionContext(null));
                call.Typecheck(new TypecheckingContext(null));
                List<VCExpr> arg1 = new List<VCExpr>();
                arg1.Add(kIndex);
                arg1.Add (gen.Integer(BigNum.FromInt(1)));
                VCExpr kMinusOne = gen.Function(VCExpressionGenerator.SubIOp,arg1);
                List<VCExpr> arg2 = new List<VCExpr>();
                arg2.Add(kIndex);
                arg2.Add(gen.Integer(BigNum.FromInt(2)));
                VCExpr kMinusTwo = gen.Function(VCExpressionGenerator.SubIOp,arg2);
                /* arg1.Add(gen.Function(gen.Integer(BigNum.FromInt(-1)));
                arg2.Add(gen.Integer(BigNum.FromInt(-2))); */
                VCExpr kMinusOneFib = gen.Function(calledFunctionInCode, kMinusOne);
                VCExpr kMinusTwoFib = gen.Function(calledFunctionInCode, kMinusTwo);
                List<VCExpr> arg3 = new List<VCExpr>();
                arg3.Add(kMinusOneFib);
                arg3.Add(kMinusTwoFib);
                VCExpr sum = gen.Function(VCExpressionGenerator.AddIOp,arg3);
                VCExpr disjunct2 = gen.Eq(sum, arrayAccess);
                VCExpr unQuant = gen.Or(disjunct1, disjunct2);
                List<VCExprVar> vars = new List<VCExprVar>();
                /* vars.Add(iIndex);
                vars.Add(jIndex); */ 
                vars.Add(kIndex);
				List<VCTrigger> triggers = new List<VCTrigger>();				
                VCExpr quant =  gen.Forall(vars,triggers,unQuant); 
                // Console.WriteLine(quant);
				return quant;                
             } else if( impl.Name.Contains("levenshtein")) {
               VCExprVar i = gen.Variable("i", new BasicType(SimpleType.Int));
               VCExprVar j = gen.Variable("j", new BasicType(SimpleType.Int));
               VCExprVar ans = gen.Variable("ans", new BasicType(SimpleType.Int));
               VCExprVar ans1 = gen.Variable("ans1", new BasicType(SimpleType.Int));
               VCExprVar ans2 = gen.Variable("ans2", new BasicType(SimpleType.Int));
               VCExprVar ans3 = gen.Variable("ans3", new BasicType(SimpleType.Int));
               VCExprVar costXY = gen.Variable("cost", new BasicType(SimpleType.Int));

               VCExpr m1 = gen.Integer(BigNum.FromInt(-1));
               VCExpr one = gen.Integer(BigNum.FromInt(1)); 
               VCExpr zero = gen.Integer(BigNum.FromInt(0));
               VCExpr iM1 = gen.Function(VCExpressionGenerator.AddIOp, i, m1);
               VCExpr jM1 = gen.Function(VCExpressionGenerator.AddIOp, j, m1);
               VCExprVar x = gen.Variable("x", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int}, BasicType.Int));
                VCExprVar y = gen.Variable("y", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int}, BasicType.Int));
               
               VCExprVar cache = gen.Variable("cache", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int, BasicType.Int}, BasicType.Int ));
                List<VCExpr> args = new List<VCExpr>();
                args.Add(cache);
                args.Add(i);
                args.Add(j);
                VCExpr cacheAccess = gen.Select(args.ToArray());
                VCExpr conjunct1 = gen.Eq(cacheAccess, m1);
                List<VCExpr> argsIShift = new List<VCExpr>();
                List<VCExpr> argsJShift = new List<VCExpr>();
                List<VCExpr> argsNoShift = new List<VCExpr>();
                argsIShift.Add(iM1); argsIShift.Add(j);
                argsJShift.Add(i); argsJShift.Add(jM1);
                argsNoShift.Add(gen.Function(VCExpressionGenerator.AddIOp, i, m1)); 
                argsNoShift.Add(gen.Function(VCExpressionGenerator.AddIOp, j, m1)); 

                VCExpr iShift = gen.Function(VCExpressionGenerator.AddIOp, one, gen.Function(callerFunctionInCode,argsIShift));
                VCExpr jShift = gen.Function(VCExpressionGenerator.AddIOp, one, gen.Function(callerFunctionInCode,argsJShift));
                VCExpr noShift = gen.Function(callerFunctionInCode, argsNoShift);
                List<VCExpr> iandj = new List<VCExpr>();
                iandj.Add(i);
                iandj.Add(j);

                List<VCExpr> xSelArgs = new List<VCExpr>();
                xSelArgs.Add(x); xSelArgs.Add(i);
                List<VCExpr> ySelArgs = new List<VCExpr>();
                ySelArgs.Add(y); ySelArgs.Add(j);

                VCExpr cost1 = gen.Add(gen.Eq(gen.Select(xSelArgs.ToArray()), gen.Select(ySelArgs.ToArray())),
                gen.Eq(costXY, one))  ;
                VCExpr cost0 = gen.Add(gen.Not(gen.Eq(gen.Select(xSelArgs.ToArray()), gen.Select(ySelArgs.ToArray()))),
                gen.Eq(costXY, zero));
                VCExpr cost = gen.Or(cost0, cost1);
                noShift = gen.Function(VCExpressionGenerator.AddIOp, cost, noShift);
                List<VCExpr> argsMin = new List<VCExpr>();
                argsMin.Add(noShift); argsMin.Add(iShift); argsMin.Add(jShift);

                
                VCExpr bind = gen.And(gen.And(gen.Eq(ans1, iShift) , gen.Eq(ans2, jShift)), gen.Eq(ans3, noShift));
                VCExpr min1 = gen.And(gen.And(lessThan(ans1, ans2, gen), lessThan(ans1, ans3, gen)), gen.Eq(ans, ans1));
                VCExpr min2 = gen.And(gen.And(lessThan(ans1, ans2, gen), lessThan(ans3, ans1, gen)), gen.Eq(ans, ans3));
                VCExpr min3 = gen.And(gen.And(lessThan(ans2, ans1, gen), lessThan(ans2, ans3, gen)), gen.Eq(ans, ans2));
                VCExpr min4 = gen.And(gen.And(lessThan(ans2, ans1, gen), lessThan(ans3, ans2, gen)), gen.Eq(ans, ans3));
                VCExpr minimum = gen.Or(gen.Or(gen.Or(min1,min2),min3),min4);

                VCExpr selectQ = gen.Select(args.ToArray());
                VCExpr conjunct2 = gen.And(gen.And(gen.Eq(selectQ, ans),minimum),bind);
                Console.WriteLine(conjunct2);
                VCExpr unQuant = gen.Or( conjunct1, conjunct2);
                List<VCExprVar> vars = new List<VCExprVar>();
                vars.Add(i); vars.Add(j);
                List<VCTrigger> trig = new List<VCTrigger>();
                VCExpr quant = gen.Forall(vars, trig, unQuant);
                return quant;
             }  else if( impl.Name.Contains("lcs")) {                
                #region disjunct 1
                VCExprVar iIndex = gen.Variable("a", new BasicType(SimpleType.Int) );
                VCExprVar jIndex = gen.Variable("b", new BasicType(SimpleType.Int) );
                VCExprVar ans = gen.Variable("ans", new BasicType(SimpleType.Int) );
                VCExprVar mArray = gen.Variable("cache", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int, BasicType.Int}, BasicType.Int ));
                // m[i,j] = -1 OR m[i,j] = mcm(i, j)
                VCExpr m1 = gen.Integer(BigNum.FromInt(-1));
                VCExpr one = gen.Integer(BigNum.FromInt(1));
                List<VCExpr> argsForSelect = new List <VCExpr>();
                argsForSelect.Add(mArray);
                argsForSelect.Add(iIndex);
                argsForSelect.Add(jIndex);
                VCExpr arrayAccess = gen.Select(argsForSelect.ToArray());
                VCExpr disjunct1 = gen.Eq(arrayAccess, m1);
                
                #endregion
                
                List<VCExpr> argsShiftB = new List<VCExpr>();
                List<VCExpr> argsShiftA = new List<VCExpr>();
                argsShiftA.Add(iIndex); argsShiftA.Add(gen.Function(VCExpressionGenerator.SubIOp, jIndex, one));
                argsShiftB.Add(gen.Function(VCExpressionGenerator.SubIOp, iIndex, one)); argsShiftB.Add(jIndex);

                VCExpr callBShift = gen.Function(calledFunctionInCode, argsShiftB);
                VCExpr callAShift = gen.Function(calledFunctionInCode, argsShiftA);

                VCExpr dis1 = gen.And(lessThan(callAShift, callBShift, gen), gen.Eq(ans, callBShift));
                VCExpr dis2 = gen.And(lessThan(callBShift, callAShift, gen), gen.Eq(ans, callAShift));

                VCExpr conj = gen.Or(dis1, dis2);
                conj = gen.And(conj, gen.Eq(ans, gen.Select(argsForSelect.ToArray())));
                
                VCExpr unQuant = gen.Or(disjunct1, conj);
                List<VCExprVar> vars = new List<VCExprVar>();
                vars.Add(iIndex);
                vars.Add(jIndex);
				List<VCTrigger> triggers = new List<VCTrigger>();				
                VCExpr quant =  gen.Forall(vars,triggers,unQuant);
                Console.WriteLine(quant);
				return quant;
            }  else if( impl.Name.Contains("levenshteinInner")) {
               VCExprVar str1 = gen.Variable("x", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int}, BasicType.Int ));
                VCExprVar str2 = gen.Variable("y", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int}, BasicType.Int ));
                VCExprVar lenX = gen.Variable("lenX", new BasicType(SimpleType.Int));
                VCExprVar lenY = gen.Variable("lenY", new BasicType(SimpleType.Int));
                VCExprVar cache1 = gen.Variable("cache", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.List ,BasicType.Int, BasicType.Int}, BasicType.Int ));
                
    




               VCExprVar i = gen.Variable("i", new BasicType(SimpleType.Int));
               VCExprVar j = gen.Variable("j", new BasicType(SimpleType.Int));
               VCExprVar ans = gen.Variable("ans", new BasicType(SimpleType.Int));
               VCExprVar ans1 = gen.Variable("ans1", new BasicType(SimpleType.Int));
               VCExprVar ans2 = gen.Variable("ans2", new BasicType(SimpleType.Int));
               VCExprVar ans3 = gen.Variable("ans3", new BasicType(SimpleType.Int));
               VCExprVar costXY = gen.Variable("cost", new BasicType(SimpleType.Int));

               VCExpr m1 = gen.Integer(BigNum.FromInt(-1));
               VCExpr one = gen.Integer(BigNum.FromInt(1)); 
               VCExpr zero = gen.Integer(BigNum.FromInt(0));
               VCExpr iM1 = gen.Function(VCExpressionGenerator.AddIOp, i, m1);
               VCExpr jM1 = gen.Function(VCExpressionGenerator.AddIOp, j, m1);
               VCExprVar x = gen.Variable("x", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int}, BasicType.Int));
                VCExprVar y = gen.Variable("y", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int}, BasicType.Int));
               
               VCExprVar cache = gen.Variable("cache", new MapType(Token.NoToken, new List<TypeVariable>()
                , new List<Microsoft.Boogie.Type>{BasicType.Int, BasicType.Int}, BasicType.Int ));
                List<VCExpr> args = new List<VCExpr>();
                args.Add(cache);
                args.Add(i);
                args.Add(j);
                VCExpr cacheAccess = gen.Select(args.ToArray());
                VCExpr conjunct1 = gen.Eq(cacheAccess, m1);
                List<VCExpr> argsIShift = new List<VCExpr>();
                List<VCExpr> argsJShift = new List<VCExpr>();
                List<VCExpr> argsNoShift = new List<VCExpr>();
                argsIShift.Add(iM1); argsIShift.Add(j);
                argsJShift.Add(i); argsJShift.Add(jM1);
                argsNoShift.Add(gen.Function(VCExpressionGenerator.AddIOp, i, m1)); 
                argsNoShift.Add(gen.Function(VCExpressionGenerator.AddIOp, j, m1)); 

                VCExpr iShift = gen.Function(VCExpressionGenerator.AddIOp, one, gen.Function(callerFunctionInCode,argsIShift));
                VCExpr jShift = gen.Function(VCExpressionGenerator.AddIOp, one, gen.Function(callerFunctionInCode,argsJShift));
                VCExpr noShift = gen.Function(callerFunctionInCode, argsNoShift);
                List<VCExpr> iandj = new List<VCExpr>();
                iandj.Add(i);
                iandj.Add(j);

                List<VCExpr> xSelArgs = new List<VCExpr>();
                xSelArgs.Add(x); xSelArgs.Add(i);
                List<VCExpr> ySelArgs = new List<VCExpr>();
                ySelArgs.Add(y); ySelArgs.Add(j);

                VCExpr cost1 = gen.Add(gen.Eq(gen.Select(xSelArgs.ToArray()), gen.Select(ySelArgs.ToArray())),
                gen.Eq(costXY, one))  ;
                VCExpr cost0 = gen.Add(gen.Not(gen.Eq(gen.Select(xSelArgs.ToArray()), gen.Select(ySelArgs.ToArray()))),
                gen.Eq(costXY, zero));
                VCExpr cost = gen.Or(cost0, cost1);
                noShift = gen.Function(VCExpressionGenerator.AddIOp, cost, noShift);
                List<VCExpr> argsMin = new List<VCExpr>();
                argsMin.Add(noShift); argsMin.Add(iShift); argsMin.Add(jShift);

                
                VCExpr bind = gen.And(gen.And(gen.Eq(ans1, iShift) , gen.Eq(ans2, jShift)), gen.Eq(ans3, noShift));
                VCExpr min1 = gen.And(gen.And(lessThan(ans1, ans2, gen), lessThan(ans1, ans3, gen)), gen.Eq(ans, ans1));
                VCExpr min2 = gen.And(gen.And(lessThan(ans1, ans2, gen), lessThan(ans3, ans1, gen)), gen.Eq(ans, ans3));
                VCExpr min3 = gen.And(gen.And(lessThan(ans2, ans1, gen), lessThan(ans2, ans3, gen)), gen.Eq(ans, ans2));
                VCExpr min4 = gen.And(gen.And(lessThan(ans2, ans1, gen), lessThan(ans3, ans2, gen)), gen.Eq(ans, ans3));
                VCExpr minimum = gen.Or(gen.Or(gen.Or(min1,min2),min3),min4);

                VCExpr selectQ = gen.Select(args.ToArray());
                VCExpr conjunct2 = gen.And(gen.And(gen.Eq(selectQ, ans),minimum),bind);
                Console.WriteLine(conjunct2);
                VCExpr unQuant = gen.Or( conjunct1, conjunct2);
                List<VCExprVar> vars = new List<VCExprVar>();
                vars.Add(i); vars.Add(j);
                List<VCTrigger> trig = new List<VCTrigger>();
                VCExpr quant = gen.Forall(vars, trig, unQuant);
                return quant;
             }
            return null;
        }
        private static VCExpr lessThan( VCExpr a, VCExpr b, VCExpressionGenerator gen) {
            return gen.Function(VCExpressionGenerator.LtOp, a, b);
        }
        private static Function getFunction( String functionName, Program program)  {
            foreach(Implementation impl in program.Implementations){
                    foreach (Block b in impl.Blocks) {
                        foreach (Cmd c in b.cmds) {
                            if( c.GetType().Name.Contains("CallCmd")){
                                CallCmd cal = (CallCmd) c;
                                if(cal.Proc.Name.Equals(functionName)) {
                                    Function ret = new Function(Token.NoToken, 
                                    "my_" + functionName, cal.Proc.InParams, cal.Proc.OutParams.ElementAt(0));
                                    return ret;
                                }
                            }
                        }
                    }
                }
                return null;
            }
            
        }
    }
