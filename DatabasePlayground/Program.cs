using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace DatabasePlayground
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        SELECT Title, Publishers                            
                        FROM video_games
                    ";

                using (var reader = command.ExecuteReader())
                {
                    int output_lines = 0;
                    string column1 = reader.GetName(0);
                    string column2 = reader.GetName(1);
                    string column1_type = reader.GetDataTypeName(0);
                    string column2_type = reader.GetDataTypeName(1);
                    Console.Write(column1 + " : " + column1_type);
                    Console.CursorLeft = Console.WindowWidth / 2;
                    Console.WriteLine(column2 + " : " + column2_type);
                    Console.WriteLine("----------------------------------------------------");
                    while (reader.Read())
                    {                       
                        string gameTitle = reader.GetString(0);
                        string publisher_if_available = (reader.IsDBNull(1) ? "null" : reader.GetString(1));                       
                        Console.Write(gameTitle);
                        Console.CursorLeft = Console.WindowWidth / 2;
                        Console.WriteLine(publisher_if_available);
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
        }
    }
}
