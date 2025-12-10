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
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
        int ticketPhase = 0;
        private string connectionString =
            "server=aura.cset.oit.edu,5433; " +
            "database=duffies; " +
            "UID=kayleb; " +
            "password=kayleb";

        public MainPage()
        {
            this.InitializeComponent();

            inventory = GetProducts();

            ticketPhase = 0;
            ShowProduct();
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

        private void ShowProduct()
        {
            IEnumerable<Product> productsToShow;

            if (ticketPhase == 0)
            {
                // Entree phase: only show sandwiches/wraps (adjust as your teacher wants)
                productsToShow = inventory.Where(p =>
                    p.Desc.IndexOf("sandwich", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Desc.IndexOf("wrap", StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else
            {
                // Side phase: show everything
                productsToShow = inventory;
            }

            DisplayGrid.ItemsSource =
                new ObservableCollection<Product>(productsToShow);

            // Clear any previous selection
            DisplayGrid.SelectedItem = null;
        }

        private void SelectProduct(object sender, SelectionChangedEventArgs e)
        {
            var product = DisplayGrid.SelectedItem as Product;
            if (product == null) return;

            // Build image
            Image image = new Image
            {
                Width = 150,
                Height = 150,
                Margin = new Thickness(4)
            };

            BitmapImage bitmap = new BitmapImage();
            bitmap.UriSource = new Uri(BaseUri, product.ImageSrc);
            image.Source = bitmap;

            // Text under image (name + price)
            TextBlock text = new TextBlock
            {
                Text = $"{product.Name}\n{product.DisplayPrice}",
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Colors.Black)
            };

            if (ticketPhase == 0)
            {
                // User just picked an entree
                Entree.Children.Clear();
                Entree.Children.Add(image);
                Entree.Children.Add(text);

                ticketPhase = 1;     // now pick a side
            }
            else
            {
                // User just picked a side
                Side.Children.Clear();
                Side.Children.Add(image);
                Side.Children.Add(text);

                ticketPhase = 0;     // go back to entree for next ticket
            }

            // Refresh what products are shown for the new phase
            ShowProduct();
        }


    }
}
