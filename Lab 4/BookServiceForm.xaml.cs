using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    public sealed partial class BookServiceForm : Page
    {
        private string connectionString = 
            "server=aura.cset.oit.edu,5433; " +
            "database=kayleb; " +
            "UID=kayleb; " +
            "password=kayleb";
        public BookServiceForm()
        {
            this.InitializeComponent();
        }

        private void SubmitData(object sender, RoutedEventArgs e)
        {
            string nameVal = Name.Text;
            string emailVal = Email.Text;
            string phoneVal = Phone.Text;
            string streetVal = Street.Text;
            string cityVal = City.Text;
            string zipVal = ZipCode.Text;
            string stateVal = ((ComboBoxItem)State.SelectedItem).Content.ToString();
            DateTime serviceTimeVal = DateTime.Parse(ServiceCalendarPicker.Date.Value.ToString("yyyy-MM-dd") + " " + ServiceTimePicker.Time);

            String query = @"
            INSERT INTO requests
                (Name, Email, Phone, Street, City, State, Zip, ServiceDateTime)
            VALUES
                (@Name, @Email, @Phone, @Street, @City, @State, @Zip, @ServiceDateTime) ";
            
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

                            cmd.Parameters.AddWithValue("@Name", nameVal);
                            cmd.Parameters.AddWithValue("@Email", emailVal);
                            cmd.Parameters.AddWithValue("@Phone", phoneVal);
                            cmd.Parameters.AddWithValue("@Street", streetVal);
                            cmd.Parameters.AddWithValue("@City", cityVal);
                            cmd.Parameters.AddWithValue("@State", stateVal);
                            cmd.Parameters.AddWithValue("@Zip", zipVal);
                            cmd.Parameters.AddWithValue("@ServiceDateTime", serviceTimeVal);

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
