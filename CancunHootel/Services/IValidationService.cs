using System;
using System.Collections.Generic;

namespace CancunHootel.Services
{
    public interface IValidationService
    {
        string ValidateId(string id);
        IList<string> ValidatePeriod(DateTime start, DateTime end);
    }
}