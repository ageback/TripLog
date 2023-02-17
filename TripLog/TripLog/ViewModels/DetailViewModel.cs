using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;

namespace TripLog.ViewModels
{
    public class DetailViewModel : BaseViewModel
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

        public DetailViewModel(TripLogEntry entry) => Entry = entry;
    }
}
