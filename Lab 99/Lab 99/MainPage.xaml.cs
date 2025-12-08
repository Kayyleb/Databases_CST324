using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab_99
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Product> inventory;
        private string connectionString =
            "server=aura.cset.oit.edu,5433; " +
            "database=duffies; " +
            "UID=kayleb; " +
            "password=kayleb";

        public MainPage()
        {
            this.InitializeComponent();

            inventory = GetProducts();
      
            DisplayGrid.ItemsSource = inventory;
        }

        private ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            string query = "SELECT Product_ID, Name, Description, Brand, Price FROM Product";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte id = reader.GetByte(0);                
                        string name = reader.GetString(1);         
                        string description = reader.GetString(2); 
                        string brand = reader.GetString(3);         
                        double price = reader.GetDouble(4);

                        Product item = new Product();
                        item.ProductID = "" + id;
                        item.Name = name;
                        item.Desc = description;
                        item.Brand = brand;
                        item.Price = price;
                        item.ImageSrc = "Assets/" + id + ".jpg";

                        products.Add(item);
                    }
                }
            }
          return products;
        }
    }
}
