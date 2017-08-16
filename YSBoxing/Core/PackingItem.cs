using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class PackingItem:Entity
    {
        [Display(Name = "款式")]
        [MaxLength(20)]
        public string Style { get; set; }
        [Display(Name = "颜色")]
        [MaxLength(20)]
        public string Color { get; set; }
        [Display(Name = "尺码")]
        [MaxLength(20)]
        public string Size { get; set; }
        [Display(Name = "细件数")]
        public int MaxQty { get; set; }
        [Display(Name = "大件数")]
        public int MaxQtyBig { get; set; }
    }
}
