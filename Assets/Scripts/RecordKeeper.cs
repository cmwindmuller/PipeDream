using UnityEngine;
using System.Collections;

public class RecordKeeper {
	private const string HIGH_SCORE = "high_score";
	private static int highScore;
	private static bool gotScore;

	public static int GetHighScore()
	{
		if(!gotScore)
		{
			gotScore = true;
			highScore = PlayerPrefs.GetInt(HIGH_SCORE);
		}
		return highScore;
	}

	public static bool NewScore(int newScore)
	{
		if(newScore > highScore)
		{
			highScore = newScore;
			PlayerPrefs.SetInt(HIGH_SCORE,newScore);
			return true;
		}
		return false;
	}
}
