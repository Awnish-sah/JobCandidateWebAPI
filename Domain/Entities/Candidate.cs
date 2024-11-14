using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Candidate
    {

        [Key]
        [Column(Order =0)]
        public int Id { get; set; }

        [Required]
        [Column(Order =1, TypeName ="varchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(Order =2, TypeName = "varchar(50)")]
        public string LastName { get; set; }

        [Column(Order = 3, TypeName = "varchar(20)")]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Column (Order = 4, TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Column(Order = 5, TypeName = "varchar(50)")]
        public string? BestTimeToCall { get; set; }

        [Column(Order = 6, TypeName = "varchar(100)")]
        [Url]
        public string? LinkedInProfile { get; set; }

        [Column(Order = 7, TypeName = "varchar(100)")]
        [Url]
        public string? GitHubProfile { get; set; }

        [Required]
        [Column(Order = 8, TypeName = "varchar(200)")]
        public string Comment { get; set; }

        [Column (Order =9, TypeName ="date")]
        public DateTime LastUpdated { get; set; } 

    }
}


