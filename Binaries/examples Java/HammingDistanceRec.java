//source : self
public class HammingDistanceRec {
    var cache[String][String][int] => int
	
//invariant : \forall{a, b, length}. cache[a, b, length] = -1 OR cache[a, b, length] = hd(a, b, length)
    public static int hd(String a, String b, int length) {
	if( length == 0 ) return 0;
	if(a.length() < length || b.length() < length) return -1; //error state
	if(cache[a][b][length] != null) return cache[a][b][length];

	int cost = a.charAt(length -1) == b.charAt(length -1) ? 0 : 1;
	ret =  hd(a, b, length -1) + cost;
	cache[a][b][length] = ret;
	return ret;
    }
    public static void main(String[] args) {
	System.out.println("Hamming distance between " +
			   args[0] + " and " + args[1] + " is : " +
			   hd(args[0], args[1], Integer.parseInt(args[2])));
    }
}
