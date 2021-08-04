using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
  
        [Table("Position")]
        public class Positions
        {
            [Key]
            public int PositionID { get; set; }
            public string Position { get; set; }
        }
    
}
