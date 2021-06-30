using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    [Table("Types")]
    public class Typeclass
    {
        [Key]
        public int TypeID { get; set; }
        public string Type { get; set; }
        public ICollection<Projects>projectss{get;set;}
            
    }
}
