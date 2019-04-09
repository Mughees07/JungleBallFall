using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables
{
	public static int score;
	public static int highScore;

	public static bool isPlay = false;


	public static string gameMode =Constants.endless ;





	//////////////////////////Level Base Game////////////////
	 
	public static int levelNumber = 0;

	public static float speed = 2;
	public static int numberOfBalls = 0;
	public static int ballColorCount = 0;


}


public class Constants
{
	public const string endless = "Endless";
	public const string levelBase = "levelBase";
	public const string best = "best";

}
