﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using TripLog.Services;

namespace TripLog.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected INavService NavService { get; private set; }

        protected BaseViewModel(INavService navService)
        {
            NavService = navService;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Init()
        {
        }
    }

    public class BaseViewModel<TParameter> : BaseViewModel
    {
        public BaseViewModel(INavService navService) : base(navService)
        {
        }

        public override void Init()
        {
            Init(default(TParameter));
        }

        public virtual void Init(TParameter parameter)
        {
        }
    }
}