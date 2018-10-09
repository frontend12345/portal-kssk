using System;
using System.Collections.Generic;

namespace Portal.Models
{
    public partial class SecureFiles
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
