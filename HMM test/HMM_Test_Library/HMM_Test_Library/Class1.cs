//using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HMM_Test_Library
{
    public class Class1
    {

        /*public static string Test()
        {
            return Class5.LeTest();
        }*/

        public static string Test2()
        {
            return "Hello world";
        }

        public static void SaveForTest()
        {
            Stream writeStream = new FileStream("MyFile1.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writeStream, "Hello");
        }

    }
}
