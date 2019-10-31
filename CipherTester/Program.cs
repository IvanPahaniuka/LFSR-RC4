using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encoders;
using System.IO;

namespace CipherTester
{
    //Класс для тестирования
    class Program
    {
        const long START_REG = 0b1010110011001110010101110;

        static void Main(string[] args)
        {
            Console.Write("Источник: ");
            string src = Console.ReadLine();
            Console.Write("Конечный файл: ");
            string dsrc = Console.ReadLine();
            
            RC4Encoder encoder = new RC4Encoder();
            FileEncoder fencoder = new FileEncoder(encoder);

            DateTime time = DateTime.Now;
            Console.WriteLine(fencoder.Encrypt(src, dsrc, new byte[] { 234, 13, 87, 148, 12, 159 }).ToBitString(30));
            Console.WriteLine($"Time: {DateTime.Now - time}");

            Console.WriteLine("OK!");
            Console.ReadKey();
        }
    }
}
