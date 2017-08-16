using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.Core
{
    public class OrderItem : Entity
    {
        private OrderItem() {

        }
        public OrderItem(string orderItemName, string orderInfo1, string orderInfo2, string orderInfo3,string orderOtherInfo="")
        {
            OrderItemName = orderItemName;
            OrderInfo1 = orderInfo1;
            OrderInfo2 = orderInfo2;
            OrderInfo3 = orderInfo3;
            OrderOtherInfo = orderOtherInfo;
            OrderItemQtys = new List<OrderItemQty>();
            OrderItemBoxings = new List<OrderItemBoxing>();
            CreateDate = DateTime.Now;
        }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [MaxLength(20)]
        public string OrderItemName { get; set; }
        /// <summary>
        /// 信息1
        /// </summary>
        [Display(Name = "信息1")]
        [MaxLength(20)]
        public string OrderInfo1 { get; set; }
        /// <summary>
        /// 信息2
        /// </summary>
        [Display(Name = "信息2")]
        [MaxLength(20)]
        public string OrderInfo2 { get; set; }
        /// <summary>
        /// 信息3
        /// </summary>
        [Display(Name = "信息3")]
        [MaxLength(20)]
        public string OrderInfo3 { get; set; }
        /// <summary>
        /// 其它信息
        /// </summary>
        [Display(Name = "其它信息")]
        [MaxLength(255)]
        public string OrderOtherInfo { get; set; }
        /// <summary>
        /// 订单件数
        /// </summary>
        [Display(Name = "订单件数")]
        public int Qty { get; set; }
        /// <summary>
        /// 已经装箱
        /// </summary>
        [Display(Name = "已经装箱")]
        public int Box { get; set; }
        /// <summary>
        /// 出货件数
        /// </summary>
        [Display(Name = "出货件数")]
        public int ShipQty { get; set; }
        /// <summary>
        /// 出货装箱
        /// </summary>
        [Display(Name = "出货装箱")]
        public int ShipBox { get; set; }
        /// <summary>
        /// 是否混款
        /// </summary>
        [Display(Name = "是否混款")]
        public bool HasMixStyle { get; set; }

        public IList<OrderItemQty> OrderItemQtys { get;private set; }

        public IList<OrderItemBoxing> OrderItemBoxings { get; private set; }

        /// <summary>
        /// 添加一个订单项
        /// </summary>
        /// <param name="orderItemQty"></param>
        public void AddOrderItemQty(OrderItemQty orderItemQty)
        {
            Qty += orderItemQty.Qty;
            OrderItemQtys.Add(orderItemQty);
        }
        /// <summary>
        /// 添加一个箱
        /// </summary>
        /// <param name="orderItemBoxing"></param>
        public void AddOrderItemBoxing(OrderItemBoxing orderItemBoxing)
        {
            Box += 1;
            orderItemBoxing.BoxNumber = OrderItemBoxing.CurrentBoxNumber;
            OrderItemBoxing.CurrentBoxNumber++;

            foreach (var item in orderItemBoxing.OrderItemBoxingQtys)
            {
                OrderItemQty itemQty = OrderItemQtys.Where(o => o.Style == item.Style && o.Color == item.Color && o.Size == item.Size).FirstOrDefault();
                if (itemQty == null) throw new NullReferenceException("订单不存在对应的订单项。");
                itemQty.BoxingQty += item.Qty; //在订单项中装箱数量加上相应的数量
            }

            OrderItemBoxings.Add(orderItemBoxing);
        }
        /// <summary>
        /// 生成装箱单
        /// </summary>
        /// <param name="packingItem"></param>
        public void GenerateBoxList(List<PackingItem> packingItem)
        {
            OrderItemBoxing.CurrentBoxNumber = 1;
            SetQtyItemMaxQty(packingItem);
            GenerateMaxQtyBox();//生成整箱
            GenerateMixSizeQtyBox();//生成混码箱
        }
        /// <summary>
        /// 设置最大装箱数
        /// </summary>
        /// <param name="packingItemList"></param>
        private void SetQtyItemMaxQty(List<PackingItem> packingItemList)
        {
            for (int i = 0; i < OrderItemQtys.Count; i++)
            {
                //匹配款式，颜色，码数
                var packingItem1 = packingItemList.FirstOrDefault(o => o.Style == OrderItemQtys[i].Style && o.Color == OrderItemQtys[i].Color && o.Size == OrderItemQtys[i].Size);
                if (packingItem1 != null)
                {
                    OrderItemQtys[i].MaxQty = packingItem1.MaxQty;
                    OrderItemQtys[i].MaxQtyBig = packingItem1.MaxQtyBig;
                    continue;
                }
                //匹配款式，码数
                var packingItem2 = packingItemList.FirstOrDefault(o => o.Style == OrderItemQtys[i].Style && o.Size == OrderItemQtys[i].Size);
                if (packingItem2 != null)
                {
                    OrderItemQtys[i].MaxQty = packingItem2.MaxQty;
                    OrderItemQtys[i].MaxQtyBig = packingItem2.MaxQtyBig;
                    continue;
                }

                //匹配款式
                var packingItem3 = packingItemList.FirstOrDefault(o => o.Style == OrderItemQtys[i].Style);
                if (packingItem3 != null)
                {
                    OrderItemQtys[i].MaxQty = packingItem3.MaxQty;
                    OrderItemQtys[i].MaxQtyBig = packingItem3.MaxQtyBig;
                    continue;
                }

                throw new Exception("没有找到匹配的包装指令。");
            }
        }
        /// <summary>
        /// 生成整箱的箱
        /// </summary>
        /// <param name="packingItemList"></param>
        private void GenerateMaxQtyBox()
        {
            foreach (OrderItemQty orderItemQty in OrderItemQtys)
            {
                if (orderItemQty.Qty>orderItemQty.MaxQty)
                {
                    for (int i = 0; i < orderItemQty.Qty/ orderItemQty.MaxQty; i++)
                    {
                        var newOrderItemBoxing = new OrderItemBoxing();
                        newOrderItemBoxing.AddOrderItemBoxingQty(new OrderItemBoxingQty(orderItemQty.Style, orderItemQty.Color, orderItemQty.Size, orderItemQty.MaxQty));
                        AddOrderItemBoxing(newOrderItemBoxing);
                    }
                }
            }
            
        }

        /// <summary>
        /// 混码装箱
        /// </summary>
        /// <param name="packingItemList"></param>
        private void GenerateMixSizeQtyBox()
        {
            //分款分色分码
            var groupList = OrderItemQtys.GroupBy(o => new { o.Style, o.Color }).ToList();
            foreach (var group in groupList)
            {
                GenerateMixSizeQtyBox(group);
            }

            //混款混色混码
            GenerateMixSizeQtyBox(OrderItemQtys);

            //最后一个尾数箱
            GenerateLastBox(OrderItemQtys);

        }

        /// <summary>
        /// 通地订单项生成混码箱
        /// </summary>
        /// <param name="OrderItemQtyList"></param>
        private void GenerateMixSizeQtyBox(IEnumerable< OrderItemQty> OrderItemQtyList)
        {
            var newOrderItemBoxing = new OrderItemBoxing();
            foreach (var item in OrderItemQtyList)
            {
                if (item.Qty == item.BoxingQty) continue;

                if ((newOrderItemBoxing.Qty + (item.Qty - item.BoxingQty)) < item.MaxQty)//不满一箱放入
                {
                    newOrderItemBoxing.AddOrderItemBoxingQty(new OrderItemBoxingQty(item.Style, item.Color, item.Size, (item.Qty - item.BoxingQty)));
                    continue;
                }
                if ((newOrderItemBoxing.Qty + (item.Qty - item.BoxingQty)) == item.MaxQty)//满一箱放入
                {
                    newOrderItemBoxing.AddOrderItemBoxingQty(new OrderItemBoxingQty(item.Style, item.Color, item.Size, (item.Qty - item.BoxingQty)));
                    AddOrderItemBoxing(newOrderItemBoxing);
                    newOrderItemBoxing = new OrderItemBoxing();
                    continue;
                }
                if ((newOrderItemBoxing.Qty + (item.Qty - item.BoxingQty)) > item.MaxQty)
                {//超过一箱

                    newOrderItemBoxing.AddOrderItemBoxingQty(new OrderItemBoxingQty(item.Style, item.Color, item.Size, item.MaxQty - newOrderItemBoxing.Qty));
                    AddOrderItemBoxing(newOrderItemBoxing);
                    newOrderItemBoxing = new OrderItemBoxing();
                    newOrderItemBoxing.AddOrderItemBoxingQty(new OrderItemBoxingQty(item.Style, item.Color, item.Size, item.Qty - (item.MaxQty - newOrderItemBoxing.Qty)));
                    continue;
                }
            }

        }
        /// <summary>
        /// 生成最后一箱
        /// </summary>
        /// <param name="OrderItemQtyList"></param>
        private void GenerateLastBox(IEnumerable<OrderItemQty> OrderItemQtyList)
        {
            var newOrderItemBoxing = new OrderItemBoxing();
            foreach (var item in OrderItemQtyList)
            {
                if (item.Qty == item.BoxingQty) continue;
                newOrderItemBoxing.AddOrderItemBoxingQty(new OrderItemBoxingQty(item.Style, item.Color, item.Size, item.Qty - item.BoxingQty));
            }
            AddOrderItemBoxing(newOrderItemBoxing);
        }

        /// <summary>
        /// 删除装箱列表
        /// </summary>
        public void DeleteBoxList() {

            foreach (var item in OrderItemQtys)
            {
                item.BoxingQty = 0;
            }
            this.Box = 0;
            OrderItemBoxings = new List<OrderItemBoxing>();
        }

    }
}
