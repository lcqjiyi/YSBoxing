using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class OrderItemBoxingQty:Entity
    {
        private OrderItemBoxingQty() { }
        public OrderItemBoxingQty(string style, string color, string size, int qty)
        {
            Style = style;
            Color = color;
            Size = size;
            Qty = qty;
            CreateDate = DateTime.Now;
        }

        public int OrderItemBoxingId { get; set; }
        public OrderItemBoxing OrderItemBoxing { get; set; }
        [Display(Name = "款式")]
        [MaxLength(20)]
        public string Style { get; set; }
        [Display(Name = "颜色")]
        [MaxLength(20)]
        public string Color { get; set; }
        [Display(Name = "尺码")]
        [MaxLength(20)]
        public string Size { get; set; }
        [Display(Name = "件数")]
        public int Qty { get; set; }
    }
}
