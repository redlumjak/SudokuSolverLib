using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// This keeps track of what is still a possible value for the current cell. It will have at 
    /// most 9 values, which are stored as bits set in an int. e.g. 2 is 0x02, 3 is 0x04, 1 and 3 is 0x05, etc. 
    /// </summary>
    public class PencilMarks
    {
        [JsonInclude]
        internal int bits;
        static private int[] valueToBits = new int[10] {0, 1, 2, 4, 8, 16, 32, 64, 128, 256};
        static private readonly Dictionary<int, bool> singleValues = new Dictionary<int, bool>() {
            {1, true },
            {2, true },
            {4, true },
            {8, true },
            {16, true },
            {32, true },
            {64, true },
            {128, true },
            {256, true },
        };
        static public readonly Dictionary<int, int> bitsToValue = new Dictionary<int, int>()
        {
            {1, 1 },
            {2, 2 },
            {4, 3 },
            {8, 4 },
            {16, 5 },
            {32, 6 },
            {64, 7 },
            {128, 8 },
            {256, 9 },
        };

        public PencilMarks()
        {
            this.bits = 0;
        }
        public PencilMarks(PencilMarks rhs)
        {
            this.bits = rhs.bits;
        }
        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="bits"></param>
        private PencilMarks(int bits)
        { this.bits = bits; }


        public static PencilMarks Combine(List<PencilMarks> marks)
        {
            var result = new PencilMarks();
            foreach (var mark in marks)
            {
                result.bits |= mark.bits;
            } 
            return result;
        }

        public void Add(PencilMarks mark)
        {
            bits |= mark.bits;
        }

        public void Add(int value)
        {
            this.bits |= valueToBits[value];
        }

        public void Remove(List<PencilMarks> marks)
        {
            foreach (var mark in marks)
            {
                this.Remove(mark);
            }
        }

        public void Remove(PencilMarks marks)
        {
            this.bits &= ~marks.bits;
        }
        public void RemoveBits(int bits)
        {
            this.bits &= ~bits;
        }
        public void Remove(int value)
        {
            this.bits &= ~valueToBits[value];
        }

        public bool IsEmpty()
        {
            return this.bits == 0;
        }

        public bool IsSingleValue()
        {
            return singleValues.ContainsKey(this.bits);
        }

        public int GetSingleValue()
        {
            return bitsToValue[bits];
        }

        public bool HasValue(int value) => (bits & valueToBits[value]) != 0;
        public bool HasValue(PencilMarks marks) => (bits & marks.bits) != 0;

        public void SetAll()
        {
            this.bits = 0x01ff;
        }

        public void Set(List<int> values)
        {
            this.bits = 0;
            foreach (var value in values)
            {
                Add(value);
            }
        }

        public int Count()
        {
            return CountBits(this.bits);
        }

        public static int CountBits(int bits)
        {
            // TODO there are faster algorithms, such as using an array for each nibble. 
            int result = 0;
            for (int idxBits = bits; idxBits != 0; idxBits >>= 1)
            {
                if ((idxBits & 1) == 1)
                {
                    result++;
                }
            }
            return result;
        }

        public override bool Equals(object? obj)
        {
            return obj is PencilMarks marks &&
                   bits == marks.bits;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(bits);
        }
    }
}
