using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class OrderGroup:Entity
    {
        [Display(Name ="订单组名")]
        public string GroupName { get; set; }

        [Display(Name = "订单组名描述")]
        public string GroupDescription { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
