using CancunHootel.Models;
using System;
using System.Collections.Generic;

namespace CancunHootel.Services
{
    public interface IBookingService
    {
        void Delete(string id);
        Booking Get(string id);
        IEnumerable<DateTime> GetAvailable(DateTime start);
        Booking Save(Booking booking);
        void AddDates(DateTime start, DateTime end);
        void RemoveDates(DateTime start, DateTime end);
    }
}