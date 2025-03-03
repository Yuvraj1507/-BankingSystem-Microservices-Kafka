using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BankAccountService.MoneyTransfer.CommandsAndQueries.CreateMoneyTransfer;
using BankAccountService.MoneyTransfer.Data;
using BankAccountService.MoneyTransfer.Messaging.Options;
using BankAccountService.MoneyTransfer.Messaging.Receiver;
using BankAccountService.MoneyTransfer.Messaging.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BankAccountService.MoneyTransfer
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
            services.AddOptions();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddControllers();

            services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IRequestHandler<CreateMoneyTransferCommand>, CreateMoneyTransferCommand.CreateMoneyTransferCommandHandler>();

            services.AddTransient<IMoneyTransferService, MoneyTransferService>();

            var enableRabbitMqReceiverEnvironmentVariable = Environment.GetEnvironmentVariable("EnableRabbitMqReceiver");

            if (enableRabbitMqReceiverEnvironmentVariable != null && bool.Parse(enableRabbitMqReceiverEnvironmentVariable))
            {
                services.AddHostedService<MoneyTransferModelReceiver>();
            }
            else
            {
                bool.TryParse(Configuration["RabbitMq:Enabled"], out var enableRabbitMqReceiverSetting);

                if (enableRabbitMqReceiverSetting)
                {
                    services.AddHostedService<MoneyTransferModelReceiver>();
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
