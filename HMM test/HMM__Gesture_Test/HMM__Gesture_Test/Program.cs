using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Leap;
using Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HMM__Gesture_Test
{
    class Program
    {
        static string MAIN_PATH = "Frames/";

        static void Main(string[] args)
        {

            CulturalLayer cl = new CulturalLayer();
            List<string> gest = new List<string>();
            List<string> ptGest = new List<string>();
            List<string> nlGest = new List<string>();

            gest.Add("WAVE");
            gest.Add("POINT");
            gest.Add("DRINK");

            cl.AddCustomGesture("WAVE", "HI");
            cl.AddCustomGesture("POINT", "HAND");
            cl.AddCustomGesture("DRINK", "DRUNK");

            cl.AddCultureGesture("WAVE", "PT", "OLA");
            cl.AddCultureGesture("WAVE", "NL", "HALLO");
            cl.AddCultureGesture("POINT", "PT", "APONTAR");

            Console.WriteLine("Wave in PT:" + cl.GetGestureName("WAVE", "PT"));
            Console.WriteLine("Wave in NL:" + cl.GetGestureName("WAVE", "NL"));
            Console.WriteLine("Point in PT:" + cl.GetGestureName("POINT", "PT"));
            Console.WriteLine("Point in NL:" + cl.GetGestureName("POINT", "NL"));
            Console.WriteLine("Drink in PT:" + cl.GetGestureName("DRINK", "PT"));
            Console.WriteLine("Drink in NL:" + cl.GetGestureName("DRINK", "NL"));

            ptGest = cl.GetGesturesNames(gest, "PT");
            nlGest = cl.GetGesturesNames(gest, "NL");

            Console.WriteLine("Wave in PT:" + ptGest[0]);
            Console.WriteLine("Wave in NL:" + nlGest[0]);
            Console.WriteLine("Point in PT:" + ptGest[1]);
            Console.WriteLine("Point in NL:" + nlGest[1]);
            Console.WriteLine("Drink in PT:" + ptGest[2]);
            Console.WriteLine("Drink in NL:" + nlGest[2]);

            //HMM.Test();
            //Gestures.TestReal();
            //Gestures.Test();
            //Gestures.Test2();
            //HMM2.Test();

            /*Stream readStream = new FileStream("Back.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList sl = SequenceList.Load(readStream);
            readStream.Close();

            Console.WriteLine(sl.sequences[0].GetDimensions());*/

            //Gestures.ToTest();

            /*SequenceList samples = new SequenceList();

            Stream writeStream = new FileStream("ASD.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            samples.Save(writeStream);
            writeStream.Close();*/

            /*List<List<Frame>> listFrame = Utils.LoadListListFrame("Frames/Right.bin");
            SequenceList seqList = Utils.FramesToSequenceList(listFrame);
            Utils.SaveSequenceList(seqList, "Right.bin");*/

            MainChoice();

            /*List<List<Frame>> frames = Utils.LoadListListFrame("Frames/Close.bin");
            SequenceList seqlist = Utils.FramesToSequenceList(frames);
            Stream writeStream = new FileStream("Close.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            seqlist.Save(writeStream);
            writeStream.Close();*/

            /*List<List<Frame>> frames = Utils.LoadListListFrame("Frames/Open.bin");
            SequenceList seq = Utils.FramesToSequenceList(frames);

            Stream writeStream = new FileStream("Open.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            seq.Save(writeStream);
            writeStream.Close();*/

            /*Stream readStream = new FileStream("Front.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList inputSeq = SequenceList.Load(readStream);
            readStream.Close();

            HiddenMarkovModel<MultivariateNormalDistribution> hmm;
            hmm = new HiddenMarkovModel<MultivariateNormalDistribution>(new Forward(5),
               new MultivariateNormalDistribution(inputSeq.GetArray()[0][0].Length));

            hmm.Save("TestModel.bin");*/

            //HelpASDFG("Front.bin", "FrontModel.bin");
            //HelpASDFG("Back.bin", "BackModel.bin");
            //HelpASDFG("Left.bin", "LeftModel.bin");
            //HelpASDFG("Right.bin", "RightModel.bin");

            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();

        }

        static void HelpASDFG(string readPath, string writePath)
        {
            Console.WriteLine("Loading");
            Stream readStream = new FileStream(readPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seq = SequenceList.Load(readStream);
            readStream.Close();

            HiddenMarkovModel<MultivariateNormalDistribution> hmm = new HiddenMarkovModel<MultivariateNormalDistribution>(new Forward(5),
                new MultivariateNormalDistribution(seq.GetArray()[0][0].Length));

            Console.WriteLine("Teaching");
            var teacher = new BaumWelchLearning<MultivariateNormalDistribution>(hmm);
            teacher.Run(seq.GetArray());

            Console.WriteLine("Saving");
            hmm.Save(writePath);
        }

        static void MainChoice()
        {
            Console.Write(
                "1) Read\n"+
                "2) Load\n"+
                "3) Test\n"
                );
            int a = int.Parse(Console.ReadLine());
            switch (a) {
                case 1:
                    Read("BackR0.bin", 100, 10, true);
                    Read("BackR1.bin", 100, 10, true);
                    Read("BackR2.bin", 100, 10, true);
                    Read("BackL0.bin", 100, 10, true);
                    Read("BackL1.bin", 100, 10, true);
                    Read("BackL2.bin", 100, 10, true);
                    break;
                case 2:
                    Load();
                    break;
                case 3:
                    Test();
                    break;
                default:
                    Console.WriteLine("NOT DEFINED");
                    break;
            }
            
        }

        static void Read(string filename, int nRead, int nFrame, bool auto)
        {
            Console.WriteLine("READ");
            Gesture1 g = new Gesture1(nRead, nFrame, auto);
            string path = MAIN_PATH + filename;
            g.Read(path);

        }

        static void Load()
        {
            Console.WriteLine("LOAD");
            List<List<Frame>> endList = new List<List<Frame>>();
            List<List<Frame>> lf0 = Utils.LoadListListFrame("Frames/BackR0.bin");
            List<List<Frame>> lf1 = Utils.LoadListListFrame("Frames/BackR1.bin");
            List<List<Frame>> lf2 = Utils.LoadListListFrame("Frames/BackR2.bin");
            List<List<Frame>> lf3 = Utils.LoadListListFrame("Frames/BackL0.bin");
            List<List<Frame>> lf4 = Utils.LoadListListFrame("Frames/BackL1.bin");
            List<List<Frame>> lf5 = Utils.LoadListListFrame("Frames/BackL2.bin");
            endList = Utils.JoinListListFrame(lf0, lf1);
            endList = Utils.JoinListListFrame(endList, lf2);
            endList = Utils.JoinListListFrame(endList, lf3);
            endList = Utils.JoinListListFrame(endList, lf4);
            endList = Utils.JoinListListFrame(endList, lf5);

            Utils.SaveListListFrame(endList, "Frames/Back.bin");
        }

        static void Test()
        {
            Gestures.Test123();
        }

    }
}
