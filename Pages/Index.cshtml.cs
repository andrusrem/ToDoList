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
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Lastname { get; set; }
        [BindProperty]
        public string Firstname { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }
        [BindProperty]
        public string BaseUrl { get; set; } = "http://demo2.z-bit.ee/users/get-token";

        public void OnGet(string message)
        {
            Message = message;
        }

        public async Task<IActionResult> OnPost()
        {

            string responseContent = "[]";
            try
            {
                var body = new { username = Username, firstname = Firstname, lastname = Lastname, newPassword = NewPassword };


                var results = await ApiService.Register(body);
                var message = "Something went wrong, please try again.";
                if (results != null)
                {
                    return RedirectToPage("Login");
                }
                return RedirectToPagePermanent("Index", new { message = message });

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
