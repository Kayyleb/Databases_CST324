using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_99
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ImageSrc { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string DisplayPrice => $"{Price:C}";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
