var x :[int] int;
var y :[int] int;
var cache :[int, int] int;
procedure {:entrypoint} lcs(a: int, b: int) returns (r: int) modifies cache;{
  var ans1 : int;
  var ans2 : int;
  if(a == 0) { r := 0;}
  if(b == 0) { r := 0;}
  if(cache[a, b] != -1) {r := cache[a, b];}
  if(x[a - 1] == y[b - 1]) {
    call r := lcs(a - 1, b - 1);
    r := r + 1;
  } else {
    call ans1 := lcs(a, b - 1);
    call ans2 := lcs(a - 1, b);
  }
  if( ans1 > ans2) { r := ans1;}
  else { r:= ans2;}
  cache[a, b] := r;
}