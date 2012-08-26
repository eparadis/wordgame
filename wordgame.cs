using System;

public class WordGame
{
	static public int Main ( string[] args)
	{
		Console.WriteLine( "wordgame!");

		Console.WriteLine( "distance from cat to dog: " + distance("cat", "dog"));
		Console.WriteLine( "distance from cat to car: " + distance("cat", "car"));

		return 0;
	}

	// returns the 'distance' between two words; which for this game is how many letters need to be changed between the two
	static int distance( string A, string B)
	{
		if(A.Length != B.Length)
			return -1;	// we do not consider unequal length words

		int count = 0;
		for( int i = 0; i<A.Length; i++)
		{
			if(A[i] != B[i])
				count++;
		}

		return count;
	}
}
