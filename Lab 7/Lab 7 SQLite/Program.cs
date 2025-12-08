using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo
{
    class Program
    {

        static void Main(string[] args)
        {
            DateTime begin = DateTime.Now;

            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            InsertData(sqlite_conn);

            DateTime end = DateTime.Now;
            Double diff = end.Subtract(begin).TotalMilliseconds;
            Console.WriteLine(diff);

            //ReadData(sqlite_conn);

        }

        static SQLiteConnection CreateConnection()
        {
            var connString = @"Data Source=C:\Users\kchit\OneDrive\Documents\Databases_CST324\Lab 7\Databases.db;Version=3;";
            var sqlite_conn = new SQLiteConnection(connString);

            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {}

            return sqlite_conn;
        }


        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = @"INSERT INTO commodities (column1, column2, column3) VALUES('Test', 1 , 'Holy')";
            sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = @"SELECT * FROM commodities 
                                       LIMIT 10";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                string myreader2 = sqlite_datareader.GetString(1);
                Console.WriteLine($"{myreader}  {myreader2}");
            }
            conn.Close();
        }
    }
}