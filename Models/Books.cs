using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace lab5.Models
{
    public partial class cards
    {
        public cards()
        {
            Color = new HashSet<Color>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? Pages { get; set; }
        public double? Price { get; set; }
        public DateTime? Published { get; set; }
        public string Abstracts { get; set; }

        public virtual ICollection<Color> Color { get; set; }
    }
}
