using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
   [Table("Customer")]
    public class Customers
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        public string ContactPerson { get; set; }
        public string CustomerGroup { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public ICollection<Projects> Projectss { get; set; }
    }
}
