using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MessengerClient.Models;

namespace MessengerClient.Services
{
    internal class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7100/api/")
            };
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("Users/login", new { username, password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result?.Token;
            }
            else
            {
                throw new HttpRequestException($"Login failed: {response.ReasonPhrase}");
            }
        }

        public async Task<List<Chat>> GetChatsAsync()
        {
            // Устанавливаем JWT токен в заголовок Authorization
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AppState.Token);

            // Отправляем запрос на сервер
            var response = await _httpClient.GetAsync("Chats");

            if (response.IsSuccessStatusCode)
            {
                // Десериализуем список чатов
                return await response.Content.ReadFromJsonAsync<List<Chat>>();
            }
            else
            {
                throw new HttpRequestException($"Failed to load chats: {response.ReasonPhrase}");
            }
        }

    }
}
