using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
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
            //A cada request cria nova inst�ncia depois despois mata a inst�ncia
            //services.AddScoped<Data.MongoDB>();

            //Cria diversas inst�ncias e as mant�m
            //services.AddTransient<Data.MongoDB>();

            //Cria uma inst�ncia �nica
            services.AddSingleton<Data.MongoDB>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
