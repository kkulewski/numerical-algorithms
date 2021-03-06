﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mushrooms.GameData
{
    public class Dice : IEnumerable<DiceFace>
    {
        public IDictionary<int, DiceFace> DiceFaces;

        private readonly Random _random = new Random();

        public DiceFace this[int index] => DiceFaces[index];

        private Dice(IDictionary<int, DiceFace> diceFaces)
        {
            if (!DiceIsValid(diceFaces))
            {
                throw new Exception("Dice total probability is not equal to 1.0");
            }

            DiceFaces = diceFaces;
        }

        public static Dice GetDice(IDictionary<int, double> diceFaces)
        {
            var faces = new Dictionary<int, DiceFace>();
            foreach (var face in diceFaces)
            {
                faces[face.Key] = new DiceFace(face.Key, face.Value);
            }

            return new Dice(faces);
        }

        public DiceFace Toss()
        {
            var number = _random.NextDouble();
            var sum = 0.0;
            DiceFace toss = null;

            var diceEnumerator = GetEnumerator();
            diceEnumerator.MoveNext();
            while (number > sum)
            {
                toss = diceEnumerator.Current ?? throw new NullReferenceException("Dice face not found in a dice.");
                sum += toss.Probability;
                diceEnumerator.MoveNext();
            }

            diceEnumerator.Dispose();
            return toss;
        }

        public IEnumerator<DiceFace> GetEnumerator()
        {
            return DiceFaces.Select(f => f.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DiceFaces.GetEnumerator();
        }

        private static bool DiceIsValid(IDictionary<int, DiceFace> diceFaces)
        {
            const double tolerance = 0.00000000000001;

            var sum = diceFaces.Sum(f => f.Value.Probability);
            return Math.Abs(1.0 - sum) < tolerance;
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
