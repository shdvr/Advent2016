/* TODO: use regex to extract ints into a two-dimensional array using System.Text.RegularExpressions; */

using System;


namespace Day3
{
    class Triangles
    {
        static void Main(string[] args)
        {
            String input;
            using (System.IO.StreamReader sr = new System.IO.StreamReader("inputDay3.txt"))
            {
                input = sr.ReadToEnd();
            }
            String[] lines = input.Split('\r');

            int valid = 0;
            int a, b, c;
            int[] colA, colB, colC;
            colA = new int[lines.Length];
            colB = new int[lines.Length];
            colC = new int[lines.Length];
            String line;
            int i = 0;

            foreach (String str in lines)
            {
                line = str.Trim();
                a = Int32.Parse(line.Split(' ')[0]);
                b = Int32.Parse(line.Remove(0, a.ToString().Length).Trim().Split(' ')[0]);
                c = Int32.Parse(line.Remove(0, a.ToString().Length).Trim().Remove(0, b.ToString().Length).Trim());
                if (a + b > c && b + c > a && a + c > b) valid++;
                colA[i] = a;
                colB[i] = b;
                colC[i] = c;
                i++;
            }
            Console.WriteLine("Valid triangles (left-to-right): " + valid);

            valid = 0;
            for (int l = 0; l < lines.Length; l += 3)
            {
                if (colA[l] + colA[l + 1] > colA[l + 2] && colA[l + 1] + colA[l + 2] > colA[l] && colA[l] + colA[l + 2] > colA[l + 1]) valid++;
                if (colB[l] + colB[l + 1] > colB[l + 2] && colB[l + 1] + colB[l + 2] > colB[l] && colB[l] + colB[l + 2] > colB[l + 1]) valid++;
                if (colC[l] + colC[l + 1] > colC[l + 2] && colC[l + 1] + colC[l + 2] > colC[l] && colC[l] + colC[l + 2] > colC[l + 1]) valid++;
            }
            Console.WriteLine("Valid triangles (top-down): " + valid);
            Console.ReadLine();
        }

    }
}
