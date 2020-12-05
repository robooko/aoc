using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("textfile1.txt");

            var slop = new Slop(allLines);
            Console.WriteLine($"Trees encountered on slop 1 is {slop.GetTrees(1, 3)}.");

            var totalTrees = slop.GetTrees(1, 1);
            totalTrees = totalTrees * slop.GetTrees(1, 3);
            totalTrees = totalTrees * slop.GetTrees(1, 5);
            totalTrees = totalTrees * slop.GetTrees(1, 7);
            totalTrees = totalTrees * slop.GetTrees(2, 1);
            Console.WriteLine($"Trees encountered on all slops is {totalTrees}.");
        }
    }

    class Slop
    {
        int _currentRow;
        int _currentColumn;
        string[] _data;
        int _maxColumn;
        public Slop(string[] data)
        {
            _data = data;
            _maxColumn = data[0].Length;
        }

        internal int GetTrees(int row, int column)
        {
            _currentRow = row;
            _currentColumn = column;
            int trees = 0;

            while (_currentRow < _data.Length)
            {
                var position = _data[_currentRow][_currentColumn];
                trees += position == '#' ? 1 : 0;
                _currentRow += row;
                _currentColumn += column;
                if (_currentColumn >= _maxColumn)
                {
                    _currentColumn = _currentColumn - _maxColumn;
                }
            }

            return trees;
        }
    }
}
