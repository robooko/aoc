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
        public Seat(string seatCode)
        {
            _seatCode = seatCode;
        }

        public int GetSeatNumber()
        {
            var sRow = string.Join("", _seatCode.Take(_seatCode.Length - 3))
                .Replace("B", "1")
                .Replace("F", "0");
            var row = Convert.ToInt32(sRow, 2);
            var sCol = string.Join("", _seatCode.TakeLast(3))
                .Replace("R", "1")
                .Replace("L", "0");
            var column = Convert.ToInt32(sCol, 2);

            return row * 8 + column;
        }
    }
}
