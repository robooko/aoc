using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("textfile1.txt");

            var seats = new Seats(allLines);

            var seatNumbers = seats.SeatNumbers;

            Console.WriteLine($"Max seat number is { seatNumbers.Max() }.");
            Console.WriteLine($"Min seat number is { seatNumbers.Min() }.");

            var missing = Enumerable.Range(seatNumbers.Min(), seatNumbers.Max() - seatNumbers.Min()).Except(seatNumbers);
            Console.WriteLine($"Your seat is { String.Join("\n", missing) }.");
        }
    }

    class Seats
    {
        IEnumerable<Seat> _seats;
        public Seats(string[] data)
        {
            _seats = data.Select(x => new Seat(x));
        }

        internal IEnumerable<int> SeatNumbers
        {
            get
            {
                return _seats.Select(x => x.GetSeatNumber());
            }
        }
    }

    class Seat
    {
        string _seatCode;
        PositionCalculator _row;
        PositionCalculator _column;
        public Seat(string seatCode)
        {
            _seatCode = seatCode;
            _row = new PositionCalculator(127, _seatCode.Take(_seatCode.Length - 3));
            _column = new PositionCalculator(7, _seatCode.TakeLast(3));
        }

        public int GetSeatNumber()
        {
            var row = _row.GetPosition();
            var column = _column.GetPosition();
            return row * 8 + column;
        }
    }

    class PositionCalculator
    {
        char[] _up = new char[] { 'L', 'F' };
        char[] _down = new char[] { 'R', 'B' };
        int _min = 0;
        int _max;
        char[] _codes;

        public PositionCalculator(int max, IEnumerable<char> codes)
        {
            _max = max;
            _codes = codes.ToArray();
        }

        public int GetPosition()
        {
            var count = _max + 1;
            var min = _min;
            var max = _max;
            foreach (var code in _codes)
            {
                count = count / 2;
                max = _up.Contains(code) ? max - count : max;
                min = _down.Contains(code) ? min + count : min;
            }

            return max;
        }

    }
}
