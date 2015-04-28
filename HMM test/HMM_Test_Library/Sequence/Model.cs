using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
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
    public class Model
    {
        HiddenMarkovModel<MultivariateNormalDistribution> hmm;
        public SequenceList inputSeq;

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


    public class Classifier
    {
        HiddenMarkovClassifier<MultivariateNormalDistribution> classifer;
        List<HiddenMarkovModel<MultivariateNormalDistribution>> models;

        public Classifier() 
        {
            models = new List<HiddenMarkovModel<MultivariateNormalDistribution>>();
            classifer = new HiddenMarkovClassifier<MultivariateNormalDistribution>(models.ToArray());
        }

        public Classifier(List<HiddenMarkovModel<MultivariateNormalDistribution>> mods)
        {
            models = mods;
            classifer = new HiddenMarkovClassifier<MultivariateNormalDistribution>(models.ToArray());
        }

        public void AddModel(HiddenMarkovModel<MultivariateNormalDistribution> m) {
            models.Add(m);
            RecreateClassifier();
        }

        public void ClearClassifier()
        {
            models.Clear();
            RecreateClassifier();
        }

        public int Run(double[][] sequence)
        {
            return classifer.Compute(sequence);
        }

        private void RecreateClassifier()
        {
            classifer = new HiddenMarkovClassifier<MultivariateNormalDistribution>(models.ToArray());
        }
    }
}
