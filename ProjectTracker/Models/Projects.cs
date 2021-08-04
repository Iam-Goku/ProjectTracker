using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    [Table("Project")]
    public class Projects
    {
        // [Key]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectID { get; set; }
        public int TypeID { get; set; }
        [ForeignKey("TypeID")]
        public Typeclass Types { get; set; }
        public string SDFRNo { get; set; } 
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers Customers { get; set; }
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Products Products { get; set; }
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public int RequestFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime RequestedDate { get; set; }
        public int ReviewedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime ReviewCompletion { get; set; }
        public int SOWCreatedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime SOWCreatedOn { get; set; }
        public int SOWReviewedBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime SOWReviewedOn { get; set; }
        public Boolean SOWSentYN { get; set; }
        public Boolean ProposalSentYN { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime ProposalSentDate { get; set; }
        public Boolean POReceivedYN { get; set; }
        public Boolean InvoiceYN { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime POReceivedDate { get; set; }
        public decimal POAmount { get; set; }
        public int DeveloperID { get; set; }
        [ForeignKey("DeveloperID")]
        //[ForeignKey("DeveloperID"), InverseProperty("EmployeeID")]
        public Employees Employees { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime DevelopmentStartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime DevelopmentEndDate { get; set; }
        public string DevelopmentStatus { get; set; }
        public int ProjectManagerID { get; set; }
       // [ForeignKey("ProjectManagerID")]
        //[ForeignKey("ProjectManagerID"), InverseProperty("EmployeeID")]
       // public Employees Manager { get; set; }
        public string ProjectStatus { get; set; }
       // public int NoteID { get; set; }
        //[ForeignKey("NoteID")]
        //public Note notes { get; set; }
        public string Comments { get; set; }


        [NotMapped]
        public string Requestfrom { get; set; }       
        [NotMapped]
        public string Reviewedby { get; set; }
        [NotMapped]
        public string SOWCreatedby { get; set; }
        [NotMapped]
        public string SOWReviewedby { get; set; }
        [NotMapped]
        public string DeveloperName { get; set; }
        [NotMapped]
        public string ProjectManagerName { get; set; }
      
        [NotMapped]
        public string Notecreate { get; set; }

        [NotMapped]
        public string Fileupload { get; set; }





    }
}
