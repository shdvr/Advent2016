using System;
using System.Text;
using System.Security.Cryptography;

namespace Day5
{
    class Password
    {
        static void Main(string[] args)
        {
            String temp;
            byte[] hash;
            int part1position = 0;
            char[] pwd1 = "--------".ToCharArray();
            char[] pwd2 = "--------".ToCharArray();
            bool keepGoing = true;
            byte displayChar = 32;
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            for (int i = 0; keepGoing; i++)
            {
                if(i%10000==0) //optional "hacking progress" UI update
                {
                    Console.Clear();
                    Console.WriteLine((new string(pwd1) + " " + new string(pwd2)).Replace('-',(char)displayChar));
                    displayChar++;
                    if (displayChar > 126)
                    {
                        displayChar = 32;
                    }
                }
                temp = args[0] + i;
                hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(temp));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] < 0x10)
                {
                    //first part
                    if (part1position < 8)
                    {
                        pwd1[part1position] = hash[2].ToString("X2")[1];
                        part1position++;
                    }
                    //second part
                    if (hash[2] < 8)
                    {
                        if (pwd2[hash[2]].Equals('-'))
                        {
                            pwd2[hash[2]] = hash[3].ToString("X2")[0];
                            keepGoing = false;
                            for (int x = 0; x < 8; x++)
                            {
                                if (pwd2[x].Equals('-'))
                                {
                                    keepGoing = true;
                                }
                            }
                        }
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Password1: " + new string(pwd1));
            Console.WriteLine("Password2: " + new string(pwd2));
            Console.ReadLine();

        }
    }
}
