using System.Text;
using System.Web;
using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using ToDoList.Services;
using System.Net.Http.Headers;
using Microsoft.OpenApi.Expressions;


namespace ToDoList.Pages
{
    public class TasksModel : PageModel
    {
        [BindProperty]
        public string RequestMethod { get; set; }
        [BindProperty]
        public string Access_token { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Desc { get; set; }
        [BindProperty]
        public List<Tasks> ListTasks { get; set; }
        [BindProperty]
        public string BaseUrl { get; set; } = "http://demo2.z-bit.ee/tasks";

        public async Task OnGet(string access)
        {
            Access_token = access;
            var list = await ApiService.GetAllTasks(Access_token);
            ListTasks = await TasksService.AddToListTasksStat(list);

        }

        public async Task<IActionResult> OnPost(int id, string access)
        {

            string responseContent = "[]";
            try
            {
                Uri baseURL = new Uri(BaseUrl);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Access_token);
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Any parameters? Get value, and then add to the client 
                string key = HttpUtility.ParseQueryString(baseURL.Query).Get("key");
                if (RequestMethod.Equals("DELETE"))
                {
                    Console.WriteLine(id);
                    Console.WriteLine(access);
                    var response = await ApiService.DeleteTask(Access_token, id);
                    return RedirectToPagePermanent("Tasks", new { access = Access_token });
                }
                if (key != "")
                {
                    client.DefaultRequestHeaders.Add("api-key", key);
                }
                if (RequestMethod.Equals("GET"))
                {

                    return RedirectToPage("ResponseTasks", new { access = Access_token });

                }
                else if (RequestMethod.Equals("POST"))
                {

                    var results = await ApiService.PostTask(BaseUrl, new { title = Title, desc = Desc }, Access_token);
                    responseContent = results.ToJson();
                    return RedirectToPage("Tasks", new { access = Access_token });
                }

                return RedirectToPage("ResponseTasks", new { result = responseContent });

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