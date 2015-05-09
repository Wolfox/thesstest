using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sequences {

    public abstract class State
    {

        private string stateName { get; set; }
        private List<string> actions;

        public State(string name) {
            stateName = name;
            actions = new List<string>();
        }

        public void AddAction(string action) {
            actions.Add(action);
        }

        public List<string> GetActions() {
            return actions;
        }
    }

    public abstract class OldState {

        private string stateName;
        private Dictionary<string, HiddenMarkovModel<MultivariateNormalDistribution>> models;

        public OldState(string name)
        {
            stateName = name;
            models = new Dictionary<string, HiddenMarkovModel<MultivariateNormalDistribution>>();
        }

        public void AddHMMToState(string name, HiddenMarkovModel<MultivariateNormalDistribution> hmm) {
            models.Add(name, hmm);
        }

        public Classifier CreateClassifier() {
            Classifier classifier = new Classifier();
            foreach (KeyValuePair<string, HiddenMarkovModel<MultivariateNormalDistribution>> entry in models)
            {
                classifier.AddModel(entry.Value, entry.Key);
            }
            return classifier;
        }

        public bool HaveModel(string modelName) {
            return models.ContainsKey(modelName);
        }

        public HiddenMarkovModel<MultivariateNormalDistribution> GetModel(string modelName) {
            HiddenMarkovModel<MultivariateNormalDistribution> value;
            if(models.TryGetValue(modelName, out value)) {
                return value;
            }
            return null;
        }
    }
}
