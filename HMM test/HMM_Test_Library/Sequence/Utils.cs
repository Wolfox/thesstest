﻿using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
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
    public static class Utils
    {
        public static Sign FrameToSign(Frame frame)
        {
            HandList hands = frame.Hands;
            Hand hand = hands.Rightmost;

            if (!hand.IsValid) { return null; }

            FingerList fingers = hand.Fingers;
            Finger finger;
            List<double> inputs = new List<double>();
            for (int i = 0; i < fingers.Count; i++)
            {
                finger = fingers[i];
                double[] asdfg = Array.ConvertAll(finger.Direction.ToFloatArray(), x => System.Convert.ToDouble(x));
                inputs.AddRange(asdfg);
            }
            return new Sign(inputs.ToArray());
        }

        public static Sequence FramesToSequence(List<Frame> frames)
        {
            List<Sign> signs = frames.ConvertAll(i => FrameToSign(i));
            return new Sequence(signs);
        }

        public static SequenceList FramesToSequenceList(List<List<Frame>> frames)
        {
            SequenceList seqList = new SequenceList();
            for (int i = 0; i < frames.Count; i++)
            {
                Sequence seq = FramesToSequence(frames[i]);
                seqList.sequences.Add(seq);
            }
            return seqList;
        }

        public static void SaveFrame(Frame f, string path)
        {
            byte[] serializedFrame = f.Serialize;
            System.IO.File.WriteAllBytes(path, serializedFrame);
        }

        public static Frame LoadFrame(string path)
        {
            byte[] frameData = System.IO.File.ReadAllBytes(path);
            Controller control = new Controller();
            Frame f = new Frame ();
            f.Deserialize (frameData);
            control.Dispose();
            return f;
        }

        public static void SaveListFrame(List<Frame> listF, string path)
        {
            List<byte[]> listB = new List<byte[]>();
            for (int i = 0; i < listF.Count; i++)
            {
                listB.Add(listF[i].Serialize);
            }

            Stream writeStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writeStream, listB);
            writeStream.Close();
        }

        public static List<Frame> LoadListFrame(string path)
        {
            List<Frame> listF = new List<Frame>();
            Stream readStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            List<byte[]> listB = (List<byte[]>)formatter.Deserialize(readStream);
            readStream.Close();

            Controller control = new Controller();
            for (int j = 0; j < listB.Count; j++)
            {
                Frame f = new Frame();
                f.Deserialize(listB[j]);
                listF.Add(f);
            }
            control.Dispose();

            return listF;
        }

        public static void SaveListListFrame(List<List<Frame>> listlistF, string path)
        {
            List<List<byte[]>> listlistB = new List<List<byte[]>>();
            for (int i = 0; i < listlistF.Count; i++)
            {
                List<byte[]> listB = new List<byte[]>();
                for (int j = 0; j < listlistF[i].Count; j++)
                {
                    listB.Add(listlistF[i][j].Serialize);
                }
                listlistB.Add(listB);
            }
            Stream writeStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(writeStream, listlistB);
            writeStream.Close();
        }

        public static List<List<Frame>> LoadListListFrame(string path)
        {
            List<List<Frame>> listListF = new List<List<Frame>>();
            Stream readStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            List<List<byte[]>> listListB = (List<List<byte[]>>)formatter.Deserialize(readStream);
            readStream.Close();

            Controller control = new Controller();
            for (int i = 0; i < listListB.Count; i++)
            {
                List<Frame> listF = new List<Frame>();
                for (int j = 0; j < listListB[i].Count; j++)
                {
                    Frame f = new Frame();
                    f.Deserialize(listListB[i][j]);
                    listF.Add(f);
                }
                listListF.Add(listF);
            }
            control.Dispose();

            return listListF;
        }

        public static List<List<Frame>> JoinListListFrame(List<List<Frame>> listF1, List<List<Frame>> listF2)
        {
            List<List<Frame>> returnVal = new List<List<Frame>>();
            returnVal.AddRange(listF1);
            returnVal.AddRange(listF2);
            return returnVal;
        }

        public static void SaveHMM(HiddenMarkovModel<MultivariateNormalDistribution> model, string path)
        {
            model.Save(path);
        }

        public static HiddenMarkovModel<MultivariateNormalDistribution> LoadHMM(string path)
        {
            return HiddenMarkovModel<MultivariateNormalDistribution>.Load(path);
        }

        public static void SaveSequenceList(SequenceList seqList, string path)
        {
            Stream writeStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            seqList.Save(writeStream);
            writeStream.Close();
        }

        public static SequenceList LoadSequenceList(string path)
        {
            Stream readStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seqList = SequenceList.Load(readStream);
            readStream.Close();
            return seqList;
        }
    }
}
