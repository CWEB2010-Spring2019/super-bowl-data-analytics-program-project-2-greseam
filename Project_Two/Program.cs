using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace Project_Two
{
    class Program
    {
        static void Main(string[] args)
        {
            superBowl mySuperBowl = new superBowl();
            while (mySuperBowl.welcome()==false)
            {
                mySuperBowl.getText(@"Super_Bowl_Project.csv"); // set up data stream
                                // create file for writing
                mySuperBowl.writeFile(mySuperBowl.generateFile());
                //write to file with formating
            }
           

            /**Your application should allow the end user to pass end a file path for output 
             * or guide them through generating the file.
             **/

            //make a list of winner, most attended, most used state, multi mvp'ers.
        }

    }

    class unkown
    {
        //unkown class
    }
}
