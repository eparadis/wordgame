using System;
using System.Collections.Generic;

public class WordGame
{
	static public int Main ( string[] args)
	{
		Console.WriteLine( "wordgame!");

		do_tests();	// little tests of the utility functions

		return 0;
	}

	// keep all the tests together
	static void do_tests()
	{
		Console.WriteLine( "distance from cat to dog: " + distance("cat", "dog"));
		Console.WriteLine( "distance from cat to car: " + distance("cat", "car"));

		List<string> setA = new List<string>();
		setA.Add("cat");
		setA.Add("car");
		setA.Add("bar");
		Console.WriteLine( "game 'cat car bar' valid? " + check_game(setA));
		setA.Add("dog");
		Console.WriteLine( "game 'cat car bar dog' valid? " + check_game(setA));
	
		setA.Clear();
		setA = find_words( "cat", 1);
		Console.WriteLine( "the words distance one from 'cat' are:" );
		foreach( string w in setA)
		{
			Console.Write( " " + w );
		}
		Console.WriteLine("\nend of list");
	}


	// returns the 'distance' between two words; which for this game is how many letters need to be changed between the two
	static int distance( string A, string B)
	{
		if(A.Length != B.Length)
			return -1;	// we do not consider unequal length words

		// distance should be case insensitive
		A = A.ToLower();
		B = B.ToLower();

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
		if( words.Count == 0)
			return false;	// a list with nothing in it is never valid

		if( words.Count == 1)
			return true;	// a list with a single word is always valid

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

	// find all the words with the given distance from a given word, besides the word
	static List<string> find_words( string word, int dist)
	{
		List<string> valid_words = new List<string>();

		string line;
		System.IO.StreamReader file =  new System.IO.StreamReader("/usr/share/dict/words");
		while( (line = file.ReadLine()) != null)
		{
			if( distance( word, line) == dist)
				valid_words.Add( line);
		}

		return valid_words;
	}


}
