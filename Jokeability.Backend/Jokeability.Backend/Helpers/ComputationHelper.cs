using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jokeability.Backend.Helpers
{
    public class ComputationHelper
    {
        public static string GetTokenValidityInSeconds(int days)
        {
            var timeSpan = new TimeSpan(DateTime.Now.AddDays(days).Ticks - DateTime.Now.Ticks);
            return (Math.Ceiling(timeSpan.TotalSeconds)).ToString();
        }
        public static string GetTokenValidityInSeconds(int days, int hours)
        {
            var _computedDaysAndHours = DateTime.Now.AddDays(days).Ticks + DateTime.Now.AddHours(hours).Ticks;
            var timeSpan = new TimeSpan(_computedDaysAndHours - DateTime.Now.Ticks);
            return (Math.Ceiling(timeSpan.TotalSeconds)).ToString();
        }        
    }
}
