using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class Customer:Entity
    {
        [Display(Name ="客户名")]
        public string CustomerName { get; set; }

        [Display(Name = "是否混款")]
        public bool HasMixStyle { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
