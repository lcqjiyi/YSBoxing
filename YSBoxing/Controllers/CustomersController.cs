using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YSBoxing;
using YSBoxing.Core;
using YSBoxing.ViewModels;

namespace YSBoxing.Controllers
{
    public class CustomersController : Controller
    {
        private readonly YSDbContext _context;

        public CustomersController(YSDbContext context)
        {
            _context = context;    
        }

        // GET: Customers
        public  IActionResult Index()
        {
            return  View();
        }

        // GET: Customers
        public async Task<JsonResult> List()
        {
            return Json(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind("CustomerName,HasMixStyle,Id")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CreateDate = DateTime.Now;
                _context.Add(customer);
                 _context.SaveChangesAsync();
                return Json(new JsonResultViewModel { error=null});
            }
            return Json(new JsonResultViewModel { error ="添加客户错误！" });
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit([Bind("CustomerName,HasMixStyle,Id,CreateDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Json(new JsonResultViewModel() { error = "编辑错误！无法保存" });
                }
                return Json(new JsonResultViewModel() { error=null});
            }
            return Json(new JsonResultViewModel() { error = "验证数据失败！无法保存" });
        }

        // Customers/Delete/5
        public async Task<string> Delete(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return "删除成功";
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
