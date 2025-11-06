using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Reviews1 : Page
    {
        private string connectionString =
            "server=aura.cset.oit.edu,5433; " +
            "database=kayleb; " +
            "UID=kayleb; " +
            "password=kayleb";
        public Reviews1()
        {
            this.InitializeComponent();
        }

        private void SubmitReviewData(object sender, RoutedEventArgs e)
        {
            double ratingVal = Rating.Value;
            string titleVal = Title.Text;
            string reviewVal = Review.Text;
            string nameVal = Name.Text;
            
            String query = @"
            INSERT INTO reviews
                (RatingControl, Title, Review, Name)
            VALUES
                (@Rating, @Title, @Review, @Name) 
                            ";

            Console.WriteLine(query);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = query;

                            cmd.Parameters.AddWithValue("@Rating", ratingVal);
                            cmd.Parameters.AddWithValue("@Title", titleVal);
                            cmd.Parameters.AddWithValue("@Review", reviewVal);
                            cmd.Parameters.AddWithValue("@Name", nameVal);

                            String newQ = cmd.ToString();
                            Console.WriteLine(newQ);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
        }
    }
}
