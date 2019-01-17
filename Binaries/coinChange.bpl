var coins : [int] int;
var cache : [int] int;
var numOfCoins : int;

procedure tryCoins(index : int, rem: int, min : int) returns (r: int) {
  var curMin: int;
  if( index > numOfCoins) {
    r := min;
  } else {
    if(curMin + 1 < min) {
      call r := tryCoins(index + 1, rem - coins[index], curMin + 1);
    } else {
      call r := tryCoins(index + 1, rem, curMin);
    }
  }
}

// invariant : \forall i. cache[i] = -1 OR cache[i] tryCoins(1, -1)
procedure {:entrypoint} makeChange(c: int) returns (r: int) modifies cache;{
  var min : int;
  if(cache[c] >= 0)  {
    r := cache[c];
  } else {
  min := -1;
  }
}
