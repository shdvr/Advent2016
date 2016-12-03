/* The keypad button locations are represented in this solution with (x,y) coordinates with center button set at (0,0) */

/* TODO: simplify the move validity checks using distance from center of the keypad */

using System;
using System.Collections.Generic;

namespace Day2
{
    class Keypad
    {
        static void Main(string[] args)
        {
            String input;
            using (System.IO.StreamReader sr = new System.IO.StreamReader("inputDay2.txt"))
            {
                input = sr.ReadToEnd();
            }
            int x = 0;
            int y = 0;
            int xFancy = -2;
            int yFancy = 0;
            List<char> fancy = new List<char>();
            List<char> regular = new List<char>();
            foreach (char c in input)
            {
                //regular keypad
                switch (c)
                {
                    case 'U':
                        y++;
                        if (y > 1) y = 1;
                        break;
                    case 'D':
                        y--;
                        if (y < -1) y = -1;
                        break;
                    case 'R':
                        x++;
                        if (x > 1) x = 1;
                        break;
                    case 'L':
                        x--;
                        if (x < -1) x = -1;
                        break;
                    case '\r':
                        regular.Add(ButtonName(x, y,false));
                        break;
                }
                //fancy keypad
                switch (c)
                {
                    case 'U':
                        if (Math.Abs( xFancy) < 2)
                        {
                            if ((xFancy == 0 && yFancy < 2) || yFancy < 1) yFancy++;
                        }
                        break;
                    case 'D':
                        if (Math.Abs(xFancy) < 2)
                        {
                            if ((xFancy == 0 && yFancy > -2) || yFancy > -1) yFancy--;
                        }
                        break;
                    case 'R':
                        if (Math.Abs(yFancy) < 2)
                        {
                            if ((yFancy == 0 && xFancy < 2) || xFancy < 1) xFancy++;
                        }
                        break;
                    case 'L':
                        if (Math.Abs(yFancy) < 2)
                        {
                            if ((yFancy == 0 && xFancy > -2) || xFancy > -1) xFancy--;
                        }
                        break;
                    case '\r':
                        fancy.Add(ButtonName(xFancy, yFancy,true));
                        break;
                }
            }
            /* get the last button for each keypad */
            regular.Add(ButtonName(x, y, false));
            fancy.Add(ButtonName(xFancy, yFancy,true));

            Console.WriteLine("Regular keypad code: " + new String(regular.ToArray()));
;           Console.WriteLine("Fancy keypad code: " + new String(fancy.ToArray()));
            Console.ReadLine();
        }

        private static char ButtonName(int x, int y, bool isFancyKeypad)
        {
            if (isFancyKeypad)
            {
                if (x == 0 && y == 2) return '1';
                if (x == 0 && y == -2) return 'D';
                if (x == 0 && y == -1) return 'B';
                if (x == 0 && y == 1) return '3';
                if (x == 0 && y == 0) return '7';
                if (x == -2 && y == 0) return '5';
                if (x == 2 && y == 0) return '9';
                if (x == 1 && y == -1) return 'C';
                if (x == 1 && y == 0) return '8';
                if (x == 1 && y == 1) return '4';
                if (x == -1 && y == -1) return 'A';
                if (x == -1 && y == 0) return '6';
                if (x == -1 && y == 1) return '2';
                return 'Z';
            }
            else
            {
                switch (x)
                {
                    case -1:
                        switch (y)
                        {
                            case -1: return '7';
                            case 0: return '4';
                            default: return '1';
                        }
                    case 0:
                        switch (y)
                        {
                            case -1: return '8';
                            case 0: return '5';
                            default: return '2';
                        }
                    default:
                        switch (y)
                        {
                            case -1: return '9';
                            case 0: return '6';
                            default: return '3';
                        }
                }
            }
        }
    }
}
