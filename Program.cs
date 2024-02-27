using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashingMacTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {

                //
                // SHA256 Hashing
                //
                Console.WriteLine("1. SHA256");
                Console.WriteLine("2. HMACSHA256");
                Console.WriteLine("");

                ConsoleKey selected = Console.ReadKey().Key;
                Console.WriteLine("");
                string message;
                string key;
                switch (selected)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Type your Message then press enter:");
                        message = Console.ReadLine();
                        SHA256Hashing(message);
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Type your Message then press enter:");
                        message = Console.ReadLine();
                        Console.WriteLine("Type your SecretKey then press enter:");
                        key = Console.ReadLine();

                        HMACSHA256Hashing(message, key);
                        break;
                    default:
                        Console.WriteLine($"Type:({selected.ToString()}) Not Allowed");
                        break;
                }

                Console.WriteLine("Press Enter to go back to start menu");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void HMACSHA256Hashing(string message, string keyText)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyText); // Secret Key

            byte[] hmacMessageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hmacValue;
            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                using (MemoryStream ms = new MemoryStream(hmacMessageBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, hmac, CryptoStreamMode.Read))
                    {
                        cs.CopyTo(Stream.Null);
                    }
                }
                hmacValue = hmac.Hash;
            }

            Console.WriteLine("");
            Console.WriteLine("HMACSHA256 Test/");

            Console.WriteLine($"Plain Text: {message}");
            Console.WriteLine($"Key Plain Text: {keyText}");
            Console.WriteLine($"Key Encoded: {BitConverter.ToString(key)}");

            Console.WriteLine($"HashValue: {BitConverter.ToString(hmacValue)}");

            Console.Write("HashValue Bytes: ");
            foreach (Byte b in hmacValue)
            {
                Console.Write(b.ToString());
            }
            Console.WriteLine("\n");
        }

        public static void SHA256Hashing(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashValue;
            using (SHA256 sha256 = SHA256.Create())
            {
                using (MemoryStream ms = new MemoryStream(messageBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, sha256, CryptoStreamMode.Read))
                    {
                        cs.CopyTo(Stream.Null);
                    }
                }
                hashValue = sha256.Hash;
            }
            Console.WriteLine("");
            Console.WriteLine("SHA256 Test/");

            Console.WriteLine($"Plain Text: {message}");
            Console.WriteLine($"HashValue: {BitConverter.ToString(hashValue)}");

            Console.Write("HashValue Bytes: ");
            foreach (Byte b in hashValue)
            {
                Console.Write(b.ToString());
            }
            Console.WriteLine("\n");
        }
    }
}
