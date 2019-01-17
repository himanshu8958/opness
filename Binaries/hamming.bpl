var s : [int] int;
var t : [int] int;
var cache : [int] int;
//invariant: forall i. cache[i] == -1 OR cache[i] == H(i)
procedure {:entrypoint} hamming(length : int) returns (r: int) modifies cache;{
var cost : int;
  if( length == 0) {
    r :=0;
  }
  if(cache[length] != -1) {
    r := cache[length];
  }
  if(s[length] == t[length]) { cost := 0;}
  else {
    cost := 1;
  }
  call r :=  hamming(length - 1);
  r := r + cost;
}