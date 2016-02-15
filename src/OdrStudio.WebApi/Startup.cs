using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdrStudio.WebApi.Models.Player;
using OdrStudio.WebApi.Models.Player.Vlc;

namespace OdrStudio.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.Configure<PlayerConfiguration>(Configuration.GetSection("Player"));

            var motSLideShowRetriever = new ServiceDescriptor(typeof(IMotSlideShowRetriever), typeof(MotSlideShowRetriever), ServiceLifetime.Singleton);
            var playerClient = new ServiceDescriptor(typeof(IPlayerClient), typeof(VlcClient), ServiceLifetime.Singleton);
            var motSlideShowSender = new ServiceDescriptor(typeof(IMotSlideshowSender), typeof(MotSlideshowSender), ServiceLifetime.Singleton);
            var dlsRetriever = new ServiceDescriptor(typeof(IDlsRetriever), typeof(DlsRetriever));

            services.Add(motSLideShowRetriever);
            services.Add(playerClient);
            services.Add(motSlideShowSender);
            services.Add(dlsRetriever);

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
                //builder.WithOrigins("http://localhost:5000", "http://localhost:5001"));
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
