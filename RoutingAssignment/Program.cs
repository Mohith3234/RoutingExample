using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Eventing.Reader;

namespace RoutingAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                var dict = new Dictionary<int, string>
                    {
                        {1,"united states" },
                        {2,"canada" },
                        {3,"united kingdom"},
                        {4,"india" },
                        {5,"japan" }
                    };
                endpoints.Map("countries", async (context) =>
                {
                   

                    foreach (var key in dict)
                    {
                        await context.Response.WriteAsync($"{key.Key}, {key.Value}\n");
                       
                       
                    }
                }
                );

                endpoints.Map("countries/{countryId:int:range(1,100)}", async (context) =>
                {

                    
                        int countryId = Convert.ToInt32(context.Request.RouteValues["countryId"]);

                        if(dict.ContainsKey(countryId))
                    {
                        await context.Response.WriteAsync($"{dict[countryId]}");
                    }
                    else
                    {
                        await context.Response.WriteAsync("key not present");
                        context.Response.StatusCode = 400;
                    }


                });
            });

            app.Run(async(HttpContext context)=>
            {
                await context.Response.WriteAsync($"Request received at {context.Request.Path}");
            }
            );
            app.Run();
            Console.ReadKey();
        }
    }
}
