using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace ToDoList.Pages
{
    public class ResponseTasksModel : PageModel
    {
        [BindProperty]
        public string ResponseBody { get; set; }
        [BindProperty]
        public string Access_token { get; set; }
        [BindProperty]
        public string Id { get; set; }
        [BindProperty]
        public bool Marked_as_done { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Desc { get; set; }
        [BindProperty]
        public List<Tasks>? Tasks { get; set; }
        private readonly TasksService _taskService;
        public ResponseTasksModel(TasksService taskService)
        {
            _taskService = taskService;
        }
        public async Task OnGetAsync(string access)
        {
            var Tasks1 = new List<Tasks>();
            Access_token = access;
            var jObject1 = await ApiService.GetAllTasks(Access_token);
            
            if (jObject1.Count() > 0)
            {
                Tasks1 = await _taskService.AddToListTasks(jObject1);
                Tasks = Tasks1.ToList();  
            }
            
            

        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var response = await ApiService.DeleteTask(Access_token, id);
            if (Tasks.Count() == 0)
            {
                return RedirectToPagePermanent("Tasks", new { access = Access_token });
            }
            return RedirectToPagePermanent("ResponseTasks", new { access = Access_token });
        }
    }
}