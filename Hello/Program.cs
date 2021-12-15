using Volo.Abp;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac();
builder.Services.ReplaceConfiguration(builder.Configuration);
builder.Services.AddApplication<MinimalModule>();
var app = builder.Build();

app.MapGet("/hi", ([FromServices] HelloService helloService) =>
{
    return helloService.SayHi();
});

app.InitializeApplication();
app.Run();

public class HelloService : ITransientDependency
{
    public string SayHi()
    {
        return "Hi from service";
    }
}

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule)
)]
public class MinimalModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAssemblyOf<MinimalModule>();
    }
}