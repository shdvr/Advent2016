using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class IPv7
    {
        static void Main(string[] args)
        {
            int tlsCount = 0;
            int sslCount = 0;
            String ip;
            List<string> sn;
            List<string> hn;
            List<char> ipSection;
            bool parsingHN;

            using (System.IO.StreamReader sr = new System.IO.StreamReader(args[0]))
            {
                while(!sr.EndOfStream)
                {
                    ip = sr.ReadLine();
                    //first parse the IP string into lists of supernet and hypernet sections
                    parsingHN = false; //whether current portion of the IP is a hypernet or supernet section. IPs are assumed to start with a supernet section
                    sn = new List<string>(); //tracks supernet sections of the IP
                    hn = new List<string>(); //tracks hypernet section of the IP
                    ipSection = new List<char>(); //current portion of the IP being parsed
                    for (int i = 0; i < ip.Length; i++)
                    {
                        if (parsingHN)
                        {
                            if (ip[i].Equals(']'))
                            {
                                hn.Add(new string(ipSection.ToArray()));
                                ipSection = new List<char>();
                                parsingHN = false;
                            }
                            else
                            {
                                ipSection.Add(ip[i]);
                            }
                        }
                        else
                        {
                            if (ip[i].Equals('['))
                            {
                                sn.Add(new string(ipSection.ToArray()));
                                ipSection = new List<char>();
                                parsingHN = true;
                            }
                            else
                            {
                                ipSection.Add(ip[i]);
                            }
                        }
                    }
                    if (ipSection.Count > 0) //add the remainig section (if any) to the supernet sections list
                    {
                        sn.Add(new string(ipSection.ToArray())); 
                    }
                    //now using the parsed supernet and hypernet data check if the IP supports TLS and SSL
                    if (SupportsTLS(sn,hn))
                    {
                        tlsCount++;
                    }
                    if (SupportsSSL(sn, hn))
                    {
                        sslCount++;
                    }
                }
            }
            Console.WriteLine("TLS IPs: " + tlsCount);
            Console.WriteLine("SSL IPs: " + sslCount);
            Console.ReadLine();
        }

        static bool SupportsSSL(List<string> sn, List<string> hn)
        {
            List<string> allABAs = new List<string>();
            List<string> allBABs = new List<string>();
            //first find all ABAs
            foreach (string s in sn)
            {
                allABAs.AddRange(GetAllABAs(s));
            }
            if (allABAs.Count == 0)
            {
                return false; //no ABAs means SSL is not supported, skip further checks
            }
            //similarly get all BABs, whic are identical to ABAs but are located in the hypernet sections of the IP
            //note that this therefore uses the same function to do the actual check
            foreach (string s in hn)
            {
                allBABs.AddRange(GetAllABAs(s));
            }
            if (allBABs.Count == 0)
            {
                return false; //no BABs means SSL is not supported, skip further checks
            }
            //now matching up those ABAs to BABs
            foreach (string s in allABAs)
            {
                if (allBABs.Contains(s.Substring(1, 2) + s.Substring(1, 1))) //ABA -> BA+B
                {
                    return true; //Found BAB matching an ABA
                }
            }
            return false; //No ABA to BAB matches found
        }

        static bool SupportsTLS(List<string> sn, List<string> hn)
        {
            foreach (string s in hn)
            {
                if(HasABBA(s))
                {
                    return false; //ABBA in hypernet means TLS is NOT supported
                }
            }
            foreach (string s in sn)
            {
                if (HasABBA(s))
                {
                    return true; //ABBA found in a supernet ("regular") section of the IP and no ABBA present in hypernet sections
                }
            }
            return false;
        }

        static bool HasABBA(string input)
        {
            for(int i=0; i<input.Length-3;i++)
            {
                if(input[i].Equals(input[i+3]) && !input[i].Equals(input[i+1]) && input[i+1].Equals(input[i+2]))
                {
                    return true; //ABBA
                }
            }
            return false;
        }

        static List<string> GetAllABAs(string input)
        {
            List<string> output = new List<string>();
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i].Equals(input[i + 2]) && !input[i].Equals(input[i + 1]))
                {
                    output.Add(input.Substring(i, 3)); //ABA
                }
            }
            return output;
        }
    }
}
