using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using Sequences;

namespace GestureRecorder.Tests
{
    class SampleListener : Listener
    {
        private Object thisLock = new Object();
        private Sequence s = new Sequence();
        private List<Frame> frameList = new List<Frame>();
        public List<List<Frame>> frameListList = new List<List<Frame>>();

        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        public override void OnConnect(Controller controller)
        {
            SafeWriteLine("Connected, using SampleListener");
        }


        public void HelpRecordFrame(Frame frame)
        {
            frameList.Add(frame);
            if (frameList.Count >= 10)
            {
                frameListList.Add(frameList);
                frameList = new List<Frame>();
            }
        }

        public override void OnFrame(Controller controller)
        {
            HelpRecordFrame(controller.Frame());
            return;
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

        private void HelpStuff(Sign c)
        {
            s.sequence.Add(c);
            if (s.sequence.Count > 10)
            {
                if (Gestures.read)
                {
                    Gestures.Stuff(s);
                }
                if (Gestures.real)
                {
                    Gestures.RealStuff(s);
                }
                s = new Sequence();
            }
        }
    }
}
