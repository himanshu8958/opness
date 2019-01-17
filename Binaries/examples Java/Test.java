public class Test {
    private int[] coins;
    private int #coins;
    public static void main(String[] args ) {

    }
    // Top down dynamic solution. Cache the values as we compute them
    public int topDownMakingChangeOptimized(int c) {
        // Initialize cache with values as -1
        cache = new int[c + 1];
        for (int i = 1; i < c + 1; i++) cache[i] = -1;
        return topDownMakingChangeOptimized1(c);
    }
    
    // Overloaded recursive function
    //changes : a. make cache global
    // b. rename topDownMakingChangeOptimized to topDownMakingChangeOptimized1
    // c. tryCoins
    public int[] cache;
    private int topDownMakingChangeOptimized1(int c) {
        // Return the value if it's in the cache
        if (cache[c] >= 0) return cache[c];
        
        int minCoins = Integer.MAX_VALUE;
        
        // Try each different coin to see which is best
        for (int coin : coins) {
            if (c - coin >= 0) {
                int currMinCoins = topDownMakingChangeOptimized1(c - coin);
                if (currMinCoins < minCoins) minCoins = currMinCoins;
            }
        }
        
        // Save the value into the cache
        cache[c] = minCoins + 1;
        return cache[c];
    }
    int tryCoins(int index, int rem, int current min) {
	if( index >= #coins) return -1;
	int nuMin = topDownMakingChangeOptimized1( c 
	
		
    }
    int min ( int a , int b) {
	if ( a > b) return b;
	return a;
    }

}
