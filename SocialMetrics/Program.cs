using Quartz;
using Quartz.AspNetCore;
using Quartz.Logging;
using SocialMetrics.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddQuartz(q =>
    {
        //LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
        var jobKey = new JobKey("Twitter");
        q.AddJob<TwitterJob>(opt => opt.WithIdentity(jobKey));
        q.AddTrigger(opt => opt
                   .ForJob(jobKey)
                              .WithIdentity("TwitterTrigger")
                                                    .WithSimpleSchedule(x => x
                                                                   .WithIntervalInSeconds(10)
                                                                                  .RepeatForever()));

    })
    .AddQuartzServer(option =>
    {
        option.WaitForJobsToComplete = true;
    })
    .AddRazorComponents()
    .AddInteractiveServerComponents();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();





// simple log provider to get something to the console
sealed class ConsoleLogProvider : ILogProvider
{
    public Logger GetLogger(string name)
    {
        return (level, func, exception, parameters) =>
        {
            if (level >= Quartz.Logging.LogLevel.Info && func != null)
            {
                Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
            }
            return true;
        };
    }

    public IDisposable OpenNestedContext(string message)
    {
        throw new NotImplementedException();
    }

    public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
    {
        throw new NotImplementedException();
    }

}

public class TwitterJob : IJob
{
    private readonly TwitterDataFetcher _twitterDataFetcher;

    public TwitterJob(TwitterDataFetcher twitterDataFetcher)
    {
        _twitterDataFetcher = twitterDataFetcher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("TwitterJob is executing");
    }
}








