using System.Collections;
using System.Collections.Generic;

namespace Mushrooms
{
    public class Dice : IEnumerable
    {
        public IDictionary<int, double> Indices;

        public double this[int index]
        {
            get => Indices[index];
            set => Indices[index] = value;
        }

        public Dice(IDictionary<int, double> indices)
        {
            Indices = indices;
        }

        public IEnumerator GetEnumerator()
        {
            return Indices.GetEnumerator();
        }
    }
}
