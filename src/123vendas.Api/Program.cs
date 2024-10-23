using _123vendas.Application.Configurations;
using _123vendas_server.v1.Configurations;
using _123vendas_server.v1.Middlewares;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerConfig();

builder.Services.AddLoggingSerilog(new LoggerConfiguration());

builder.Services.AddLogging(c => c.ClearProviders());

builder.Services.AddCustomApiVersioning();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.ResolveDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware(typeof(ExceptionMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
