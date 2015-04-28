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
using HMM_Test_Library;
using Leap;
using System.Collections.Generic;
using System.IO;

public class Test : MonoBehaviour {

	public HandController controller;

	private Sample sample;
	private Classifier classifier;

	void Awake() {
		sample = new Sample(100);
	}

	void Start () {
		List<HiddenMarkovModel<MultivariateNormalDistribution>> models = new List<HiddenMarkovModel<MultivariateNormalDistribution>>();

		Stream readStream = new FileStream("Front.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
		SequenceList seqF = SequenceList.Load(readStream);
		Model modelF = new Model(seqF);
		readStream.Close();

		readStream = new FileStream("Left.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
		SequenceList seqL = SequenceList.Load(readStream);
		Model modelL = new Model(seqL);
		readStream.Close();

		readStream = new FileStream("Right.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
		SequenceList seqR = SequenceList.Load(readStream);
		Model modelR = new Model(seqR);
		readStream.Close();

		readStream = new FileStream("Back.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
		SequenceList seqB = SequenceList.Load(readStream);
		Model modelB = new Model(seqB);
		readStream.Close();

		modelF.Teach();
		Stream writeStream = new FileStream("FrontModel.bin", FileMode.Create, FileAccess.Write, FileShare.None);
		modelF.Save(writeStream);
		writeStream.Close();

		modelL.Teach();
		writeStream = new FileStream("LeftModel.bin", FileMode.Create, FileAccess.Write, FileShare.None);
		modelL.Save(writeStream);
		writeStream.Close();

		modelR.Teach();
		writeStream = new FileStream("RightModel.bin", FileMode.Create, FileAccess.Write, FileShare.None);
		modelR.Save(writeStream);
		writeStream.Close();

		modelB.Teach();
		writeStream = new FileStream("BackModel.bin", FileMode.Create, FileAccess.Write, FileShare.None);
		modelB.Save(writeStream);
		writeStream.Close();

		models.Add (modelF.getModel());
		models.Add (modelL.getModel());
		models.Add (modelR.getModel());
		models.Add (modelB.getModel());

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
