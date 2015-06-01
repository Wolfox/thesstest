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

namespace Gesture_Recorder
{
    class Program
    {
        static string FRAMES_PATH = "Frames/";
        static string SAMPLE_PATH = "Frames/Samples/";
        static string EXTENSION = ".frs";

        static void Main(string[] args)
        {
            MainChoice();

            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
        }

        static void MainChoice()
        {
            Console.Write(
                "1) Read\n" +
                "2) Aggregate\n" +
                "3) Test\n"
                );
            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    MainRead();
                    break;
                case 2:
                    MainLoad();
                    break;
                case 3:
                    testanother();
                    //aggregateAll();
                    //testAgain();
                    //TestNumOfFrames();
                    break;
                default:
                    Console.WriteLine("NOT DEFINED");
                    break;
            }

        }

        static void MainRead()
        {
            Console.WriteLine("Read!");
            string gestureName = ReadString("Gesture Name: ");
            bool bothHands = ReadYN("Read both hands? (y/n) ");
            int numOfSepFiles = ReadInt("Number of separated files (per hand): ");
            bool autoRead = ReadYN("AutoRead? (y/n) ");
            int numOfRead = ReadInt("Number of reads per file: ");
            int numOfFrames = ReadInt("Number of frames per read: ");

            if (bothHands) {
                ReadN(gestureName + "R", numOfRead, numOfFrames, autoRead, numOfSepFiles);
                ReadN(gestureName + "L", numOfRead, numOfFrames, autoRead, numOfSepFiles);
            }
            else {
                ReadN(gestureName, numOfRead, numOfFrames, autoRead, numOfSepFiles);
            }

            //AggregateFrames(gestureName, bothHands, numOfSepFiles);
        }

        static bool ReadYN(string text) {
            while (true) {
                Console.Write(text);
                string read = Console.ReadLine();
                if (read == "y") {
                    return true;
                }

                if (read == "n") {
                    return false;
                }
            }
        }

        static string ReadString(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }

        static int ReadInt(string text)
        {
            Console.Write(text);
            return Convert.ToInt32(Console.ReadLine());
        }

        static void ReadN(string filename, int nRead, int nFrame, bool auto, int N) {
            for (int i = 0; i < N; i++)
            {
                Read(filename + i, nRead, nFrame, auto);
            }
        }

        static void Read(string filename, int nRead, int nFrame, bool auto) {
            Gesture g = new Gesture(nRead, nFrame, auto);
            string path = SAMPLE_PATH + filename + EXTENSION;
            g.Read(path);

        }

        static void MainLoad()
        {
            Console.WriteLine("Aggregate!");

            string filename = ReadString("filename: ");
            bool bothHands = ReadYN("Both hands? (y/n) ");
            int numOfFiles = ReadInt("Number of Files? ");

            AggregateFrames(filename, bothHands, numOfFiles);
        }

        static void AggregateFrames(string filename, bool bothHands, int numOfFiles) {

            List<List<Frame>> endList = new List<List<Frame>>();

            if (bothHands)
            {
                endList = MixFrames(
                    AggregateFrames(filename + "R", numOfFiles),
                    AggregateFrames(filename + "L", numOfFiles));
            }
            else
            {
                endList = AggregateFrames(filename, numOfFiles);
            }

            if (endList.Count == 0)
            {
                Console.WriteLine("Files not found, or empty files");
                return;
            }

            Console.WriteLine("Saving file " + filename + EXTENSION + " with " + endList.Count + " examples.");
            Utils.SaveListListFrame(endList, FRAMES_PATH + filename + EXTENSION);
        }

        static List<List<Frame>> AggregateFrames(string filename, int numOfFiles)
        {
            List<List<Frame>> endList = new List<List<Frame>>();

            for (int i = 0; i < numOfFiles; i++)
            {
                endList.AddRange(Utils.LoadListListFrame(SAMPLE_PATH + filename + i + EXTENSION));
            }
            
            return endList;
        }

        static void Test()
        {
            Console.WriteLine("Aggregate");

            string filename = ReadString("filename: ");

            List<List<Frame>> frameList = Utils.LoadListListFrame(FRAMES_PATH + filename + EXTENSION);

            Console.WriteLine("Num of Seq: " + frameList.Count);
            Console.WriteLine("Num of Frames in first seq:" + frameList[0].Count);

        }

