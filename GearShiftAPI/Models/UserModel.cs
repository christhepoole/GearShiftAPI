using System.ComponentModel.DataAnnotations;

namespace GearShiftAPI.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Street_address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zipcode { get; set; }

        public string Phone { get; set; }

    }
}
