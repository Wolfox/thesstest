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
        static string MAIN_PATH = "";

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


            MainChoice();

            //HelpASDFG("Front.bin", "FrontModel.bin");

            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
        }

        static void HelpASDFG(String readPath, string writePath)
        {
            /*Console.WriteLine("Loading");
            Stream readStream = new FileStream(readPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seq = SequenceList.Load(readStream);
            readStream.Close();

            Model model = new Model(seq);
            Console.WriteLine("Teaching");
            model.Teach();

            Console.WriteLine("Saving");
            Stream writeStream = new FileStream(writePath, FileMode.Create, FileAccess.Write, FileShare.None);
            model.Save(writeStream);
            writeStream.Close();*/
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
                    Read();
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

        static void Read()
        {
            Console.WriteLine("READ");
            Gesture1 g = new Gesture1(10, 5, true);
            string path = MAIN_PATH + "Test.bin";
            g.Read(path);

            Stream readStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            List<List<byte[]>> hsl = (List<List<byte[]>>)formatter.Deserialize(readStream);
            readStream.Close();

            Frame f = new Frame();
            f.Deserialize(hsl[0][0]);
            Console.WriteLine(f.Hands[0].Fingers[0].Direction.ToString());
        }

        static void Load()
        {
            Console.WriteLine("LOAD");
        }

        static void Test()
        {
            Gestures.Test123();
        }

    }
}
