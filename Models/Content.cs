using System;
using System.Collections.Generic;

namespace Portal.Models
{
    public partial class Content
    {
        public Content()
        {
            Files = new HashSet<Files>();
        }

        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Url { get; set; }
        public string Content1 { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public Menu Menu { get; set; }
        public ICollection<Files> Files { get; set; }
    }
}
