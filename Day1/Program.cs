using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        public enum Directions : int { North = 0, East = 1, South = 2, West = 3 }

        public struct Location
        {
            public int lon;
            public int lat;
        }
        static void Main(string[] args)
        {
            String[] steps;
            using (System.IO.StreamReader sr = new System.IO.StreamReader("input.txt"))
            {
                steps = sr.ReadToEnd().Split(',');
            }

            int currentLong = 0;
            int currentLat = 0;
            long totalWalk = 0;
            bool reVisited = false;
            Location loc = new Location()
            {
                lon = currentLong,
                lat = currentLat
            };
            Location firstRevisit = loc;
            int myDirection = (int)Directions.North;
            List<Location> visited = new List<Location>();
            int blocksToWalk;
            Location temp;

            foreach (String step in steps)
            {
                blocksToWalk = Int32.Parse(step.Trim().Remove(0, 1));
                totalWalk += blocksToWalk;
                myDirection += (step.Trim()[0].Equals('R') ? 1 : -1);
                if (myDirection < 0) { myDirection += 4; }
                if (myDirection > 3) { myDirection -= 4; }
                switch ((Directions)myDirection)
                {
                    case Directions.North:
                        currentLat += blocksToWalk;
                        if (!reVisited)
                        {
                            for (int i = loc.lat + 1; i <= currentLat; i++)
                            {
                                temp.lon = currentLong;
                                temp.lat = i;
                                if (visited.Contains(temp))
                                {
                                    firstRevisit = temp;
                                    reVisited = true;
                                }
                                else
                                {
                                    visited.Add(temp);
                                }
                            }
                        }
                        break;
                    case Directions.South:
                        currentLat -= blocksToWalk;
                        if (!reVisited)
                        {
                            for (int i = loc.lat - 1; i >= currentLat; i--)
                            {
                                temp.lon = currentLong;
                                temp.lat = i;
                                if (visited.Contains(temp))
                                {
                                    firstRevisit = temp;
                                    reVisited = true;
                                }
                                else
                                {
                                    visited.Add(temp);
                                }
                            }
                        }
                        break;
                    case Directions.East:
                        currentLong += blocksToWalk;
                        if (!reVisited)
                        {
                            for (int i = loc.lon + 1; i <= currentLong; i++)
                            {
                                temp.lon = i;
                                temp.lat = currentLat;
                                if (visited.Contains(temp))
                                {
                                    firstRevisit = temp;
                                    reVisited = true;
                                }
                                else
                                {
                                    visited.Add(temp);
                                }
                            }
                        }
                        break;
                    case Directions.West:
                        currentLong -= blocksToWalk;
                        if (!reVisited)
                        {
                            for (int i = loc.lon - 1; i >= currentLong; i--)
                            {
                                temp.lon = i;
                                temp.lat = currentLat;
                                if (visited.Contains(temp))
                                {
                                    firstRevisit = temp;
                                    reVisited = true;
                                }
                                else
                                {
                                    visited.Add(temp);
                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid direction: " + myDirection);
                        break;
                }
                loc.lon = currentLong;
                loc.lat = currentLat;
            }
            Console.WriteLine("Distance from starting point: " + (Math.Abs(currentLong) + Math.Abs(currentLat)) + " blocks");
            Console.WriteLine("Walked a total of " + totalWalk + " blocks");
            if (reVisited) Console.WriteLine("First revisited a location at " + firstRevisit.lon + "," + firstRevisit.lat + " (" + (Math.Abs(firstRevisit.lon) + Math.Abs(firstRevisit.lat)) + " blocks away)");
            Console.ReadLine();
        }
    }
}
