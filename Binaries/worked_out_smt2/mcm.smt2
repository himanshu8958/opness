(declare-fun my_mcm (Int Int) Int)
(declare-fun my_foo (Int Int Int Int) Int)
(declare-const m (Array Int Int Int))
an(push)
(assert
(exists ((i Int) (j Int)
(m@0 (Array Int Int Int)) (m@@0 (Array Int Int Int)) (m@3 (Array Int
Int Int)) (r@1 Int) (mcm_r Int) (mcm_m (Array Int Int Int)) (p2_i Int)
(p2_j Int) (p2_m@0 (Array Int Int Int)) (p2_m (Array Int Int Int))
(p2_m@3 (Array Int Int Int)) (p2_r@1 Int) (p2_mcm_r Int) (p2_mcm_m
(Array Int Int Int)) )
(and (and (and (and (and (and (and (and (forall
((i@@0 Int) (j@@0 Int) ) (or (= (select m i@@0 j@@0) (- 0 1)) (=
(my_foo i@@0 j@@0 i@@0 (- 0 1)) (select m i@@0 j@@0)))) (= p2_i p2_j))
(= p2_m@0 (store p2_m p2_i p2_j 0))) (= p2_m@3 p2_m@0)) (= p2_r@1 0))
(not (not (and (= p2_mcm_r p2_r@1) (= p2_mcm_m p2_m@3))))) (and (and
(and (and (and (forall ((i@@1 Int) (j@@1 Int) ) (or (= (select m i@@1
j@@1) (- 0 1)) (= (my_foo i@@1 j@@1 i@@1 (- 0 1)) (select m i@@1
j@@1)))) (= i j)) (= m@0 (store m@@0 i j 0))) (= m@3 m@0)) (= r@1 0))
(not (not (and (= mcm_r r@1) (= mcm_m m@3)))))) (and (= i p2_i) (= j
p2_j))) (not (= p2_mcm_r mcm_r))))
)
(assert
(exists ((i@@2 Int) (j@@2 Int) (m@0@@0 (Array Int
Int Int)) (m@@1 (Array Int Int Int)) (m@3@@0 (Array Int Int Int))
(r@1@@0 Int) (mcm_r@@0 Int) (mcm_m@@0 (Array Int Int Int)) (p2_i@@0
Int) (p2_j@@0 Int) (p2_m@@0 (Array Int Int Int)) (p2_call3formal@min@0
Int) (p2_call5formal@r@0 Int) (p2_m@2 (Array Int Int Int)) (p2_m@1
(Array Int Int Int)) (p2_m@3@@0 (Array Int Int Int)) (p2_r@1@@0 Int)
(p2_mcm_r@@0 Int) (p2_mcm_m@@0 (Array Int Int Int)) ) (and (and (and
(and (and (and (and (and (and (and (and (and (forall ((i@@3 Int) (j@@3
Int) ) (or (= (select m i@@3 j@@3) (- 0 1)) (= (my_foo i@@3 j@@3 i@@3
(- 0 1)) (select m i@@3 j@@3)))) (not (= p2_i@@0 p2_j@@0))) (>= 0
(select p2_m@@0 p2_i@@0 p2_j@@0))) (= p2_call3formal@min@0 (select
p2_m@@0 p2_i@@0 p2_j@@0))) (= (my_foo p2_i@@0 p2_j@@0 p2_i@@0
p2_call3formal@min@0) p2_call5formal@r@0)) (forall ((i@@4 Int) (j@@4
Int) ) (or (= (select m i@@4 j@@4) (- 0 1)) (= (my_foo i@@4 j@@4 i@@4
(- 0 1)) (select m i@@4 j@@4))))) (= p2_m@2 (store p2_m@1 p2_i@@0
p2_j@@0 p2_call5formal@r@0))) (= p2_m@3@@0 p2_m@2)) (= p2_r@1@@0
p2_call5formal@r@0)) (not (not (and (= p2_mcm_r@@0 p2_r@1@@0) (=
p2_mcm_m@@0 p2_m@3@@0))))) (and (and (and (and (and (forall ((i@@5
Int) (j@@5 Int) ) (or (= (select m i@@5 j@@5) (- 0 1)) (= (my_foo i@@5
j@@5 i@@5 (- 0 1)) (select m i@@5 j@@5)))) (= i@@2 j@@2)) (= m@0@@0
(store m@@1 i@@2 j@@2 0))) (= m@3@@0 m@0@@0)) (= r@1@@0 0)) (not (not
(and (= mcm_r@@0 r@1@@0) (= mcm_m@@0 m@3@@0)))))) (and (= i@@2
p2_i@@0) (= j@@2 p2_j@@0))) (not (= p2_mcm_r@@0 mcm_r@@0))))
)
(assert
(exists ((i@@6 Int) (j@@6 Int) (m@0@@1 (Array Int
Int Int)) (m@@2 (Array Int Int Int)) (m@3@@1 (Array Int Int Int))
(r@1@@1 Int) (mcm_r@@1 Int) (mcm_m@@1 (Array Int Int Int)) (p2_i@@1
Int) (p2_j@@1 Int) (p2_m@@1 (Array Int Int Int)) (p2_r@0 Int)
(p2_m@3@@1 (Array Int Int Int)) (p2_r@1@@1 Int) (p2_mcm_r@@1 Int)
(p2_mcm_m@@1 (Array Int Int Int)) ) (and (and (and (and (and (and (and
(and (and (forall ((i@@7 Int) (j@@7 Int) ) (or (= (select m i@@7 j@@7)
(- 0 1)) (= (my_foo i@@7 j@@7 i@@7 (- 0 1)) (select m i@@7 j@@7))))
(not (= p2_i@@1 p2_j@@1))) (> (select p2_m@@1 p2_i@@1 p2_j@@1) 0)) (=
p2_r@0 (select p2_m@@1 p2_i@@1 p2_j@@1))) (= p2_m@3@@1 p2_m@@1)) (=
p2_r@1@@1 p2_r@0)) (not (not (and (= p2_mcm_r@@1 p2_r@1@@1) (=
p2_mcm_m@@1 p2_m@3@@1))))) (and (and (and (and (and (forall ((i@@8
Int) (j@@8 Int) ) (or (= (select m i@@8 j@@8) (- 0 1)) (= (my_foo i@@8
j@@8 i@@8 (- 0 1)) (select m i@@8 j@@8)))) (= i@@6 j@@6)) (= m@0@@1
(store m@@2 i@@6 j@@6 0))) (= m@3@@1 m@0@@1)) (= r@1@@1 0)) (not (not
(and (= mcm_r@@1 r@1@@1) (= mcm_m@@1 m@3@@1)))))) (and (= i@@6
p2_i@@1) (= j@@6 p2_j@@1))) (not (= p2_mcm_r@@1 mcm_r@@1))))
)
(assert
(exists ((i@@9 Int) (j@@9 Int) (m@@3 (Array Int Int Int))
(call3formal@min@0 Int) (call5formal@r@0 Int) (m@2 (Array Int Int
Int)) (m@1 (Array Int Int Int)) (m@3@@2 (Array Int Int Int)) (r@1@@2
Int) (mcm_r@@2 Int) (mcm_m@@2 (Array Int Int Int)) (p2_i@@2 Int)
(p2_j@@2 Int) (p2_m@@2 (Array Int Int Int)) (p2_call3formal@min@0@@0
Int) (p2_call5formal@r@0@@0 Int) (p2_m@2@@0 (Array Int Int Int))
(p2_m@1@@0 (Array Int Int Int)) (p2_m@3@@2 (Array Int Int Int))
(p2_r@1@@2 Int) (p2_mcm_r@@2 Int) (p2_mcm_m@@2 (Array Int Int Int)) )
(and (and (and (and (and (and (and (and (and (and (and (and (forall
((i@@10 Int) (j@@10 Int) ) (or (= (select m i@@10 j@@10) (- 0 1)) (=
(my_foo i@@10 j@@10 i@@10 (- 0 1)) (select m i@@10 j@@10)))) (not (=
p2_i@@2 p2_j@@2))) (>= 0 (select p2_m@@2 p2_i@@2 p2_j@@2))) (=
p2_call3formal@min@0@@0 (select p2_m@@2 p2_i@@2 p2_j@@2))) (= (my_foo
p2_i@@2 p2_j@@2 p2_i@@2 p2_call3formal@min@0@@0)
p2_call5formal@r@0@@0)) (forall ((i@@11 Int) (j@@11 Int) ) (or (=
(select m i@@11 j@@11) (- 0 1)) (= (my_foo i@@11 j@@11 i@@11 (- 0 1))
(select m i@@11 j@@11))))) (= p2_m@2@@0 (store p2_m@1@@0 p2_i@@2
p2_j@@2 p2_call5formal@r@0@@0))) (= p2_m@3@@2 p2_m@2@@0)) (= p2_r@1@@2
p2_call5formal@r@0@@0)) (not (not (and (= p2_mcm_r@@2 p2_r@1@@2) (=
p2_mcm_m@@2 p2_m@3@@2))))) (and (and (and (and (and (and (and (and
(and (forall ((i@@12 Int) (j@@12 Int) ) (or (= (select m i@@12 j@@12)
(- 0 1)) (= (my_foo i@@12 j@@12 i@@12 (- 0 1)) (select m i@@12
j@@12)))) (not (= i@@9 j@@9))) (>= 0 (select m@@3 i@@9 j@@9))) (=
call3formal@min@0 (select m@@3 i@@9 j@@9))) (= (my_foo i@@9 j@@9 i@@9
call3formal@min@0) call5formal@r@0)) (forall ((i@@13 Int) (j@@13 Int)
) (or (= (select m i@@13 j@@13) (- 0 1)) (= (my_foo i@@13 j@@13 i@@13
(- 0 1)) (select m i@@13 j@@13))))) (= m@2 (store m@1 i@@9 j@@9
call5formal@r@0))) (= m@3@@2 m@2)) (= r@1@@2 call5formal@r@0)) (not
(not (and (= mcm_r@@2 r@1@@2) (= mcm_m@@2 m@3@@2)))))) (and (= i@@9
p2_i@@2) (= j@@9 p2_j@@2))) (not (= p2_mcm_r@@2 mcm_r@@2))))
)
(assert
(exists ((i@@14 Int) (j@@14 Int) (m@@4 (Array Int Int Int))
(call3formal@min@0@@0 Int) (call5formal@r@0@@0 Int) (m@2@@0 (Array Int
Int Int)) (m@1@@0 (Array Int Int Int)) (m@3@@3 (Array Int Int Int))
(r@1@@3 Int) (mcm_r@@3 Int) (mcm_m@@3 (Array Int Int Int)) (p2_i@@3
Int) (p2_j@@3 Int) (p2_m@@3 (Array Int Int Int)) (p2_r@0@@0 Int)
(p2_m@3@@3 (Array Int Int Int)) (p2_r@1@@3 Int) (p2_mcm_r@@3 Int)
(p2_mcm_m@@3 (Array Int Int Int)) ) (and (and (and (and (and (and (and
(and (and (forall ((i@@15 Int) (j@@15 Int) ) (or (= (select m i@@15
j@@15) (- 0 1)) (= (my_foo i@@15 j@@15 i@@15 (- 0 1)) (select m i@@15
j@@15)))) (not (= p2_i@@3 p2_j@@3))) (> (select p2_m@@3 p2_i@@3
p2_j@@3) 0)) (= p2_r@0@@0 (select p2_m@@3 p2_i@@3 p2_j@@3))) (=
p2_m@3@@3 p2_m@@3)) (= p2_r@1@@3 p2_r@0@@0)) (not (not (and (=
p2_mcm_r@@3 p2_r@1@@3) (= p2_mcm_m@@3 p2_m@3@@3))))) (and (and (and
(and (and (and (and (and (and (forall ((i@@16 Int) (j@@16 Int) ) (or
(= (select m i@@16 j@@16) (- 0 1)) (= (my_foo i@@16 j@@16 i@@16 (- 0
1)) (select m i@@16 j@@16)))) (not (= i@@14 j@@14))) (>= 0 (select
m@@4 i@@14 j@@14))) (= call3formal@min@0@@0 (select m@@4 i@@14
j@@14))) (= (my_foo i@@14 j@@14 i@@14 call3formal@min@0@@0)
call5formal@r@0@@0)) (forall ((i@@17 Int) (j@@17 Int) ) (or (= (select
m i@@17 j@@17) (- 0 1)) (= (my_foo i@@17 j@@17 i@@17 (- 0 1)) (select
m i@@17 j@@17))))) (= m@2@@0 (store m@1@@0 i@@14 j@@14
call5formal@r@0@@0))) (= m@3@@3 m@2@@0)) (= r@1@@3
call5formal@r@0@@0)) (not (not (and (= mcm_r@@3 r@1@@3) (= mcm_m@@3
m@3@@3)))))) (and (= i@@14 p2_i@@3) (= j@@14 p2_j@@3))) (not (=
p2_mcm_r@@3 mcm_r@@3))))
)
(assert 
(exists ((i@@18 Int) (j@@18 Int) (m@@5 (Array Int Int Int)) (r@0 Int)
(m@3@@4 (Array Int Int Int)) (r@1@@4 Int) (mcm_r@@4 Int) (mcm_m@@4
(Array Int Int Int)) (p2_i@@4 Int) (p2_j@@4 Int) (p2_m@@4 (Array Int
Int Int)) (p2_r@0@@1 Int) (p2_m@3@@4 (Array Int Int Int)) (p2_r@1@@4
Int) (p2_mcm_r@@4 Int) (p2_mcm_m@@4 (Array Int Int Int)) ) (and (and
(and (and (and (and (and (and (and (forall ((i@@19 Int) (j@@19 Int) )
(or (= (select m i@@19 j@@19) (- 0 1)) (= (my_foo i@@19 j@@19 i@@19 (-
0 1)) (select m i@@19 j@@19)))) (not (= p2_i@@4 p2_j@@4))) (> (select
p2_m@@4 p2_i@@4 p2_j@@4) 0)) (= p2_r@0@@1 (select p2_m@@4 p2_i@@4
p2_j@@4))) (= p2_m@3@@4 p2_m@@4)) (= p2_r@1@@4 p2_r@0@@1)) (not (not
(and (= p2_mcm_r@@4 p2_r@1@@4) (= p2_mcm_m@@4 p2_m@3@@4))))) (and (and
(and (and (and (and (forall ((i@@20 Int) (j@@20 Int) ) (or (= (select
m i@@20 j@@20) (- 0 1)) (= (my_foo i@@20 j@@20 i@@20 (- 0 1)) (select
m i@@20 j@@20)))) (not (= i@@18 j@@18))) (> (select m@@5 i@@18 j@@18)
0)) (= r@0 (select m@@5 i@@18 j@@18))) (= m@3@@4 m@@5)) (= r@1@@4
r@0)) (not (not (and (= mcm_r@@4 r@1@@4) (= mcm_m@@4 m@3@@4)))))) (and
(= i@@18 p2_i@@4) (= j@@18 p2_j@@4))) (not (= p2_mcm_r@@4 mcm_r@@4))))
)
(check-sat)