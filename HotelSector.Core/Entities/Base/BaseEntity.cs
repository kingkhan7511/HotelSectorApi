using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSector.Core.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDateTime = DateTime.Now;
        }
        [Required]
        [Range(1, long.MaxValue)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [Range(1, long.MaxValue)]
        public long CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
        public long? EditBy { get; set; }
        public DateTime? EditDateTime { get; set; } 
    }

     
}
