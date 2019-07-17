using System;

namespace DemoApi
{
   public static class Constants
   {
      public static readonly string ApiUri = Environment.GetEnvironmentVariable("API_URI") ?? "http://localhost:8080";

      public static readonly TimeSpan DefaultHttpClientTimeout = TimeSpan.FromSeconds(1);

      public static readonly string DefaultRetryDurationsMs = "500,1000,2000";
   }
}