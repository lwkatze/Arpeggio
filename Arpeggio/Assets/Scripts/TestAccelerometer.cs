using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAccelerometer : MonoBehaviour {

	public Text test;

	string theString;

	// Update is called once per frame
	void Update () {
		theString = "X: " + Input.acceleration.x + "\nY: " + Input.acceleration.y + "\nZ: " + Input.acceleration.z;
		test.text = theString;
	}
}
