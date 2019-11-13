using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EP.Cryptocurrency.Core.Abstractions;
using EP.Cryptocurrency.Core.Implementations;
using EP.Cryptocurrency.DataSupplier.Abstractions;
using EP.Cryptocurrency.DataSupplier.Implementations;
using EP.Cryptocurrency.Repository.Abstractions;
using EP.Cryptocurrency.Repository.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EP.Cryptocurrency.Web
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
            services.AddControllersWithViews();
            services.AddDbContext<Storage.CryptoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CryptocurrencyDatabase")));
            //services.AddHttpClient("CoinMarketClient",
            //    options =>
            //    {
            //        options.BaseAddress = new Uri(Configuration.GetSection("CoinMarketCapApi:BaseAddress").Value); 
            //        options.DefaultRequestHeaders.Accept.Clear();
            //        options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //        options.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", Configuration["API_KEY"]);
            //    });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowedOrigins",
            //        builder => builder
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //    );
            //});
            services.AddTransient<ICryptocurrencyService, CryptocurrencyService>();
            services.AddTransient<ICryptocurrencyRepository, CryptocurrencyRepository>();
        //    services.AddTransient<ICryptocurrencyDataSupplier, ExternalApiDataSupplier>();
        //    services.AddTransient<ICoinMarketCapService, CoinMarketCapService>();
        //    services.AddTransient<ICryptocurrencyMapper, CryptocurrencyMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseCors();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Cryptocurrencies}/{action=Index}/{id?}");
            });
        }
    }
}
