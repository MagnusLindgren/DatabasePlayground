using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;

namespace DatabasePlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Database Menu");
                Console.WriteLine("1) View the database");
                Console.WriteLine("2) Add to the database");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        DBEngine test = new DBEngine("video_games_v03.db");
                        DBEngine.DatabaseHandler();
                        break;
                    case "2":
                        
                        break;
                    default:
                        break;
                }
            }
           
        }
    }
}
