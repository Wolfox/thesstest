using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;
using Sequences;

namespace HMM__Gesture_Test
{
    class MyListener : Listener
    {
        private Sequence seq = new Sequence();
        private SequenceList saveSeq = new SequenceList();

        private int sequenceSize = 10;
        public bool toSave = false;

        public override void OnConnect(Controller controller)
        {
            Console.WriteLine("Connected, using MyListener");
        }

        public Sequence getSeq()
        {
            return seq.Clone();
        }

        public SequenceList getSaveSeq()
        {
            return saveSeq;
        }

        public void clearSaveSeq()
        {
            saveSeq = new SequenceList();
        }

        public void setSequenceSize(int size)
        {
            sequenceSize = size;
        }

        public int getSequenceSize()
        {
            return sequenceSize;
        }

        public void startSaving()
        {
            seq = new Sequence();
            toSave = true;
        }

        public override void OnFrame(Controller controller)
        {
            Frame frame = controller.Frame();
            HandList hands = frame.Hands;
            Hand hand = hands.Rightmost;
            if (!hand.IsValid) { return; }
            //Console.WriteLine(hand.ToString());
            FingerList fingers = hand.Fingers;
            Finger finger;
            List<double> inputs = new List<double>();
            for (int i = 0; i < fingers.Count; i++)
            {
                finger = fingers[i];
                double[] asdfg = Array.ConvertAll(finger.Direction.ToFloatArray(), x => System.Convert.ToDouble(x));
                inputs.AddRange(asdfg);
            }
            Sign sign = new Sign(inputs.ToArray());
            HelpStuff(sign);
        }

        private void HelpStuff(Sign sign)
        {
            if (false)
            {
                seq.sequence.Add(sign);

                if (toSave && seq.sequence.Count >= sequenceSize)
                {
                    toSave = false;
                    saveSeq.sequences.Add(seq);
                }

                if (seq.sequence.Count > sequenceSize)
                {
                    seq.sequence.RemoveAt(0);
                }
            }
            else
            {
                if (Gestures.real)
                {
                    seq.sequence.Add(sign);
                    if (seq.sequence.Count > 10)
                    {
                        Gestures.RealStuff(seq);
                        seq = new Sequence();
                    }
                }
            }
        }
    }
}
