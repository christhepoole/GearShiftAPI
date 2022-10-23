using System.ComponentModel.DataAnnotations;

namespace GearShiftAPI.Models
{
    public class RentalModel
    {
        [Key]
        public int Id { get; set; }

        public string RentalName { get; set; }

        public string RentalType { get; set; }

        public decimal RentalCost { get; set; }

        public int MaxRentalPeriod { get; set; }

        public int RenterId { get; set; }
    }
}