        static void TestNumOfFrames()
        {
            TestListener listener = new TestListener();
            listener.Init();
            Controller controller = new Controller(listener);

            Console.WriteLine("Press Enter to start counting frames!");
            Console.ReadLine();

            listener.startCounting();

            Console.WriteLine("Counting... Press Enter again to stop counting frames!");
            Console.ReadLine();


            int numOfFrames = listener.endCounting();
            Console.WriteLine("Stopped! The number of frames read was " + numOfFrames + " frame(s).");

            controller.RemoveListener(listener);
            controller.Dispose();
        }

        static void aggregateAll()
        {
            AggregateFrames("DRINK_NL", true, 30); //600
            AggregateFrames("DRINK_PT", true, 30); //600
            AggregateFrames("GRAB", true, 30); //600
            AggregateFrames("HALT_HAND", true, 1); //600
            AggregateFrames("HAND_ROTATING", true, 15); //600
            AggregateFrames("INDEX_HUSH", true, 3); //600
            AggregateFrames("INDEX_ROTATING", true, 15); //600
            AggregateFrames("MOUTH_MIMIC", true, 10); //600
            AggregateFrames("NUM1", true, 3); //600
            AggregateFrames("NUM2", true, 3); //600
            AggregateFrames("NUM3", true, 3); //600
            AggregateFrames("OPEN_FRONT", true, 3); //600
            AggregateFrames("OPEN_LEFT", true, 3); //600
            AggregateFrames("OPEN_RIGHT", true, 3); //600
            AggregateFrames("THE_RING", true, 3); //600
            AggregateFrames("THUMBS_DOWN", true, 3); //600
            AggregateFrames("THUMBS_UP", true, 3); //600
            AggregateFrames("WAVE", true, 6); //600
            AggregateFrames("WAVE_NO_THANKS", true, 6); //600
        }

        static void testanother()
        {
            List<List<Frame>> listl = Utils.LoadListListFrame(SAMPLE_PATH + "GRABR26" + EXTENSION);
            Console.WriteLine(Utils.FramesToSequenceList(listl).GetMaxSize());
        }

        static void testAgain()
        {
            List<List<Frame>> endList = new List<List<Frame>>();
            List<List<Frame>> testList = new List<List<Frame>>();

            List<List<Frame>> listr = Utils.LoadListListFrame(SAMPLE_PATH + "HALT_HANDR0" + EXTENSION);
            List<List<Frame>> listl = Utils.LoadListListFrame(SAMPLE_PATH + "HALT_HANDL0" + EXTENSION);

            int index = 0;

            for (index = 0; index < listr.Count && index < listl.Count; index++)
            {
                testList.Add(listr[index]);
                testList.Add(listl[index]);
            }

            testList.AddRange(listl.GetRange(index, listl.Count - index));
            testList.AddRange(listr.GetRange(index, listr.Count - index));

            Utils.SaveListListFrame(testList, FRAMES_PATH + "HALT_HAND_alt" + EXTENSION);

            /*for (int i = 0; i < 150; i++)
            {
                testList.Add(listr[i]);
            }
            for (int i = 0; i < 150; i++)
            {
                testList.Add(listl[i]);
            }

            /*endList.AddRange(Utils.LoadListListFrame(SAMPLE_PATH + "HALT_HANDR0" + EXTENSION));
            endList.AddRange(Utils.LoadListListFrame(SAMPLE_PATH + "HALT_HANDR0" + EXTENSION));*/
            /*endList.AddRange(testList);
            Utils.SaveListListFrame(endList, FRAMES_PATH + "HALT_HAND_half" + EXTENSION);
            endList.AddRange(testList);
            Utils.SaveListListFrame(endList, FRAMES_PATH + "HALT_HAND_half2x" + EXTENSION);*/
        }

        static List<List<Frame>> MixFrames(List<List<Frame>> list1, List<List<Frame>> list2)
        {
            List<List<Frame>> returnList = new List<List<Frame>>();
            int index = 0;

            for (index = 0; index < list1.Count && index < list2.Count; index++)
            {
                returnList.Add(list1[index]);
                returnList.Add(list2[index]);
            }

            returnList.AddRange(list1.GetRange(index, list1.Count - index));
            returnList.AddRange(list2.GetRange(index, list2.Count - index));

            return returnList;
        }

    }
}
