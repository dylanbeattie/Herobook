using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Formatters;
using Herobook.Workshop.Data;
using System.Buffers;

namespace Herobook
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
            services.AddTransient<IDatabase, DemoDatabase>();
            services.AddMvc(options => {
                options.AddHalJsonSupport();
            }).AddControllersAsServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseMvc();
        }
    }

    public static class MvcOptionsExtensions {
        public static void AddHalJsonSupport(this Microsoft.AspNetCore.Mvc.MvcOptions options) {
            // https://stackoverflow.com/questions/38084437/provide-arraypool-object-to-jsonoutputformatter-constructor
            var formatterSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            formatterSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            var formatter = new HalJsonOutputFormatter(formatterSettings, ArrayPool<Char>.Shared);
            options.OutputFormatters.Insert(0, formatter);
        }
    }
}
