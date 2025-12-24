using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TableApp
{
    public partial class TableProducts : Window
    {
        private readonly string connectionString = "Data Source=DanteewPC\\EQS_DB_HOME42;Initial Catalog=CompTech;Integrated Security=True"; // тест с бд

        // public static string connectionString = "Data Source=(LocalDB)\\ИМЯ_ЛОКАЛКИ;AttachDbFilename=|DataDirectory|\\ИМЯ_БД.mdf;Integrated Security=True"; // локальная бд

        public TableProducts()
        {
            InitializeComponent();
            LoadBooks(); // метод загрузки товаров
            SetupUI(); // установка видимости кнопок
        }

        /// <summary>установка видимости кнопок</summary>
        private void SetupUI() 
        {
            txtFullNameAndRole.Text = $"{LoginWindow.CurrentUserFullName} ({LoginWindow.CurrentUserEmployeeRole})";

            // Упрощенная настройка видимости кнопок
            switch (LoginWindow.CurrentUserEmployeeRole)
            {
                case "Гость": ShowOnlyGuestButtons(); break;
                case "Клиент": ShowOnlyClientButtons(); break;
                case "Менеджер": ShowOnlyManagerButtons(); break;
                case "Администратор": ShowOnlyAdminButtons(); break;
            }
        }

        // Visibility.Visible выключает скрытность, НЕ_СКРЫВАЕТ
        // Visibility.Collapsed включает скрытность СКРЫВАЕТ

        /// <summary>кнопка гостя</summary>
        private void ShowOnlyGuestButtons() 
        {
            HideAllButtons();
        }

        /// <summary>кнопки авт клиента</summary>
        private void ShowOnlyClientButtons()
        {
            HideAllButtons();
            btnMakeOrder.Visibility = Visibility.Visible;
            btnProducts.Visibility = Visibility.Visible;
        }

        /// <summary>кнопки менеджера</summary>
        private void ShowOnlyManagerButtons()
        {
            HideAllButtons();
            btnProducts.Visibility = Visibility.Visible;
            btnProducts.Visibility = Visibility.Visible;
        }

        /// <summary>кнопки админа</summary>
        private void ShowOnlyAdminButtons()
        {
            HideAllButtons();
            btnEdit.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            btnOrdersAdmin.Visibility = Visibility.Visible;
            btnProducts.Visibility = Visibility.Visible;
        }

        /// <summary>метод скрытия всех кнопок</summary>
        private void HideAllButtons()
        {
            btnMakeOrder.Visibility = Visibility.Collapsed;
            btnProducts.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
            btnOrdersAdmin.Visibility = Visibility.Collapsed;
        }

        /// <summary>метод загрузки товара</summary>
        private void LoadBooks() // 1.
        {
            try
            {
                //string query = "SELECT * FROM Books"; // 2.
                //List<Book> books = new List<Book>(); // 3.

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //using (SqlCommand command = new SqlCommand(query, connection))
                //{
                //    connection.Open();
                //    SqlDataReader reader = command.ExecuteReader();

                //    while (reader.Read()) // цикл считывания данных с бд
                //    {
                //        books.Add(new Book // 4.
                //        {
                //            BookID = Convert.ToInt32(reader["BookID"]),
                //            Code = reader["Code"].ToString(),
                //            ProductName = reader["ProductName"].ToString(),
                //            Unit = reader["Unit"].ToString(),
                //            Price = Convert.ToDecimal(reader["Price"]),
                //            Supplier = reader["Supplier"].ToString(),
                //            Producer = reader["Producer"].ToString(),
                //            Category = reader["Category"].ToString(),
                //            CurrentDiscount = Convert.ToDecimal(reader["CurrentDiscount"]),
                //            QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]),
                //            Description = reader["Description"].ToString(),
                //            Photo = reader["Photo"].ToString()
                //        });
                //    }
                //}
                //booksGrid.ItemsSource = books; // 5.

                string query = "SELECT * FROM Products"; // 2.
                List<TechnicaBlya> products = new List<TechnicaBlya>(); // 3.

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read()) // цикл считывания данных с бд
                    {
                        products.Add(new TechnicaBlya // 4.
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            ProductName = reader["ProductName"].ToString(),
                            Category = reader["Category"].ToString(),
                            Description = reader["Description"].ToString(),
                            Manufacturer = reader["Manufacturer"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Unit = reader["Unit"].ToString(),
                            QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]),
                            Discount = Convert.ToInt32(reader["Discount"]),
                            Photo = reader["Photo"].ToString()
                        });
                    }
                }
                booksGrid.ItemsSource = products; // 5.
            }
            catch (Exception ex) // ошибка при загрузке
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        /// <summary>устновка цвета строк</summary>
        //private void booksGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    if (e.Row.DataContext is Book book)
        //    {
        //        // Приоритет 1: Если количество = 0, то голубой цвет
        //        if (book.QuantityInStock == 0)
        //        {
        //            e.Row.Background = Brushes.LightBlue;
        //        }
        //        // Приоритет 2: Если скидка > 15%, то темно-зеленый цвет
        //        else if (book.CurrentDiscount > 15)
        //        {
        //            e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));
        //        }
        //        // Иначе белый фон
        //        else
        //        {
        //            e.Row.Background = Brushes.White;
        //        }
        //    }
        //}
        private void booksGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.DataContext is TechnicaBlya products)
            {
                // Приоритет 1: Если количество = 0, то голубой цвет
                if (products.QuantityInStock == 0)
                {
                    e.Row.Background = Brushes.LightBlue;
                }
                // Приоритет 2: Если скидка > 15%, то темно-зеленый цвет
                else if (products.Discount > 15)
                {
                    e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));
                }
                // Иначе белый фон
                else
                {
                    e.Row.Background = Brushes.White;
                }
            }
        }

        /// <summary>метод возвращения в окно аутенфикации</summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        // один метод на 4 кнопки, я че блять далбаеб одну и тоже писать 4 раза??
        private void NoWorking_Click(object sender, RoutedEventArgs e) =>
            MessageBox.Show("Функция в разработке", "Информация");

        /// <summary>метод обновление товара</summary>
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }
    }
}