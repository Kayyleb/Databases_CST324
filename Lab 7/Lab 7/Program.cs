using System;
using Google.Cloud.BigQuery.V2;

/*private string connectionString =
       "server=aura.cset.oit.edu,5433; " +
       "database=kayleb; " +
       "UID=kayleb; " +
       "password=kayleb";
*/

namespace GoogleCloudSamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Code used to establish connection 
            //********************************************************

            //DateTime begin = DateTime.Now;

            //string projectId = "lab-2-474618";
            //var client = BigQueryClient.Create(projectId);

            //DateTime end = DateTime.Now;
            //Double diff = end.Subtract(begin).TotalMilliseconds;
            //Console.WriteLine(diff);


            // Code used to establish connection, run, and print query
            //********************************************************

            //DateTime begin = DateTime.Now;

            //string projectId = "lab-2-474618";
            //var client = BigQueryClient.Create(projectId);
            //string query = @"SELECT PLU ,Variety
            //            FROM `lab-2-474618.commodities.commodities` 
            //            LIMIT 10;";
            //var result = client.ExecuteQuery(query, parameters: null);
            //Console.Write("\nQuery Results:\n------------\n");
            //foreach (var row in result)
            //{
            //    Console.WriteLine($"{row["PLU"]}: {row["Variety"]}");

            //}

            //DateTime end = DateTime.Now;
            //Double diff = end.Subtract(begin).TotalMilliseconds;
            //Console.WriteLine(diff);


            // Code used to establish connection, and single insert
            //********************************************************

            DateTime begin = DateTime.Now;

            string projectId = "lab-2-474618";
            var client = BigQueryClient.Create(projectId);
            string query = @"
                        
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                        INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
                        VALUES(11111);
                          ";
            var result = client.ExecuteQuery(query, parameters: null);
            Console.Write("\nQuery Results:\n------------\n");
            foreach (var row in result)
            {
                Console.WriteLine($"{row["PLU"]}: {row["Variety"]}");

            }

            DateTime end = DateTime.Now;
            Double diff = end.Subtract(begin).TotalMilliseconds;
            Console.WriteLine(diff);


            // Code used to establish connection, and batch insert
            //********************************************************

            //DateTime begin = DateTime.Now;

            //string projectId = "lab-2-474618";
            //var client = BigQueryClient.Create(projectId);
            //string query = @"

            //            INSERT INTO `lab-2-474618.commodities.commodities` (PLU)
            //            VALUES(0001) , (0002) , (0003) , (0004) , (0005),
            //                   (0006) , (0007) , (0008) , (0009) , (0010),
            //                   (0011) , (0012) , (0013) , (0014) , (0015),
            //                   (0016) , (0017) , (0018) , (0019) , (0020) 
            //                   ;
            //                ";
            //var result = client.ExecuteQuery(query, parameters: null);
            //Console.Write("\nQuery Results:\n------------\n");
            //foreach (var row in result)
            //{
            //    Console.WriteLine($"{row["PLU"]}: {row["Variety"]}");

            //}

            //DateTime end = DateTime.Now;
            //Double diff = end.Subtract(begin).TotalMilliseconds;
            //Console.WriteLine(diff);
        }
    }
}