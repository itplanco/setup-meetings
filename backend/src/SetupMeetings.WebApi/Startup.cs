using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SetupMeetings.Commands.Users;
using SetupMeetings.Infrastructure.EventSourcing;
using SetupMeetings.Infrastructure.Messaging;
using SetupMeetings.Queries.Meetings;
using SetupMeetings.Queries.Users;
using SetupMeetings.WebApi.Infrastracture.DataStore;
using SetupMeetings.WebApi.Infrastracture.EventSourcing;
using SetupMeetings.WebApi.Infrastracture.Messaging;
using SetupMeetings.WebApi.Infrastracture.Queries;
using SetupMeetings.WebApi.Services;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace SetupMeetings.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var context = new SetupMeetingsQueryContext();
            var eventDispatcher = new EventDispatcher();
            eventDispatcher.Register(new UserRepositoryUppdator(context));
            services.AddSingleton<IEventSourcedRepository<UserAggregate>, EventSourcedRepository<UserAggregate>>();
            services.AddSingleton<ICommandBus, CommandBus>();
            services.AddSingleton(context);
            services.AddSingleton(eventDispatcher);
            services.AddSingleton<IEventBus, EventBus>();
            services.AddTransient<IEventAwaiter, EventAwaiter>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IMeetingsRepository, MeetingsRepository>();
            services.AddTransient<IMeetingsService, MeetingsService>();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "e-幹事 API",
                    Version = "v1",
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "e-幹事 API");
            });

            // Routing for angular2
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    await next();
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
