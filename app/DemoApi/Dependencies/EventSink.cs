using System.Threading.Tasks;
using DemoApi.HttpClients;
using Grouchy.Abstractions;

namespace DemoApi.Dependencies
{
   public class EventSink
   {
      private readonly IBackgroundTaskRunner _backgroundTaskRunner;
      private readonly ITimingBlockFactory _timingBlockFactory;

      public EventSink(
         IBackgroundTaskRunner backgroundTaskRunner,
         ITimingBlockFactory timingBlockFactory)
      {
         _backgroundTaskRunner = backgroundTaskRunner;
         _timingBlockFactory = timingBlockFactory;
      }

      public Task Post(IHttpClientWrapper httpClient, string name)
      {
         // Fire and forget
         _backgroundTaskRunner.Execute(PostEventAsync(httpClient, name));

         return Task.CompletedTask;
      }

      private async Task PostEventAsync(IHttpClientWrapper httpClient, string name)
      {
         var timingBlock = _timingBlockFactory.Create("post_event");

         //TODO: Mmmmm not seeing exceptions thrown when the httpclient times-out
         await timingBlock.ExecuteAsync(() => httpClient.PostAsync($"events/{name}"));
      }
   }
}