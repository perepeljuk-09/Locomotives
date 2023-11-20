using Locomotives.API.Extentions;
using Locomotives.API.Services;
using Locomotives.API.Services.Contracts;
using Locomotives.Infrastructure.DataBases;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var configBuilder = new ConfigurationBuilder();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

configBuilder.AddJsonFile("appsettings.json");
var config = configBuilder.Build();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PgContext>(options =>
    options.UseNpgsql(config.GetConnectionString("pg"))
)
    .AddTransient<IDepotsService, DepotsService>()
    .AddTransient<IDriversService, DriversService>()
    .AddTransient<ILocomotivesService, LocomotivesService>()
    .AddTransient<ILocomotiveCategoriesService, LocomotiveCategoriesService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCustomLogError();
	app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
/*
app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";
                            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                            if (null != exceptionObject)
                            {
                                var errorMessage = $"<b>Exception Error: {exceptionObject.Error.Message} </b> {exceptionObject.Error.StackTrace}";
                                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                            }
                        });
                }
            );
*/
app.MapControllers();

app.Run();
