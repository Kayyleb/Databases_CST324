using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SQLServerTest
{
    class MySqlConnector
    {
        private static string connectionString =
        "server=aura.cset.oit.edu,5433; " +
        "database=kayleb; " +
        "UID=kayleb; " +
        "password=kayleb";
        static void Main(string[] args)
        {
            string[] Fruits = { "Apple" , "Banana" };
            int num = 50;

            string query = @"
                        SELECT Plu ,Category, Commodity 
                        FROM commodities 
                        WHERE Commodity = @Apple";

            try
            {
                DateTime begin = DateTime.Now;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    for (int i = 0; i < num; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Apple", "Apple");
                            cmd.ExecuteReader().Close();
                        }
                    }
                }
                DateTime end = DateTime.Now;

                Double diff = end.Subtract(begin).TotalMilliseconds;

                Console.WriteLine(diff);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}