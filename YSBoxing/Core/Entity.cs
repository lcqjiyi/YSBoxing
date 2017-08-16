using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="创建时间")]
        public DateTime CreateDate { get; set; }
    }
}
