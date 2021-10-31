using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstates.Models
{
    public class RealEstatePropertyTag
    {
        public int RealStatePropertyId { get; set; }
        public virtual RealEstateProperty RealEstateProperty { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
