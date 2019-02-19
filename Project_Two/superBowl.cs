using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project_Two
{
    class superBowl
    {
        public List<superBowl> superBowlList;
        private bool end = false;


        private string name { get; set; } //name of superbowl
        private string date { get; set; } //date it happend
        private int attendance { get; set; } // # of attendance
        private string city { get; set; } // city
        private string stadium { get; set; } // which statdium?
        private string state { get; set; } // which state?

        public void getText(string filepath)
        {
            StreamReader reader = new StreamReader(filepath);
            string serializedData = reader.ReadToEnd();
            var superBowlList = (JArray)JsonConvert.DeserializeObject(serializedData); //uses a JSON file to read in data
        }

        public string generateFile()
        {
                string someval = "";
                string filePath = Directory.GetCurrentDirectory();
                string stepBack = Directory.GetParent(filePath).ToString();
                string stepBackTwo = Directory.GetParent(stepBack).ToString();
                string stepBackThree = Directory.GetParent(stepBackTwo).ToString();
                string adjustedFilePath = $@"{stepBackThree}\{someval}.txt";
                Console.WriteLine("Type what you want your file to be called.\n(please don't add an extension to your name)\n");
                someval = Console.ReadLine();
                adjustedFilePath = $@"{stepBackThree}\{someval}.txt";
                File.Create(adjustedFilePath);

                return adjustedFilePath;
        }

        public bool welcome()
        {
            Console.WriteLine("Hello, welcome to this simple program to organize and find data on superbowls.\nTo start hit any key.\nTo exit it the esc key");
            ConsoleKey Input = Console.ReadKey().Key;
            if (Input == ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Thank you, have a nice day");
                Console.WriteLine("Press and key to continue....");
                Console.ReadKey();
                end = true;
            }
            else
            {
                end = false;
                Console.Clear();
                
            }

            return end;
        }

        public void writeFile(string file)
        {
            File.OpenWrite(file);

        }

        public void DisplaySuperbowl()
        {
            Console.WriteLine(superBowlList[0]);
        }
    }

    class winner : superBowl
    {
        public string teamName { get; set; }
        public string QB { get; set; }
        public string LPoints { get; set; }
        public string coach { get; set; }

    }

    class Loser : superBowl
    {
        public string teamName { get; set; }
        public string QB { get; set; }
        public string WPoints { get; set; }
        public string coach { get; set; }


    }
}
