using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YSBoxing.Core;

namespace YSBoxing.Controllers
{
    public class OrderItemsController : BaseController
    {
        public readonly YSDbContext _Context;
        public OrderItemsController(YSDbContext Context) {
            _Context = Context;

        }

        public IActionResult GetList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  _Context.Order
                .Include(o => o.Customer)
                .Include(o => o.OrderGroup)
                .Include("OrderItems.OrderItemQtys")
                .SingleOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Boxing( int Id ) {

            OrderItem  orderItem = _Context.OrderItems.Include(o => o.OrderItemQtys)
                .Include(o => o.OrderItemBoxings)
                .SingleOrDefault(o => o.Id == Id);
            if (orderItem == null) return ErrorMessage("没有找到订单，订单系统Id为" + Id);
            if (orderItem.Box > 0) return ErrorMessage("该订单已经装过箱不能再生成装箱单。");
            if (orderItem.OrderItemQtys.Count <= 0) return ErrorMessage("该订单没有订单数据不能生成装箱单");
            List<string> styles = orderItem.OrderItemQtys.Select(o => o.Style).Distinct().ToList();
            List<PackingItem> packingitems = _Context.PackingItems.Where(o => styles.Contains(o.Style)).ToList();
            if (packingitems.Count <= 0) ErrorMessage("该订单没有可用的包装指令。请添加包装指令后再试！");
            orderItem.GenerateBoxList(packingitems);
            _Context.SaveChanges();
            return Json(new { ok = "ok" });
        }

        public IActionResult DelBoxList(int Id)
        {

            OrderItem orderItem = _Context.OrderItems.Include(o => o.OrderItemQtys)
                .Include(o => o.OrderItemBoxings)
                .SingleOrDefault(o => o.Id == Id);
            if (orderItem == null) return ErrorMessage("没有找到订单，订单系统Id为" + Id);
            if (orderItem.Box <= 0) return ErrorMessage("该订单没胡装过箱不能删除箱单。");
            orderItem.DeleteBoxList();

            _Context.SaveChanges();
            return Json(new { ok = "ok" });
        }

        public IActionResult ViewBoxList(int Id)
        {

            OrderItem orderItem = _Context.OrderItems
                .Include(o=>o.Order)
                .Include(o => o.OrderItemQtys)
                .Include("OrderItemBoxings.OrderItemBoxingQtys")
                .SingleOrDefault(o => o.Id == Id);
            if (orderItem == null) return ErrorMessage("没有找到订单，订单系统Id为" + Id);

            return View(orderItem);
        }
    }
}