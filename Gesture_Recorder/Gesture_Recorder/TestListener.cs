using Leap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gesture_Recorder
{
    class TestListener : Listener
    {

        private int numOfFrames;
        private bool active;


        public void Init() {
            numOfFrames = 0;
            active = false;
        }

        public override void OnConnect(Controller controller) {
            Console.WriteLine("Connected, using TestListener");
        }

        public override void OnFrame(Controller controller) {
            if (active) {
                numOfFrames++;
            }
        }

        public void startCounting()  {
            numOfFrames = 0;
            active = true;
        }

        public int endCounting() {
            active = false;
            return numOfFrames;
        }

    }
}
