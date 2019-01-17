var nineteen: int;
procedure {:entrypoint} Fact(a: int) returns (r: int) modifies nineteen;{
	if( a <= 1) { r := 1;}
	else {
		if( a == 19) {
			if( nineteen == -1) {
				call nineteen := Fact(18);
				nineteen := nineteen * 19;
				r := nineteen;
			} else {
				r := nineteen;
			}
		} else {
			call r := Fact( a - 1);		
			r := a * r;
		}
	}
}
//procedure invariant(a: int) returns (r: int) modifies nineteen ;{
//	r := (nineteen == -1) || (nineteen == 19 * Fact(18));
//}
// function foo(int) returns (int);