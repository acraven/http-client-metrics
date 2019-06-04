using DemoApi.BackgroundServices;
using DemoApi.Dependencies;
using DemoApi.HttpClients;
using DemoApi.Middleware;
using Grouchy.Abstractions;
using Grouchy.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace DemoApi
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
         services.AddTransient<IMetricSink, LoggingMetricSink>();
         services.AddTransient<ITimingBlockFactory, TimingBlockFactory>();

         services.AddSingleton<TaskRunnerBackgroundService>();
         services.AddTransient<IBackgroundTaskRunner>(sp => sp.GetRequiredService<TaskRunnerBackgroundService>());
         services.AddTransient<IHostedService>(sp => sp.GetRequiredService<TaskRunnerBackgroundService>());

         services.AddTransient<EventSink>();

         services.AddSingleton<DecoratedClient>();
         services.AddSingleton<DelegatingHandlerClient>();
         services.AddSingleton<UndecoratedClient>();

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         app.UseMiddleware<ServerLoggingMiddleware>();

         app.UseMvc();
      }
   }
}