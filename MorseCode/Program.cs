using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create dictionary with string and with char values
            var morseMap = new Dictionary<char, string>();
            const string FILE_PATH = "../../../assets/morse.csv";
            // use stremreader to read in the file.
            using (var reader = new StreamReader(FILE_PATH))
            {
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine().Split(',');
                    Console.WriteLine($"{line[0]} , {line[1]}");
                }
            }
            Console.ReadLine();
            // Grab all of the data in the file.
            // Append it into dictionary.
        }
    }
}
