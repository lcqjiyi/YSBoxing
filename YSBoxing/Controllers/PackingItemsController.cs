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
    public class PackingItemsController : Controller
    {
        private readonly YSDbContext _context;

        public PackingItemsController(YSDbContext context)
        {
            _context = context;    
        }

        // GET: PackingItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.PackingItems.ToListAsync());
        }

        // GET: PackingItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packingItem = await _context.PackingItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (packingItem == null)
            {
                return NotFound();
            }

            return View(packingItem);
        }

        // GET: PackingItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PackingItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Style,Color,Size,MaxQty,MaxQtyBig,Id,CreateDate")] PackingItem packingItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packingItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(packingItem);
        }

        // GET: PackingItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packingItem = await _context.PackingItems.SingleOrDefaultAsync(m => m.Id == id);
            if (packingItem == null)
            {
                return NotFound();
            }
            return View(packingItem);
        }

        // POST: PackingItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Style,Color,Size,MaxQty,MaxQtyBig,Id,CreateDate")] PackingItem packingItem)
        {
            if (id != packingItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packingItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackingItemExists(packingItem.Id))
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
            return View(packingItem);
        }

        // GET: PackingItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packingItem = await _context.PackingItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (packingItem == null)
            {
                return NotFound();
            }

            return View(packingItem);
        }

        // POST: PackingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packingItem = await _context.PackingItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.PackingItems.Remove(packingItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PackingItemExists(int id)
        {
            return _context.PackingItems.Any(e => e.Id == id);
        }
    }
}
