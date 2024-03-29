﻿using Akavache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly IBlobCache _cache;
        private readonly ITripLogDataService _tripLogService;
        private Command _refreshCommand;
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(LoadEntries));
        ObservableCollection<TripLogEntry> _logEntries;

        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public Command<TripLogEntry> ViewCommand => new Command<TripLogEntry>(async entry =>
            await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry));

        public Command NewCommand => new Command(async () => await NavService.NavigateTo<NewEntryViewModel>());

        public MainViewModel(INavService navService, ITripLogDataService tripLogService, IBlobCache cache, IAnalyticsService analyticsService)
             : base(navService, analyticsService)
        {
            _cache = cache;
            _tripLogService = tripLogService;
            LogEntries = new ObservableCollection<TripLogEntry>();
        }

        public override void Init()
        {
            LoadEntries();
        }

        void LoadEntries()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                _cache.GetAndFetchLatest("entries", async () => await _tripLogService.GetEntriesAsync())
                    .Subscribe(entries =>
                    {
                        LogEntries = new ObservableCollection<TripLogEntry>(entries);
                        IsBusy = false;
                    });
                //throw new Exception("我让程序崩溃了");
            }
            catch (Exception e)
            {
                AnalyticsService.TrackError(e, new Dictionary<string, string>
                {
                    {"Method","MainViewModel.LoadEntries()" }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}