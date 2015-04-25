using Leap;
using Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMM__Gesture_Test
{
    class ReadListener : Listener
    {
        //private Gesture

        private int numOfFramesPerSeq;
        private List<byte[]> sequence;
        private Gesture1 parent;

        public void Initialization(int num, Gesture1 p)
        {
            numOfFramesPerSeq = num;
            parent = p;
            sequence = new List<byte[]>();
        }

        public override void OnConnect(Controller controller)
        {
            Console.WriteLine("Connected, using SampleListener");
        }

        public override void OnFrame(Controller controller)
        {
            if (parent.state != Gesture1.GestureState.Reading) { return; }

            Frame frame = controller.Frame();
            HandList hands = frame.Hands;
            Hand hand = hands.Rightmost;
            if (!hand.IsValid) { return; }
            if (hands.Count > 1) { Console.WriteLine("MORE THAN 1 HAND"); return; }

            sequence.Add(frame.Serialize);
            if (sequence.Count >= numOfFramesPerSeq)
            {
                parent.Store(sequence);
                sequence = new List<byte[]>();
            }
        }
    }
}
