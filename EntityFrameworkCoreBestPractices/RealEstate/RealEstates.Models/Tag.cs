using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstates.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Tags = new HashSet<RealEstatePropertyTag>();
        }
        public int Id { get; set; }

        
        public string Name { get; set; }
        public string Descryption { get; set; }
        public virtual ICollection<RealEstatePropertyTag> Tags { get; set; }
    }
}
