
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;




namespace trialSetuApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                string authHeader = context.Request.Headers["Authorization"];
                string[] parts = authHeader.Split(" ");
                string token = parts[1];

                context.Response.ContentType = "application/json";

                JwtAuthManager jwtAuthManager = new JwtAuthManager();
                bool isValid = jwtAuthManager.ValidateToken(token);

                if (isValid)
                {
                    await context.Response.WriteAsync("{\"Hello\": \"World\"}");
                }
                else
                {
                    string newToken = jwtAuthManager.GenerateJWTToken();
                    Console.WriteLine(newToken);
                    await context.Response.WriteAsync("{\"Hello\": \"Error\"}");
                }
            });
        }
    }
}
