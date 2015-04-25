using Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HMM_Test_Library
{
    public class Sample
    {
        private Sequence seq = new Sequence();
        private int sequenceSize = 10;

        public void setSequenceSize(int size)
        {
            sequenceSize = size;
        }

        public int getSequenceSize()
        {
            return sequenceSize;
        }

        public Sequence getSequece()
        {
            return seq.Clone();
        }

        public void AddSign(Sign sign)
        {
            if (sign != null)
            {
                seq.sequence.Add(sign);
                if (seq.sequence.Count > sequenceSize)
                {
                    seq.sequence.RemoveAt(0);
                }
            }
        }

        public void ClearSequence()
        {
            seq.sequence.Clear();
        }

    }
}
