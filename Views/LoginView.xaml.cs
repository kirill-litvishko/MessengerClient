using MessengerClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessengerClient.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>


    public partial class LoginView : Page
    {
        private readonly ApiService _apiService;

        public LoginView()
        {
            InitializeComponent();
            _apiService = new ApiService(); // Инициализируем API сервис
        }

        private async void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text; // Предполагается наличие UsernameTextBox
            var password = PasswordBox.Password; // Предполагается наличие PasswordBox

            try
            {
                // Вызов API для авторизации
                var token = await _apiService.LoginAsync(username, password);

                // Сохраняем токен (можно использовать SecureStorage, если нужно)
                App.Token = token;

                // Переходим на экран чатов
                NavigationService.Navigate(new ChatsView());
            }
            catch (HttpRequestException ex)
            {
                // Покажите ошибку пользователю
                MessageBox.Show($"Login failed: {ex.Message}");
            }
        }
    }
}
