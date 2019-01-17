var g: [int] int;
procedure {:entrypoint} FactArray(n: int) returns (r: int) modifies g;{
  var k :int;
  if( n <= 1) { r := 1;}
  else {
    if( g[n] == 0) {
      call k := FactArray(n - 1);
      g[n] := k * n;
    } 
    r  := g[n];
  }
}