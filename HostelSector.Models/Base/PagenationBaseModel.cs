using System.ComponentModel.DataAnnotations;

namespace HotelSector.Models.Base
{
    public class PagenationBaseModel
    {
        [Range(0, int.MaxValue)]
        public int Skip { get; set; } 
        [Range(1,int.MaxValue)]
        public int MaxResultCount { get; set; }
      
    }
}
