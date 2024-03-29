﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;

namespace TripLog.Services
{
    public interface ITripLogDataService
    {
        Task<IList<TripLogEntry>> GetEntriesAsync();
        Task<TripLogEntry> AddEntryAsync(TripLogEntry entry);
    }
}
