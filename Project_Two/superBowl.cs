using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace Project_Two
{
    class SuperBowl
    {
        public List<SuperBowl> superBowlList = new List<SuperBowl>();
        public List<string> stringList = new List<string>();
        private bool end = false;
        int runOnce = 1;
        int a = 1;


        public string name { get; set; } //name of superbowl
        public string date { get; set; } //date it happend
        public int attendance { get; set; } // # of attendance
        public string city { get; set; } // city
        public string stadium { get; set; } // which statdium?
        public string state { get; set; } // which state?
        public string MVP { get; set; }

        public string winnerTeamName { get; set; }
        public string winnerQB { get; set; }
        public int winningPoints { get; set; }
        public string winningCoach { get; set; }


        public string loserTeamName { get; set; }
        public string loserQB { get; set; }
        public int loserPoints { get; set; }
        public string loserCoach { get; set; }

        public SuperBowl()
        {

        }

        public SuperBowl(string date, string name, int Attendance,string winnerQB,string WinnerCoach, string Winner, int WinningPts, string loserQB, string LoserCoach, string loser, int LosingPts,string MVP, string stadium, string StadiumCity, string State)
        {
            this.name = name;
            this.date = date;
            this.attendance = Attendance;
            this.winnerQB = winnerQB;
            this.winningCoach = WinnerCoach;
            this.winnerTeamName = Winner;
            this.loserTeamName = loser;
            this.winningPoints = WinningPts;
            this.loserQB = loserQB;
            this.loserCoach = LoserCoach;
            this.loserPoints = LosingPts;
            this.MVP = MVP;
            this.stadium = stadium;
            this.city = StadiumCity;
            this.state = State;
        }

        public void getText(string filepath)
        {
            string[] ArrayOfObjects;
            FileStream inputFileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inputFileStream);
            //var superBowlList = JsonConvert.DeserializeObject<List<SuperBowl>>(serializedData); //uses a JSON file to read in data
            
            //for (int i = 0; i < 52; i++)
            while (!reader.EndOfStream)
            {
                while (runOnce == 1)
                {
                    ArrayOfObjects = (reader.ReadLine().Split(","));
                    runOnce--;
                }
                
                    try
                    {
                        ArrayOfObjects = (reader.ReadLine().Split(","));
                        superBowlList.Add(new SuperBowl(Convert.ToString(ArrayOfObjects[0]), Convert.ToString(ArrayOfObjects[1]),
                            Convert.ToInt32(ArrayOfObjects[2]), Convert.ToString(ArrayOfObjects[3]), Convert.ToString(ArrayOfObjects[4]), Convert.ToString(ArrayOfObjects[5]),
                            Convert.ToInt32(ArrayOfObjects[6]),
                            Convert.ToString(ArrayOfObjects[7]), Convert.ToString(ArrayOfObjects[8]), Convert.ToString(ArrayOfObjects[9]),
                            Convert.ToInt32(ArrayOfObjects[10]), Convert.ToString(ArrayOfObjects[11]), Convert.ToString(ArrayOfObjects[12]),
                            Convert.ToString(ArrayOfObjects[13]), Convert.ToString(ArrayOfObjects[14])));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    
            }
                reader.Close();
                inputFileStream.Close();
        }

        public string generateFile()
        {
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string someval = "";
            string filePath = Directory.GetCurrentDirectory();
            string stepBack = Directory.GetParent(filePath).ToString();
            string stepBackTwo = Directory.GetParent(stepBack).ToString();
            string stepBackThree = Directory.GetParent(stepBackTwo).ToString();
            string adjustedFilePath = $@"{stepBackThree}\{someval}.txt";
            Console.WriteLine("Type what you want your file to be called.\n(please don't add an extension to your name)\n");
            someval = Console.ReadLine();
            adjustedFilePath = $@"{deskPath}\{someval}.txt";
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
            try
            {
                    StreamWriter myFile = new StreamWriter(file);
                stringList.Add(superBowlList[0].name);
                myFile.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                myFile.WriteLine("╬" + "Winners".PadLeft(61)+ "╬".PadLeft(60));
                myFile.WriteLine("╬════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╬");
                myFile.WriteLine("╬" + "Team".PadRight(22) +"| "+ "Year".PadRight(10) + "| " + "Quarter-Back".PadRight(28) + "| " + "Coach".PadRight(16)+ "| " + "MVP".PadRight(27)+ "| " + "Points"+ " ╬");
                myFile.WriteLine("|------------------------------------------------------------------------------------------------------------------------|");
                for (int i = 0; i < 51; i++)
                {
                    myFile.WriteLine("║" + superBowlList[i].winnerTeamName.PadRight(22) + "| " + yearManager(superBowlList[i]).PadRight(10)+ "| " + superBowlList[i].winnerQB.PadRight(28) + "| " + superBowlList[i].winningCoach.PadRight(16) + "| " + superBowlList[i].MVP.PadRight(27) + "| " + Convert.ToString((superBowlList[i].winningPoints - superBowlList[i].loserPoints)).PadRight(7) + "║");
                }
                myFile.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
                var mostAttended = from superBowl in superBowlList
                    where Convert.ToString( superBowl.attendance).Length > 5
                    orderby superBowl.attendance descending 
                    select superBowl;
                myFile.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                myFile.WriteLine("╬" + "Top 5 most Attended".PadLeft(72) + "╬".PadLeft(49));
                myFile.WriteLine("╬════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╬");
                myFile.WriteLine("╬" + "Year".PadRight(10) + "| " + "Winner Team".PadRight(27) + "| " + "Losing Team".PadRight(27) + "| " + "City".PadRight(15) + "| " + "State".PadRight(11) + "| " + "Stadium".PadRight(19) + " ╬");
                myFile.WriteLine("|------------------------------------------------------------------------------------------------------------------------|");
                foreach (var superBowl in mostAttended)
                {
                    myFile.WriteLine("║" + yearManager(superBowl)?.PadRight(10) + "| " + superBowl.winnerTeamName?.PadRight(27) + "| " + superBowl.loserTeamName?.PadRight(27) + "| " + superBowl.city?.PadRight(15) + "| " + superBowl.state?.PadRight(11) + "| " + superBowl.stadium?.PadRight(20) + "║");
                }
                myFile.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");
                myFile.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                myFile.WriteLine("╬" + "State that hosted the most SuperBowls".PadLeft(72) + "╬".PadLeft(49));
                myFile.WriteLine("╬════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╬");
                myFile.WriteLine("╬" + "City".PadRight(18) + "| " + "State".PadRight(20) + "| " + "Stadium".PadRight(27) + "| " + "Winner".PadRight(25)+ "| " + "Loser".PadRight(21) + " ╬");
                myFile.WriteLine("|------------------------------------------------------------------------------------------------------------------------|");
                //state that hosted the most superbowls
                var mostHosted = (from superBowl in superBowlList
                    group superBowl by superBowl.state
                    into superBowlStates
                    orderby superBowlStates.Count() descending
                    select superBowlStates).First();

                foreach (var superBowl in mostHosted)
                {
                    myFile.WriteLine("║" + superBowl.city?.PadRight(18)+ "| " + superBowl.state?.PadRight(20)+"| "+ superBowl.stadium?.PadRight(27)+ "| " +superBowl.winnerTeamName.PadRight(25) + "| " + superBowl.loserTeamName.PadRight(22) + "║");
                }
                myFile.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");

                // additional information

                myFile.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                myFile.WriteLine("╬" + "Additional Information".PadLeft(72) + "╬".PadLeft(49));
                myFile.WriteLine("╬════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╬");
                myFile.WriteLine("╬" + "Coach who lost the most".PadRight(25) + "| " + "Coach who won the most".PadRight(25) + "| " + "Most Winning Team".PadRight(22) + "| " + "Most Losing Team".PadRight(18) + "| " + "Average Attendance".PadRight(21) + " ╬");
                myFile.WriteLine("|------------------------------------------------------------------------------------------------------------------------|");
                myFile.WriteLine("╬" + superBowlList[2].loserCoach.PadRight(25)+ "| "+ superBowlList[35].winningCoach.PadRight(25)+ "| "+ superBowlList[8].winnerTeamName.PadRight(22) + "| "+ superBowlList[11].loserTeamName.PadRight(18) + "| " + Convert.ToString(averageAttendance()).PadRight(22)+ "╬");
                myFile.WriteLine("╠════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣\n");
                myFile.WriteLine("╬"+ "The Superbowl that had the biggest points difference was SuperBowl "+ superBowlList[23].name +" with a " + (superBowlList[23].winningPoints - superBowlList[23].loserPoints) + " points difference".PadRight(39)+"╬");
                myFile.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");

                myFile.Close();
                Console.WriteLine("You can find your file here at: " +file+"\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("welp that didnt work, maybe the thing is broke");
                Console.WriteLine(e);
            }
        }

        public string yearManager(SuperBowl thing)
        {
            string year = "";
            for (int i = 0; i < a; i++)
            {
                year = Convert.ToString(thing.date.Split("-")[2]);
                if (Convert.ToInt32(year) >= 66)
                {
                    year = "19" + year;
                }
                else if (Convert.ToInt32(year) <=66)
                {
                    year = "20" + year;
                }
            }
            a++;
            return year;
        }

        public int averageAttendance()
        {
            int counter = 0;
            int average = 0;
            for (int i = 0; i < 50; i++)
            {
                average = superBowlList[i].attendance + average;
                counter++;
            }
            average = average / counter;
            return average;
        }

    }
}
