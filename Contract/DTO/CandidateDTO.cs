using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Contract.DTO
{
    public class CandidateDTO
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The maximum length for {0} is {1}")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The maximum length for {0} is {1}")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "The maximum length for {0} is {1}")]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The maximum length for {0} is {1}")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        public string? BestTimeToCall { get; set; }

        public string? LinkedInProfile { get; set; }

        public string? GitHubProfile { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The maximum length for {0} is {1}")]
        public string Comment { get; set; }

        [JsonIgnore]
        public DateTime LastUpdated { get; set; } 
    }
}
