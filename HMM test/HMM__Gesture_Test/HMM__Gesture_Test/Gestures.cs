using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;
using System.IO;
using Sequences;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HMM__Gesture_Test
{

    public class Gesture1
    {
        public enum GestureState { Starting, Reading, Saving };

        int numOfReads;
        int numOfFramesPerRead;
        bool isAuto;
        List<List<byte[]>> sequenceToRead;
        public GestureState state;

        private int actualNumOfReads;

        public Gesture1(int numReads, int numFramesRead, bool auto)
        {
            numOfReads = numReads;
            numOfFramesPerRead = numFramesRead;
            isAuto = auto;
            sequenceToRead = new List<List<byte[]>>();
            state = GestureState.Starting;
            actualNumOfReads = 0;

        }

        public void Read(string path)
        {

            ReadListener listener = new ReadListener();
            listener.Initialization(numOfFramesPerRead, this);
            Controller controller = new Controller();
            controller.AddListener(listener);

            actualNumOfReads = 0;
            state = GestureState.Reading;
            while (state == GestureState.Reading) { }

            Save(path);

        }

        public void Store(List<byte[]> handSeq)
        {
            sequenceToRead.Add(handSeq);
            actualNumOfReads++;
            if (actualNumOfReads >= numOfReads)
            {
                state = GestureState.Saving;
            }
        }

        public void Save(string path)
        {
            Stream writeStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writeStream, sequenceToRead);
            writeStream.Close();
        }
    }

    class Gestures
    {
        static int typeHelp = 0;
        static int typeMAGICNUMBER = 500;
        static public bool read = false;
        static public bool real = false;

        static public SequenceList samples;

        static public void Test()
        {
            SampleListener listener = new SampleListener();
            Controller controller = new Controller();
            controller.AddListener(listener);


            samples = new SequenceList();
            Console.WriteLine("Press Enter to start read Front");
            Console.ReadLine();
            typeHelp = 0;
            read = true;
            Console.WriteLine("Reading");
            while (read);
            Console.WriteLine("Done");
            Stream writeStream = new FileStream("Front.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            samples.Save(writeStream);
            writeStream.Close();


            samples = new SequenceList();
            Console.WriteLine("Press Enter to start read Right");
            Console.ReadLine();
            typeHelp = 0;
            read = true;
            Console.WriteLine("Reading");
            while (read) ;
            Console.WriteLine("Done");
            writeStream = new FileStream("Right.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            samples.Save(writeStream);
            writeStream.Close();


            samples = new SequenceList();
            Console.WriteLine("Press Enter to start read Left");
            Console.ReadLine();
            typeHelp = 0;
            read = true;
            Console.WriteLine("Reading");
            while (read) ;
            Console.WriteLine("Done");
            writeStream = new FileStream("Left.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            samples.Save(writeStream);
            writeStream.Close();


            samples = new SequenceList();
            Console.WriteLine("Press Enter to start read Back");
            Console.ReadLine();
            typeHelp = 0;
            read = true;
            Console.WriteLine("Reading");
            while (read) ;
            Console.WriteLine("Done");
            writeStream = new FileStream("Back.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            samples.Save(writeStream);
            writeStream.Close();

            //Console.WriteLine("samples:" + Samples.inputSequences[20].Count.ToString());

            /*Console.WriteLine("Start learning");
            HMM.Test();
            Console.WriteLine("End learning");
            real = true;

            double[][][] inputSeq = Samples.getInputs();

            Console.WriteLine("Press Enter to end read gestures...");
            Console.ReadLine();*/

            controller.RemoveListener(listener);
            controller.Dispose();
        }

        static public void Test123()
        {
            MyListener listener = new MyListener();
            Controller controller = new Controller();
            controller.AddListener(listener);

            Console.WriteLine("Start learning");
            HMM.Test123();
            Console.WriteLine("End learning");
            real = true;

            while (real)
            {
                //HMM.ElRun(listener.getSeq().GetArray());
            }

            Console.WriteLine("Press Enter to end read gestures...");
            Console.ReadLine();
        }

        static public void TestReal()
        {

            MyListener listener = new MyListener();
            Controller controller = new Controller();
            controller.AddListener(listener);

            Console.WriteLine("Start learning");
            HMM.Test();
            Console.WriteLine("End learning");
            real = true;

            while (real)
            {
                //HMM.ElRun(listener.getSeq().GetArray());
            }

            Console.WriteLine("Press Enter to end read gestures...");
            Console.ReadLine();

        }

        static public void Stuff(Sequence s)
        {
            samples.sequences.Add(s);
            typeHelp++;

            //Console.WriteLine(typeHelp);
            if (typeHelp > typeMAGICNUMBER)
            {
                read = false;
            }
        }

        static public void RealStuff(Sequence signs)
        {
            double[][] inputs = signs.GetArray();
            HMM.ElRun(inputs);
        }
        
    }
}
