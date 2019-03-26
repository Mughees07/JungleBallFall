using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody>().AddForce((Magnet.transform.position - transform.position) * forceFactor * Time.smoothDeltaTime);

	}


	public GameObject Magnet ;
	public float forceFactor = 10;

}
