using CancunHootel.Models;
using System;
using System.Collections.Generic;

namespace CancunHootel.Services
{
    public class BookingStorage : IBookingStorage
    {
        public IDictionary<string, Booking> Bookings { get; private set; }
        public HashSet<DateTime> Dates { get; private set; }

        public BookingStorage()
        {
            Bookings = new Dictionary<string, Booking>();
            Dates = new HashSet<DateTime>();
        }
    }
}
