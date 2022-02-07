using CancunHootel.Models;
using CancunHootel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CancunHootel.Controllers
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var valueAsString = valueProviderResult.FirstValue;

            var dateTime = DateTime.ParseExact(valueAsString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            bindingContext.Result = ModelBindingResult.Success(dateTime);

            return Task.CompletedTask;
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        IBookingService _bookingService;
        IValidationService _validationService;
        public BookingController(IBookingService bookingService, IValidationService validationService)
        {
            _bookingService = bookingService;
            _validationService = validationService;
        }

        [HttpPost]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
        public IActionResult Book([FromBody] Booking booking)
        {
            var errors = _validationService.ValidatePeriod(booking.Start.Date, booking.End);
            if (errors.Any()) return BadRequest(errors);

            booking.Id = null;
            var created = _bookingService.Save(booking);
            return Ok(created);
        }

        [HttpPut]
        [Route("change")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
        public IActionResult Change([FromBody] Booking booking)
        {
            string existError = null;
            if ((existError = _validationService.ValidateId(booking.Id)) != null) return BadRequest(existError);

            var origin = _bookingService.Get(booking.Id);
            _bookingService.RemoveDates(origin.Start.Date, origin.End.Date);
            var errors = _validationService.ValidatePeriod(booking.Start.Date, booking.End);
            if (errors.Any())
            {
                _bookingService.AddDates(origin.Start.Date, origin.End.Date);
                return BadRequest(errors);
            }

            var saved = _bookingService.Save(booking);
            return Ok(saved);
        }

        [HttpDelete]
        [Route("cancel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Cancel(string id)
        {
            string existError = null;
            if ((existError = _validationService.ValidateId(id)) != null)
                return BadRequest(existError);

            _bookingService.Delete(id);
            return Ok($"Booking {id} was canceled");
        }


        [HttpGet]
        [Route("available/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<DateTime>))]
        public IActionResult GetAvailableDates([FromRoute][ModelBinder(BinderType = typeof(DateTimeModelBinder))] DateTime date = default(DateTime))
        {
            date = (date == default(DateTime)) ? DateTime.Today.AddDays(1).Date : date;
            return Ok(_bookingService.GetAvailable(date));
        }
    }
}
