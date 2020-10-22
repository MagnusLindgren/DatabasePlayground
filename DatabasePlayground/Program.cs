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
            Console.SetWindowSize(150, 40);
            string connection_string = new SqliteConnectionStringBuilder()
            {
                DataSource = @"..\..\..\video_games_v03.db",
                Mode = SqliteOpenMode.ReadWrite,
            }.ToString();

            using (var connection = new SqliteConnection(connection_string))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    $@"
                        SELECT Title, Publishers, Year                           
                        FROM video_games
                    ";

                using (var reader = command.ExecuteReader())
                {                   
                    string column1 = reader.GetName(0);
                    string column2 = reader.GetName(1);
                    string column3 = reader.GetName(2);
                    string column1_type = reader.GetDataTypeName(0);
                    string column2_type = reader.GetDataTypeName(1);
                    string column3_type = reader.GetDataTypeName(2);

                    ColumnOutput(column1, column2, column3);
                    ColumnOutput(column1_type, column2_type, column3_type);
                  
                    int output_lines = 0;
                    while (reader.Read())
                    {                       
                        string gameTitle = reader.GetString(0);
                        string publisher_if_available = (reader.IsDBNull(1) ? "null" : reader.GetString(1));
                        string year = reader.GetString(2);

                        ColumnOutput(gameTitle, publisher_if_available, year);

                        output_lines++;
                        if (output_lines == 10)
                        {
                            Console.WriteLine("\n\nPress any key to continue...\n");
                            Console.ReadKey(true);
                            output_lines = 0;
                        }
                    }
                }
            }
            
            static void ColumnOutput(string in1, string in2, string in3)
            {
                Console.WriteLine($"{in1,-50}" +  $"{in2, -20}" + $"{in3, 10}");                
            }
        }
    }
}
