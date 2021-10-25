using DataProvider.Abstractions.Interfaces.Actions;
using DataProvider.Abstractions.Interfaces.Configuration;
using DataProvider.Abstractions.Interfaces.MongoDb;
using DataProvider.Abstractions.Interfaces.Services;
using DataProvider.Engines.Actions;
using DataProvider.Engines.Configuration;
using DataProvider.Engines.MongoDb;
using DataProvider.Engines.Services;
using DataProvider.Models.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDos.Abstractions.Actions;
using ToDos.Abstractions.Services;
using ToDos.Engines.Actions;
using ToDos.Engines.Services;

namespace ToDosWebApp
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

            services.AddHttpClient();
            services.AddSingleton<IConfigOptions, ConfigOptions>();
            services.AddSingleton<IJsonSerialization, JsonSerialization>();
            services.AddSingleton<IDataProviderConfiguration, DataProviderConfiguration>();
            services.AddSingleton<IMongoDatabaseObject, MongoDatabaseObject>();
            services.AddSingleton<IMongoCollectionObject<Category>, MongoCollectionObject<Category>>();
            services.AddSingleton<IMongoCollectionObject<ToDo>, MongoCollectionObject<ToDo>>();
            services.AddSingleton<ICategoryCollectionService, CategoryCollectionService>();
            services.AddSingleton<IToDoCollectionService, ToDoCollectionService>();
            services.AddSingleton<ICollectionActions, CollectionActions>();
            services.AddSingleton<ICategoryViewService, CategoryViewService>();
            services.AddSingleton<IToDoViewService, ToDoViewService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
