using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Lab_99
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Product> inventory;
        private int ticketPhase = 0;

        private byte entreeId = 0;
        private byte sideId = 0;

        private string connectionString =
            "server=aura.cset.oit.edu,5433; " +
            "database=kayleb; " +
            "UID=kayleb; " +
            "password=kayleb";

        public MainPage()
        {
            this.InitializeComponent();

            entreeId = 0;
            sideId = 0;
            ticketPhase = 0;

            inventory = GetProducts();
            ShowProduct();

            StatusText.Text = "Pick an entree.";
        }

        private ObservableCollection<Product> GetProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            string query = "SELECT ProductID, Name, Description, Price, InventoryCount FROM Product";

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
                        double price = reader.GetDouble(3);
                        byte inv = reader.GetByte(4);

                        Product item = new Product();
                        item.ProductID = id.ToString();
                        item.Name = name;
                        item.Desc = description;
                        item.Price = price;
                        item.inventoryCount = inv;   // lowercase on purpose
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
                productsToShow = inventory.Where(p =>
                    p.Desc.IndexOf("sandwich", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    p.Desc.IndexOf("wrap", StringComparison.OrdinalIgnoreCase) >= 0);

                StatusText.Text = "Pick an entree.";
            }
            else
            {
                productsToShow = inventory;
                StatusText.Text = "Pick a side.";
            }

            DisplayGrid.ItemsSource = new ObservableCollection<Product>(productsToShow);
            DisplayGrid.SelectedItem = null;
        }

        private void SelectProduct(object sender, SelectionChangedEventArgs e)
        {
            var product = DisplayGrid.SelectedItem as Product;
            if (product == null) return;

            Image image = new Image
            {
                Width = 150,
                Height = 150,
                Margin = new Thickness(4)
            };

            BitmapImage bitmap = new BitmapImage();
            bitmap.UriSource = new Uri(BaseUri, product.ImageSrc);
            image.Source = bitmap;

            TextBlock text = new TextBlock
            {
                Text = $"{product.Name}\n{product.DisplayPrice}\nIn stock: {product.inventoryCount}",
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(Colors.Black)
            };

            if (ticketPhase == 0)
            {
                Entree.Children.Clear();
                Entree.Children.Add(image);
                Entree.Children.Add(text);

                entreeId = Convert.ToByte(product.ProductID);
                ticketPhase = 1;

                StatusText.Text = $"Entree selected (ID={entreeId}). Now pick a side.";
            }
            else
            {
                Side.Children.Clear();
                Side.Children.Add(image);
                Side.Children.Add(text);

                sideId = Convert.ToByte(product.ProductID);
                ticketPhase = 0;

                StatusText.Text = $"Side selected (ID={sideId}). Click Buy.";
            }

            ShowProduct();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(StudentIdBox.Text, out int sid))
            {
                StatusText.Text = "Enter a valid Student ID.";
                return;
            }

            if (entreeId == 0 || sideId == 0)
            {
                StatusText.Text = $"Missing selection. EntreeId={entreeId}, SideId={sideId}";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("purchaseTicket", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EntreeId", SqlDbType.TinyInt).Value = entreeId;
                    cmd.Parameters.Add("@SideId", SqlDbType.TinyInt).Value = sideId;
                    cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = sid;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                StatusText.Text = $"Purchased! (EntreeId={entreeId}, SideId={sideId})";

                inventory = GetProducts();
                ShowProduct();

                Entree.Children.Clear();
                Side.Children.Clear();
                entreeId = 0;
                sideId = 0;
                ticketPhase = 0;
            }
            catch (SqlException ex)
            {
                StatusText.Text = ex.Message;
            }
        }
    }
}
