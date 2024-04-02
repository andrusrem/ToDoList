using System.Text;
using System.Web;
using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using ToDoList.Services;


namespace ToDoList.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string RequestMethod { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string BaseUrl { get; set; } = "http://demo2.z-bit.ee/users/get-token";
        
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {

            string responseContent = "[]";
            try
            {
                BaseUrl = "http://demo2.z-bit.ee/users/get-token";
                Uri baseURL = new Uri(BaseUrl);

                HttpClient client = new HttpClient();
                var body = new {username=Username, password=Password};
                // Any parameters? Get value, and then add to the client 
                string key = HttpUtility.ParseQueryString(baseURL.Query).Get("key");
                if (key != "")
                {
                    client.DefaultRequestHeaders.Add("api-key", key);
                    Console.WriteLine("GetKey");
                }
                if (RequestMethod.Equals("POST"))
                {
                    var results = await ApiService.Login(BaseUrl, body);
                    responseContent = results.ToJToken().ElementAt(4).Last().ToString();
                    return RedirectToPage("Tasks", new { access = responseContent});
                    
                }
                else if (RequestMethod.Equals("GET"))
                {
                    var results = await ApiService.Login(BaseUrl, body);
                    var id = results.Id;
                    var token = results.Access_token;
                    return RedirectToPage("Profile", new {id = id, token = token});
                }

                return RedirectToPage("Response", new { result =  responseContent});

            }
            catch (ArgumentNullException uex)
            {
                return RedirectToPage("Error", new { msg = uex.Message + " | URL missing or invalid." });
            }
            catch (JsonReaderException jex)
            {
                return RedirectToPage("Error", new { msg = jex.Message + " | Json data could not be read." });
            }
            catch (Exception ex)
            {
                return RedirectToPage("Error", new { msg = ex.Message + " | Are you missing some Json keys and values? Please check your Json data." });
            }
        }
    }
}
