using ToDoList.Models;

namespace ToDoList.Services
{
    public class TasksService
    {
        public List<Tasks> Tasks { get; set; }

        public static async Task<List<Tasks>> AddToListTasksStat(List<Tasks> FromList)
        {
            var list = new List<Tasks>();
            for (int i = 0; i < FromList.Count(); i++)
            {
                var new_task = new Tasks();
                new_task.Id = FromList[i].Id;
                new_task.Title = FromList[i].Title;
                new_task.Desc = FromList[i].Desc;
                new_task.Marked_as_done = FromList[i].Marked_as_done;
                new_task.Created_at = FromList[i].Created_at;
                list.Add(new_task);
                
            }
            Console.WriteLine("AddList");
            return list;
        }
        public async Task<List<Tasks>> AddToListTasks(List<Tasks> FromList)
        {
            var list = new List<Tasks>();
            for (int i = 0; i < FromList.Count(); i++)
            {
                var new_task = new Tasks();
                new_task.Id = FromList[i].Id;
                new_task.Title = FromList[i].Title;
                new_task.Desc = FromList[i].Desc;
                new_task.Marked_as_done = FromList[i].Marked_as_done;
                new_task.Created_at = FromList[i].Created_at;
                list.Add(new_task);
                
            }
            Console.WriteLine("AddList");
            return list;
        }
    }
}