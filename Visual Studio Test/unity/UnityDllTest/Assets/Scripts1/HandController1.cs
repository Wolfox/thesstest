//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using Leap;
//
//public class HandController : MonoBehaviour {
//
//	protected Controller leapController;
//	//protected LeapRecorder recorder_;
//
//	public bool showHands = true;
//	private long prevFrameId = 0;
//
//	protected Dictionary<int, HandModel> hand_graphics_;
//
//	public Vector3 handMovementScale = Vector3.one;
//
//	// Use this for initialization before Start()
//	void Awake() {
//		leapController = new Controller();
//	}
//
//	// Use this for initialization
//	void Start() {
//		if(leapController == null) {
//			Debug.LogWarning("Cannot connect to controller.");
//		}
//	}
//	
//	// Update is called once per frame
//	void Update() {
//		if(leapController == null) {
//			return;
//		}
//
//		Frame frame = GetFrame();
//
//		if(showHands) {
//			if(frame.Id != prevFrameId) {
//				prevFrameId = frame.Id;
//			//UpdateHandsModels();
//			}
//		} else {
//			//Destroy Hands
//		}
//	}
//
//	void FixedUpdate() {
//		if(leapController == null) {
//			return;
//		}
//
//		Frame frame = GetFrame();
//		
//		if (frame.Id != prevFrameId)
//		{
//			/*UpdateHandModels(hand_physics_, frame.Hands, leftPhysicsModel, rightPhysicsModel);
//			UpdateToolModels(tools_, frame.Tools, toolModel);
//			prev_physics_id_ = frame.Id;*/
//		}
//	}
//
//	public Frame GetFrame() {
//		/*if (enableRecordPlayback && recorder_.state == RecorderState.Playing)
//			return recorder_.GetCurrentFrame();*/
//		return leapController.Frame();
//	}
//
//	public Controller GetLeapController() {
//		return leapController;
//	}
//
//	protected void UpdateHandModels(
//		Dictionary<int, HandModel> allHands,
//		HandList leapHands,
//		HandModel handModel
//		//HandModel left_model,
//		//HandModel right_model
//		) {
//		List<int> idsToCheck = new List<int>(allHands.Keys);
//		
//		// Go through all the active hands and update them.
//		for (int i = 0; i < leapHands.Count; ++i) {
//			Hand leapHand = leapHands[i];
//
//			//HandModel model = (mirrorZAxis != leap_hand.IsLeft) ? left_model : right_model;
//			HandModel model = handModel;
//			
//			// If we've mirrored since this hand was updated, destroy it.
//			/*if (allHands.ContainsKey(leapHand.Id) &&
//			    allHands[leapHand.Id].IsMirrored() != mirrorZAxis) {
//				DestroyHand(allHands[leapHand.Id]);
//				allHands.Remove(leapHand.Id);
//			}*/
//			
//			// Only create or update if the hand is enabled.
//			if (model != null) {
//				idsToCheck.Remove(leapHand.Id);
//				
//				// Create the hand and initialized it if it doesn't exist yet.
//				if (!allHands.ContainsKey(leapHand.Id)) {
//					HandModel newHand = CreateHand(model);
//					newHand.SetLeapHand(leapHand);
//					newHand.MirrorZAxis(mirrorZAxis);
//					newHand.SetController(this);
//					
//					// Set scaling based on reference hand.
//					float hand_scale = MM_TO_M * leapHand.PalmWidth / newHand.handModelPalmWidth;
//					newHand.transform.localScale = hand_scale * transform.lossyScale;
//					
//					newHand.InitHand();
//					newHand.UpdateHand();
//					allHands[leapHand.Id] = newHand;
//				}
//				else {
//					// Make sure we update the Leap Hand reference.
//					HandModel hand_model = allHands[leapHand.Id];
//					hand_model.SetLeapHand(leapHand);
//					hand_model.MirrorZAxis(mirrorZAxis);
//					
//					// Set scaling based on reference hand.
//					float hand_scale = MM_TO_M * leapHand.PalmWidth / hand_model.handModelPalmWidth;
//					hand_model.transform.localScale = hand_scale * transform.lossyScale;
//					hand_model.UpdateHand();
//				}
//			}*/
//		}
//		
//		// Destroy all hands with defunct IDs.
//		/*for (int i = 0; i < ids_to_check.Count; ++i) {
//			DestroyHand(all_hands[ids_to_check[i]]);
//			all_hands.Remove(ids_to_check[i]);
//		}*/
//	}
//
//	protected HandModel CreateHand(HandModel model) {
//		HandModel hand_model = Instantiate(model, transform.position, transform.rotation)
//			as HandModel;
//		hand_model.gameObject.SetActive(true);
//		//Leap.Utils.IgnoreCollisions(hand_model.gameObject, gameObject);
//		return hand_model;
//	}
//}
