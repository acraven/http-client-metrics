using System;
using System.Threading;

namespace DemoApi.Extensions
{
   public static class CancellationTokenExtensions
   {
      public static IDisposable Register<T>(this CancellationToken cancellationToken, Action<T> action, T state)
      {
         return cancellationToken.Register(s => { action((T) s); }, state);
      }
   }
}