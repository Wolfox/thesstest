using Leap;
using Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMM_Test_Library
{
    public static class FrameToSign
    {
        public static Sign Frame2Sign(Frame frame)
        {
            HandList hands = frame.Hands;
            Hand hand = hands.Rightmost;
            
            if (!hand.IsValid) { return null; }

            FingerList fingers = hand.Fingers;
            Finger finger;
            List<double> inputs = new List<double>();
            for (int i = 0; i < fingers.Count; i++)
            {
                finger = fingers[i];
                double[] asdfg = Array.ConvertAll(finger.Direction.ToFloatArray(), x => System.Convert.ToDouble(x));
                inputs.AddRange(asdfg);
            }
            return new Sign(inputs.ToArray());
        }
    }
}