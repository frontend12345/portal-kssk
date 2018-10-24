using System;
using System.Collections.Generic;

namespace Portal.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
