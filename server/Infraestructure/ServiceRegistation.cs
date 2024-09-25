using Whatsapp_Api.Core.Models;

using Whatsapp_Api.Infraestructure.Context;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whatsapp_Api.Infraestructure.Repository;

namespace Infraestructure
{
    public static class ServiceRegistation
    {
        public static void AddAppContext(this IServiceCollection services, IConfiguration configuration)
        {
         
            services.AddDbContext<SocialMediaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Connection"));
            });

  
            // Configurar autenticación JWT
           
            services.AddScoped<Response>();

            services.AddScoped<GenericRepository<User>>();
            services.AddScoped<GenericRepository<Message>>();
            services.AddScoped<GenericRepository<Group>>();
            services.AddScoped<GenericRepository<GroupMembers>>();
            services.AddScoped<GenericRepository<Conversation>>();

            services.AddScoped<MessageRepository>();
            services.AddScoped<GroupRepository>();
            services.AddScoped<ConversationRepository>();


            //services.AddScoped<UserManager<Usuario>>();
            // services.AddScoped<SignInManager<Usuario>>();


        }

    }
}
