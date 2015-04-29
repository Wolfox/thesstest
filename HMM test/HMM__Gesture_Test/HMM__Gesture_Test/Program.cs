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

            /*List<List<Frame>> lf5 = Utils.LoadListListFrame("Frames/Open.bin");
            Console.WriteLine(lf5.Count);*/

            MainChoice();

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
                    Read("CloseR0.bin", 100, 10, true);
                    Read("CloseR1.bin", 100, 10, true);
                    Read("CloseR2.bin", 100, 10, true);
                    Read("CloseL0.bin", 100, 10, true);
                    Read("CloseL1.bin", 100, 10, true);
                    Read("CloseL2.bin", 100, 10, true);
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
            List<List<Frame>> lf0 = Utils.LoadListListFrame("Frames/CloseR0.bin");
            List<List<Frame>> lf1 = Utils.LoadListListFrame("Frames/CloseR1.bin");
            List<List<Frame>> lf2 = Utils.LoadListListFrame("Frames/CloseR2.bin");
            List<List<Frame>> lf3 = Utils.LoadListListFrame("Frames/CloseL0.bin");
            List<List<Frame>> lf4 = Utils.LoadListListFrame("Frames/CloseL1.bin");
            List<List<Frame>> lf5 = Utils.LoadListListFrame("Frames/CloseL2.bin");
            endList = Utils.JoinListListFrame(lf0, lf1);
            endList = Utils.JoinListListFrame(endList, lf2);
            endList = Utils.JoinListListFrame(endList, lf3);
            endList = Utils.JoinListListFrame(endList, lf4);
            endList = Utils.JoinListListFrame(endList, lf5);

            Utils.SaveListListFrame(endList, "Frames/Close.bin");
        }

        static void Test()
        {
            Gestures.Test123();
        }

    }
}
