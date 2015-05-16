using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HMM_Test_Library
{
    [Serializable]
    public class Model
    {
        HiddenMarkovModel<MultivariateNormalDistribution> hmm;
        public SequenceList inputSeq;

        public Model()
        {
            inputSeq = new SequenceList();
        }

        public Model(SequenceList inp)
        {
            inputSeq = inp;
            hmm = new HiddenMarkovModel<MultivariateNormalDistribution>(new Forward(5),
               new MultivariateNormalDistribution(inputSeq.GetArray()[0][0].Length));
        }

        public void Teach()
        {
            var teacher = new BaumWelchLearning<MultivariateNormalDistribution>(hmm);
            teacher.Run(inputSeq.GetArray());
        }

        public HiddenMarkovModel<MultivariateNormalDistribution> getModel()
        {
            return hmm;
        }

        public void Save(Stream stream)
        {   //Stream writeStream = new FileStream("MyFile1.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public static Model Load(Stream stream)
        {   //Stream readStream = new FileStream("MyFile1.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            Model obj = (Model)formatter.Deserialize(stream);
            return obj;
        }

    }

}
