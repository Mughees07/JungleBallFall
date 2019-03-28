using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallFallController : MonoBehaviour {

	// Use this for initialization
	public Text scroe;

	public Transform scorePopupParent;
	public Text scorePopup;
	public Material [] mats;
	public GameObject ball;
	void Start () {

		StartCoroutine (CreateBalls ());

	}

	public Transform slide;

	IEnumerator CreateBalls()
	{
		while (true) {
		
			GameObject g = Instantiate (ball , slide );
			g.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
			int i = Random.Range (0, mats.Length);
			g.GetComponent<Renderer>().material = mats [i];
			g.name = mats [i].name;
			Destroy (g, 10f);

			yield return new WaitForSeconds(2f);
		
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
		Variables.score -= score;
		this.scroe.text = Variables.score.ToString();
		Text g = Instantiate (scorePopup , scorePopupParent);
		Destroy (g.gameObject, 0.6f);
		g.text = "-" + score.ToString ();

		//iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", "time", 1f, "easetype", "linear", "onupdate", "setAlpha"));	

		if (int.Parse (this.scroe.text.ToString ()) <= -10) 
		{
			Time.timeScale = 0;
			this.scroe.text = "GameOver";
		}
	}


	public void tim()
	{
		Time.timeScale = 1f;
	}


}
