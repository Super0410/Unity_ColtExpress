using System.Collections;
using System.Collections.Generic;

public static class Utillity
{

	#region Public Methods

	public static T[] ShuffleArray<T> (T[] array, int seed)
	{
		//pseudo random number generator
		System.Random prng = new System.Random (seed);

		for (int i = 0; i < array.Length - 1; i++) {
			int randomIndex = prng.Next (i, array.Length);
			T thisObj = array [i];
			array [i] = array [randomIndex];
			array [randomIndex] = thisObj;
		}

		return array;
	}

	public static T[] ShuffleArray<T> (T[] array)
	{
		//pseudo random number generator
		System.Random prng = new System.Random ();

		for (int i = 0; i < array.Length - 1; i++) {
			int randomIndex = prng.Next (i, array.Length);
			T thisObj = array [i];
			array [i] = array [randomIndex];
			array [randomIndex] = thisObj;
		}

		return array;
	}

	#endregion

}
