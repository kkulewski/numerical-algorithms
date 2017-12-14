using System;
using System.Collections.Generic;
using System.IO;

namespace Mushrooms
{
    public class GameConfig
    {
        public int BoardBound;

        public int BoardSize => BoardBound * 2 + 1;

        public int Player1Position;

        public int Player2Position;

        public int DiceFacesCount;

        public Dictionary<int, double> DiceFaces;

        public GameConfig(string fileName)
        {
            try
            {
                var lines = File.ReadAllLines(fileName);

                BoardBound = int.Parse(lines[0]);
                var positions = lines[1].Split(' ');
                Player1Position = int.Parse(positions[0]);
                Player2Position = int.Parse(positions[1]);
                DiceFacesCount = int.Parse(lines[2]);
                DiceFaces = ParseDiceFaces(DiceFacesCount, lines[3], lines[4]);
            }
            catch
            {
                throw new FormatException("Invalid config file format!");
            }
        }

        private Dictionary<int, double> ParseDiceFaces(int diceFacesCount, string diceValues, string diceProbabilities)
        {
            var diceFaceValues = diceValues.Split(' ');
            var diceFaceProbabilities = diceProbabilities.Split(' ');

            var probabilitySum = 0;
            for (var i = 0; i < DiceFacesCount; i++)
            {
                probabilitySum += int.Parse(diceFaceProbabilities[i]);
            }

            var diceFaces = new Dictionary<int, double>();
            for (var i = 0; i < diceFacesCount; i++)
            {
                var value = int.Parse(diceFaceValues[i]);
                var probability = int.Parse(diceFaceProbabilities[i]);
                diceFaces.Add(value, (double) probability / probabilitySum);
            }

            return diceFaces;
        }
    }
}
