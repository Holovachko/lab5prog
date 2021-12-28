using System;
using System.Collections.Generic;


namespace lab5.Models
{
    public partial class Color
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? StartNumber { get; set; }
        public int? EndNumber { get; set; }
        public int? cardId { get; set; }

        public virtual cards card { get; set; }
    }
}
