//invariant : forall k. cache[k] = 0 OR cache[k] = fib(k -1) + fib( k -2)
var cache:[int] int;
procedure {:entrypoint} fib(n: int) returns (r: int) modifies cache;{
  var a, b : int;
  if( n <= 2) {
    r := 1;
  } else {
    if(cache[n] != 0) {
      r := cache[n];
    } else {
      call a := fib(n -1);
      call b := fib(n -2);
      r := a + b;
      cache[n] := r;
    }
  }
}