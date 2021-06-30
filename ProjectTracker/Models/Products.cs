using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    [Table("Product")]
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public ICollection<Projects> Projectss { get; set; }
    }
}
