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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDos.DataProvider.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDos.DataProvider.Api", Version = "v1" });
            });
            services.AddHttpClient();
            services.AddSingleton<IConfigOptions, ConfigOptions>();
            services.AddSingleton<IJsonSerialization, JsonSerialization>();
            services.AddSingleton<IDataProviderConfiguration, DataProviderConfiguration>();
            services.AddSingleton<IMongoDatabaseObject, MongoDatabaseObject>();
            services.AddSingleton<IMongoCollectionObject<Category>, MongoCollectionObject<Category>>();
            services.AddSingleton<IMongoCollectionObject<ToDo>, MongoCollectionObject<ToDo>>();
            services.AddSingleton<ICategoryCollectionService, CategoryCollectionService>();
            services.AddSingleton<IToDoCollectionService, ToDoCollectionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDos.DataProvider.Api v1"));
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
