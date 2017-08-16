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
    public class OrderGroupsController : Controller
    {
        private readonly YSDbContext _context;

        public OrderGroupsController(YSDbContext context)
        {
            _context = context;    
        }

        // GET: OrderGroups
        public async Task<JsonResult> List()
        {
            var total = await _context.OrderGroup.CountAsync();
            var list = await _context.OrderGroup.ToListAsync();

            return Json(new PagedResultOutput<OrderGroup>() {
                 totalCount = total, items=list });
        }

        // GET: OrderGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderGroup.ToListAsync());
        }

        // GET: OrderGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderGroup = await _context.OrderGroup
                .SingleOrDefaultAsync(m => m.Id == id);
            if (orderGroup == null)
            {
                return NotFound();
            }

            return View(orderGroup);
        }

        // GET: OrderGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupName,GroupDescription,Id")] OrderGroup orderGroup)
        {
            if (ModelState.IsValid)
            {
                orderGroup.CreateDate = DateTime.Now;
                _context.Add(orderGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(orderGroup);
        }

        // GET: OrderGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderGroup = await _context.OrderGroup.SingleOrDefaultAsync(m => m.Id == id);
            if (orderGroup == null)
            {
                return NotFound();
            }
            return View(orderGroup);
        }

        // POST: OrderGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupName,GroupDescription,Id,CreateDate")] OrderGroup orderGroup)
        {
            if (id != orderGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderGroupExists(orderGroup.Id))
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
            return View(orderGroup);
        }

        // GET: OrderGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderGroup = await _context.OrderGroup
                .SingleOrDefaultAsync(m => m.Id == id);
            if (orderGroup == null)
            {
                return NotFound();
            }

            return View(orderGroup);
        }

        // POST: OrderGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderGroup = await _context.OrderGroup.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderGroup.Remove(orderGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderGroupExists(int id)
        {
            return _context.OrderGroup.Any(e => e.Id == id);
        }
    }
}
