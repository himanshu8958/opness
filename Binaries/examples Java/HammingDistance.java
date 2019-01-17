public class HammingDistance {
    public static int hd(int x, int y) {
	int dist = 0;
	int val = x ^ y;
	System.out.println("val :" + val );
	while (val != 0) {
	    dist++;
	    val &= val - 1;
	}
	return dist;
    }
    public static void main(String[] args) {
	int a = 2;
	int b = 3;
	System.out.println("Hamming distance between " +
			   a + " and " + b + " is : " + hd(a, b));
    }
}
