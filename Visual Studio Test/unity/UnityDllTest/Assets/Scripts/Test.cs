using UnityEngine;
using System.Collections;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
//using DLLTest; //DON'T WORK, .NET4
//using DLLTest4; //DON'T WORK, .NET4
//using DLLTest3._5;
//using Accord.Statistics.Models.Markov;
//using HMM_Test_Library;
using LeapMotionFrameworkAdapter;
using Sequences;
using Leap;
using System.Collections.Generic;
using System.IO;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using HMM_Test_Library;

public class Test : MonoBehaviour {

	public HandController controller;

	private Sample sample;
	private Classifier classifier;

	void Awake() {
		sample = new Sample(100);
	}

	HiddenMarkovModel<MultivariateNormalDistribution> HelpLoad(string path) {
		return HiddenMarkovModel<MultivariateNormalDistribution>.Load(path);
	}

	static void HelpASDFG(string readPath, string writePath)
	{
		Stream readStream = new FileStream(readPath, FileMode.Open, FileAccess.Read, FileShare.Read);
		SequenceList seq = SequenceList.Load(readStream);
		readStream.Close();
		
		HiddenMarkovModel<MultivariateNormalDistribution> hmm = new HiddenMarkovModel<MultivariateNormalDistribution>(new Forward(5),
		                                                                                                              new MultivariateNormalDistribution(seq.GetArray()[0][0].Length));

		var teacher = new BaumWelchLearning<MultivariateNormalDistribution>(hmm);
            teacher.Run(seq.GetArray());
		hmm.Save(writePath);
	}
	
	void Start () {

		/*HelpASDFG("Front.bin", "FrontModel.bin");
		HelpASDFG("Back.bin", "BackModel.bin");
		HelpASDFG("Left.bin", "LeftModel.bin");
		HelpASDFG("Right.bin", "RightModel.bin");*/
		
		List<HiddenMarkovModel<MultivariateNormalDistribution>> models = new List<HiddenMarkovModel<MultivariateNormalDistribution>>();

		HiddenMarkovModel<MultivariateNormalDistribution> modelF = HelpLoad("FrontModel.bin");
		HiddenMarkovModel<MultivariateNormalDistribution> modelR = HelpLoad("RightModel.bin");
		HiddenMarkovModel<MultivariateNormalDistribution> modelL = HelpLoad("LeftModel.bin");
		HiddenMarkovModel<MultivariateNormalDistribution> modelB = HelpLoad("BackModel.bin");

		/*Stream readStream = new FileStream("Front.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
		SequenceList seq = SequenceList.Load(readStream);
		readStream.Close();

		Debug.Log("Teach");
		var teacher = new BaumWelchLearning<MultivariateNormalDistribution>(modelF);
		teacher.Run(seq.GetArray());

		Debug.Log("Saving");
		modelF.Save("testFront.bin");

		HiddenMarkovModel<MultivariateNormalDistribution> modelFT = HelpLoad("testFront.bin");*/
		
		models.Add (modelF);
		models.Add (modelL);
		models.Add (modelR);
		models.Add (modelB);

		classifier = new Classifier(models);
	}

	void Update () {
		int k = classifier.Run(sample.getSequence().GetArray());
		Debug.Log(k);
	}


	void FixedUpdate() {
		Frame frame = controller.GetFrame();
		Sign s = FrameToSign.Frame2Sign(frame);
		sample.AddSign(s);
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
