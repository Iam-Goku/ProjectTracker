using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    [Table("Employee")]
    public class Employees
    {
        // [Key]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public int PositionID { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        //[ForeignKey("EmployeeID")]
        public ICollection<Projects> Projectss { get; set; }
    }
}
