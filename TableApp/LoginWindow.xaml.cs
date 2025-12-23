using System;
using System.Data.SqlClient;
using System.Windows;

namespace TableApp
{
    public partial class LoginWindow : Window
    {
        private readonly string connectionString = "Data Source=DanteewPC\\EQS_DB_HOME42;Initial Catalog=BookStoreDB17x;Integrated Security=True"; // тест с бд

        // public static string connectionString = "Data Source=(LocalDB)\\ИМЯ_ЛОКАЛКИ;AttachDbFilename=|DataDirectory|\\ИМЯ_БД.mdf;Integrated Security=True"; // локальная бд

        // Обьявление строк, нужны для того что бы присвоить значение считанные с бд
        public static string CurrentUserFullName { get; private set; } // имя
        public static string CurrentUserEmployeeRole { get; private set; } // роль

        public LoginWindow()
        {
            InitializeComponent();
            txtLogin.Focus();
        }

        /// <summary>метод входа по логину и паролю</summary>
        private void Login_Click(object sender, RoutedEventArgs e) // нажатие кнопки
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))  // проверка на пустоту для строки ввода логина
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Password)) // проверка на пустоту для строки ввода пароля
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AuthenticateUser(txtLogin.Text, txtPassword.Password); // после успешно проверки, переходим в метод Аутентификации
        }

        /// <summary>метод Аутентификации</summary>
        private void AuthenticateUser(string login, string password) // тот самый метод Аутентификации
        {
            try 
            {
                string query = "SELECT FullName, EmployeeRole FROM Users WHERE Login = @login AND Password = @password"; // создает запрос к базе данных

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) // в том случае если найдет пользователя с введенным логином и паролем, он считывает роль и имя
                    {
                        CurrentUserFullName = reader["FullName"].ToString();
                        CurrentUserEmployeeRole = reader["EmployeeRole"].ToString();
                        reader.Close();

                        var mainWindow = new TableProducts(); // переход в главное окно
                        mainWindow.Show();
                        this.Close();
                    }
                    else // если не нашел пользователя с таким логином и паролем
                    {
                        MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex) // если не смог подкючиться к бд
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>метод входа как гость</summary>
        private void GuestLogin_Click(object sender, RoutedEventArgs e) 
        {
            CurrentUserFullName = "Гость";
            CurrentUserEmployeeRole = "Гость";

            var mainWindow = new TableProducts(); // переход в главное окно
            mainWindow.Show();
            this.Close();
        }
    }
}