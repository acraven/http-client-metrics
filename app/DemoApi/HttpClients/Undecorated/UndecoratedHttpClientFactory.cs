using System;
using System.Net.Http;

namespace DemoApi.HttpClients.Undecorated
{
   // ReSharper disable once ClassNeverInstantiated.Global
   public class UndecoratedHttpClientFactory : IHttpClientFactory
   {
      public IHttpClient Create()
      {
         var httpClient = new HttpClient
         {
            BaseAddress = new Uri(Constants.ApiUri),
            Timeout = Constants.DefaultHttpClientTimeout
         };

         return new HttpClientWrapper(httpClient) {Name = "undecorated"};
      }
   }
}