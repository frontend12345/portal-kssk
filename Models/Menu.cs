using System;
using System.Collections.Generic;

namespace Portal.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Content = new HashSet<Content>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public ICollection<Content> Content { get; set; }
    }
}
