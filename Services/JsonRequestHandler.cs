using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ToDoList.Services
{
    public static class JsonRequestHandler
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<T> SendRequest<T>(string url, HttpMethod method, object data = null, string token = null)
        {
            
            var request = new HttpRequestMessage(method, url);
            
            
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            if (!string.IsNullOrEmpty(token))
            {
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (data != null)
            {
                request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }
            
            var response = await _httpClient.SendAsync(request);
            Console.WriteLine(response.ToString());
            
            Console.WriteLine("SendRequest");

            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseData);
            
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseData);
            
            
            
        }
    }
}