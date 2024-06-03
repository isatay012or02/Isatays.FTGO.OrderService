using Isatays.FTGO.OrderService.Api.Common;
using Isatays.FTGO.OrderService.Api.Features.AutoMapper;
using Isatays.FTGO.OrderService.Api.Features.Middlewares;
using Isatays.FTGO.OrderService.Api.Features.Swagger;
using Isatays.FTGO.OrderService.Api.Features.Versioning;
using Isatays.FTGO.OrderService.Api.Features.WebApi;
using Isatays.FTGO.OrderService.Core;
using Isatays.FTGO.OrderService.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var webHostOptions = new WebHostOptions(
    instanceName: builder.Configuration.GetValue<string>($"{WebHostOptions.SectionName}:InstanceName"),
    webAddress: builder.Configuration.GetValue<string>($"{WebHostOptions.SectionName}:WebAddress"));

try
{
    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(Log.Logger);

    builder.ConfigureHostVersioning();

    builder.ConfigureWebHost();

    builder.Services.ConfigureApplicationAssemblies();
    
    builder.Services
        .AddConfiguredAutoMapper()
        .ConfigureInfrastructurePersistence(builder.Configuration, builder.Environment.EnvironmentName)
        .ConfigureInfrastructureServices()
        .ConfigureInfrastructureMassTransit()
        .ConfigureInfrastructureOptions(builder.Configuration);

    var app = builder.Build();

    Log.Information("{Msg} ({ApplicationName})...",
        "������ ������ �������",
        webHostOptions.InstanceName);

    app.UseConfiguredSwagger();
    app.UseConfiguredVersioning();
    app.UseHttpsRedirection();
    app.UseMiddleware<LoggingMiddleware>();
    app.UseMiddleware<ExceptionHandleMiddleware>();
    app.MapHealthChecks("/healthcheck");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "��������� ���� ��������� � ����������� ({ApplicationName})!",
        webHostOptions.InstanceName);
}
finally
{
    Log.Information("{Msg}!", "������������� �������� ������������");
    await Log.CloseAndFlushAsync();
}
