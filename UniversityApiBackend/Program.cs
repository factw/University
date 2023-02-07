//1. Usings to work with entityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//2. Connection with SQL Server Express
const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add Context
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddJwtTokenServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddScoped<IStudentService, StudentService>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Configurar Swagger to take care of Autorization of JWT

builder.Services.AddSwaggerGen( options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorizaton Header using Bearer Scheme"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {           
            {
                new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                new string[]{}           
            }
                
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.AddCors();//option =>
//{
//    option.AddPolicy(name: "CorsePolicy", builder =>
//    {
//        builder.AllowAnyOrigin();
//        //builder.AllowAnyMethod();
//        //builder.AllowAnyHeader();
//    });
//});

//Supported culture
var supportedCultures = new[] { "en-US", "es-ES", "fr-FR" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedCultures(supportedCultures);

//Add localization to app

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsePolicy");

app.Run();
