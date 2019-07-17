using System.Net.Http;

namespace DemoApi.HttpClients.InternallyDecorated
{
   public class HttpMessageHandlerBuilder
   {
      public HttpMessageHandler Handler { get; private set; }

      public HttpMessageHandlerBuilder()
      {
         Handler = new HttpClientHandler();
      }

      public HttpMessageHandlerBuilder AddHandler(DelegatingHandler handler)
      {
         handler.InnerHandler = Handler;
         Handler = handler;

         return this;
      }
   }
}