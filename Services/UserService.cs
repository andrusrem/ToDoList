using System.Text.Json;
using System.Net;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class UserService
    {
        static readonly HttpClient client = new HttpClient();
        
        public UserService()
        {
            
        }

        public async Task<User?> GetData()
        {

            using var client = new HttpClient();
            
            return await client.GetFromJsonAsync<User?>("https://demo2.z-bit.ee/users");


            
        }
        public async void OnGetAsync()
        {
            string apiUrl = "http://demo2.z-bit.ee/tasks";
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    dynamic responseObj = JsonSerializer.Deserialize<dynamic>(jsonResponse, options);

                    if (responseObj.ContentsKey("error"))
                    {
                        string errorMessage = responseObj["error"]["message"];
                        Console.WriteLine($"API Error: {errorMessage}");
                    }
                    else
                    {
                        var users = JsonSerializer.Deserialize<List<User>>(jsonResponse, options);
                        Console.WriteLine("API Success");
                    }
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("API returned 404: Not Found");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine("API returned 400: Bad Request");
                    }
                    else
                    {
                        Console.WriteLine($"API returned status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}