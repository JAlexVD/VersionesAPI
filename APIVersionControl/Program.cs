
//Para control de versiones instalar dependencias
//1.Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
//2.Microsoft.AspNetCore.Mvc.Versioning
//3. versiones y lueg DTO
//4.configuracion de versionado para swaggers


using APIVersionControl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

//4.1 para el versionado
//Http Client para hacer las peticiones
builder.Services.AddHttpClient();

//4.2 Añdir gestion de versiones
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    //que tome la 1.0 si no está especificada
    setup.AssumeDefaultVersionWhenUnspecified= true;
    setup.ReportApiVersions = true;
});

//4.3 configuración para como queremos documentar nuestras versiones
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";//1.0.0-1.1.0...
    setup.SubstituteApiVersionInUrl= true;//version en URL
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();//de momento porq ya está configurado
builder.Services.AddSwaggerGen();

//4.4 configuarar las opciones para que se agreguen a Swagger
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

//4.5 configuarar endpoints para swagger DOCS para cado version de nuestr API
var apiVersionDescriptionPrivider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.//añadimos
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    //4.6 configuarar los swagger docs
    app.UseSwaggerUI( options =>
    {
        foreach (var description in apiVersionDescriptionPrivider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
             $"/swagger/{description.GroupName}/swagger.json",//swagger/V1 o swagger/v2
             description.GroupName.ToUpperInvariant()
            ) ;
            // /swagger/V1/swagger.json
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
