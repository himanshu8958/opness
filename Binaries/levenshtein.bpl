var cache: [int , int] int;
var x: [int] int;
var y: [int] int;
procedure {:entrypoint} levenshtein(lenX :int, lenY:int ) returns (r: int) modifies cache;{
  var cost: int;
  var ans: int;
  var ans1 : int;
  var ans2 : int;
  var ans3 : int;
  if(lenX == 0) { r := lenY;}
  if(lenY == 0) { r := lenX;}
  if(cache[lenX, lenY] != -1) { r := cache[lenX, lenY];}
  else {
    if(x[lenX-1] == y[lenY-1]) { cost := 0;}
    else { cost := 1;}
    // call cost := getCost(lenX, lenY);
    call ans1 := levenshtein(lenX - 1, lenY);
    ans1 := ans1 + 1;
    call ans2 := levenshtein(lenX, lenY -1);
    ans2 := ans2 + 1;
    call ans3 := levenshtein(lenX -1, lenY -1);
    ans3 := ans3 + cost;

    if(ans1 < ans2) {
      if(ans1 < ans3) { ans := ans1;}
      else { ans := ans3;}
    } else {
      if(ans2 < ans3) {ans := ans2;}
      else { ans := ans3;}
    }

   cache[lenX,lenY] := ans;
   r:=ans;
  }
}
