using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class ViewReviews : Page
    {
        private string connectionString =
        "server=aura.cset.oit.edu,5433; " +
        "database=kayleb; " +
        "UID=kayleb; " +
        "password=kayleb";
        public ViewReviews()
        {
            this.InitializeComponent();
        }
        private void ViewReviewData(object sender, RoutedEventArgs e)
        {
            const string query = @"
                SELECT RatingControl, Title, Review, Name 
                FROM reviews
                ORDER BY RatingControl DESC"; 

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

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {   // review panel to differentiate the different reviews
                                    StackPanel reviewPanel = new StackPanel();
                                    reviewPanel.Orientation = Orientation.Vertical;
                                    reviewPanel.Background = new SolidColorBrush(Colors.SkyBlue);
                                        //SolidColorBrush(Windows.UI.Colors.LightGray);
                                    reviewPanel.Margin = new Thickness(10);
                                    reviewPanel.Padding = new Thickness(10);
                                    
                                    // text block just for the name
                                    TextBlock nameBlock = new TextBlock();
                                    nameBlock.Text = "Username: " + reader.GetString(3);

                                    // text block for the rating out of 5 stars
                                    double ratingValue = reader.GetDouble(0);
                                    var ratingBlock = new RatingControl();
                                    ratingBlock.Value = ratingValue;
                                    TextBlock ratingBlockNum = new TextBlock();
                                    ratingBlockNum.Text = "Rating: " + ratingValue;

                                    // text block for the title of the review
                                    TextBlock titleBlock = new TextBlock();
                                    titleBlock.Text = "Title: " + reader.GetString(1);

                                    // text block for the actual review, wraps around
                                    TextBlock reviewBlock = new TextBlock();
                                    reviewBlock.Text = "Review: " + reader.GetString(2);
                                    reviewBlock.TextWrapping = TextWrapping.Wrap;

                                    //TextBlock timeBlock = new TextBlock();
                                    //timeBlock.Text = "Time: " + reader.GetDateTime(4).ToString("f");

                                    // all text blocks will go onto the review pannel
                                    reviewPanel.Children.Add(ratingBlock);
                                    reviewPanel.Children.Add(nameBlock);
                                    reviewPanel.Children.Add(titleBlock);
                                    reviewPanel.Children.Add(ratingBlockNum);
                                    reviewPanel.Children.Add(reviewBlock);

                                    //reviewPanel.Children.Add(timeBlock);

                                    yourPagesMainPanel.Children.Add(reviewPanel);
                                }
                            }
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