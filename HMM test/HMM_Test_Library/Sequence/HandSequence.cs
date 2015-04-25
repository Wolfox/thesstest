using Leap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Sequences
{
    [Serializable]
    public class HandSequence
    {
        public List<Hand> handSequence;

        public HandSequence()
        {
            handSequence = new List<Hand>();
        }
    }

    [Serializable]
    public class HandSequenceList
    {
        public List<HandSequence> sequences;

        public HandSequenceList()
        {
            sequences = new List<HandSequence>();
        }

        public void Save(Stream stream)
        {   //Stream writeStream = new FileStream("MyFile1.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public void Save(string path)
        {
            Stream writeStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writeStream, this);
            writeStream.Close();
        }

        public static HandSequenceList Load(Stream stream)
        {   //Stream readStream = new FileStream("MyFile1.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            HandSequenceList obj = (HandSequenceList)formatter.Deserialize(stream);
            return obj;
        }

        public static HandSequenceList Load(string path)
        {
            Stream readStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            HandSequenceList obj = (HandSequenceList)formatter.Deserialize(readStream);
            readStream.Close();
            return obj;
        }
    }
}
