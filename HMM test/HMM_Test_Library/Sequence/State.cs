using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sequences {

    public class State
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

        public List<HiddenMarkovModel<MultivariateNormalDistribution>> GetModelsWithCulture(
            Dictionary<string, HiddenMarkovModel<MultivariateNormalDistribution>> allModels,
            CulturalLayer cultureLayer, string culture = ""){
                return GetModels(cultureLayer.GetGesturesNames(actions, culture), allModels);
        }

        public static List<HiddenMarkovModel<MultivariateNormalDistribution>> GetModels(
            List<string> gestureNames,
            Dictionary<string, HiddenMarkovModel<MultivariateNormalDistribution>> allModels) {
            return gestureNames.ConvertAll(gestureName => allModels[gestureName]);
        }
    }
}
