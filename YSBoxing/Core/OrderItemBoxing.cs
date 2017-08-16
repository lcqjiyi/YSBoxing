using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class OrderItemBoxing : Entity
    {
        public static int CurrentBoxNumber =0;
        public OrderItemBoxing()
        {
            OrderItemBoxingQtys = new List<OrderItemBoxingQty>();
            CreateDate = DateTime.Now;
        }

        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        [Display(Name = "箱号")]
        public int BoxNumber { get; set; }
        [Display(Name = "箱码")]
        public string BoxCode { get; set; }
        [Display(Name = "件数")]
        public int Qty { get; set; }
        [Display(Name = "是否出货")]
        public bool IsShip { get; set; }
        [Display(Name = "出货时间")]
        public DateTime ShipDate { get; set; }

        public ICollection<OrderItemBoxingQty> OrderItemBoxingQtys { get; private set; }

        public void AddOrderItemBoxingQty(OrderItemBoxingQty orderItemBoxingQty) {
            Qty += orderItemBoxingQty.Qty;
            OrderItemBoxingQtys.Add(orderItemBoxingQty);
        }
    }
}
