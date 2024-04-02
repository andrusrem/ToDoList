using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ToDoList.Models;
using NuGet.Protocol;
namespace ToDoList.Services
{
    public class ApiService
    {

        public static async Task<List<Tasks>?> GetAllTasks(string token)
        {
            Console.WriteLine("GetAllTasks");
            var tasks = await JsonRequestHandler.SendRequest<List<Tasks>?>("http://demo2.z-bit.ee/tasks", HttpMethod.Get, null, token);
            Console.WriteLine(tasks);
            return tasks;
        }

        public static async Task<User> GetUserById(int id, string token)
        {
            Console.WriteLine("GetUserById");
            var user = await JsonRequestHandler.SendRequest<User>($"http://demo2.z-bit.ee/users/{id}", HttpMethod.Get, null, token);
            return user;
        }
        public static async Task<User> Login(string url, object body)
        {
            Console.WriteLine("Login");
            var user = await JsonRequestHandler.SendRequest<User>(url, HttpMethod.Post, body, null);
            return user;
        }
        public static async Task<User> Register(object body)
        {
            Console.WriteLine("Register");
            var user = await JsonRequestHandler.SendRequest<User>("http://demo2.z-bit.ee/users", HttpMethod.Post, body, null);
            return user;
        }
        public static async Task<User> ChangePassword(object body, string token, int id)
        {
            Console.WriteLine("ChangePassword");
            var user = await JsonRequestHandler.SendRequest<User>($"http://demo2.z-bit.ee/users/{id}", HttpMethod.Put, body, token);
            return user;
        }
        public static async Task<string?> DeleteTask(string token, int id)
        {
            Console.WriteLine("DeleteTask");
            string Id = id.ToString();
            var url = $"http://demo2.z-bit.ee/tasks/{Id}";
            var result = await JsonRequestHandler.SendRequest<string?>(url, HttpMethod.Delete, null, token);
            return result;
        }

        public static async Task<T?> GetString<T>(string access_token, string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(data);
            // return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string?> Post(string url, object data)
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(data);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url.ToString(), stringContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();


            }
            return null;

        }

        public static async Task<string> PostTask(string url, object data, string access_token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonConvert.SerializeObject(data);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await client.PostAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync());
                return await response.Content.ReadAsStringAsync();
            }
            Console.WriteLine(response.StatusCode);
            return null;

        }
        public static async Task<object?> PostJson(string url, object data, string responseContent)
        {
            var result = await Post(url, data);
            if (result != null)
            {
                return JsonConvert.DeserializeObject(result);
            }
            return null;
        }
    }
}