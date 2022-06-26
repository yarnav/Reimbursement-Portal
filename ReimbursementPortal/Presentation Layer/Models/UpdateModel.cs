using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entity
{
    public class UpdateModel
    {
        public int ReimbursementID { get; set; }
        public DateTime Date { get; set; }
        public string ReimbursementType { get; set; }
        public int RequestedValue { get; set; }
        public string Currency { get; set; }
        public string ImageURL { get; set; }
    }
}
