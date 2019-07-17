using System;
using DemoApi.BackgroundServices;
using DemoApi.Dependencies;
using DemoApi.HttpClients;
using DemoApi.HttpClients.ExternallyDecorated;
using DemoApi.HttpClients.InternallyDecorated;
using DemoApi.HttpClients.Undecorated;
using DemoApi.Middleware;
using Grouchy.Abstractions;
using Grouchy.Metrics;
using Grouchy.Polly.RateLimit;
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

         services.AddSingleton(CreateHttpClient<ExternallyDecoratedClientFactory>);
         services.AddSingleton(CreateHttpClient<InternallyDecoratedClientFactory>);
         services.AddSingleton(CreateHttpClient<UndecoratedHttpClientFactory>);

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      }

      private static IHttpClient CreateHttpClient<TFactory>(IServiceProvider sp)
         where TFactory : IHttpClientFactory
      {
         var factory = ActivatorUtilities.CreateInstance<TFactory>(sp);

         return factory.Create();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         app.UseMiddleware<MetricsPushingMiddleware>();
         app.UseMiddleware<ExceptionHandlingMiddleware>();
         app.UseMiddleware<RateLimitingMiddleware>();

         app.UseMvc();
      }
   }
}