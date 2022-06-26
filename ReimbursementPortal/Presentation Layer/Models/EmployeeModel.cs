using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation_Layer.Models
{
    public class EmployeeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("ReimbursementModel")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Please enter your Email Address")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please provide a valid Email Address")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password, ErrorMessage = "Please Provide a valid Password")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Length must be atleast 8.")]
        public string Password { get; set; }

        [RegularExpression("^[a-zA-Z]+$")]
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Length must be 12.")]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string PAN { get; set; }
        public string Bank { get; set; }

        [StringLength(12, MinimumLength = 12, ErrorMessage = "Must be 12 integers numbers.")]
        public string AccountNumber { get; set; }
        public bool isApprover { get; set; }
    }
}
