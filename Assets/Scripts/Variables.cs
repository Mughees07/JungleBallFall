using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables
{
	public static int score;
	public static int highScore;
	public static int highScorelevel;

	public static int levelScore;

	public static bool isPlay = false;


	public static string gameMode = Constants.endless;


	public static int ballCountNow = 0;


	//////////////////////////Level Base Game////////////////
	 
	public static int levelNumber = 0;


	public static int numberOfBalls = 0;
	public static int ballColorCount = 0;
	public static float ballAfter = 2;

}


public class Constants
{
	public const string endless = "Endless";
	public const string levelBase = "levelBase";
	public const string best = "best";
	public const string bestlevel = "bestlevel";

}
