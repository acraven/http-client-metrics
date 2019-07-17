namespace DemoApi.HttpClients
{
   public interface IHttpClientFactory
   {
      IHttpClient Create();
   }
}