using System.Collections;
using System.Collections.Generic;

namespace Mushrooms
{
    public class Dice : IEnumerable<DiceFace>
    {
        public IDictionary<int, double> DiceFaces;

        public double this[int index]
        {
            get => DiceFaces[index];
            set => DiceFaces[index] = value;
        }

        public Dice(IDictionary<int, double> tossResults)
        {
            DiceFaces = tossResults;
        }

        public IEnumerator<DiceFace> GetEnumerator()
        {
            foreach (var f in DiceFaces)
                yield return new DiceFace(f.Key, f.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DiceFaces.GetEnumerator();
        }
    }
    
    public class DiceFace
    {
        public int Value;

        public double Probability;

        public DiceFace(int value, double probability)
        {
            Value = value;
            Probability = probability;
        }
    }
}
