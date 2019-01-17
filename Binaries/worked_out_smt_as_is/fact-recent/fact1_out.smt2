(declare-fun my_Fact (Int) Int)
(assert
(and (and (and (and (and (and (and (and (and (exists ((nineteen Int)
(a Int) (nineteen@3 Int) (r@1 Int) (Fact_r Int) (Fact_nineteen Int)
(p2_nineteen Int) (p2_a Int) (p2_nineteen@3 Int) (p2_r@1 Int)
(p2_Fact_r Int) (p2_Fact_nineteen Int) ) (and (and (and (and (and (and
(and (or (= p2_nineteen (- 0 1)) (= p2_nineteen (* (my_Fact 18) 19)))
(<= p2_a 1)) (= p2_nineteen@3 p2_nineteen)) (= p2_r@1 1)) (not (not
(and (= p2_Fact_r p2_r@1) (= p2_Fact_nineteen p2_nineteen@3))))) (and
(and (and (and (or (= nineteen (- 0 1)) (= nineteen (* (my_Fact 18)
19))) (<= a 1)) (= nineteen@3 nineteen)) (= r@1 1)) (not (not (and (=
Fact_r r@1) (= Fact_nineteen nineteen@3)))))) (= a p2_a)) (not (=
p2_Fact_r Fact_r)))) (exists ((nineteen@@0 Int) (a@@0 Int)
(nineteen@3@@0 Int) (r@1@@0 Int) (Fact_r@@0 Int) (Fact_nineteen@@0
Int) (p2_nineteen@@0 Int) (p2_a@@0 Int) (p2_call2formal@r@0 Int)
(p2_nineteen@1 Int) (p2_nineteen@2 Int) (p2_nineteen@3@@0 Int)
(p2_r@1@@0 Int) (p2_Fact_r@@0 Int) (p2_Fact_nineteen@@0 Int) ) (and
(and (and (and (and (and (and (and (and (and (and (and (or (=
p2_nineteen@@0 (- 0 1)) (= p2_nineteen@@0 (* (my_Fact 18) 19))) (< 1
p2_a@@0)) (= p2_a@@0 19)) (= p2_nineteen@@0 (- 0 1))) (= (my_Fact
18) p2_call2formal@r@0)) (or (= p2_nineteen@1 (- 0 1)) (=
p2_nineteen@1 (* (my_Fact 18) 19)))) (= p2_nineteen@2 (*
p2_call2formal@r@0 19))) (= p2_nineteen@3@@0 p2_nineteen@2)) (=
p2_r@1@@0 p2_nineteen@2)) (not (not (and (= p2_Fact_r@@0 p2_r@1@@0) (=
p2_Fact_nineteen@@0 p2_nineteen@3@@0))))) (and (and (and (and (or (=
nineteen@@0 (- 0 1)) (= nineteen@@0 (* (my_Fact 18) 19))) (<= a@@0 1))
(= nineteen@3@@0 nineteen@@0)) (= r@1@@0 1)) (not (not (and (=
Fact_r@@0 r@1@@0) (= Fact_nineteen@@0 nineteen@3@@0)))))) (= a@@0
p2_a@@0)) (not (= p2_Fact_r@@0 Fact_r@@0))))) (exists ((nineteen@@1
Int) (a@@1 Int) (nineteen@3@@1 Int) (r@1@@1 Int) (Fact_r@@1 Int)
(Fact_nineteen@@1 Int) (p2_nineteen@@1 Int) (p2_a@@1 Int)
(p2_nineteen@3@@1 Int) (p2_r@1@@1 Int) (p2_Fact_r@@1 Int)
(p2_Fact_nineteen@@1 Int) ) (and (and (and (and (and (and (and (and
(and (or (= p2_nineteen@@1 (- 0 1)) (= p2_nineteen@@1 (* (my_Fact 18)
19))) (< 1 p2_a@@1)) (= p2_a@@1 19)) (not (= p2_nineteen@@1 (- 0 1))))
(= p2_nineteen@3@@1 p2_nineteen@@1)) (= p2_r@1@@1 p2_nineteen@@1))
(not (not (and (= p2_Fact_r@@1 p2_r@1@@1) (= p2_Fact_nineteen@@1
p2_nineteen@3@@1))))) (and (and (and (and (or (= nineteen@@1 (- 0 1))
(= nineteen@@1 (* (my_Fact 18) 19))) (<= a@@1 1)) (= nineteen@3@@1
nineteen@@1)) (= r@1@@1 1)) (not (not (and (= Fact_r@@1 r@1@@1) (=
Fact_nineteen@@1 nineteen@3@@1)))))) (= a@@1 p2_a@@1)) (not (=
p2_Fact_r@@1 Fact_r@@1))))) (exists ((nineteen@@2 Int) (a@@2 Int)
(nineteen@3@@2 Int) (r@1@@2 Int) (Fact_r@@2 Int) (Fact_nineteen@@2
Int) (p2_nineteen@@2 Int) (p2_a@@2 Int) (p2_call0formal@a@0 Int)
(p2_call2formal@r@0@@0 Int) (p2_nineteen@0 Int) (p2_r@0 Int)
(p2_nineteen@3@@2 Int) (p2_r@1@@2 Int) (p2_Fact_r@@2 Int)
(p2_Fact_nineteen@@2 Int) ) (and (and (and (and (and (and (and (and
(and (and (and (and (or (= p2_nineteen@@2 (- 0 1)) (= p2_nineteen@@2
(* (my_Fact 18) 19))) (< 1 p2_a@@2)) (not (= p2_a@@2 19))) (=
p2_call0formal@a@0 (- p2_a@@2 1))) (= (my_Fact p2_call0formal@a@0)
p2_call2formal@r@0@@0)) (or (= p2_nineteen@0 (- 0 1)) (= p2_nineteen@0
(* (my_Fact 18) 19)))) (= p2_r@0 (* p2_a@@2 p2_call2formal@r@0@@0)))
(= p2_nineteen@3@@2 p2_nineteen@0)) (= p2_r@1@@2 p2_r@0)) (not (not
(and (= p2_Fact_r@@2 p2_r@1@@2) (= p2_Fact_nineteen@@2
p2_nineteen@3@@2))))) (and (and (and (and (or (= nineteen@@2 (- 0 1))
(= nineteen@@2 (* (my_Fact 18) 19))) (<= a@@2 1)) (= nineteen@3@@2
nineteen@@2)) (= r@1@@2 1)) (not (not (and (= Fact_r@@2 r@1@@2) (=
Fact_nineteen@@2 nineteen@3@@2)))))) (= a@@2 p2_a@@2)) (not (=
p2_Fact_r@@2 Fact_r@@2))))) (exists ((nineteen@@3 Int) (a@@3 Int)
(call2formal@r@0 Int) (nineteen@1 Int) (nineteen@2 Int) (nineteen@3@@3
Int) (r@1@@3 Int) (Fact_r@@3 Int) (Fact_nineteen@@3 Int)
(p2_nineteen@@3 Int) (p2_a@@3 Int) (p2_call2formal@r@0@@1 Int)
(p2_nineteen@1@@0 Int) (p2_nineteen@2@@0 Int) (p2_nineteen@3@@3 Int)
(p2_r@1@@3 Int) (p2_Fact_r@@3 Int) (p2_Fact_nineteen@@3 Int) ) (and
(and (and (and (and (and (and (and (and (and (and (and (or (=
p2_nineteen@@3 (- 0 1)) (= p2_nineteen@@3 (* (my_Fact 18) 19))) (< 1
p2_a@@3)) (= p2_a@@3 19)) (= p2_nineteen@@3 (- 0 1))) (= (my_Fact
18) p2_call2formal@r@0@@1)) (or (= p2_nineteen@1@@0 (- 0 1)) (=
p2_nineteen@1@@0 (* (my_Fact 18) 19)))) (= p2_nineteen@2@@0 (*
p2_call2formal@r@0@@1 19))) (= p2_nineteen@3@@3 p2_nineteen@2@@0)) (=
p2_r@1@@3 p2_nineteen@2@@0)) (not (not (and (= p2_Fact_r@@3 p2_r@1@@3)
(= p2_Fact_nineteen@@3 p2_nineteen@3@@3))))) (and (and (and (and (and
(and (and (and (and (or (= nineteen@@3 (- 0 1)) (= nineteen@@3 (*
(my_Fact 18) 19))) (< 1 a@@3)) (= a@@3 19)) (= nineteen@@3 (- 0 1)))
(= (my_Fact 18) call2formal@r@0)) (or (= nineteen@1 (- 0 1)) (=
nineteen@1 (* (my_Fact 18) 19)))) (= nineteen@2 (* call2formal@r@0
19))) (= nineteen@3@@3 nineteen@2)) (= r@1@@3 nineteen@2)) (not (not
(and (= Fact_r@@3 r@1@@3) (= Fact_nineteen@@3 nineteen@3@@3)))))) (=
a@@3 p2_a@@3)) (not (= p2_Fact_r@@3 Fact_r@@3))))) (exists
((nineteen@@4 Int) (a@@4 Int) (call2formal@r@0@@0 Int) (nineteen@1@@0
Int) (nineteen@2@@0 Int) (nineteen@3@@4 Int) (r@1@@4 Int) (Fact_r@@4
Int) (Fact_nineteen@@4 Int) (p2_nineteen@@4 Int) (p2_a@@4 Int)
(p2_nineteen@3@@4 Int) (p2_r@1@@4 Int) (p2_Fact_r@@4 Int)
(p2_Fact_nineteen@@4 Int) ) (and (and (and (and (and (and (and (and
(and (or (= p2_nineteen@@4 (- 0 1)) (= p2_nineteen@@4 (* (my_Fact 18)
19))) (< 1 p2_a@@4)) (= p2_a@@4 19)) (not (= p2_nineteen@@4 (- 0 1))))
(= p2_nineteen@3@@4 p2_nineteen@@4)) (= p2_r@1@@4 p2_nineteen@@4))
(not (not (and (= p2_Fact_r@@4 p2_r@1@@4) (= p2_Fact_nineteen@@4
p2_nineteen@3@@4))))) (and (and (and (and (and (and (and (and (and (or
(= nineteen@@4 (- 0 1)) (= nineteen@@4 (* (my_Fact 18) 19))) (< 1
a@@4)) (= a@@4 19)) (= nineteen@@4 (- 0 1))) (= (my_Fact 18)
call2formal@r@0@@0)) (or (= nineteen@1@@0 (- 0 1)) (= nineteen@1@@0 (*
(my_Fact 18) 19)))) (= nineteen@2@@0 (* call2formal@r@0@@0 19))) (=
nineteen@3@@4 nineteen@2@@0)) (= r@1@@4 nineteen@2@@0)) (not (not (and
(= Fact_r@@4 r@1@@4) (= Fact_nineteen@@4 nineteen@3@@4)))))) (= a@@4
p2_a@@4)) (not (= p2_Fact_r@@4 Fact_r@@4))))) (exists ((nineteen@@5
Int) (a@@5 Int) (call2formal@r@0@@1 Int) (nineteen@1@@1 Int)
(nineteen@2@@1 Int) (nineteen@3@@5 Int) (r@1@@5 Int) (Fact_r@@5 Int)
(Fact_nineteen@@5 Int) (p2_nineteen@@5 Int) (p2_a@@5 Int)
(p2_call0formal@a@0@@0 Int) (p2_call2formal@r@0@@2 Int)
(p2_nineteen@0@@0 Int) (p2_r@0@@0 Int) (p2_nineteen@3@@5 Int)
(p2_r@1@@5 Int) (p2_Fact_r@@5 Int) (p2_Fact_nineteen@@5 Int) ) (and
(and (and (and (and (and (and (and (and (and (and (and (or (=
p2_nineteen@@5 (- 0 1)) (= p2_nineteen@@5 (* (my_Fact 18) 19))) (< 1
p2_a@@5)) (not (= p2_a@@5 19))) (= p2_call0formal@a@0@@0 (- p2_a@@5
1))) (= (my_Fact p2_call0formal@a@0@@0) p2_call2formal@r@0@@2)) (or
(= p2_nineteen@0@@0 (- 0 1)) (= p2_nineteen@0@@0 (* (my_Fact 18)
19)))) (= p2_r@0@@0 (* p2_a@@5 p2_call2formal@r@0@@2))) (=
p2_nineteen@3@@5 p2_nineteen@0@@0)) (= p2_r@1@@5 p2_r@0@@0)) (not (not
(and (= p2_Fact_r@@5 p2_r@1@@5) (= p2_Fact_nineteen@@5
p2_nineteen@3@@5))))) (and (and (and (and (and (and (and (and (and (or
(= nineteen@@5 (- 0 1)) (= nineteen@@5 (* (my_Fact 18) 19))) (< 1
a@@5)) (= a@@5 19)) (= nineteen@@5 (- 0 1))) (= (my_Fact 18)
call2formal@r@0@@1)) (or (= nineteen@1@@1 (- 0 1)) (= nineteen@1@@1 (*
(my_Fact 18) 19)))) (= nineteen@2@@1 (* call2formal@r@0@@1 19))) (=
nineteen@3@@5 nineteen@2@@1)) (= r@1@@5 nineteen@2@@1)) (not (not (and
(= Fact_r@@5 r@1@@5) (= Fact_nineteen@@5 nineteen@3@@5)))))) (= a@@5
p2_a@@5)) (not (= p2_Fact_r@@5 Fact_r@@5))))) (exists ((nineteen@@6
Int) (a@@6 Int) (nineteen@3@@6 Int) (r@1@@6 Int) (Fact_r@@6 Int)
(Fact_nineteen@@6 Int) (p2_nineteen@@6 Int) (p2_a@@6 Int)
(p2_nineteen@3@@6 Int) (p2_r@1@@6 Int) (p2_Fact_r@@6 Int)
(p2_Fact_nineteen@@6 Int) ) (and (and (and (and (and (and (and (and
(and (or (= p2_nineteen@@6 (- 0 1)) (= p2_nineteen@@6 (* (my_Fact 18)
19))) (< 1 p2_a@@6)) (= p2_a@@6 19)) (not (= p2_nineteen@@6 (- 0 1))))
(= p2_nineteen@3@@6 p2_nineteen@@6)) (= p2_r@1@@6 p2_nineteen@@6))
(not (not (and (= p2_Fact_r@@6 p2_r@1@@6) (= p2_Fact_nineteen@@6
p2_nineteen@3@@6))))) (and (and (and (and (and (and (or (= nineteen@@6
(- 0 1)) (= nineteen@@6 (* (my_Fact 18) 19))) (< 1 a@@6)) (= a@@6 19))
(not (= nineteen@@6 (- 0 1)))) (= nineteen@3@@6 nineteen@@6)) (=
r@1@@6 nineteen@@6)) (not (not (and (= Fact_r@@6 r@1@@6) (=
Fact_nineteen@@6 nineteen@3@@6)))))) (= a@@6 p2_a@@6)) (not (=
p2_Fact_r@@6 Fact_r@@6))))) (exists ((nineteen@@7 Int) (a@@7 Int)
(nineteen@3@@7 Int) (r@1@@7 Int) (Fact_r@@7 Int) (Fact_nineteen@@7
Int) (p2_nineteen@@7 Int) (p2_a@@7 Int) (p2_call0formal@a@0@@1 Int)
(p2_call2formal@r@0@@3 Int) (p2_nineteen@0@@1 Int) (p2_r@0@@1 Int)
(p2_nineteen@3@@7 Int) (p2_r@1@@7 Int) (p2_Fact_r@@7 Int)
(p2_Fact_nineteen@@7 Int) ) (and (and (and (and (and (and (and (and
(and (and (and (and (or (= p2_nineteen@@7 (- 0 1)) (= p2_nineteen@@7
(* (my_Fact 18) 19))) (< 1 p2_a@@7)) (not (= p2_a@@7 19))) (=
p2_call0formal@a@0@@1 (- p2_a@@7 1))) (= (my_Fact
p2_call0formal@a@0@@1) p2_call2formal@r@0@@3)) (or (= p2_nineteen@0@@1
(- 0 1)) (= p2_nineteen@0@@1 (* (my_Fact 18) 19)))) (= p2_r@0@@1 (*
p2_a@@7 p2_call2formal@r@0@@3))) (= p2_nineteen@3@@7
p2_nineteen@0@@1)) (= p2_r@1@@7 p2_r@0@@1)) (not (not (and (=
p2_Fact_r@@7 p2_r@1@@7) (= p2_Fact_nineteen@@7 p2_nineteen@3@@7)))))
(and (and (and (and (and (and (or (= nineteen@@7 (- 0 1)) (=
nineteen@@7 (* (my_Fact 18) 19))) (< 1 a@@7)) (= a@@7 19)) (not (=
nineteen@@7 (- 0 1)))) (= nineteen@3@@7 nineteen@@7)) (= r@1@@7
nineteen@@7)) (not (not (and (= Fact_r@@7 r@1@@7) (= Fact_nineteen@@7
nineteen@3@@7)))))) (= a@@7 p2_a@@7)) (not (= p2_Fact_r@@7
Fact_r@@7))))) (exists ((nineteen@@8 Int) (a@@8 Int) (call0formal@a@0
Int) (call2formal@r@0@@2 Int) (nineteen@0 Int) (r@0 Int)
(nineteen@3@@8 Int) (r@1@@8 Int) (Fact_r@@8 Int) (Fact_nineteen@@8
Int) (p2_nineteen@@8 Int) (p2_a@@8 Int) (p2_call0formal@a@0@@2 Int)
(p2_call2formal@r@0@@4 Int) (p2_nineteen@0@@2 Int) (p2_r@0@@2 Int)
(p2_nineteen@3@@8 Int) (p2_r@1@@8 Int) (p2_Fact_r@@8 Int)
(p2_Fact_nineteen@@8 Int) ) (and (and (and (and (and (and (and (and
(and (and (and (and (or (= p2_nineteen@@8 (- 0 1)) (= p2_nineteen@@8
(* (my_Fact 18) 19))) (< 1 p2_a@@8)) (not (= p2_a@@8 19))) (=
p2_call0formal@a@0@@2 (- p2_a@@8 1))) (= (my_Fact
p2_call0formal@a@0@@2) p2_call2formal@r@0@@4)) (or (= p2_nineteen@0@@2
(- 0 1)) (= p2_nineteen@0@@2 (* (my_Fact 18) 19)))) (= p2_r@0@@2 (*
p2_a@@8 p2_call2formal@r@0@@4))) (= p2_nineteen@3@@8
p2_nineteen@0@@2)) (= p2_r@1@@8 p2_r@0@@2)) (not (not (and (=
p2_Fact_r@@8 p2_r@1@@8) (= p2_Fact_nineteen@@8 p2_nineteen@3@@8)))))
(and (and (and (and (and (and (and (and (and (or (= nineteen@@8 (- 0
1)) (= nineteen@@8 (* (my_Fact 18) 19))) (< 1 a@@8)) (not (= a@@8
19))) (= call0formal@a@0 (- a@@8 1))) (= (my_Fact call0formal@a@0)
call2formal@r@0@@2)) (or (= nineteen@0 (- 0 1)) (= nineteen@0 (*
(my_Fact 18) 19)))) (= r@0 (* a@@8 call2formal@r@0@@2))) (=
nineteen@3@@8 nineteen@0)) (= r@1@@8 r@0)) (not (not (and (= Fact_r@@8
r@1@@8) (= Fact_nineteen@@8 nineteen@3@@8)))))) (= a@@8 p2_a@@8)) (not
(= p2_Fact_r@@8 Fact_r@@8)))))
)
(check-sat)