using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripLog.Services
{
    public class AppCenterAnalyticsService : IAnalyticsService
    {
        public void TrackError(Exception exception)
        {
            Crashes.TrackError(exception);
        }

        public void TrackError(Exception exception, IDictionary<string, string> data)
        {
            Crashes.TrackError(exception, data);
        }

        public void TrackEvent(string eventKey)
        {
            Analytics.TrackEvent(eventKey);
        }

        public void TrackEvent(string eventKey, IDictionary<string, string> data)
        {
            Analytics.Equals(eventKey, data);
        }
    }
}
