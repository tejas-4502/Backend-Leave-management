using System.ComponentModel.DataAnnotations;

namespace Backend_Leave.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Joindate { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)] // Adjust the length according to your security requirements
        public string Password { get; set; }
    }
}
