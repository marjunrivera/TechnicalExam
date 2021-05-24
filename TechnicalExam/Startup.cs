using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechnicalExam.Repository;
using TechnicalExam.Repository.Repository;
using TechnicalExam.Repository.Repository.Interface;
using TechnicalExam.Services.Models;
using TechnicalExam.Services.Services;
using TechnicalExam.Services.Services.Interface;

namespace TechnicalExam
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
            services.AddDbContext<TechnicalExamContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:TechnicalExamDB"]));
            services.AddControllers()
                .AddFluentValidation(fv => {
                    fv.RegisterValidatorsFromAssemblyContaining<AccountModelValidator>();
                });

            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<ITransactionServices, TransactionServices>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
