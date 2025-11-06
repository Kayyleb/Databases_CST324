using System;
using Google.Cloud.BigQuery.V2;

namespace GoogleCloudSamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string projectId = "lab-2-474618";
            var client = BigQueryClient.Create(projectId);
            string query = @"SELECT PLU ,Variety
                        FROM `lab-2-474618.commodities.commodities` 
                        LIMIT 10;";
            var result = client.ExecuteQuery(query, parameters: null);
            Console.Write("\nQuery Results:\n------------\n");
            foreach (var row in result)
            {
                Console.WriteLine($"{row["PLU"]}: {row["Variety"]}");

            }
        }
    }
}