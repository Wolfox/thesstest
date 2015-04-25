using UnityEngine;
using System.Collections;
//using DLLTest; //DON'T WORK, .NET4
//using DLLTest4; //DON'T WORK, .NET4
//using DLLTest3._5;
//using Accord.Statistics.Models.Markov;
//using HMM_Test_Library;
using LeapMotionFrameworkAdapter;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//multiModalFrameworkTest();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void multiModalFrameworkTest() {
		LeapMotionManager control = new LeapMotionManager();
		if(control.isConnected()) {
			Debug.Log("Connected");
		} else {
			Debug.Log ("Not Connected");
		}
	}
}
