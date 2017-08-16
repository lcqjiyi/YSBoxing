using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using YSBoxing.Services.YS;
using YSBoxing.Core.Helper;
using YSBoxing.Core.YS;
using Microsoft.EntityFrameworkCore;
using YSBoxing.Core;

namespace YSBoxing.Controllers
{
    public class ImportOrderController : BaseController
    {
        private readonly YSDbContext _context;
        private readonly IYsOrderServices _IYsOrderServices;
        public ImportOrderController(YSDbContext context, IYsOrderServices IYsOrderServices) {
            _context = context;
            _IYsOrderServices = IYsOrderServices;
        }
        public IActionResult ImportYSOrder(int CustomerId=0,int OrderGroupId=0)
        {
            ViewData["CustomerList"] = new SelectList(_context.Customers, "Id", "CustomerName");
            ViewData["OrderGroupList"] = new SelectList(_context.OrderGroup, "Id", "GroupName");
            ViewData["CustomerId"] = CustomerId;
            ViewData["OrderGroupId"] = OrderGroupId;
            if (CustomerId == 0 && OrderGroupId == 0) return View(new List<InputYsOrder>());
            var list = _context.InputYsOrders.Where(o=>o.IsGenerated == false);
            if (CustomerId>0)
            {
                list = list.Where(o => o.CustomerId == CustomerId);
            }
            if (OrderGroupId > 0) {
                list = list.Where(o => o.OrderGroupId == OrderGroupId);
            }

            return View( list.AsNoTracking().ToList());
        }
        [HttpPost]
        public IActionResult ImportYsOrderPost(int CustomerId,int OrderGroupId, IFormFile ExcelFile) {
            if (CustomerId <= 0 && OrderGroupId <= 0) return NotFound();
            if (!ExcelFile.FileName.Contains(".xlsx")) return ErrorMessage("错误！只支持.xlsx文件！");
            MemoryStream memoryStream = new MemoryStream();
            ExcelFile.CopyTo(memoryStream);
            var ETM = new ExcelToModel<InputYsOrder>(memoryStream);
            var verifError = ETM.VerificationHeader();
            if (verifError.Length > 0) return ErrorMessage(verifError);
            var modellsit = ETM.GetModelList();
            for (int i = modellsit.Count-1; i >=0; i--)
            {
                if (string.IsNullOrWhiteSpace( modellsit[i].YsOrderNo))
                {
                    modellsit.Remove(modellsit[i]);
                }
                else
                {
                    modellsit[i].CustomerId = CustomerId;
                    modellsit[i].OrderGroupId = OrderGroupId;
                }

            }
            _context.InputYsOrders.AddRange(modellsit);
            _context.SaveChanges();
            return RedirectToAction("ImportYSOrder",new { CustomerId = CustomerId, OrderGroupId = OrderGroupId });
        }

        public IActionResult DeleteImportYSOrder(int CustomerId = 0, int OrderGroupId = 0)
        {
            if (CustomerId == 0 && OrderGroupId == 0)return  ErrorMessage("错误！没有选择客户 与 订单组！");
            var list = _context.InputYsOrders.Where(o => o.IsGenerated == false);
            if (CustomerId > 0)
            {
                list = list.Where(o => o.CustomerId == CustomerId);
            }
            if (OrderGroupId > 0)
            {
                list = list.Where(o => o.OrderGroupId == OrderGroupId);
            }
            _context.InputYsOrders.RemoveRange(list);
            _context.SaveChanges();
            return RedirectToAction("ImportYSOrder", new { CustomerId = CustomerId, OrderGroupId = OrderGroupId });
        }

        public IActionResult GenerateOrders(int CustomerId = 0, int OrderGroupId = 0)
        {
            if (CustomerId == 0 && OrderGroupId == 0) return ErrorMessage("错误！没有选择客户 与 订单组！");
            var list = _context.InputYsOrders.Where(o => o.IsGenerated == false);
            if (CustomerId > 0)
            {
                list = list.Where(o => o.CustomerId == CustomerId);
            }
            if (OrderGroupId > 0)
            {
                list = list.Where(o => o.OrderGroupId == OrderGroupId);
            }

            var Tolsit = list.GroupBy(o => o.Style).ToList();

            foreach (var ysorder in Tolsit)
            {
                InputYsOrder firstInputYsOrder = ysorder.First();
                Order TempOrder = new Order();
                TempOrder.Style = firstInputYsOrder.Style;
                TempOrder.StyleDescription = firstInputYsOrder.StyleDescription;
                TempOrder.OrderGroupId = OrderGroupId;
                TempOrder.CustomerId = CustomerId;
                TempOrder.CreateDate = DateTime.Now;
                TempOrder.OrderItems = new List<OrderItem>();
                foreach (var item in ysorder)
                {
                    OrderItem getOrderItem =  TempOrder.OrderItems.FirstOrDefault(o => o.OrderItemName == item.YsOrderNo);
                    if (getOrderItem == null)
                    {
                        TempOrder.OrderItems.Add(new OrderItem(item.YsOrderNo, item.AddressName, item.AddressCode , item.YsGoodNo));
                    }

                    OrderItem TempOrderItem = TempOrder.OrderItems.FirstOrDefault(o => o.OrderItemName == item.YsOrderNo);
                    if (item.Qty1 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "XXS",item.Qty1));
                    }
                    if (item.Qty2 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "XS", item.Qty2));
                    }
                    if (item.Qty3 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "S", item.Qty3));
                    }
                    if (item.Qty4 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "M", item.Qty4));
                    }
                    if (item.qty5 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "L", item.qty5));
                    }
                    if (item.qty6 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "XL", item.qty6));
                    }
                    if (item.qty7 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "XXL", item.qty7));
                    }
                    if (item.qty8 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "XXXL", item.qty8));
                    }
                    if (item.qty9 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "XXXXL", item.qty9));
                    }
                    if (item.qty10 > 0)
                    {
                        TempOrderItem.AddOrderItemQty(new OrderItemQty(item.Style, item.Color, "XXXXXXL", item.qty10));
                    }
                    item.IsGenerated = true;
                }
                _context.Order.Add(TempOrder);
            }

            _context.SaveChanges();
            return RedirectToAction("ImportYSOrder", new { CustomerId = CustomerId, OrderGroupId = OrderGroupId });
        }

    }
}