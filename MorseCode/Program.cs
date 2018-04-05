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

        static void PrintReverseMorseMap(Dictionary<string, char> morseMap)
        {
            foreach (var character in morseMap)
            {
                Console.WriteLine($"{character.Key} , {character.Value}");
            }
        }

        static void PrintUserInputs(Dictionary<string, string> userInputs)
        {
            Console.WriteLine("\n");
            foreach(var input in userInputs)
            {
                Console.WriteLine($"{input.Key} , {input.Value}");
            }
            Console.WriteLine("---------------------------------------------");
        }

        static void OutputToFile(Dictionary<string, string> outputDict, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var line in outputDict)
                {
                    writer.WriteLine($"{line.Key},{line.Value}");
                }
            }
        }

        static void PrintStrings(string str1, string str2, string type)
        {
            Console.WriteLine($"\n---------------------------------------------\n\n" +
            $"'{str1}' in {(type == "alpha" ? "morse code" : "alphabetical characters")} is '{str2}' \n" +
            $"\n---------------------------------------------\n");
        }

        static string ConvertToMorseCode(string phrase, Dictionary<char, string> morseMap)
        {
            string morsePhrase = "";

            for (int i = 0; i < phrase.Count(); i++)
            {
                if (phrase[i] == ' ')
                {
                    morsePhrase += " / ";
                }
                else if (morseMap.ContainsKey(phrase[i])) 
                {
                    morsePhrase += morseMap[phrase[i]] + " ";
                } else
                {
                    morsePhrase += phrase[i] + " ";
                }
            }
            return morsePhrase;
        }

        static string ConvertToAlpha(string phrase, Dictionary<string, char> reverseMap)
        {
            var splitPhrase = phrase.Split(' ');
            for(int i = 0; i < splitPhrase.Count(); i++)
            {
                if (reverseMap.ContainsKey(splitPhrase[i]))
                {
                    splitPhrase[i] = reverseMap[splitPhrase[i]].ToString();
                }
                else if(splitPhrase[i] == "/")
                {
                    splitPhrase[i] = " ";
                }
            }
            phrase = string.Join("", splitPhrase);
            return phrase;
        }

        static void Main(string[] args)
        {
            bool isRunning = true;

            // Create dictionary with string and with char values
            var morseMap = new Dictionary<char, string>();
            var reverseMorseMap = new Dictionary<string, char>();
            var userInputs = new Dictionary<string, string>();

            // Getting filepaths
            const string MORSE_CODE_FILE_PATH = "../../../assets/morse.csv";
            const string USER_OUTPUT_FILE_PATH = "../../../assets/user_output.csv";

            // use streamreader to read in the file.
            using (var reader = new StreamReader(MORSE_CODE_FILE_PATH))
            {
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine().Split(',');
                    morseMap.Add(Char.ToLower(Convert.ToChar(line[0])), line[1]);
                    reverseMorseMap.Add(line[1], Char.ToLower(Convert.ToChar(line[0])));
                }
            }

            // use streamReader to check if a file exists
            if (File.Exists(USER_OUTPUT_FILE_PATH))
            {
                // if the file exists, read from it!
                using (var output = new StreamReader(USER_OUTPUT_FILE_PATH))
                {
                    while (output.Peek() > -1)
                    {
                        var line = output.ReadLine().Split(',');
                        userInputs.Add(line[0], line[1]);
                    }
                }
            } else
            {
                Console.WriteLine("File does not exist, creating file...");
            }

            Console.WriteLine("*********************************************\n" +
                "***Welcome to the Morse Code Translator!!!***\n" +
                "*********************************************");

            while(isRunning == true)
            {
                Console.WriteLine("\nWhat do you want to do? \n\n" +
                    "(1) Convert alphabetical to morse code. \n" +
                    "(2) Convert morse code to alphabetical \n" +
                    "(3) Print all saved alpha-morse translations \n" +
                    "(exit) quit program\n");
                string userChoice = Console.ReadLine();
                string phrase;

                if(userChoice == "1")
                {
                    Console.WriteLine("\nPlease type something to convert to morse code");
                    phrase = Console.ReadLine().ToLower();

                    string morsePhrase = ConvertToMorseCode(phrase, morseMap);

                    PrintStrings(phrase, morsePhrase, "alpha");

                    // Check to see if the file already has the phrase. If not,save it.
                    if(!userInputs.ContainsKey(phrase))
                    {
                        userInputs.Add(phrase, morsePhrase);
                    }
                    OutputToFile(userInputs, USER_OUTPUT_FILE_PATH);

                }
                else if(userChoice == "2")
                {
                    Console.WriteLine("\nPlease type something to convert from morse code \n" +
                        "(Make sure you have spaces to separate characters, and \"/\" to separate words\n");
                    phrase = Console.ReadLine().ToLower();

                    string alphaPhrase = ConvertToAlpha(phrase, reverseMorseMap);
                    PrintStrings(phrase, alphaPhrase, "morse");

                } else if(userChoice.ToLower() == "exit")
                {
                    Console.WriteLine("Great! Hope you enjoyed the game! Later!!!");
                    isRunning = false;
                }
                else if(userChoice == "3")
                {
                    PrintUserInputs(userInputs);
                }
                else
                {
                    Console.WriteLine("Invalid command. Please try again");
                }
            }
            Console.ReadLine();
        }
    }
}