var cache: [[int] int, [int] int, int , int] int;
procedure {:entrypoint} levenshtein(x:[int] int, y:[int] int, lenX :int, lenY:int ) returns (r: int) modifies cache;{
  var cost: int;
  var ans: int;
  var ans1 : int;
  var ans2 : int;
  var ans3 : int;
  if(lenX == 0) { r := lenY;}
  if(lenY == 0) { r := lenX;}
  if(cache[x,y,lenX, lenY] != -1) { r := cache[x,y,lenX, lenY];}
  else {
    if(x[lenX-1] == y[lenY-1]) { cost := 0;}
    else { cost := 1;}
    call ans1 := levenshtein( x, y, lenX - 1, lenY);
    ans1 := ans1 + 1;
    call ans2 := levenshtein( x, y, lenX, lenY -1);
    ans2 := ans2 + 1;
    call ans3 := levenshtein( x, y, lenX -1, lenY -1);
    ans3 := ans3 + cost;
   if( ans1 <= ans2 && ans2 <= ans3) { ans := ans1;}	//1<2<3
   if( ans1 <= ans3 && ans3 <= ans2) { ans := ans1;}	//1<3<2
   if( ans2 <= ans1 && ans1 <= ans3) { ans := ans2;}	//2<1<3
   if( ans2 <= ans3 && ans3 <= ans1) { ans := ans2;}	//2<3<1
   if( ans3 <= ans2 && ans2 <= ans3) { ans := ans3;}	//3<2<1
   if( ans3 <= ans1 && ans1 <= ans2) { ans := ans3;}	//3<1<2
   cache[x,y,lenX,lenY] := ans;
   r:=ans;
  }
}
