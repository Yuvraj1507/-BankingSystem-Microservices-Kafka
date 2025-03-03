using AuthService.Common.Filters;
using AuthService.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace AuthService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(provider => provider.GetService<UserManager<Entities.Account>>());
            services.AddScoped<NameFilter>();

            services.AddIdentity<Entities.Account, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                 .AddDefaultTokenProviders();

            services.AddTransient<UserManager<Entities.Account>>();
            services.AddTransient<DataContext>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
