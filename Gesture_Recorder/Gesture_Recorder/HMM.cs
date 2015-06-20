using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Models.Markov.Topology;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Leap;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.Fitting;
using System.IO;
using Sequences;

namespace GestureRecorder.Tests
{
    class HMM
    {

        static HiddenMarkovClassifier<MultivariateNormalDistribution> hmm;

        static public void Test123()
        {
            Stream readStream;
            HiddenMarkovModel<MultivariateNormalDistribution> hmmR;
            HiddenMarkovModel<MultivariateNormalDistribution> hmmL;


            readStream = new FileStream("Right.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seqRight = SequenceList.Load(readStream);
            readStream.Close();

            readStream = new FileStream("Left.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SequenceList seqLeft = SequenceList.Load(readStream);
            readStream.Close();


            hmmR = new HiddenMarkovModel<MultivariateNormalDistribution>(new Forward(5),
                new MultivariateNormalDistribution(seqRight.GetArray()[0][0].Length));

            hmmL = new HiddenMarkovModel<MultivariateNormalDistribution>(new Forward(5),
                new MultivariateNormalDistribution(seqLeft.GetArray()[0][0].Length));

            var teacherR = new BaumWelchLearning<MultivariateNormalDistribution>(hmmR);
            var teacherL = new BaumWelchLearning<MultivariateNormalDistribution>(hmmL);

            Console.WriteLine("Starting Teacher Right");
            teacherR.Run(seqRight.GetArray());
            Console.WriteLine("Starting Teacher Left");
            teacherL.Run(seqLeft.GetArray());
            Console.WriteLine("End Teacher");

            HiddenMarkovModel<MultivariateNormalDistribution>[] models = { hmmR, hmmL };

            hmm = new HiddenMarkovClassifier<MultivariateNormalDistribution>(models);


        }

        static public void Test()
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

            double[][][] inputSequenc = inputHelp.ToArray();
            int[] outputLabels = outputHelp.ToArray();


            Console.WriteLine(inputSequenc.Length);
            Console.WriteLine(outputLabels.Length);

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


            // Then let's call its Run method to start learning
            double error = teacher.Run(inputSequenc, outputLabels);

            /*for (int i = 0; i < hmm.Models.Length; i++)
            {
                Console.WriteLine("i:" + i + "dim: " + hmm.Models[i].States);
            }
            Console.WriteLine("WHAT");
            Console.ReadLine();*/
        }

        static public void ElRun(double[][] toRun)
        {
            int result = hmm.Compute(toRun);
            Console.WriteLine(result);
        }
    }

    /*class HMM2
    {

        static HiddenMarkovClassifier<MultivariateNormalDistribution> hmm;

        static double[] asd1 = new double[] { 0.5, 1 };
        static double[] asd2 = new double[] { 1, 3 };
        static double[] asd3 = new double[] { 2, 3 };
        static double[] asd4 = new double[] { 2, 1 };

        static double[][][][] inputsTest2 = new double[][][][]{
                                             new double[][][]{
                                                 new double[][] { asd1 , asd1 },
                                                 new double[][] { asd2 , asd2 },
                                                 new double[][] { asd3 , asd3 }
                                             },
                                             new double[][][]{
                                                 new double[][] { asd1 , asd1 },
                                                 new double[][] { asd2 , asd2 },
                                             },
                                             new double[][][]{
                                                 new double[][] { asd1 , asd1 },
                                                 new double[][] { asd2 , asd2 },
                                                 new double[][] { asd3 , asd3 },
                                                 new double[][] { asd4 , asd4 }
                                             },
                                             new double[][][]{
                                                 new double[][] { asd1 , asd1 },
                                                 new double[][] { asd2 , asd2 },
                                                 new double[][] { asd3 , asd3 }
                                             },
                                         };


        static float[][][] inputsTest = new float[][][]{
                                             new float[][]{
                                                 new float[] { 0.5f , 3  },
                                                 new float[] { 0.5f , 2  },
                                                 new float[] { 0.5f , 1  }
                                             },
                                             new float[][]{
                                                 new float[] { 0.5f , 3  },
                                                 new float[] { 0.5f , 2  }
                                             },
                                             new float[][]{
                                                 new float[] { 0.5f , 3  },
                                                 new float[] { 0.5f , 2  },
                                                 new float[] { 0.5f , 1  }
                                             },
                                             new float[][]{
                                                 new float[] { 0.5f , 3  },
                                                 new float[] { 0.5f , 2  },
                                                 new float[] { 0.5f , 1  }
                                             }
                                         };

        static double[][][][] inputSequences;
        static int[] outputLabels = new int[] { 0, 0, 0, 0 };

        static public double[][][] Help(float[][][] inputs)
        {
            return inputs.Select<float[][], double[][]>(i => i.Select<float[], double[]>(j => j.Select<float, double>(k => k).ToArray()).ToArray()).ToArray();
        }

        static public void Test()
        {
            //inputSequences = Help(inputsTest);
            inputSequences = inputsTest2;
            string[] classes = { "front", "left", "right" };

            MultivariateNormalDistribution mnd = new MultivariateNormalDistribution(inputSequences[0][0].Length);

            hmm = new HiddenMarkovClassifier<MultivariateNormalDistribution>(classes.Length,
                new Forward(5), mnd, classes);

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

            // Then let's call its Run method to start learning
            double error = teacher.Run(inputSequences, outputLabels);
            Console.WriteLine("URREY");


            // After training has finished, we can check the 
            // output classificaton label for some sequences. 

            int y1 = classifier.Compute(new[] { 0, 1, 1, 1, 0 });    // output is y1 = 0
            int y2 = classifier.Compute(new[] { 0, 0, 1, 1, 0, 0 }); // output is y1 = 0

            int y3 = classifier.Compute(new[] { 2, 2, 2, 2, 1, 1 }); // output is y2 = 1
            int y4 = classifier.Compute(new[] { 2, 2, 1, 1 });       // output is y2 = 1

            int y5 = classifier.Compute(new[] { 0, 0, 1, 3, 3, 3 }); // output is y3 = 2
            int y6 = classifier.Compute(new[] { 2, 0, 2, 2, 3, 3 }); // output is y3 = 2
        }
    }*/
}
