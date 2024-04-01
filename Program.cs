using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using ToDoList.Services;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace ToDoList
{
    class Program : System.Net.Http.HttpMessageInvoker
    {
        static readonly HttpClient client = new HttpClient();

        public Program(HttpMessageHandler handler) : base(handler)
        {
        }

        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            builder.Services.AddRazorPages();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<TasksService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();

            // var data = new UserService();
            // data.Serializing();
            // System.Threading.Tasks.Task.Delay(2000).Wait();


        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args);

    }
    
}

