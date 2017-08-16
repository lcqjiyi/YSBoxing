using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class Order : Entity
    {
        [Display(Name = "货号")]
        [MaxLength(20)]
        public string JcNo { get; set; }

        [Display(Name = "款式")]
        [MaxLength(20)]
        public string Style { get; set; }

        [Display(Name = "款式描述")]
        [MaxLength(255)]
        public String StyleDescription { get; set; }
        [Display(Name = "客户")]
        public int CustomerId { get; set; }
        [Display(Name ="客户")]
        public Customer Customer { get; set; }
        [Display(Name = "订单组")]
        public int OrderGroupId { get; set; }
        [Display(Name = "订单组")]
        public OrderGroup OrderGroup { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
