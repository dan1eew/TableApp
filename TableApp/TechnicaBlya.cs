
using System;
using System.Windows.Media.Imaging;

namespace TableApp
{
    /// <summary>Класс сущности TechnicaBlya</summary>
    public class TechnicaBlya // 1.
    {
        public int ProductId { get; set; } // 1.
        public string ProductName { get; set; } // 2.
        public string Category { get; set; } // 3.
        public string Description { get; set; } // 4.
        public string Manufacturer { get; set; } // 5.
        public decimal Price { get; set; } // 6.
        public string Unit { get; set; } // 7.
        public int QuantityInStock { get; set; } // 8.
        public int Discount { get; set; } // 9.
        public string Photo { get; set; } // 10.
        public BitmapImage PhotoPath // 11.
        {
            get { return new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}Images2/{Photo}")); }
        }
        public decimal DiscountedPrice // 15.
        {
            get
            {
                if (Discount > 0)
                {
                    decimal discounted = Price * (1 - Discount / 100);
                    return Math.Round(discounted, 2);
                }
                return Price;
            }
        }

        // Флаг наличия скидки
        public bool HasDiscount => Discount > 0; // 16.
    }
    
}
