using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class OrderItemQty : Entity
    {
        private OrderItemQty() { }
        public OrderItemQty(string style, string color, string size, int qty,string otherInfo="")
        {
            Style = style;
            Color = color;
            Size = size;
            Qty = qty;
            OtherInfo = otherInfo;
            CreateDate = DateTime.Now;
        }

        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        /// <summary>
        /// 款式
        /// </summary>
        [Display(Name ="款式")]
        [MaxLength(20)]
        public string Style { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [Display(Name = "颜色")]
        [MaxLength(20)]
        public string Color { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        [Display(Name = "尺码")]
        [MaxLength(20)]
        public string Size { get; set; }
        /// <summary>
        /// 订单件数
        /// </summary>
        [Display(Name = "订单件数")]
        public int Qty { get; set; }

        /// <summary>
        /// 装箱件数
        /// </summary>
        [Display(Name = "装箱件数")]
        public int BoxingQty { get; set; }

        /// <summary>
        /// 出货件数
        /// </summary>
        [Display(Name = "出货件数")]
        public int ShipQty { get; set; }
        /// <summary>
        /// 其它信息
        /// </summary>
        [Display(Name = "其它信息")]
        [MaxLength(255)]
        public string OtherInfo { get; set; }
        /// <summary>
        /// 细箱的装箱数
        /// </summary>
        [NotMapped]
        public int MaxQty { get; set; }
        /// <summary>
        /// 大箱的装箱数
        /// </summary>
        [NotMapped]
        public int MaxQtyBig { get; set; }

    }
}
