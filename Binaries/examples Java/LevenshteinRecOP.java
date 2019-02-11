//f(string a, string b, i) returns 1 if strings a and b agree on position i -1; otherwise 0

public class LevenshteinRec {
    
    static d[String][String][Int][Int] => int; //pseudo code
    
    // invariant : \forall {i, j, s, t}. d[s][t][i][j] = Minimum(LD(s, t, i -i, j + 1), LD(s, t, i, j -1), LD(s, t, i -1, j -1) + f(s, t, i-1))
    public static int LD (String s, String t, int i, int j) {
	if( i == 0 ) return j;
	if( j == 0 ) return i;
	if( d [s] [t] [i-1] [j-1] != null )
	    return d [s] [t] [i-1][j-1];
	cost = s.charAt(i -1) == t.charAt(j - 1) ? 0: 1;
	int ans = Minimum( LD( i-1 , j) + 1,
			   LD( i   , j-1) + 1,
			   LD( i-1 , j -1) + cost);
	d[s][t][i-1][j-1] = ans;
	return ans;
    }

    private static int Minimum (int a, int b, int c) {
	int mi;
	mi = a;
	if (b < mi) {
	    mi = b;
	}
	if (c < mi) {
	    mi = c;
	}
	return mi;
    }
    public static void main(String[] args) {
	System.out.println("Distance between " + args[0] + " and "
			   + args[1] + " is " + LD(args[0], args[1]));
    }
}
