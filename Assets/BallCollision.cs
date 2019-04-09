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

			if (collision.name == gameObject.name) {
				//print ("Matched");
				FindObjectOfType<BallFallController> ().IncreaseScroe (1);
			} else {
				if (Variables.gameMode == Constants.levelBase)
				{
					Variables.isPlay = false;
					FindObjectOfType<BallFallController> ().GameOver ();
				}
				else
					FindObjectOfType<BallFallController> ().DecreaseScroe (1);
			}

		}


	}

}
