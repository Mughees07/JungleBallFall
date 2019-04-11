using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallFallController : MonoBehaviour {

	// Use this for initialization

	public GameObject MainMenuPanle;
	public GameObject levelScreenPanel;
	public GameObject gameOverPanel;
	public GameObject gameOverPanelLevelBased;

	public GameObject levelPassed;
	public GameObject levelFailed;

	public GameObject CameraController;

	public Text scroe;
	public Text highScore;
	public Text ScoreGameOver;
	public Text highScoreGameOver;

	public Transform scorePopupParent;
	public Text scorePopup;
	public Material [] mats;
	public GameObject ball;

	void Awake()
	{
		MainMenuPanle.SetActive (false);
		Variables.highScore = PlayerPrefs.GetInt (Constants.best);
		highScore.text = "Best : " + Variables.highScore;
	}
	void Start () 
	{
		if (Variables.isPlay) {
			Play ();
		} else {
			MainMenuPanle.SetActive (true);
		}
		Advertisements.Instance.Initialize ();
	}

	public Transform slide;

	IEnumerator CreateBalls()
	{
		int temp = 0;
	
		if (Variables.gameMode == Constants.endless) {
			while (true) {
				if (Variables.isPlay) {


					GameObject g = Instantiate (ball, slide);
					g.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
					int i = Random.Range (0, mats.Length);
					g.GetComponent<Renderer> ().material = mats [i];
					g.name = mats [i].name;
					Destroy (g, 10f);

					yield return new WaitForSeconds (2);

				} else {
					var balls = FindObjectsOfType<BallCollision> ();
					foreach (BallCollision BC in balls) {
						Destroy (BC.gameObject);
					}
				}
				yield return new WaitForSeconds (0f);
			}

		} else {
			print ("Level number is : " + Variables.levelNumber);
			for (int j = 0; j < Variables.numberOfBalls ; j++) 
			{
				if (Variables.isPlay)
				{
					GameObject g = Instantiate (ball, slide);
					g.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
//					int i = Random.Range (0, Variables.ballColorCount-1);
					int i = randomNumber();
					g.GetComponent<Renderer> ().material = mats [i];
					g.name = mats [i].name;
					Destroy (g, 10f);

					yield return new WaitForSeconds (2);

				} else {
					var balls = FindObjectsOfType<BallCollision> ();
					foreach (BallCollision BC in balls) {
						Destroy (BC.gameObject);
					}
				}
				yield return new WaitForSeconds (0f);
			}
			if (Variables.isPlay) {
				yield return new WaitForSeconds (4f);
				print ("Level Passed");
				levelPassed.SetActive (true);
				yield return new WaitForSeconds (2f);

				Variables.levelNumber++;
				Variables.ballColorCount = FindObjectOfType<LevelEditor> ().level [Variables.levelNumber].numberOfColor;
				Application.LoadLevel (0);
			}
			yield return new WaitForSeconds (0f);
		}


	}


	public void IncreaseScroe(int score)
	{
		Variables.score += score;
		Text g = Instantiate (scorePopup , scorePopupParent);
		Destroy (g.gameObject, 0.6f);
		g.text = "+" + score.ToString ();
		this.scroe.text = Variables.score.ToString();

	}
	public void DecreaseScroe(int score)
	{
		GameOver ();
		Variables.isPlay = false;
		return;

		Variables.score -= score;
		this.scroe.text = Variables.score.ToString();
		Text g = Instantiate (scorePopup , scorePopupParent);
		Destroy (g.gameObject, 0.45f);
		g.text = "-" + score.ToString ();

		//iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", "time", 1f, "easetype", "linear", "onupdate", "setAlpha"));	

		if (int.Parse (this.scroe.text.ToString ()) <= -3) 
		{
//			Time.timeScale = 0;
//			this.scroe.text = "Game  Over";
			GameOver();
			Variables.isPlay = false;
		}
	}


	public void Play()
	{
		if (Variables.gameMode == Constants.endless) 
		{
			scroe.gameObject.SetActive (true);
		}
		
		Variables.isPlay = true;
		//Variables.gameMode = Constants.endless;
		Variables.score = 0;
		MainMenuPanle.GetComponent<ITweenMagic> ().PlayForwardUIMovement ();
		CameraController.GetComponent<CPC_CameraPath> ().enabled = true;

		StartCoroutine (CreateBalls ());
		Advertisements.Instance.ShowBanner (BannerPosition.BOTTOM, BannerType.Banner);
	}

	public void levelScreen()
	{
		levelScreenPanel.SetActive (true);
	}

	public void levelScreenBack()
	{
		levelScreenPanel.SetActive (false);
	}

	public void GameOver()
	{
		if (Variables.gameMode == Constants.levelBase) 
		{
			gameOverPanelLevelBased.SetActive (true);
		}
		else
		{
			ScoreGameOver.text = Variables.score.ToString();
			if (Variables.score > Variables.highScore) 
			{
				highScoreGameOver.text = Variables.score.ToString();
				PlayerPrefs.SetInt (Constants.best, Variables.score);
			}
			else
				highScoreGameOver.text = Variables.highScore.ToString();

				

			gameOverPanel.SetActive (true);
		}
		Variables.isPlay = false;

		Advertisements.Instance.ShowInterstitial ();
	}
		
	public void Restart()
	{
		if (Variables.gameMode == Constants.levelBase) 
		{
			Variables.isPlay = true;
		}
		Application.LoadLevel (0);
	}

	public void Home()
	{
		Variables.gameMode = Constants.endless;
		Application.LoadLevel (0);
	}


	int last = 0;
	int randomNumber()
	{
		int j = Variables.ballColorCount - 1;
		int	i = Random.Range (0,j);
//		print ("J " + j);
		while (last == i)
		{		
			i = Random.Range (0,j);
		}

		last = i;
//		print ("return " + i);
		return i;
	}

}
