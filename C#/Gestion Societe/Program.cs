using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Societe
{
    internal class Program
    {
        public static string GeneratePassword(int passLength)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz@#$&ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, passLength)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(GeneratePassword(10));
            Console.WriteLine(GeneratePassword(10));
            Console.WriteLine(GeneratePassword(10));
            Console.WriteLine(GeneratePassword(10));
            Console.WriteLine(GeneratePassword(10));
        }
    }
}
