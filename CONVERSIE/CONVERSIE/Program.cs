using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CONVERSIE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("nr=");
            string nr = Console.ReadLine();

            Console.Write("b1=");
            int b1 = int.Parse(Console.ReadLine());
            if(b1 < 2 || b1 >16)
            {
                throw new Exception("Baza este <out of range>");
            }
            
            Console.Write("b2=");
            int b2 = int.Parse(Console.ReadLine());
            if (b2 < 2 || b2 > 16)
            {
                throw new Exception("Baza este <out of range>");
            }

            decimal nrB10 = ConvertesteInB10(b1, nr);

            Console.Write("Rezultat: ");

            int ParteIntNrB10 = (int)Math.Truncate(nrB10);
            decimal ParteFractNrB10 = nrB10 - ParteIntNrB10;

            if (ParteIntNrB10 == 0)
                  Console.Write("0");
            else
                  ConvertesteParteIntInB2(ParteIntNrB10, b2);

            if(ParteFractNrB10 > 0) 
            {
                Console.Write(".");
                ConvertesteParteFractInB2(ParteFractNrB10, b2);
            }
            
            
        }

     
        private static void ConvertesteParteFractInB2(decimal parteFract, int b2)
        {
            List<decimal> fractii = new List<decimal>();
            List<char> cifre = new List<char>();
        
            fractii.Add(parteFract);
            bool periodica = false;
            while (parteFract != 0 && !periodica)
            {
                parteFract = parteFract * b2;
                if(parteFract < 10)
                {
                  int cif =(int)Math.Truncate(parteFract);
                  string cif1 = cif.ToString();
                  char cif2 = Convert.ToChar(cif1);
                  cifre.Add(cif2);
                }
                else
                {
                    int c = (int)Math.Truncate(parteFract);
                    char c1 = Convert.ToChar( 'A' + c % 10 );
                    cifre.Add(c1);
                }
                  
                parteFract = parteFract - (int)Math.Truncate(parteFract);
        
                if (!fractii.Contains(parteFract))
                {
                    fractii.Add(parteFract);
                }
                else
                    periodica = true;
        
            }
            if (!periodica)
            {
                foreach (var item in cifre)
                    Console.Write(item);
            }
            else
            {
                for (int i = 0; i < fractii.Count; i++)
                {
                    if (fractii[i] == parteFract)
                    {
                        Console.Write("(");
                    }
                    Console.Write(cifre[i]);
                }
                Console.WriteLine(")");
            }
        }

        private static void ConvertesteParteIntInB2(int parteInt, int b2)
        {
           if(parteInt > 0)
            {
                ConvertesteParteIntInB2(parteInt/b2 , b2);
                if(parteInt % b2 < 10)
                   Console.Write(parteInt % b2);
                else
                    Console.Write(Convert.ToChar('A' + (parteInt % b2) % 10));
            }
        }

        private static decimal ConvertesteInB10(int b1, string nr)
        {
            
            string cif = "0123456789ABCDEF";
            decimal rezultat = 0;
            int parteInt = 0;
            decimal parteFract = 0;
            int pozPunct = nr.IndexOf('.');
            int poz = 0;
            if (pozPunct != -1)
                poz = pozPunct - 1;
            else
                poz = nr.Length - 1;
            

            for (int i = 0; i <= poz; i++)
            {
                int cifra = cif.IndexOf(nr[i], 0, b1);
                if (cifra == -1)
                    throw new ArgumentException("Numarul nu este scris in baza b1.");
                parteInt = parteInt * b1 + cifra;
            }

            decimal p = 1 / (decimal)b1;

            if(nr.IndexOf(".") != -1) 
            {
                for(int i = nr.IndexOf(".") + 1; i < nr.Length; i++) 
                {
                    decimal cifra = cif.IndexOf(nr[i], 0, b1);
                    if (cifra == -1)
                        throw new ArgumentException("Numarul nu este scris in baza b1.");
                    parteFract = parteFract + (cifra * p);
                    p = p * (1 / (decimal)b1); 
                }
            }
            
            rezultat = parteInt + parteFract;
            return rezultat;
        }

    }
}

   
