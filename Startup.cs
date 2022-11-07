using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiRestaurante2.Filtros;
using WebApiRestaurante2.Middlewares;
using WebApiRestaurante2.Services;

namespace WebApiRestaurante2
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opciones =>
            {
                opciones.Filters.Add(typeof(FiltroDeExcepcion));
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddTransient<FiltroDeRegistro>();
            services.AddHostedService<EscribirArchivo>();
            services.AddAutoMapper(typeof(Startup));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                /*c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiRestaurante", Version = "v1" });
            }*/);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseHttpMiddleware();
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
