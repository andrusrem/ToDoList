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
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public string RequestMethod { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Access_token { get; set; }

        
        public async Task OnGetAsync(int id, string token)
        {
            var url = "http://demo2.z-bit.ee/users/get-token";
            var userInfo = await ApiService.GetUserById(id, token);
            // var tokenInfo = await ApiService.Login(url, )
            
            Name = userInfo.Firstname;
            Username = userInfo.Username;
            Access_token = token;
            Id = userInfo.Id;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(Access_token);
            Console.WriteLine(Id);
            
            var body = new {newPassword = Password};
            var result  = await ApiService.ChangePassword(body, Access_token, Id);
            if(result != null)
            {
                return RedirectToPage("Login");
            }
            return RedirectToPagePermanent("Profile", new {id = Id, token = Access_token});
        }
    }
}

