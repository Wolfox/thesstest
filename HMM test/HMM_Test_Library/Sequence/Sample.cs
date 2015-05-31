/*using Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sequences
{
    public class Sample
    {
        private Sequence sample;
        private int sequenceSize;

        public Sample(int size = 10)
        {
            sample = new Sequence();
            sequenceSize = size;
        }

        public void setSequenceSize(int size)
        {
            sequenceSize = size;
        }

        public int getSequenceSize()
        {
            return sequenceSize;
        }

        public Sequence getSequence()
        {
            return sample.Clone();
        }

        public void AddSign(Sign sign)
        {
            if (sign != null)
            {
                sample.sequence.Add(sign);
                if (sample.sequence.Count > sequenceSize)
                {
                    sample.sequence.RemoveAt(0);
                }
            }
        }

        public void ClearSequence()
        {
            sample.sequence.Clear();
        }

    }
}*/
