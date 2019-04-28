using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LevelEditor : MonoBehaviour {


	public List<LevelClass> level = new List<LevelClass> ();

	public Transform levelGrid;
	public GameObject levelButton;
	public GameObject levelScreen;
	public GameObject levelIndicator;

	public  Text start;
	public  Text end;
	public  Image fill;


	// Use this for initialization
	int levelIndex = 0;

	void Awake ()
	{
//		PlayerPrefs.DeleteAll ();

		if (!PlayerPrefs.HasKey ("lvl")) 
		{
			PlayerPrefs.SetInt ("lvl", 1);
		}
		levelIndex = PlayerPrefs.GetInt("lvl");
		SetLevelValues ();


	}
	void Start () 
	{
		print ("COlor " + Variables.ballColorCount);
		for (int i = 0; i < level.Count-1; i++) 
		{
			GameObject g = Instantiate (levelButton, levelGrid);
			g.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text = (i+1).ToString();

			if (levelIndex >= i)
				g.transform.GetChild (1).gameObject.SetActive (false);

			g.transform.GetChild (0).GetComponent<Button> ().onClick.AddListener (Level);

		}

	}


	public void Level()
	{
		print (Variables.levelNumber);

		Variables.gameMode = Constants.levelBase;
		FindObjectOfType<BallFallController> ().Play ();
		levelIndicator.SetActive (true);
		levelScreen.SetActive (false);
	}


	void SetLevelValues()
	{
		


		Variables.levelNumber  = PlayerPrefs.GetInt("lvl");
		if (Variables.levelNumber == level.Count) 
		{
			Variables.levelNumber = 1;
			PlayerPrefs.SetInt ("lvl" , Variables.levelNumber);
		}
		Variables.numberOfBalls = level [Variables.levelNumber].numberOfBalls;
		Variables.ballAfter = level [Variables.levelNumber].ballAfter;
		Variables.ballColorCount =  level [Variables.levelNumber].numberOfColor;
		print (Variables.levelNumber+"__*__");
		start.text = Variables.levelNumber.ToString();
		end.text = (Variables.levelNumber + 1).ToString ();
		Variables.ballCountNow = 0;
	}


	public void debug()
	{
		GameObject[] g = GameObject.FindGameObjectsWithTag ("lock");
		foreach (GameObject gO in g) 
		{
			gO.SetActive (false);
		}
	}


	public void LevelProgress()
	{	
		Variables.ballCountNow++;
		//print (Variables.ballCountNow + " " + Variables.numberOfBalls);
		//print (Variables.ballCountNow / Variables.numberOfBalls + "");
		fill.fillAmount = float.Parse(Variables.ballCountNow.ToString()) / float.Parse(Variables.numberOfBalls.ToString());
	}

}
