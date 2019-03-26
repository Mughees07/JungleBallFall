using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallFall : MonoBehaviour {

	// Use this for initialization

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
			g.GetComponent<Renderer>().material = mats [Random.Range (0, mats.Length)];
			Destroy (g, 10f);

			yield return new WaitForSeconds(2f);
		
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
