using HotelSector.Shared.Room;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSector.Core.Entities.Room
{
    [Table("Room")]
    public class RoomEntity : BaseEntity
    {
        [Required]
        [StringLength(RoomStaticValue.RoomNoMaxLength, MinimumLength = RoomStaticValue.RoomNoMinLength)]
        public string RoomNo { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int RoomFloor { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public double RentPerHour { get; set; }
        [Required]
        public bool IsAvailable { get; set; } 
    }
}
