var cache : [[int] int, [int] int, int] int;

procedure {:entrypoint} outerHamm ( s:string,  t:[int] int, length : int) returns (r: int) modifies cache;{
var cost : int;
  if( length == 0) {
    r :=0;
  }
  if(cache[s,t,length] != -1) {
    r := cache[s,t,length];
  }
  if(s[length] == t[length]) { cost := 0;}
  else {
    cost := 1;
  }
  call r :=  outerHamm(s,t,length - 1);
  r := r + cost;
}