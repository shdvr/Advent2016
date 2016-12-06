using System;
using System.Collections.Generic;

namespace Day6
{
    class SignalCorrection
    {
        static void Main(string[] args)
        {
            String line;
            int msgLength; //peeking message length from the first line of the input file instead of hard coding it:
            using (System.IO.StreamReader sr = new System.IO.StreamReader(args[0]))
            {
                msgLength = sr.ReadLine().Length;
            }
            Dictionary<char, int>[] c = new Dictionary<char, int>[msgLength];
            char[] msg = new char[msgLength];
            char[] msg2 = new char[msgLength];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new Dictionary<char, int>();
            }

            using (System.IO.StreamReader sr = new System.IO.StreamReader(args[0]))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (c[i].ContainsKey(line[i]))
                        {
                            c[i][line[i]] += 1;
                        }
                        else
                        {
                            c[i].Add(line[i], 1);
                        }
                    }
                }
            }

            for (int i = 0; i < c.Length; i++)
            {
                msg[i] = GetMostCommonChar(c[i]);
                msg2[i] = GetLeastCommonChar(c[i]);
            }
            Console.WriteLine("Original Message: " + new String(msg));
            Console.WriteLine("Modified Message: " + new String(msg2));
            Console.ReadLine();
        }
        private static char GetMostCommonChar(Dictionary<char, int> d)
        {
            char result = ' ';
            int currMax = 0;
            foreach (char k in d.Keys)
            {
                if (d[k] > currMax)
                {
                    currMax = d[k];
                    result = k;
                }
            }
            return result;
        }
        private static char GetLeastCommonChar(Dictionary<char, int> d)
        {
            char result = ' ';
            int currMax = int.MaxValue;
            foreach (char k in d.Keys)
            {
                if (d[k] < currMax)
                {
                    currMax = d[k];
                    result = k;
                }
            }
            return result;
        }
    }

}
