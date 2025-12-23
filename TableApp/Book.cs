using System;
using System.Windows.Media.Imaging;

namespace TableApp
{
    /// <summary>Класс сущности book</summary>
    public class Book // 1.
    {
        public int BookID { get; set; } // 1.
        public string Code { get; set; } // 2.
        public string ProductName { get; set; } // 3.
        public string Unit { get; set; } // 4.
        public decimal Price { get; set; } // 5.
        public string Supplier { get; set; } // 6.
        public string Producer { get; set; } // 7.
        public string Category { get; set; } // 8.
        public decimal CurrentDiscount { get; set; } // 9.
        public int QuantityInStock { get; set; } // 10.
        public string Description { get; set; } // 11.
        public string Photo { get; set; } // 12.
        public BitmapImage PhotoPath // 13.
        {
            get
            {
                try
                {
                    return new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}Images/{Photo}")); // 14.
                }
                catch
                {
                    return null;
                }
            }
        }
        public decimal DiscountedPrice // 15.
        {
            get
            {
                if (CurrentDiscount > 0)
                {
                    decimal discounted = Price * (1 - CurrentDiscount / 100);
                    return Math.Round(discounted, 2);
                }
                return Price;
            }
        }

        // Флаг наличия скидки
        public bool HasDiscount => CurrentDiscount > 0; // 16.
    }
}