using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public class RequestPhaseModel
    {
        public int ReimbursementID { get; set; }
        public int ApprovedValue { get; set; }
        public string ApprovedBy { get; set; }
        public string InternalNotes { get; set; }
    }
}
