var p: [int] int;
var m: [int, int] int;
//invariant : \forall {i, j}. m[i, j] = -1 OR m[i, j] = foo(i, j, i, infinity)
procedure  mcm(i: int, j: int) returns (r: int) modifies m;{
	var k, q : int;
	var a, b :int;
	if(i == j) {
		m[i, j] := 0;
		r := 0;
	} else {
		if( m[i, j] > 0) {
			r := m[i, j];
		} else {
			k := i;
			call r := foo(i, j, k, m[i, j]);
			m[i, j] := r;
		}
	}
}

procedure {:entrypoint} foo(i: int, j: int, k:int, min :int) returns (r: int) modifies m;{
	var a, b, q : int;
	var min1 :int;
	if(k >= j) {
		r:= min;
	} else {	
		call a := mcm(i, k);
		call b := mcm(k+1, j);
		q := a + b + p[i-1] * p[k] * p[j];
		if( q < min) {
		  min1 := q;
		} else {
		  min1 := min;
		}
		call r := foo(i, j, k + 1, min1);
	}	
}
	
// m[i,j] = -1 OR m[i,j] = foo(i, j, i, -1)


//		if(q < m[i, j]) {
//			m[i, j] := q;
//		}