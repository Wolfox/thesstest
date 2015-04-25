using UnityEngine;
using System.Collections;
using Leap;

public class TestHndCtrl : MonoBehaviour {

	protected Controller leapController;
	//protected LeapRecorder recorder_;

	public bool showHands = true;
	private long prevFrameId = 0;

	// Use this for initialization before Start()
	void Awake() {
		leapController = new Controller();
	}

	// Use this for initialization
	void Start () {
		if(leapController == null) {
			Debug.LogWarning("Cannot connect to controller.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(leapController == null) {
			return;
		}

		Frame frame = GetFrame();

		if(showHands) {
			if(frame.Id != prevFrameId) {
				prevFrameId = frame.Id;
			//UpdateHandsModels();
			}

		}
	}

	public Frame GetFrame() {
		/*if (enableRecordPlayback && recorder_.state == RecorderState.Playing)
			return recorder_.GetCurrentFrame();*/
		return leapController.Frame();
	}

	public Controller GetLeapController() {
		return leapController;
	}

}
