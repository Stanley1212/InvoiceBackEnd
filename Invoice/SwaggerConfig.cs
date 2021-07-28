using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Invoice
{
    public static class SwaggerConfig
    {
        const string DocumentXMLname = "WebApi.xml";
        /// <summary>
        /// Configura Swagger en el Api,configura el versionamiento del Api.
        /// para el correcto funcionamiento del versionamiento los controladores deben crearse en un subfolder ejemplo: 
        ///   Para Version1:  Controllers/v1/usuariosController
        ///   Para Version2:  Controllers/v2/usuariosController
        ///   Acceda a la Documentacion de Swagger desde la Web con la Ruta Docs
        /// </summary>
        /// <param name="services"></param>  
        /// <param name="env"></param>
        /// <returns></returns>
        public static IServiceCollection ApplySwaggerDoc(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                return services;
            }

            var controllersVersions = GetControllersVersions();
            string pathOutpu = Path.GetDirectoryName(typeof(Startup).Assembly.CodeBase.Replace("file:///", ""));

            services.AddSwaggerGen(config =>
            {
                foreach (var version in controllersVersions)
                {
                    config.SwaggerDoc(version, new OpenApiInfo()
                    {
                        Title = string.Format("Api", version),
                        Version = version,
                        Description = "Api",
                    });
                }
                var xmlpath = Path.Combine(pathOutpu, DocumentXMLname);
                if (File.Exists(xmlpath))
                {
                    config.IncludeXmlComments(xmlpath);
                }

                if (!env.IsDevelopment())
                {
                    var dockerPath = Environment.GetEnvironmentVariable("DOCKER_CONT_PATH") ?? "";
                    var serverUrl = Environment.GetEnvironmentVariable("SERVER_URL") ?? "";
                    config.AddServer(new OpenApiServer()
                    {
                        Description = "Staging",
                        Url = $"{serverUrl}/{dockerPath}"
                    });
                }

            });
            return services;
        }

        public static IApplicationBuilder ApplySwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {

                config.RoutePrefix = "docs";
                var versions = GetControllersVersions();
                foreach (var v in versions)
                {
                    config.SwaggerEndpoint($"../swagger/{v}/swagger.json", $"Api {v}");

                }
            });
            return app;
        }
        private static List<string> GetControllersVersions()
        {
            var assembly = typeof(Startup).Assembly;
            return assembly.ExportedTypes
                       .Where(c => typeof(ControllerBase).IsAssignableFrom(c))
                       .Select(x => x.Namespace.Split(".").Last())
                       .Distinct()
                       .ToList();
        }
    }
}
