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

	// Use this for initialization
	int levelIndex = 0;

	void Awake ()
	{
//		Variables.ballColorCount = level [Variables.levelNumber].numberOfColor;
		//PlayerPrefs.DeleteAll();
		levelIndex = PlayerPrefs.GetInt("level");
		print ("Leevbel" + levelIndex.ToString ());
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


	void Level()
	{
		Variables.levelNumber = int.Parse (EventSystem.current.currentSelectedGameObject.transform.GetChild (0).GetComponent<Text> ().text);

		Variables.numberOfBalls = level [Variables.levelNumber].numberOfBalls;
		Variables.speed = level [Variables.levelNumber].speed;
		Variables.ballColorCount =  level [Variables.levelNumber].numberOfColor;
		print (Variables.levelNumber);

		Variables.gameMode = Constants.levelBase;
		FindObjectOfType<BallFallController> ().Play ();

		levelScreen.SetActive (false);
	}



	public void debug()
	{
		GameObject[] g = GameObject.FindGameObjectsWithTag ("lock");
		foreach (GameObject gO in g) 
		{
			gO.SetActive (false);
		}
	}

}
