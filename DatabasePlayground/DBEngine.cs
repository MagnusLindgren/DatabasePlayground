using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace DatabasePlayground
{
    class DBEngine
    {
        static SqliteConnection connection;
        public DBEngine(string databaseName)
        {
            string connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = $@"..\..\..\{databaseName}",
                Mode = SqliteOpenMode.ReadWrite,
            }.ToString();

            connection = new SqliteConnection(connectionString);
            connection.Open();
        }

        static SqliteCommand Command(string input)
        {
            var command = connection.CreateCommand();
            command.CommandText = input;
            return command;
        }

        public static void DatabaseHandler()
        {
            Console.SetWindowSize(185, 40); // To make the database fit in the window

            
            using (var reader = Command($@"SELECT Title, Publishers, Year, Genres, Review_Score, Sales, Console FROM video_games").ExecuteReader())
            {
                // METADATA outputs
                string[] column = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    column[i] = reader.GetName(i);
                }
                ColumnOutput(column[0], column[1], column[2], column[3], column[4], column[5], column[6]);

                string[] column_type = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    column_type[i] = reader.GetDataTypeName(i);
                }
                ColumnOutput(column_type[0], column_type[1], column_type[2], column_type[3], column_type[4], column_type[5], column_type[6]);

                /*
                string column1 = reader.GetName(0);
                string column2 = reader.GetName(1);
                string column3 = reader.GetName(2);
                string column4 = reader.GetName(3);
                string column5 = reader.GetName(4);
                string column6 = reader.GetName(5);
                string column7 = reader.GetName(6);

                string column1_type = reader.GetDataTypeName(0);
                string column2_type = reader.GetDataTypeName(1);
                string column3_type = reader.GetDataTypeName(2);
                string column4_type = reader.GetDataTypeName(3);
                string column5_type = reader.GetDataTypeName(4);
                string column6_type = reader.GetDataTypeName(5);
                string column7_type = reader.GetDataTypeName(6);

                ColumnOutput(column1, column2, column3, column4, column5, column6, column7);
                ColumnOutput(column1_type, column2_type, column3_type, column4_type, column5_type, column6_type, column7_type);
                */

                int output_lines = 0;
                while (reader.Read())
                {
                    string[] row = new string[reader.FieldCount]; 
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = (reader.IsDBNull(i) ? "no info" : reader.GetString(i));
                    }

                    ColumnOutput(row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
                    /*
                    // DB outputs                        
                    string gameTitle = reader.GetString(0);
                    string publisher = (reader.IsDBNull(1) ? "no info" : reader.GetString(1));
                    string year = (reader.IsDBNull(2) ? "no info" : reader.GetString(2));
                    string genres = (reader.IsDBNull(3) ? "no info" : reader.GetString(3));
                    string review = (reader.IsDBNull(4) ? "no info" : reader.GetString(4));
                    string sales = (reader.IsDBNull(5) ? "no info" : reader.GetString(5));
                    string console = (reader.IsDBNull(6) ? "no info" : reader.GetString(6));
                        
                    ColumnOutput(gameTitle, publisher, year, genres, review, sales, console);
                    */
                    output_lines++;
                    if (output_lines == 10)
                    {
                        Console.WriteLine("\nPress any key to continue...\n");
                        Console.ReadKey(true);

                        output_lines = 0;
                    }

                }
            }

            static void ColumnOutput(string in1, string in2, string in3, string in4, string in5, string in6, string in7)
            {
                Console.WriteLine($"{in1,-50}" + "| " + $"{in2,-20}" + "| " + $"{in3,10}" + "| " + $"{in4,-35}" + "| " + $"{in5,12}" + "| " + $"{in6,10}" + "| " + $"{in7,-20}");
            }

            static void DBInsert(string input, string connectionString)
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText =
                        $@"
                            {input}
                        ";
                }
            }
        }

    }
}
