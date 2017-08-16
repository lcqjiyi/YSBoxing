using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YSBoxing.Core.Helper;
namespace YSBoxing.Core.YS
{
    public class InputYsOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderGroupId { get; set; }
        [Display(Name = "申购单号")]
        [Keywords("申购单号")]
        public string YsOrderNo { get; set; }
        [Display(Name = "以纯订单")]
        [Keywords("订单单号")]
        public string YsGoodNo { get; set; }
        [Display(Name = "款式")]
        [Keywords("款号")]
        public string Style { get; set; }
        [Display(Name = "款式描述")]
        [Keywords("款式")]
        public string StyleDescription { get; set; }
        [Display(Name = "城区编码")]
        [Keywords("客户编号")]
        public string AddressCode { get; set; }
        [Display(Name = "城区")]
        [Keywords("客户")]
        public string AddressName { get; set; }
        [Display(Name = "颜色")]
        [Keywords("颜色")]
        public string Color { get; set; }
        [Display(Name = "XXS")]
        [Keywords("订单|XXS", false, true)]
        public int Qty1 { get; set; }
        [Display(Name = "XS")]
        [Keywords("订单|XS", false, true)]
        public int Qty2 { get; set; }
        [Display(Name = "S")]
        [Keywords("订单|S", false, true)]
        public int Qty3 { get; set; }
        [Display(Name = "M")]
        [Keywords("订单|M", false, true)]
        public int Qty4 { get; set; }
        [Display(Name = "L")]
        [Keywords("订单|L", false, true)]
        public int qty5 { get; set; }
        [Display(Name = "XL")]
        [Keywords("订单|XL", false, true)]
        public int qty6 { get; set; }
        [Display(Name = "XXL")]
        [Keywords("订单|XXL", false, true)]
        public int qty7 { get; set; }
        [Display(Name = "XXXL")]
        [Keywords("订单|XXXL", false, true)]
        public int qty8 { get; set; }
        [Display(Name = "XXXXL")]
        [Keywords("订单|XXXXL", false, true)]
        public int qty9 { get; set; }
        [Display(Name = "XXXXXL")]
        [Keywords("订单|XXXXXL", false, true)]
        public int qty10 { get; set; }

        [Display(Name = "已经生成")]
        public bool IsGenerated { get; set; }

    }
}
