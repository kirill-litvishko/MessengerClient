using MessengerClient.Models;
using MessengerClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ChatsView.xaml
    /// </summary>
    public partial class ChatsView : Page
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Chat> Chats { get; set; }

        public ChatsView()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Chats = new ObservableCollection<Chat>();
            DataContext = this;

            LoadChats();
        }

        private async void LoadChats()
        {
            try
            {
                var chats = await _apiService.GetChatsAsync();
                foreach (var chat in chats)
                {
                    Chats.Add(chat);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Failed to load chats: {ex.Message}");
            }
        }
    }
}
