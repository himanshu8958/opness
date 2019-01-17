(declare-fun my_mcm (Int Int) Int)
(declare-fun my_foo (Int Int Int Int) Int)
(declare-const m (Array Int Int Int))
(declare-const p2_m@0 (Array Int Int Int))
(declare-const p2_m (Array Int Int Int))
(declare-const p2_m@3 (Array Int Int Int))
(declare-const p2_mcm_m (Array Int Int Int))
(declare-const m@0 (Array Int Int Int))
(declare-const m@@0 (Array Int Int Int))
(declare-const m@3 (Array Int Int Int))
(declare-const mcm_m (Array Int Int Int))
(declare-const p2_m@@0 (Array Int Int Int))
(declare-const p2_m@2 (Array Int Int Int))
(declare-const p2_m@1 (Array Int Int Int))
(declare-const p2_m@3@@0 (Array Int Int Int))
(declare-const p2_mcm_m@@0 (Array Int Int Int))
(declare-const m@0@@0 (Array Int Int Int))
(declare-const m@@1 (Array Int Int Int))
(declare-const m@3@@0 (Array Int Int Int))
(declare-const mcm_m@@0 (Array Int Int Int))
(push)
(assert
(exists ((i Int) (j Int)
(r@1 Int) (mcm_r Int) (p2_i Int)
(p2_j Int) (p2_r@1 Int) (p2_mcm_r Int) )
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
(check-sat)
(assert
(exists ((i@@2 Int) (j@@2 Int) 
(r@1@@0 Int) (mcm_r@@0 Int) (p2_i@@0
Int) (p2_j@@0 Int) (p2_call3formal@min@0
Int) (p2_call5formal@r@0 Int)  (p2_r@1@@0 Int)
(p2_mcm_r@@0 Int) ) (and (and (and
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
