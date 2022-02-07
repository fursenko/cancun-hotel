using CancunHootel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CancunHootel.Services
{
    public class BookingService : IBookingService
    {
        IBookingStorage _storage;

        public BookingService(IBookingStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<DateTime> GetAvailable(DateTime start)
        {
            var dates = new List<DateTime>();
            var n = DateTime.Today.AddDays(30).Date;

            for (DateTime i = start.Date; i <= n; i = i.AddDays(1))
                if (!_storage.Dates.Contains(i.Date)) dates.Add(i.Date);

            return dates;
        }

        public Booking Save(Booking booking)
        {
            booking.Id = string.IsNullOrWhiteSpace(booking.Id) ? Guid.NewGuid().ToString() : booking.Id;

            if (_storage.Bookings.ContainsKey(booking.Id))
            {
                var origin = _storage.Bookings[booking.Id];
                origin.BookedBy = booking.BookedBy;
                RemoveDates(origin.Start, origin.End);
                _storage.Bookings[booking.Id].Start = booking.Start;
                _storage.Bookings[booking.Id].End = booking.End;
            }
            else _storage.Bookings.Add(booking.Id, booking);
            
            AddDates(booking.Start, booking.End);

            return booking;
        }

        public Booking Get(string id)
        {
            return _storage.Bookings[id];
        }
        public void Delete(string id)
        {
            var booking = _storage.Bookings[id];
            RemoveDates(booking.Start, booking.End);
            _storage.Bookings.Remove(id);
        }

        public void AddDates(DateTime start, DateTime end)
        {
            for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
                if (!_storage.Dates.Contains(i))
                    _storage.Dates.Add(i);
        }

        public void RemoveDates(DateTime start, DateTime end)
        {
            for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
                if (_storage.Dates.Contains(i))
                    _storage.Dates.Remove(i);
        }
    }
}
