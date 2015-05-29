using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sequences
{
    public class Classifier
    {
        HiddenMarkovClassifier<MultivariateNormalDistribution> classifer;
        List<HiddenMarkovModel<MultivariateNormalDistribution>> models;
        List<string> names;

        public Classifier()
        {
            models = new List<HiddenMarkovModel<MultivariateNormalDistribution>>();
            names = new List<string>();
        }

        public Classifier(List<HiddenMarkovModel<MultivariateNormalDistribution>> models, List<string> names)
        {
            this.models = models;
            this.names = names;
        }

        public void AddModel(HiddenMarkovModel<MultivariateNormalDistribution> model, string name)
        {
            models.Add(model);
            names.Add(name);
        }

        public void ClearClassifier()
        {
            models.Clear();
            names.Clear();
        }

        public int ComputeToInt(double[][] sequence)
        {
            return classifer.Compute(sequence);
        }

        public string ComputeToString(double[][] sequence)
        {
            return names[ComputeToInt(sequence)];
        }

        public void StartClassifier()
        {
            classifer = new HiddenMarkovClassifier<MultivariateNormalDistribution>(models.ToArray());
        }

        public double testModel(int i, double[][] sequence)
        {
            return models[i].Evaluate(sequence);
        }
    }
}
