(declare-fun my_recent_fact(Int) Int)
(assert
(and (and (and (and (and (exists ((lastAns@@5 Int) (lastN@@5 Int)
 (a@@5 Int) (lastN@1@@5 Int) (lastAns@1@@5 Int) (r@1@@5 Int)
 (recent_fact_r@@5 Int) (recent_fact_lastN@@5 Int)
 (recent_fact_lastAns@@5 Int) (p2_lastAns@@5 Int) (p2_lastN@@5 Int)
 (p2_a@@5 Int) (p2_lastN@1@@5 Int) (p2_lastAns@1@@5 Int) (p2_r@1@@5
 Int) (p2_recent_fact_r@@5 Int) (p2_recent_fact_lastN@@5 Int)
 (p2_recent_fact_lastAns@@5 Int) ) (and (and (and (and (and (and (and
 (and (or (= p2_lastAns@@5 (- 0 1)) (= p2_lastAns@@5 (*
 (my_recent_fact (+ (- 0 1) p2_lastN@@5)) p2_lastN@@5))) (<= p2_a@@5
 1)) (= p2_lastN@1@@5 p2_lastN@@5)) (= p2_lastAns@1@@5 p2_lastAns@@5))
 (= p2_r@1@@5 1)) (not (not (and (and (= p2_recent_fact_r@@5
 p2_r@1@@5) (= p2_recent_fact_lastN@@5 p2_lastN@1@@5)) (=
 p2_recent_fact_lastAns@@5 p2_lastAns@1@@5))))) (and (and (and (and
 (and (or (= lastAns@@5 (- 0 1)) (= lastAns@@5 (* (my_recent_fact (+
 (- 0 1) lastN@@5)) lastN@@5))) (<= a@@5 1)) (= lastN@1@@5 lastN@@5))
 (= lastAns@1@@5 lastAns@@5)) (= r@1@@5 1)) (not (not (and (and (=
 recent_fact_r@@5 r@1@@5) (= recent_fact_lastN@@5 lastN@1@@5)) (=
 recent_fact_lastAns@@5 lastAns@1@@5)))))) (= a@@5 p2_a@@5)) (not (=
 p2_recent_fact_r@@5 recent_fact_r@@5)))) (exists ((lastAns@@6 Int)
 (lastN@@6 Int) (a@@6 Int) (lastN@1@@6 Int) (lastAns@1@@6 Int) (r@1@@6
 Int) (recent_fact_r@@6 Int) (recent_fact_lastN@@6 Int)
 (recent_fact_lastAns@@6 Int) (p2_lastAns@@6 Int) (p2_lastN@@6 Int)
 (p2_a@@6 Int) (p2_call0formal@a@0@@1 Int) (p2_call3formal@r@0@@1 Int)
 (p2_lastAns@0@@1 Int) (p2_lastN@0@@1 Int) (p2_r@0@@1 Int)
 (p2_lastN@1@@6 Int) (p2_lastAns@1@@6 Int) (p2_r@1@@6 Int)
 (p2_recent_fact_r@@6 Int) (p2_recent_fact_lastN@@6 Int)
 (p2_recent_fact_lastAns@@6 Int) ) (and (and (and (and (and (and (and
 (and (and (and (and (and (and (or (= p2_lastAns@@6 (- 0 1)) (=
 p2_lastAns@@6 (* (my_recent_fact (+ (- 0 1) p2_lastN@@6))
 p2_lastN@@6))) (< 1 p2_a@@6)) (not (and (= p2_a@@6 p2_lastN@@6) (not
 (= p2_lastAns@@6 (- 0 1)))))) (= p2_call0formal@a@0@@1 (- p2_a@@6
 1))) (= (my_recent_fact p2_call0formal@a@0@@1)
 p2_call3formal@r@0@@1)) (or (= p2_lastAns@0@@1 (- 0 1)) (=
 p2_lastAns@0@@1 (* (my_recent_fact (+ (- 0 1) p2_lastN@0@@1))
 p2_lastN@0@@1)))) (= p2_r@0@@1 (* p2_a@@6 p2_call3formal@r@0@@1))) (=
 p2_lastN@1@@6 p2_a@@6)) (= p2_lastAns@1@@6 p2_r@0@@1)) (= p2_r@1@@6
 p2_r@0@@1)) (not (not (and (and (= p2_recent_fact_r@@6 p2_r@1@@6) (=
 p2_recent_fact_lastN@@6 p2_lastN@1@@6)) (= p2_recent_fact_lastAns@@6
 p2_lastAns@1@@6))))) (and (and (and (and (and (or (= lastAns@@6 (- 0
 1)) (= lastAns@@6 (* (my_recent_fact (+ (- 0 1) lastN@@6))
 lastN@@6))) (<= a@@6 1)) (= lastN@1@@6 lastN@@6)) (= lastAns@1@@6
 lastAns@@6)) (= r@1@@6 1)) (not (not (and (and (= recent_fact_r@@6
 r@1@@6) (= recent_fact_lastN@@6 lastN@1@@6)) (=
 recent_fact_lastAns@@6 lastAns@1@@6)))))) (= a@@6 p2_a@@6)) (not (=
 p2_recent_fact_r@@6 recent_fact_r@@6))))) (exists ((lastAns@@7 Int)
 (lastN@@7 Int) (a@@7 Int) (lastN@1@@7 Int) (lastAns@1@@7 Int) (r@1@@7
 Int) (recent_fact_r@@7 Int) (recent_fact_lastN@@7 Int)
 (recent_fact_lastAns@@7 Int) (p2_lastAns@@7 Int) (p2_lastN@@7 Int)
 (p2_a@@7 Int) (p2_lastN@1@@7 Int) (p2_lastAns@1@@7 Int) (p2_r@1@@7
 Int) (p2_recent_fact_r@@7 Int) (p2_recent_fact_lastN@@7 Int)
 (p2_recent_fact_lastAns@@7 Int) ) (and (and (and (and (and (and (and
 (and (and (or (= p2_lastAns@@7 (- 0 1)) (= p2_lastAns@@7 (*
 (my_recent_fact (+ (- 0 1) p2_lastN@@7)) p2_lastN@@7))) (< 1
 p2_a@@7)) (and (= p2_a@@7 p2_lastN@@7) (not (= p2_lastAns@@7 (- 0
 1))))) (= p2_lastN@1@@7 p2_lastN@@7)) (= p2_lastAns@1@@7
 p2_lastAns@@7)) (= p2_r@1@@7 p2_lastAns@@7)) (not (not (and (and (=
 p2_recent_fact_r@@7 p2_r@1@@7) (= p2_recent_fact_lastN@@7
 p2_lastN@1@@7)) (= p2_recent_fact_lastAns@@7 p2_lastAns@1@@7)))))
 (and (and (and (and (and (or (= lastAns@@7 (- 0 1)) (= lastAns@@7 (*
 (my_recent_fact (+ (- 0 1) lastN@@7)) lastN@@7))) (<= a@@7 1)) (=
 lastN@1@@7 lastN@@7)) (= lastAns@1@@7 lastAns@@7)) (= r@1@@7 1)) (not
 (not (and (and (= recent_fact_r@@7 r@1@@7) (= recent_fact_lastN@@7
 lastN@1@@7)) (= recent_fact_lastAns@@7 lastAns@1@@7)))))) (= a@@7
 p2_a@@7)) (not (= p2_recent_fact_r@@7 recent_fact_r@@7))))) (exists
 ((lastAns@@8 Int) (lastN@@8 Int) (a@@8 Int) (call0formal@a@0@@1 Int)
 (call3formal@r@0@@1 Int) (lastAns@0@@1 Int) (lastN@0@@1 Int) (r@0@@1
 Int) (lastN@1@@8 Int) (lastAns@1@@8 Int) (r@1@@8 Int)
 (recent_fact_r@@8 Int) (recent_fact_lastN@@8 Int)
 (recent_fact_lastAns@@8 Int) (p2_lastAns@@8 Int) (p2_lastN@@8 Int)
 (p2_a@@8 Int) (p2_call0formal@a@0@@2 Int) (p2_call3formal@r@0@@2 Int)
 (p2_lastAns@0@@2 Int) (p2_lastN@0@@2 Int) (p2_r@0@@2 Int)
 (p2_lastN@1@@8 Int) (p2_lastAns@1@@8 Int) (p2_r@1@@8 Int)
 (p2_recent_fact_r@@8 Int) (p2_recent_fact_lastN@@8 Int)
 (p2_recent_fact_lastAns@@8 Int) ) (and (and (and (and (and (and (and
 (and (and (and (and (and (and (or (= p2_lastAns@@8 (- 0 1)) (=
 p2_lastAns@@8 (* (my_recent_fact (+ (- 0 1) p2_lastN@@8))
 p2_lastN@@8))) (< 1 p2_a@@8)) (not (and (= p2_a@@8 p2_lastN@@8) (not
 (= p2_lastAns@@8 (- 0 1)))))) (= p2_call0formal@a@0@@2 (- p2_a@@8
 1))) (= (my_recent_fact p2_call0formal@a@0@@2)
 p2_call3formal@r@0@@2)) (or (= p2_lastAns@0@@2 (- 0 1)) (=
 p2_lastAns@0@@2 (* (my_recent_fact (+ (- 0 1) p2_lastN@0@@2))
 p2_lastN@0@@2)))) (= p2_r@0@@2 (* p2_a@@8 p2_call3formal@r@0@@2))) (=
 p2_lastN@1@@8 p2_a@@8)) (= p2_lastAns@1@@8 p2_r@0@@2)) (= p2_r@1@@8
 p2_r@0@@2)) (not (not (and (and (= p2_recent_fact_r@@8 p2_r@1@@8) (=
 p2_recent_fact_lastN@@8 p2_lastN@1@@8)) (= p2_recent_fact_lastAns@@8
 p2_lastAns@1@@8))))) (and (and (and (and (and (and (and (and (and
 (and (or (= lastAns@@8 (- 0 1)) (= lastAns@@8 (* (my_recent_fact (+
 (- 0 1) lastN@@8)) lastN@@8))) (< 1 a@@8)) (not (and (= a@@8
 lastN@@8) (not (= lastAns@@8 (- 0 1)))))) (= call0formal@a@0@@1 (-
 a@@8 1))) (= (my_recent_fact call0formal@a@0@@1)
 call3formal@r@0@@1)) (or (= lastAns@0@@1 (- 0 1)) (= lastAns@0@@1 (*
 (my_recent_fact (+ (- 0 1) lastN@0@@1)) lastN@0@@1)))) (= r@0@@1 (*
 a@@8 call3formal@r@0@@1))) (= lastN@1@@8 a@@8)) (= lastAns@1@@8
 r@0@@1)) (= r@1@@8 r@0@@1)) (not (not (and (and (= recent_fact_r@@8
 r@1@@8) (= recent_fact_lastN@@8 lastN@1@@8)) (=
 recent_fact_lastAns@@8 lastAns@1@@8)))))) (= a@@8 p2_a@@8)) (not (=
 p2_recent_fact_r@@8 recent_fact_r@@8))))) (exists ((lastAns@@9 Int)
 (lastN@@9 Int) (a@@9 Int) (call0formal@a@0@@2 Int)
 (call3formal@r@0@@2 Int) (lastAns@0@@2 Int) (lastN@0@@2 Int) (r@0@@2
 Int) (lastN@1@@9 Int) (lastAns@1@@9 Int) (r@1@@9 Int)
 (recent_fact_r@@9 Int) (recent_fact_lastN@@9 Int)
 (recent_fact_lastAns@@9 Int) (p2_lastAns@@9 Int) (p2_lastN@@9 Int)
 (p2_a@@9 Int) (p2_lastN@1@@9 Int) (p2_lastAns@1@@9 Int) (p2_r@1@@9
 Int) (p2_recent_fact_r@@9 Int) (p2_recent_fact_lastN@@9 Int)
 (p2_recent_fact_lastAns@@9 Int) ) (and (and (and (and (and (and (and
 (and (and (or (= p2_lastAns@@9 (- 0 1)) (= p2_lastAns@@9 (*
 (my_recent_fact (+ (- 0 1) p2_lastN@@9)) p2_lastN@@9))) (< 1
 p2_a@@9)) (and (= p2_a@@9 p2_lastN@@9) (not (= p2_lastAns@@9 (- 0
 1))))) (= p2_lastN@1@@9 p2_lastN@@9)) (= p2_lastAns@1@@9
 p2_lastAns@@9)) (= p2_r@1@@9 p2_lastAns@@9)) (not (not (and (and (=
 p2_recent_fact_r@@9 p2_r@1@@9) (= p2_recent_fact_lastN@@9
 p2_lastN@1@@9)) (= p2_recent_fact_lastAns@@9 p2_lastAns@1@@9)))))
 (and (and (and (and (and (and (and (and (and (and (or (= lastAns@@9
 (- 0 1)) (= lastAns@@9 (* (my_recent_fact (+ (- 0 1) lastN@@9))
 lastN@@9))) (< 1 a@@9)) (not (and (= a@@9 lastN@@9) (not (=
 lastAns@@9 (- 0 1)))))) (= call0formal@a@0@@2 (- a@@9 1))) (=
 (my_recent_fact call0formal@a@0@@2) call3formal@r@0@@2)) (or (=
 lastAns@0@@2 (- 0 1)) (= lastAns@0@@2 (* (my_recent_fact (+ (- 0 1)
 lastN@0@@2)) lastN@0@@2)))) (= r@0@@2 (* a@@9 call3formal@r@0@@2)))
 (= lastN@1@@9 a@@9)) (= lastAns@1@@9 r@0@@2)) (= r@1@@9 r@0@@2)) (not
 (not (and (and (= recent_fact_r@@9 r@1@@9) (= recent_fact_lastN@@9
 lastN@1@@9)) (= recent_fact_lastAns@@9 lastAns@1@@9)))))) (= a@@9
 p2_a@@9)) (not (= p2_recent_fact_r@@9 recent_fact_r@@9))))) (exists
 ((lastAns@@10 Int) (lastN@@10 Int) (a@@10 Int) (lastN@1@@10 Int)
 (lastAns@1@@10 Int) (r@1@@10 Int) (recent_fact_r@@10 Int)
 (recent_fact_lastN@@10 Int) (recent_fact_lastAns@@10 Int)
 (p2_lastAns@@10 Int) (p2_lastN@@10 Int) (p2_a@@10 Int)
 (p2_lastN@1@@10 Int) (p2_lastAns@1@@10 Int) (p2_r@1@@10 Int)
 (p2_recent_fact_r@@10 Int) (p2_recent_fact_lastN@@10 Int)
 (p2_recent_fact_lastAns@@10 Int) ) (and (and (and (and (and (and (and
 (and (and (or (= p2_lastAns@@10 (- 0 1)) (= p2_lastAns@@10 (*
 (my_recent_fact (+ (- 0 1) p2_lastN@@10)) p2_lastN@@10))) (< 1
 p2_a@@10)) (and (= p2_a@@10 p2_lastN@@10) (not (= p2_lastAns@@10 (- 0
 1))))) (= p2_lastN@1@@10 p2_lastN@@10)) (= p2_lastAns@1@@10
 p2_lastAns@@10)) (= p2_r@1@@10 p2_lastAns@@10)) (not (not (and (and
 (= p2_recent_fact_r@@10 p2_r@1@@10) (= p2_recent_fact_lastN@@10
 p2_lastN@1@@10)) (= p2_recent_fact_lastAns@@10 p2_lastAns@1@@10)))))
 (and (and (and (and (and (and (or (= lastAns@@10 (- 0 1)) (=
 lastAns@@10 (* (my_recent_fact (+ (- 0 1) lastN@@10)) lastN@@10))) (<
 1 a@@10)) (and (= a@@10 lastN@@10) (not (= lastAns@@10 (- 0 1))))) (=
 lastN@1@@10 lastN@@10)) (= lastAns@1@@10 lastAns@@10)) (= r@1@@10
 lastAns@@10)) (not (not (and (and (= recent_fact_r@@10 r@1@@10) (=
 recent_fact_lastN@@10 lastN@1@@10)) (= recent_fact_lastAns@@10
 lastAns@1@@10)))))) (= a@@10 p2_a@@10)) (not (= p2_recent_fact_r@@10
 recent_fact_r@@10)))))
 )
 (check-sat)