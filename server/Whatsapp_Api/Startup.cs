//using Microsoft.EntityFrameworkCore;

using System.Reflection;
using Microsoft.OpenApi.Models;
using Infraestructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Whatsapp_Api.Core.Models;

using Microsoft.AspNetCore.Identity;
using Whatsapp_Api.Infraestructure.Context;
using Whatsapp_Api.Infraestructure;
using Microsoft.AspNetCore.Builder;



namespace Whatsapp_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            /*
            AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            }); ;
            */
            //services.AddTransient<GlobalHandlerException>();



            services.AddAuthentication();
            services.AddAuthorization();
            services.AddSignalR();
            services.AddAppContext(Configuration);
            services.AddApplicationServices();
            services.AddScoped<MessageHub>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
               };
           });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi Api de Libreria", Version = "v1" });
                // c.EnableAnnotations();
            });


            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.MapHub<MessageHub>("/messageHub");

            // app.UseMiddleware<GlobalHandlerException>();

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API medica v1"); });

            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); endpoints.MapHub<MessageHub>("/messageHub"); });

        }
    }
}