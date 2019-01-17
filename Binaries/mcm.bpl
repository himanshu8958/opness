var p: [int] int;
var m: [int, int] int;
procedure {:entrypoint} mcm(i: int, j: int) returns (r: int) modifies p, m;{
	var k, q : int;
	var a, b :int;
	if(i == j) {										//anon9_then
		m[i, j] := 0;
		r := 0;
	} else { 											// anno9_else
		if( m[i, j] > 0) {								// anon10_then
			r := m[i, j];
		} else {										// anno10_else
			k := i;
			while(k < j) {								//anno11_loopbody
				call a := mcm(i, k);
				call b := mcm(k+1, j);
				q := a + b + p[i-1] * p[k] * p[j];
				if( q < m[i, j]) {						//anno12_then
					m[i, j] := q;
				} else {								// anon12_else
				}
														//anon7
			k := k +1;
			}											//anon11_loopdone
			r := m[i, j];
		}
	}
}
	
// m[i,j] = -1 OR m[i,j] = mcm(i, j)