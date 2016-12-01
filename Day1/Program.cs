using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {

        public enum directions : int 
        {
            north=0, east=1, south=2 , west=3
        }

        public struct Location
        {
            public int x;
            public int y;
        }
        static void Main(string[] args)
        {
            String[] steps;
            System.IO.StreamReader sr = new System.IO.StreamReader("input.txt");
            steps = sr.ReadToEnd().Split(',');
            sr.Close();
            char turn;
            int blocks;
          
            int x = 0;
            int y = 0;
            Location loc = new Location();
            loc.x = x;
            loc.y = y;
            Location temp = new Location();
            int dir = (int)directions.north;
            List<Location> locations = new List<Location>();
            foreach(String step in steps)
            {
                turn = step.Trim()[0];
                blocks = Int32.Parse(step.Trim().Remove(0, 1));
                if (turn.Equals('L'))
                {
                    dir--;
                }
                else
                {
                    dir++;
                }
                if (dir < 0) { dir += 4; }
                if (dir > 3) { dir -= 4; }
                switch ((directions)dir)
                {
                    case directions.north:
                        y += blocks;
                        for (int i = loc.y+1; i <= y; i++)
                        {
                            temp.x = x;
                            temp.y = i;
                            if (locations.Contains(temp))
                            {
                                Console.WriteLine("Already visited " + x + "," + i + " (" + (Math.Abs(x) + Math.Abs(i)) + " blocks away)");
                            }
                            else
                            {
                                locations.Add(temp);
                            }
                        }
                        break;
                    case directions.south:
                        y -= blocks;
                        for (int i = loc.y-1; i >= y; i--)
                        {
                            temp.x = x;
                            temp.y = i;
                            if (locations.Contains(temp))
                            {
                                Console.WriteLine("Already visited " + x + "," + i + " (" + (Math.Abs(x) + Math.Abs(i)) + " blocks away)");
                            }
                            else
                            {
                                locations.Add(temp);
                            }
                        }
                        break;
                    case directions.east:
                        x += blocks;
                        for (int i = loc.x+1; i <= x; i++)
                        {
                            temp.x = i;
                            temp.y = y;
                            if (locations.Contains(temp))
                            {
                                Console.WriteLine("Already visited " + i + "," + y + " (" + (Math.Abs(i) + Math.Abs(y)) + " blocks away)");
                            }
                            else
                            {
                                locations.Add(temp);
                            }
                        }
                        break;
                    case directions.west:
                        x -= blocks;
                        for (int i = loc.x-1; i >= x; i--)
                        {
                            temp.x = i;
                            temp.y = y;
                            if (locations.Contains(temp))
                            {
                                Console.WriteLine("Already visited " + i + "," + y + " (" + (Math.Abs(i) + Math.Abs(y)) + " blocks away)");
                            }
                            else
                            {
                                locations.Add(temp);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid direction: " + dir);
                        break;
                }
            
                loc.x = x;
                loc.y = y;
               
            }
            Console.WriteLine("Distance: " + (Math.Abs(x)+Math.Abs(y)) + " blocks");
            Console.ReadLine();
        }
    }
}
