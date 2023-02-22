using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;

namespace TripLog.ViewModels
{
    public class DetailViewModel : BaseViewModel<TripLogEntry>
    {
        TripLogEntry _entry;

        public TripLogEntry Entry
        {
            get { return _entry; }
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public DetailViewModel(INavService navService, IAnalyticsService analyticsService) : base(navService, analyticsService)
        {
        }

        public override void Init(TripLogEntry parameter)
        {
            AnalyticsService.TrackEvent("Entry Detail Page", new Dictionary<string, string>
            {
                {"Title",parameter.Title }
            });
            Entry = parameter;
        }
    }
}