using System;
using Grouchy.Abstractions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Grouchy.Metrics.Tests.SimpleMetricScenarios
{
   public class happy_path
   {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void Test1()
      {
         var m = new LoggingMetricWriter(new StubLogger<LoggingMetricWriter>());
         
         m.Write(new Metric { Name = "foo", Dimensions = { {"", null}}});
      }
   }

   public class StubLogger<T> : ILogger<T>
   {
      public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
      {
         //TODO:
         Console.WriteLine(formatter(state, exception));
      }

      public bool IsEnabled(LogLevel logLevel)
      {
         return true;
      }

      public IDisposable BeginScope<TState>(TState state)
      {
         return null;
      }
   }
}