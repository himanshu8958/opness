(declare-fun my_fib(Int) Int)
(assert
(exists ((n Int) (cache@3 (Array Int Int)) (cache@@0 (Array Int Int))
(r@2 Int) (fib_r Int) (fib_cache (Array Int Int)) (p2_n Int)
(p2_cache@3 (Array Int Int)) (p2_cache (Array Int Int)) (p2_r@2 Int) (cache (Array Int Int))
(p2_fib_r Int) (p2_fib_cache (Array Int Int)) ) (and (and (and (and
(and (and (and (forall ((k Int) ) (or (= (select cache k) 0) (= (+
(my_fib (- k 1)) (my_fib (- k 2))) (select cache k)))) (<= p2_n 2)) (=
p2_cache@3 p2_cache)) (= p2_r@2 1)) (not (not (and (= p2_fib_r p2_r@2)
(= p2_fib_cache p2_cache@3))))) (and (and (and (and (forall ((k@@0
Int) ) (or (= (select cache k@@0) 0) (= (+ (my_fib (- k@@0 1)) (my_fib
(- k@@0 2))) (select cache k@@0)))) (<= n 2)) (= cache@3 cache@@0)) (=
r@2 1)) (not (not (and (= fib_r r@2) (= fib_cache cache@3)))))) (= n
p2_n)) (not (= p2_fib_r fib_r))))
)
(assert
(exists ((n@@0 Int) (cache@3@@0 (Array Int Int)) (cache@@1 (Array Int
Int)) (r@2@@0 Int) (fib_r@@0 Int) (fib_cache@@0 (Array Int Int)) (cache (Array Int Int))
(p2_n@@0 Int) (p2_cache@@0 (Array Int Int)) (p2_call0formal@n@0 Int)
(p2_call2formal@r@0 Int) (p2_call0formal@n@0@@0 Int)
(p2_call2formal@r@0@@0 Int) (p2_r@1 Int) (p2_cache@2 (Array Int Int))
(p2_cache@1 (Array Int Int)) (p2_cache@3@@0 (Array Int Int))
(p2_r@2@@0 Int) (p2_fib_r@@0 Int) (p2_fib_cache@@0 (Array Int Int)) )
(and (and (and (and (and (and (and (and (and (and (and (and (and (and
(and (and (forall ((k@@1 Int) ) (or (= (select cache k@@1) 0) (= (+
(my_fib (- k@@1 1)) (my_fib (- k@@1 2))) (select cache k@@1)))) (< 2
p2_n@@0)) (= (select p2_cache@@0 p2_n@@0) 0)) (= p2_call0formal@n@0 (-
p2_n@@0 1))) (= (my_fib p2_call0formal@n@0) p2_call2formal@r@0))
(forall ((k@@2 Int) ) (or (= (select cache k@@2) 0) (= (+ (my_fib (-
k@@2 1)) (my_fib (- k@@2 2))) (select cache k@@2))))) (=
p2_call0formal@n@0@@0 (- p2_n@@0 2))) (= (my_fib
p2_call0formal@n@0@@0) p2_call2formal@r@0@@0)) (forall ((k@@3 Int) )
(or (= (select cache k@@3) 0) (= (+ (my_fib (- k@@3 1)) (my_fib (-
k@@3 2))) (select cache k@@3))))) (= p2_r@1 (+ p2_call2formal@r@0
p2_call2formal@r@0@@0))) (= p2_cache@2 (store p2_cache@1 p2_n@@0
p2_r@1))) (= p2_cache@3@@0 p2_cache@2)) (= p2_r@2@@0 p2_r@1)) (not
(not (and (= p2_fib_r@@0 p2_r@2@@0) (= p2_fib_cache@@0
p2_cache@3@@0))))) (and (and (and (and (forall ((k@@4 Int) ) (or (=
(select cache k@@4) 0) (= (+ (my_fib (- k@@4 1)) (my_fib (- k@@4 2)))
(select cache k@@4)))) (<= n@@0 2)) (= cache@3@@0 cache@@1)) (= r@2@@0
1)) (not (not (and (= fib_r@@0 r@2@@0) (= fib_cache@@0
cache@3@@0)))))) (= n@@0 p2_n@@0)) (not (= p2_fib_r@@0 fib_r@@0)))) )
(assert
(exists ((n@@1 Int) (cache@3@@1 (Array Int Int)) (cache@@2
(Array Int Int)) (r@2@@1 Int) (fib_r@@1 Int) (fib_cache@@1 (Array Int
Int)) (p2_n@@1 Int) (p2_cache@@1 (Array Int Int)) (p2_r@0 Int) (cache (Array Int Int))
(p2_cache@3@@1 (Array Int Int)) (p2_r@2@@1 Int) (p2_fib_r@@1 Int)
(p2_fib_cache@@1 (Array Int Int)) ) (and (and (and (and (and (and (and
(and (and (forall ((k@@5 Int) ) (or (= (select cache k@@5) 0) (= (+
(my_fib (- k@@5 1)) (my_fib (- k@@5 2))) (select cache k@@5)))) (< 2
p2_n@@1)) (not (= (select p2_cache@@1 p2_n@@1) 0))) (= p2_r@0 (select
p2_cache@@1 p2_n@@1))) (= p2_cache@3@@1 p2_cache@@1)) (= p2_r@2@@1
p2_r@0)) (not (not (and (= p2_fib_r@@1 p2_r@2@@1) (= p2_fib_cache@@1
p2_cache@3@@1))))) (and (and (and (and (forall ((k@@6 Int) ) (or (=
(select cache k@@6) 0) (= (+ (my_fib (- k@@6 1)) (my_fib (- k@@6 2)))
(select cache k@@6)))) (<= n@@1 2)) (= cache@3@@1 cache@@2)) (= r@2@@1
1)) (not (not (and (= fib_r@@1 r@2@@1) (= fib_cache@@1
cache@3@@1)))))) (= n@@1 p2_n@@1)) (not (= p2_fib_r@@1 fib_r@@1)))) )
(assert
(exists ((n@@2 Int) (cache@@3 (Array Int Int)) (cache (Array Int Int))
(call0formal@n@0 Int) (call2formal@r@0 Int) (call0formal@n@0@@0 Int)
(call2formal@r@0@@0 Int) (r@1 Int) (cache@2 (Array Int Int)) (cache@1
(Array Int Int)) (cache@3@@2 (Array Int Int)) (r@2@@2 Int) (fib_r@@2
Int) (fib_cache@@2 (Array Int Int)) (p2_n@@2 Int) (p2_cache@@2 (Array
Int Int)) (p2_call0formal@n@0@@1 Int) (p2_call2formal@r@0@@1 Int)
(p2_call0formal@n@0@@2 Int) (p2_call2formal@r@0@@2 Int) (p2_r@1@@0
Int) (p2_cache@2@@0 (Array Int Int)) (p2_cache@1@@0 (Array Int Int))
(p2_cache@3@@2 (Array Int Int)) (p2_r@2@@2 Int) (p2_fib_r@@2 Int)
(p2_fib_cache@@2 (Array Int Int)) ) (and (and (and (and (and (and (and
(and (and (and (and (and (and (and (and (and (forall ((k@@7 Int) ) (or
(= (select cache k@@7) 0) (= (+ (my_fib (- k@@7 1)) (my_fib (- k@@7
2))) (select cache k@@7)))) (< 2 p2_n@@2)) (= (select p2_cache@@2
p2_n@@2) 0)) (= p2_call0formal@n@0@@1 (- p2_n@@2 1))) (= (my_fib
p2_call0formal@n@0@@1) p2_call2formal@r@0@@1)) (forall ((k@@8 Int) )
(or (= (select cache k@@8) 0) (= (+ (my_fib (- k@@8 1)) (my_fib (-
k@@8 2))) (select cache k@@8))))) (= p2_call0formal@n@0@@2 (- p2_n@@2
2))) (= (my_fib p2_call0formal@n@0@@2) p2_call2formal@r@0@@2)) (forall
((k@@9 Int) ) (or (= (select cache k@@9) 0) (= (+ (my_fib (- k@@9 1))
(my_fib (- k@@9 2))) (select cache k@@9))))) (= p2_r@1@@0 (+
p2_call2formal@r@0@@1 p2_call2formal@r@0@@2))) (= p2_cache@2@@0 (store
p2_cache@1@@0 p2_n@@2 p2_r@1@@0))) (= p2_cache@3@@2 p2_cache@2@@0)) (=
p2_r@2@@2 p2_r@1@@0)) (not (not (and (= p2_fib_r@@2 p2_r@2@@2) (=
p2_fib_cache@@2 p2_cache@3@@2))))) (and (and (and (and (and (and (and
(and (and (and (and (and (and (forall ((k@@10 Int) ) (or (= (select
cache k@@10) 0) (= (+ (my_fib (- k@@10 1)) (my_fib (- k@@10 2)))
(select cache k@@10)))) (< 2 n@@2)) (= (select cache@@3 n@@2) 0)) (=
call0formal@n@0 (- n@@2 1))) (= (my_fib call0formal@n@0)
call2formal@r@0)) (forall ((k@@11 Int) ) (or (= (select cache k@@11)
0) (= (+ (my_fib (- k@@11 1)) (my_fib (- k@@11 2))) (select cache
k@@11))))) (= call0formal@n@0@@0 (- n@@2 2))) (= (my_fib
call0formal@n@0@@0) call2formal@r@0@@0)) (forall ((k@@12 Int) ) (or (=
(select cache k@@12) 0) (= (+ (my_fib (- k@@12 1)) (my_fib (- k@@12
2))) (select cache k@@12))))) (= r@1 (+ call2formal@r@0
call2formal@r@0@@0))) (= cache@2 (store cache@1 n@@2 r@1))) (=
cache@3@@2 cache@2)) (= r@2@@2 r@1)) (not (not (and (= fib_r@@2
r@2@@2) (= fib_cache@@2 cache@3@@2)))))) (= n@@2 p2_n@@2)) (not (=
p2_fib_r@@2 fib_r@@2)))) )
(assert
(exists ((n@@3 Int) (cache@@4 (Array Int Int)) (cache (Array Int Int))
(call0formal@n@0@@1 Int) (call2formal@r@0@@1 Int) (call0formal@n@0@@2
Int) (call2formal@r@0@@2 Int) (r@1@@0 Int) (cache@2@@0 (Array Int
Int)) (cache@1@@0 (Array Int Int)) (cache@3@@3 (Array Int Int))
(r@2@@3 Int) (fib_r@@3 Int) (fib_cache@@3 (Array Int Int)) (p2_n@@3
Int) (p2_cache@@3 (Array Int Int)) (p2_r@0@@0 Int) (p2_cache@3@@3
(Array Int Int)) (p2_r@2@@3 Int) (p2_fib_r@@3 Int) (p2_fib_cache@@3
(Array Int Int)) ) (and (and (and (and (and (and (and (and (and
(forall ((k@@13 Int) ) (or (= (select cache k@@13) 0) (= (+ (my_fib (-
k@@13 1)) (my_fib (- k@@13 2))) (select cache k@@13)))) (< 2 p2_n@@3))
(not (= (select p2_cache@@3 p2_n@@3) 0))) (= p2_r@0@@0 (select
p2_cache@@3 p2_n@@3))) (= p2_cache@3@@3 p2_cache@@3)) (= p2_r@2@@3
p2_r@0@@0)) (not (not (and (= p2_fib_r@@3 p2_r@2@@3) (=
p2_fib_cache@@3 p2_cache@3@@3))))) (and (and (and (and (and (and (and
(and (and (and (and (and (and (forall ((k@@14 Int) ) (or (= (select
cache k@@14) 0) (= (+ (my_fib (- k@@14 1)) (my_fib (- k@@14 2)))
(select cache k@@14)))) (< 2 n@@3)) (= (select cache@@4 n@@3) 0)) (=
call0formal@n@0@@1 (- n@@3 1))) (= (my_fib call0formal@n@0@@1)
call2formal@r@0@@1)) (forall ((k@@15 Int) ) (or (= (select cache
k@@15) 0) (= (+ (my_fib (- k@@15 1)) (my_fib (- k@@15 2))) (select
cache k@@15))))) (= call0formal@n@0@@2 (- n@@3 2))) (= (my_fib
call0formal@n@0@@2) call2formal@r@0@@2)) (forall ((k@@16 Int) ) (or (=
(select cache k@@16) 0) (= (+ (my_fib (- k@@16 1)) (my_fib (- k@@16
2))) (select cache k@@16))))) (= r@1@@0 (+ call2formal@r@0@@1
call2formal@r@0@@2))) (= cache@2@@0 (store cache@1@@0 n@@3 r@1@@0)))
(= cache@3@@3 cache@2@@0)) (= r@2@@3 r@1@@0)) (not (not (and (=
fib_r@@3 r@2@@3) (= fib_cache@@3 cache@3@@3)))))) (= n@@3 p2_n@@3))
(not (= p2_fib_r@@3 fib_r@@3)))) )
(assert
(exists ((n@@4 Int) (cache@@5 (Array Int Int)) (r@0 Int) (cache (Array Int Int )) (cache@3@@4
(Array Int Int)) (r@2@@4 Int) (fib_r@@4 Int) (fib_cache@@4 (Array Int
Int)) (p2_n@@4 Int) (p2_cache@@4 (Array Int Int)) (p2_r@0@@1 Int)
(p2_cache@3@@4 (Array Int Int)) (p2_r@2@@4 Int) (p2_fib_r@@4 Int)
(p2_fib_cache@@4 (Array Int Int)) ) (and (and (and (and (and (and (and
(and (and (forall ((k@@17 Int) ) (or (= (select cache k@@17) 0) (= (+
(my_fib (- k@@17 1)) (my_fib (- k@@17 2))) (select cache k@@17)))) (<
2 p2_n@@4)) (not (= (select p2_cache@@4 p2_n@@4) 0))) (= p2_r@0@@1
(select p2_cache@@4 p2_n@@4))) (= p2_cache@3@@4 p2_cache@@4)) (=
p2_r@2@@4 p2_r@0@@1)) (not (not (and (= p2_fib_r@@4 p2_r@2@@4) (=
p2_fib_cache@@4 p2_cache@3@@4))))) (and (and (and (and (and (and
(forall ((k@@18 Int) ) (or (= (select cache k@@18) 0) (= (+ (my_fib (-
k@@18 1)) (my_fib (- k@@18 2))) (select cache k@@18)))) (< 2 n@@4))
(not (= (select cache@@5 n@@4) 0))) (= r@0 (select cache@@5 n@@4))) (=
cache@3@@4 cache@@5)) (= r@2@@4 r@0)) (not (not (and (= fib_r@@4
r@2@@4) (= fib_cache@@4 cache@3@@4)))))) (= n@@4 p2_n@@4)) (not (=
p2_fib_r@@4 fib_r@@4))))
)
(check-sat)