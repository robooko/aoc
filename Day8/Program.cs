using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("textfile1.txt");
            var instruction = new Instructions(allLines);
            Console.WriteLine($"Q1 answer is {instruction.Run()}");
        }
    }

    class Instructions
    {
        Collection<Instruction> items = new Collection<Instruction>();

        public Instructions(string[] data)
        {
            foreach(var i in data)
            {
                items.Add(new Instruction(i));
            }
        }

        public long Run()
        {
            var lineRun = new int[] { };
            var acc = 0;
            var line = 0;

            while (line < items.Count())
            {
                if (lineRun.Contains(line))
                {
                    Console.WriteLine($"line {line} is a loop.");
                    return acc;
                }

                lineRun = lineRun.Append(line).ToArray();
                Console.WriteLine($"line is {line + 1}. acc is {acc}. action is {items[line].action}. value is {items[line].value}.");

                switch (items[line].action)
                {
                    case "acc":
                        acc = items[line].value.StartsWith("+") ? acc + Convert.ToInt32(items[line].value.TrimStart('+')) : acc - Convert.ToInt32(items[line].value.TrimStart('-'));
                        line = line + 1;
                        break;
                    case "nop":
                        line = line + 1;
                        break;
                    case "jmp":
                        line = items[line].value.StartsWith("+") ? line + Convert.ToInt32(items[line].value.TrimStart('+')) : line - Convert.ToInt32(items[line].value.TrimStart('-') );
                        break;
                }
            }

            return acc;
        }
    }

    internal class Instruction
    {
        public string action { get; init; }
        public string value { get; init; }
        public Instruction(string i)
        {
            action = i.Split(' ')[0];
            value = i.Split(' ')[1];
        }
    }
}
