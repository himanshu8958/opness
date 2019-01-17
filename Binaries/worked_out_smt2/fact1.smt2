(declare-fun my_Fact (Int) Int)
(assert
(exists ((nineteen@@7 Int) (a@@7 Int) (nineteen@3@@7 Int) (r@1@@7 Int)
(Fact_r@@7 Int) (Fact_nineteen@@7 Int) (p2_nineteen@@7 Int) (p2_a@@7
Int) (p2_call0formal@a@0@@1 Int) (p2_call2formal@r@0@@3 Int)
(p2_nineteen@0@@1 Int) (p2_r@0@@1 Int) (p2_nineteen@3@@7 Int)
(p2_r@1@@7 Int) (p2_Fact_r@@7 Int) (p2_Fact_nineteen@@7 Int) ) (and
(and (and (and (and (and (and (and (and (and (and (and (or (=
p2_nineteen@@7 (- 0 1)) (= p2_nineteen@@7 (* (my_Fact 18) 19))) (< 1
p2_a@@7)) (not (= p2_a@@7 19))) (= p2_call0formal@a@0@@1 (- p2_a@@7
1))) (= (my_Fact p2_call0formal@a@0@@1) p2_call2formal@r@0@@3)) (or
(= p2_nineteen@0@@1 (- 0 1)) (= p2_nineteen@0@@1 (* (my_Fact 18)
19)))) (= p2_r@0@@1 (* p2_a@@7 p2_call2formal@r@0@@3))) (=
p2_nineteen@3@@7 p2_nineteen@0@@1)) (= p2_r@1@@7 p2_r@0@@1)) (not (not
(and (= p2_Fact_r@@7 p2_r@1@@7) (= p2_Fact_nineteen@@7
p2_nineteen@3@@7))))) (and (and (and (and (and (and (or (= nineteen@@7
(- 0 1)) (= nineteen@@7 (* (my_Fact 18) 19))) (< 1 a@@7)) (= a@@7 19))
(not (= nineteen@@7 (- 0 1)))) (= nineteen@3@@7 nineteen@@7)) (=
r@1@@7 nineteen@@7)) (not (not (and (= Fact_r@@7 r@1@@7) (=
Fact_nineteen@@7 nineteen@3@@7)))))) (= a@@7 p2_a@@7)) (not (=
p2_Fact_r@@7 Fact_r@@7)))))

(check-sat)