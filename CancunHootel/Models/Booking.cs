using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CancunHootel.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public string  BookedBy { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
