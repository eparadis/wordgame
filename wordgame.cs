using System;
using System.Collections.Generic;

public class WordGame
{
	static string wordlist_filename;

	static public int Main ( string[] args)
	{
		Console.WriteLine( "wordgame!");
		if( args.Length > 0)
			wordlist_filename = args[0];
		else
			wordlist_filename = "/usr/share/dict/words";

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
	
		write_list( find_words( "cat",1) , "the words distance one from 'cat' are:" );
		write_list( create_game("cat", 100),  "game starting with 'cat':" );
		write_list( create_game("foot", 10), "game starting with 'foot':" );
		write_list( create_game("rabbit", 10), "game starting with 'rabbit':" );
		write_list( create_game("methods", 10), "game starting with 'methods':" );
	}

	// convenience function to write out a list of words nicely
	static void write_list( List<string> words, string label )
	{
		Console.WriteLine( label );
		foreach( string w in words)
		{
			Console.Write( " " + w );
		}
		Console.WriteLine("\nend of list (" + words.Count + " elements)" );
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
		//System.IO.StreamReader file =  new System.IO.StreamReader("/usr/share/dict/words");
		System.IO.StreamReader file =  new System.IO.StreamReader(wordlist_filename);
		while( (line = file.ReadLine()) != null)
		{
			if( distance( word, line) == dist)
				valid_words.Add( line);
		}

		return valid_words;
	}

	// make a 'game', a list of unduplicated words that are each distance one from the previous
	// use the given starting word and find the given number of words
	static List<string> create_game( string start, int length)
	{
		List<string> ret = new List<string>();

		if( length == 0 )
			return ret;

		// the game starts with the first word
		ret.Add(start);

		if( length == 1 )
			return ret;		// return just the single word

		// this is grossly inefficient
		for( int i = 1; i<length; i++)
		{
			List<string> possible = find_words(ret[i-1], 1);	// get a list of possible words
			
			string next_word = null;
			foreach( string r in ret )
			{
				if( possible.Contains(r))
					possible.Remove(r);		// remove previously used words
			}

			if( possible.Count > 0 )
				next_word = possible[0];
		
			if( next_word != null )
				ret.Add(next_word);
			else
				break;	// stop trying to add words if we can't find any more
		}

		return ret;
	}
}
