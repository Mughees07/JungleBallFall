using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFixed : MonoBehaviour {

	public GameObject Magnet ;
	public float forceFactor = 10;


	void OnCollisionEnter(Collision collision)
	{


		if (collision.gameObject.name == "pink" || collision.gameObject.name == "Red" || collision.gameObject.name == "green"
			|| collision.gameObject.name == "yellow" || collision.gameObject.name == "blue" || collision.gameObject.name == "Orange") 
		{

//			GetComponent<Rigidbody>().AddForce((Magnet.transform.position - transform.position) * forceFactor * Time.smoothDeltaTime);

			//print (collision.gameObject.name);
			GetComponent<Rigidbody> ().drag = 0.1f;
		}
	}


}
