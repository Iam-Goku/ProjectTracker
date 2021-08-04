using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    [Table("Notes")]
    public class Note
    {
        [Key]
        public int NoteID { get; set; }
        public string Notes { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public int ProjectID { get; set; }
       
      //  public ICollection<Projects> Projectss { get; set; }
    }
}
