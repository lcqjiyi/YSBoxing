using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YSBoxing;
using YSBoxing.Core;

namespace YSBoxing.Controllers
{
    public class OrdersController : Controller
    {
        private readonly YSDbContext _context;

        public OrdersController(YSDbContext context)
        {
            _context = context;    
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var ySDbContext = _context.Order.Include(o => o.Customer).Include(o => o.OrderGroup);
            return View(await ySDbContext.ToListAsync());
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "CustomerName");
            ViewData["OrderGroupId"] = new SelectList(_context.OrderGroup, "Id", "GroupName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JcNo,Style,StyleDescription,CustomerId,OrderGroupId,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreateDate = DateTime.Now;
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            ViewData["OrderGroupId"] = new SelectList(_context.OrderGroup, "Id", "Id", order.OrderGroupId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "CustomerName", order.CustomerId);
            ViewData["OrderGroupId"] = new SelectList(_context.OrderGroup, "Id", "GroupName", order.OrderGroupId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JcNo,Style,StyleDescription,CustomerId,OrderGroupId,Id,CreateDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            ViewData["OrderGroupId"] = new SelectList(_context.OrderGroup, "Id", "Id", order.OrderGroupId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.OrderGroup)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
