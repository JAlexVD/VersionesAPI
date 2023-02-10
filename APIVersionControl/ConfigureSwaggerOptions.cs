using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIVersionControl
{
    //Contiene las configuraciones de Swagger para que incluya
    //la gestion de versiones
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        //para proporcionar version a swagger
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        //Metodo
        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            //descripsion para version específica
            //que novedades rae la version
            var info = new OpenApiInfo
            {
                Title="My .Net Api Restful",
                Version = description.ApiVersion.ToString(),//1.0--2.0
                Description="This is my first API Verioning control",
                Contact=new OpenApiContact()
                {
                    Email="jhonvd@gmail.com",
                    Name="Alex",
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "This API version has been Deprecated";
            }

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            //añadir una documentacion de Swagger para caa una de las versiones de nuestra app
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }


        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        
    }
}
