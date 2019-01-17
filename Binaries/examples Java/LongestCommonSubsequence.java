
public class LongestCommonSubsequence 
{ 

    /* Returns length of LCS for X[0..m-1], Y[0..n-1] */

    
        var cache[char[]][char[]][int][int] => int;
    
    // invariant : \forall { X, Y, m, n}. cache[X, Y, m, n] = 0 OR cache[X, Y, m, n] = lcs(X, Y, m, n)
    int lcs( char[] X, char[] Y, int m, int n ) { 
	if (m == 0 || n == 0) 
	    return 0;
	if(cache[X][Y][m][n] != null )
	    return cache[X][Y][m][n];
	if (X[m-1] == Y[n-1]) {
	    ret = 1 + lcs(X, Y, m-1, n-1);
	} else {
	    ret =  max(lcs(X, Y, m, n-1), lcs(X, Y, m-1, n));
	}
	cache[X][Y][m][n] = ret;
	return ret;
    } 


	int max(int a, int b) 
    { 
	return (a > b)? a : b; 
    } 

    public static void main(String[] args) 
    { 
	LongestCommonSubsequence lcs = new LongestCommonSubsequence(); 
	String s1 = "AGGTAB"; 
	String s2 = "GXTXAYB"; 

	char[] X=s1.toCharArray(); 
	char[] Y=s2.toCharArray(); 
	int m = X.length; 
	int n = Y.length; 

	System.out.println("Length of LCS is" + " " + 
			   lcs.lcs( X, Y, m, n ) ); 
    } 

} 

// This Code is Contributed by Saket Kumar 
