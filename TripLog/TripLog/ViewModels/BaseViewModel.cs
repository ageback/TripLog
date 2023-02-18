using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TripLog.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseViewModel() { }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Init() { }
    }

    public class BaseViewModel<TParameter> : BaseViewModel
    {
        public BaseViewModel() { }

        public override void Init()
        {
            Init(default(TParameter));
        }

        public virtual void Init(TParameter parameter) { }
    }
}
