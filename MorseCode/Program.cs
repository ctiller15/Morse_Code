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
        static void PrintMorseMap(Dictionary<char, string> morseMap)
        {
            foreach(var character in morseMap)
            {
                Console.WriteLine($"{character.Key} , {character.Value}");
            }
        }

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
                    morseMap.Add(Char.ToLower(Convert.ToChar(line[0])), line[1]);
                }
            }

            PrintMorseMap(morseMap);

            bool isRunning = true;
            while(isRunning == true)
            {
                Console.WriteLine("Please type something to convert to morse code");
                string phrase = Console.ReadLine().ToLower();
                Console.WriteLine(phrase);

                string morsePhrase = "";
                for(int i = 0; i < phrase.Count(); i++)
                {
                    if(phrase[i] == ' ')
                    {
                        morsePhrase += " / ";
                    } else
                    {
                        morsePhrase += morseMap[phrase[i]] + " ";
                    }

                }

                Console.WriteLine(morsePhrase);

                Console.WriteLine("Would you like to try converting another phrase? (Y/N)");
                string userPrompt = Console.ReadLine();
                if(userPrompt.ToLower() != "y")
                {
                    isRunning = false;
                    Console.WriteLine("Great! Hope you enjoyed the application!");
                } else
                {
                    Console.WriteLine("Sweet! Let's go do another one!");
                }

            }

            Console.ReadLine();
            // Grab all of the data in the file.
            // Append it into dictionary.
        }
    }
}
