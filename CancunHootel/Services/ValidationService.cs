using CancunHootel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CancunHootel.Services
{
    public class ValidationService : IValidationService
    {
        IBookingStorage _storage;
        public ValidationService(IBookingStorage storage)
        {
            _storage = storage;
        }

        public IList<string> ValidatePeriod(DateTime start, DateTime end)
        {
            var messages = new List<string>();

            if (start > end)
            {
                messages.Add("Reservating period is not valid");
                return messages;
            }

            if (start <= DateTime.Today.Date)
                messages.Add("Room reservation should start at least the next day of booking");

            if (end > DateTime.Today.AddDays(30))
                messages.Add("Room can’t be reserved more than 30 days in advance.");

            if (end > start.AddDays(2))
                messages.Add("Room reservation can’t be longer than 3 days");

            var n = end.Date;
            for (DateTime i = start.Date; i <= n; i = i.AddDays(1))
                if (_storage.Dates.Contains(i))
                {
                    messages.Add($"Room is not available at this period {start.ToString()} - {end.ToString()}");
                    break;
                }

            return messages;
        }

        public string ValidateId(string id)
        {
            if (!_storage.Bookings.ContainsKey(id)) return $"Booking with ID {id} does not exist";

            return null;
        }
    }
}
