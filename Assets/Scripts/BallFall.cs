using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallFall : MonoBehaviour {

	// Use this for initialization

	public Material [] mats;
	public GameObject ball;
	public Transform[] waypoints;
	void Start () {

		StartCoroutine (CreateBalls ());
		
	}

	IEnumerator CreateBalls()
	{
		while (true) {
		
			GameObject g = Instantiate (ball);
			g.GetComponent<Renderer>().material = mats [Random.Range (0, mats.Length)];

			//Vector3 [] arr = new Vector3[waypoints.Length];
			//for (int i = 0; i < waypoints.Length; i++)
				//arr [i] = waypoints [i].position;

			//iTween.MoveTo(g,iTween.Hash("path", arr,"time",10f,"easetype","Linear"));
			//iTween.MoveTo(g,iTween.Hash("path", iTweenPath.GetPath("BallMovePath"),"time",10f));
			yield return new WaitForSeconds(5f);
		
		}

	}
	public void Done()
	{
		print ("Done");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
