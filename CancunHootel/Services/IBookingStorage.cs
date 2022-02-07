using CancunHootel.Models;
using System;
using System.Collections.Generic;

namespace CancunHootel.Services
{
    public interface IBookingStorage
    {
        IDictionary<string, Booking> Bookings { get; }
        HashSet<DateTime> Dates { get; }
    }
}