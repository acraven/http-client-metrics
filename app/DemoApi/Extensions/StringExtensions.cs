using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoApi.Extensions
{
   public static class StringExtensions
   {
      public static IEnumerable<TimeSpan> ToDurations(this string durationsMs)
      {
         try
         {
            return durationsMs.Split(',').Select(double.Parse).Select(TimeSpan.FromMilliseconds);
         }
         catch (Exception e)
         {
            throw new ArgumentException($"Failed to parse durations (durationsMs={durationsMs})",
               nameof(durationsMs), e);
         }
      }
   }
}