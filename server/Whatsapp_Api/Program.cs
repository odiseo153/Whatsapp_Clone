using Whatsapp_Api.Infraestructure.Context;
using Whatsapp_Api;
using Whatsapp_Api.Presentation.WebApi.Seed;
using MediatR;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var startup = new Startup(builder.Configuration);

        // Add services to the container.
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        using (var service = app.Services.CreateScope())
        {
            /*
             */
            var servicio = service.ServiceProvider;
            var db = servicio.GetRequiredService<SocialMediaContext>();
            var mediator = servicio.GetRequiredService<IMediator>();


           //  db.Database.EnsureDeleted();
           //  db.Database.EnsureCreated();

           // db.Seed_UserAsync(mediator).GetAwaiter().GetResult();
            
           // db.Seed_MessagesAsync(mediator).GetAwaiter().GetResult();
            //db.Seed_Conversation();

            // db.Seed_Pacientes();
            //db.Seed_Medico();
            //db.Seed_Citas();

        }


        // Configure the HTTP request pipeline.
        startup.Configure(app, app.Environment);

        app.Run();

    }
}