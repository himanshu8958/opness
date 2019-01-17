// invariant : \forall i. \forall j. d[i][j] = Minimum( LD( i -i, j + 1), LD( i, j -1), LD( i -1, j -1) + f(s, t)) AND s = 'some string' AND t = 'some string'
//f(string a, string b, i) returns 1 if strings a and b agree on position i; otherwise 0

public class LevenshteinRec {
    //*****************************
    // Compute Levenshtein distance
    //*****************************
    public static int LD( String s, String t) {
	LevenshteinRec.s = s;
	LevenshteinRec.t = t;
	d = new int[s.length()][t.length()];
	return LD(s.length(), t.length());
    }

    static int d[][];
    static String s;
    static String t;
    
    public static int LD (int i, int j) {
	int cost; // cost
	if( i == 0 ) return j;
	if( j == 0 ) return i;
	if( d[i-1] [j-1] != 0 )
	    return d[i-1][j-1];
	cost = s.charAt(i -1) == t.charAt(j - 1) ? 0: 1;
	int ans = Minimum( LD( i-1 , j) + 1,
			   LD( i   , j-1) + 1,
			   LD( i-1 , j -1) + cost);
	d[i-1][j-1] = ans;
	return ans;

    }

    //****************************
    // Get minimum of three values
    //****************************
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
