using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class Attachments
    {
        public int AttachmentID { get; set; }
        public string ImageTitle { get; set; }
        public byte[] Files { get; set; }

        public int ProjectID { get; set; }
    }
}
