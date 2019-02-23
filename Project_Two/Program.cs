using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Project_Two
{
    class Program
    {


        static void Main(string[] args)
        {
            bool firstRun = true;
            SuperBowl mySuperBowl = new SuperBowl();
            while (firstRun == true && mySuperBowl.welcome() == false)
            {
                mySuperBowl.getText(@"Super_Bowl_Project.csv");
                mySuperBowl.writeFile(mySuperBowl.generateFile());
                Console.ReadKey();
                Console.WriteLine("Press any Key to Continue....");
                firstRun = false;
            }
        }
    }
}
