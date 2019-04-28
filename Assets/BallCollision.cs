using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

	public Transform NewContainer;
	public float forceFactor = 10;


	void Start()
	{
		NewContainer = GameObject.Find ("NewContainer").transform;
	}



	void OnTriggerEnter(Collider collision)
	{

		if (collision.CompareTag ("door")) 
		{
			transform.SetParent(NewContainer);
			GetComponent<Rigidbody> ().drag = 0.01f;
			gameObject.layer = LayerMask.NameToLayer("Default");

			if (collision.name == gameObject.name) 
			{
				if (Variables.gameMode != Constants.levelBase)
				{
					FindObjectOfType<BallFallController> ().IncreaseScroe (1,false);
				}
				else if (Variables.gameMode == Constants.levelBase)
				{
					FindObjectOfType<LevelEditor> ().LevelProgress ();
					FindObjectOfType<BallFallController> ().IncreaseScroe (Variables.levelNumber,true);
				}
			}

			else 
			{

				StartCoroutine (delayGameOver());
			}
		}
	}

	IEnumerator delayGameOver()
	{
		yield return new WaitForSeconds (0.25f);
		GetComponent<Renderer> ().enabled = false;
		Renderer rend =	transform.GetChild (0).gameObject.GetComponent<Renderer> ();
		rend.material = GetComponent<Renderer> ().material;
		rend.gameObject.SetActive (true);
		GetComponent<TrailRenderer> ().enabled = false;
		yield return new WaitForSeconds(0.5f);
		FindObjectOfType<BallFallController> ().GameOver ();

	}



}
