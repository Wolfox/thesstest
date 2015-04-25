using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Markov;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Sequences;
using Accord.Statistics.Models.Markov.Topology;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Distributions.Fitting;

namespace HMM_Test_Library
{
    public class HMM
    {

        private HiddenMarkovClassifier<MultivariateNormalDistribution> hmm;
        private double[][][] inputSequenc;
        private int[] outputLabels;

        public void Load()
        {
            Stream readStream;
            List<double[][]> inputHelp = new List<double[][]>();
            List<int> outputHelp = new List<int>();

            readStream = new FileStream("Front.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seqFront = SequenceList.Load(readStream);
            readStream.Close();

            readStream = new FileStream("Right.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seqRight = SequenceList.Load(readStream);
            readStream.Close();

            readStream = new FileStream("Left.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seqLeft = SequenceList.Load(readStream);
            readStream.Close();

            readStream = new FileStream("Back.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seqBack = SequenceList.Load(readStream);
            readStream.Close();

            int[] otputs = new int[seqFront.sequences.Count];
            for (int i = 0; i < otputs.Length; i++)
            {
                otputs[i] = 0;
            }
            outputHelp.AddRange(otputs);

            otputs = new int[seqRight.sequences.Count];
            for (int i = 0; i < otputs.Length; i++)
            {
                otputs[i] = 1;
            }
            outputHelp.AddRange(otputs);

            otputs = new int[seqLeft.sequences.Count];
            for (int i = 0; i < otputs.Length; i++)
            {
                otputs[i] = 2;
            }
            outputHelp.AddRange(otputs);

            otputs = new int[seqBack.sequences.Count];
            for (int i = 0; i < otputs.Length; i++)
            {
                otputs[i] = 3;
            }
            outputHelp.AddRange(otputs);

            inputHelp.AddRange(seqFront.GetArray());
            inputHelp.AddRange(seqLeft.GetArray());
            inputHelp.AddRange(seqRight.GetArray());
            inputHelp.AddRange(seqBack.GetArray());

            inputSequenc = inputHelp.ToArray();
            outputLabels = outputHelp.ToArray();
        }

        public void Finit()
        {
            string[] classes = { "front", "right", "left", "back" };
            hmm = new HiddenMarkovClassifier<MultivariateNormalDistribution>(classes.Length,
                new Forward(5), new MultivariateNormalDistribution(inputSequenc[0][0].Length), classes);

            // And create a algorithms to teach each of the inner models
            var teacher = new HiddenMarkovClassifierLearning<MultivariateNormalDistribution>(hmm,

                // We can specify individual training options for each inner model:
                modelIndex => new BaumWelchLearning<MultivariateNormalDistribution>(hmm.Models[modelIndex])
                {
                    Tolerance = 0.001, // iterate until log-likelihood changes less than 0.001
                    Iterations = 0,     // don't place an upper limit on the number of iterations

                    FittingOptions = new NormalOptions()
                    {
                        Regularization = 1e-5
                    }
                });

            Console.WriteLine("WHAT");
            // Then let's call its Run method to start learning
            double error = teacher.Run(inputSequenc, outputLabels);
            Console.WriteLine("WHAT");
        }

        public int ElRun(double[][] toRun)
        {
            return hmm.Compute(toRun);
        }

    }
}
