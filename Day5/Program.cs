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

            var seatNumbers = seats.SeatNumbers.ToList();
            seatNumbers.Sort();

            Console.WriteLine($"Max seat number is { seatNumbers.Max() }.");
            Console.WriteLine($"Min seat number is { seatNumbers.Min() }.");
            var missing = Enumerable.Range(seatNumbers.Min(), seatNumbers.Max() - seatNumbers.Min()).Except(seatNumbers);
            Console.WriteLine($"Your seat is { String.Join("\n", missing) }.");
            // Console.WriteLine($"{ String.Join("\n", seatNumbers) }.");
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
            var row = getRow();
            var column = getColumn();
            return row * 8 + column;
        }

        int getRow()
        {
            var rowCodes = _seatCode.Take(_seatCode.Length - 3);
            var rowMax = 127;
            var rowMin = 0;
            var options = 128;
            foreach(var rowCode in rowCodes)
            {
                options = options / 2;
                rowMax = rowCode == 'F' ? rowMax - options : rowMax;
                rowMin = rowCode == 'B' ? rowMin + options : rowMin;
            }

            return (rowMax + rowMin) / 2;
        }

        int getColumn()
        {
            var columnCodes = _seatCode.TakeLast(3);
            var columnMax = 7;
            var columnMin = 0;
            var options = 8;
            foreach (var columnCode in columnCodes)
            {
                options = options / 2;
                columnMax = columnCode == 'L' ? columnMax - options : columnMax;
                columnMin = columnCode == 'R' ? columnMin + options : columnMin;
            }

            return columnMax;
        }

    }
}
