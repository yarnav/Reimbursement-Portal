using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation_Layer.Models
{
    public class ReimbursementModel
    {
        public int EmployeeID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReimbursementID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string ReimbursementType { get; set; }
        public int RequestedValue { get; set; }
        public int ApprovedValue { get; set; }
        public string Currency { get; set; }
        public string RequestPhase { get; set; }
        public string ImageURL { get; set; }
        public string RequestedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string InternalNotes { get; set; }
    }
}
