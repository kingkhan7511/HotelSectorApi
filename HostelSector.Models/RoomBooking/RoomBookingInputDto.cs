using HotelSector.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelSector.Models.RoomBooking
{
    public class RoomBookingInputDto : IValidatableObject
    {
        public long? Id { get; set; }
        [Required]
        [Range(1, long.MaxValue)]
        public long RoomId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [JsonIgnore]
        public int HowManyHours { get; set; }
        [JsonIgnore]
        public double TotalCharges { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Id != null && Id< 1)
            {
                yield return new ValidationResult(HotelSectorMessages.IdShouldNotLessThan1);
            }
            else if (StartDate < DateTime.Now)
            {
                yield return new ValidationResult(HotelSectorMessages.StartDateShouldBeGreaterThanNow);
            }
            else if (StartDate > EndDate)
            {
                yield return new ValidationResult(HotelSectorMessages.EndDateShouldBeGreaterThanStartDate);
            }
            else if ((int) EndDate.Subtract(StartDate).TotalHours == 0)
            {
                yield return new ValidationResult(HotelSectorMessages.ShouldBe1HrDifference);

            }

        }
    }
}
