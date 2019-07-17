using System.Net.Http;
using System.Threading;
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

      public Task Post(IHttpClient httpClient, string name)
      {
         // Fire and forget
         _backgroundTaskRunner.Execute(PostEventAsync(httpClient, name));

         return Task.CompletedTask;
      }

      private async Task PostEventAsync(IHttpClient httpClient, string name)
      {
         var timingBlock = _timingBlockFactory.Create("post_event");

         var request = new HttpRequestMessage(HttpMethod.Post, $"events/{name}") {Content = new StringContent("{}")};

         // TODO: CancellationToken???
         await timingBlock.ExecuteAsync(() => httpClient.SendAsync(request, CancellationToken.None));
      }
   }
}