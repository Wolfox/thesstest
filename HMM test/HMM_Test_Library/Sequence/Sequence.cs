﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Sequences
{
    [Serializable]
    public class Sequence
    {

        public List<Sign> sequence;

        public Sequence()
        {
            sequence = new List<Sign>();
        }

        public Sequence(List<Sign> seq)
        {
            sequence = seq;
        }

        public Sequence Clone()
        {
            return new Sequence(sequence.ConvertAll<Sign>(i => i));
        }

        public int GetDimensions()
        {
            if (sequence.Count < 1)
            {
                return 0;
            }
            return sequence[0].GetDimensions();
        }

        public bool CheckDimensions()
        {
            int dim = GetDimensions();
            for (int i = 1; i < sequence.Count; i++)
            {
                if (sequence[i].GetDimensions() != dim) { return false; }
            }
                return true;
        }

        public double[][] GetArray()
        {
            return sequence.Select<Sign, double[]>(i => i.GetValues()).ToArray();
        }

    }

    [Serializable]
    public class SequenceList
    {
        public List<Sequence> sequences;

        public SequenceList()
        {
            sequences = new List<Sequence>();
        }

        public double[][][] GetArray()
        {
            return sequences.Select<Sequence, double[][]>(i => i.GetArray()).ToArray();
        }

        public void Save(Stream stream)
        {   //Stream writeStream = new FileStream("MyFile1.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public static SequenceList Load(Stream stream)
        {   //Stream readStream = new FileStream("MyFile1.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            SequenceList obj = (SequenceList)formatter.Deserialize(stream);
            return obj;
        }
    }
}
