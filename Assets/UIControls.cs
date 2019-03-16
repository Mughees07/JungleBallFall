using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControls : MonoBehaviour {


	public Text y;
	public Text z;


	public void SwitchScene(int index)
	{

		SceneManager.LoadScene (index);
	}

	public void CameraVertical(bool up)
	{
		if(Camera.main.transform.position.y > -1 && Camera.main.transform.position.y < 5)
		{
			float inc = up ? 0.1f : -0.1f;
			Camera.main.transform.position += (Vector3.up * inc);
			y.text = (Camera.main.transform.position.y.ToString ());
			}
	}

	public void CameraZ(bool up)
	{
		if(Camera.main.fieldOfView > 10 && Camera.main.fieldOfView < 50)
		{
			float inc = up ? 0.1f : -0.1f;
			Camera.main.fieldOfView += inc;
			z.text = Camera.main.fieldOfView.ToString();
		}
	}

}
