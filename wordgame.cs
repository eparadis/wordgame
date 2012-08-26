using System;
using System.Collections.Generic;

public class WordGame
{
	static public int Main ( string[] args)
	{
		Console.WriteLine( "wordgame!");

		Console.WriteLine( "distance from cat to dog: " + distance("cat", "dog"));
		Console.WriteLine( "distance from cat to car: " + distance("cat", "car"));

		List<string> setA = new List<string>();
		setA.Add("cat");
		setA.Add("car");
		setA.Add("bar");
		Console.WriteLine( "game 'cat car bar' valid? " + check_game(setA));
		setA.Add("dog");
		Console.WriteLine( "game 'cat car bar dog' valid? " + check_game(setA));


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

	// given a list of words, make sure the distance between each word is only one
	static bool check_game( List<string> words )
	{
		bool valid = true;
		string prev = words[0];
		for(int i = 1; i<words.Count; i++)
		{
			if(distance(prev, words[i]) != 1)
			{
				valid = false;
				break;
			}
			prev = words[i];
		}

		return valid;
	}
}
