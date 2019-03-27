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



	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Slider" && gameObject.tag == "Ball") 
		{
			gameObject.tag = "Passed";
			print ("___________________________");

			print ("Ball " + gameObject.name);
			print ("Slider " + collision.gameObject.name);

			if (collision.gameObject.name.Contains (gameObject.name))
			{
				FindObjectOfType<BallFallController> ().IncreaseScroe (1);
				print ("Score increment");
			} 
			else if (!collision.gameObject.name.Contains (gameObject.name))
			{
				collision.gameObject.AddComponent<SwipeController> ();
				Time.timeScale = 0;
				print ("Score decrement");
				FindObjectOfType<BallFallController> ().DecreaseScroe (1);
			}
		}
//		if (collision.gameObject.name == "pink" || collision.gameObject.name == "Red" || collision.gameObject.name == "green"
//			|| collision.gameObject.name == "yellow" || collision.gameObject.name == "blue" || collision.gameObject.name == "Orange") 
//		{
//
////			GetComponent<Rigidbody>().AddForce((Magnet.transform.position - transform.position) * forceFactor * Time.smoothDeltaTime);
//			transform.SetParent(NewContainer);
//			//print (collision.gameObject.name);
//			GetComponent<Rigidbody> ().drag = 0.01f;
//			gameObject.layer = LayerMask.NameToLayer("Default");
//		}
	}

	void OnTriggerEnter(Collider collision)
	{


		if (collision.gameObject.name == "door") 
		{

			transform.SetParent(NewContainer);
			GetComponent<Rigidbody> ().drag = 0.01f;
			gameObject.layer = LayerMask.NameToLayer("Default");
		}
	}

}
