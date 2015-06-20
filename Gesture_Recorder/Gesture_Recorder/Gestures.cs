using Leap;
using Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GestureRecorder.Tests
{
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
            while (read) ;
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
