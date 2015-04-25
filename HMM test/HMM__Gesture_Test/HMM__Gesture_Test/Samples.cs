using Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMM__Gesture_Test
{
    class Samples
    {

        static public SequenceList inputSequences = new SequenceList();
        static public List<int> outputLabels = new List<int> { };


        static public int[] getOutputs()
        {
            int[] outputs = new int[outputLabels.Count];

            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i] = outputLabels[i];
            }

            return outputs;
        }

        static public double[][][] getInputs()
        {
            double[][][] inputs = new double[inputSequences.sequences.Count][][];

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = help(i);
            }

            return inputs;
        }

        static private double[][] help(int index)
        {
            double[][] returnVal = { };

            List<double[]> helpVal = new List<double[]>();
            for (int i = 0; i < inputSequences.sequences[index].sequence.Count ; i++)
            {

                helpVal.Add(inputSequences.sequences[index].sequence[i].GetValues());
            }

            returnVal = helpVal.ToArray();
            return returnVal;
        }

    }
}
