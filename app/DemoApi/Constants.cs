using System;

namespace DemoApi
{
   public static class Constants
   {
      public static readonly string ApiUri = Environment.GetEnvironmentVariable("API_URI") ?? "http://locahost:8080";
   }
}