using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeeTime.Data;
using MeeTime.Models;

namespace MeeTime.Controllers
{
    public class MeetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Meets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meet.ToListAsync());
        }

        // GET: Meets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meet = await _context.Meet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meet == null)
            {
                return NotFound();
            }

            return View(meet);
        }

        // GET: Meets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MeetName,MeetCode")] Meet meet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meet);
        }

        // GET: Meets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meet = await _context.Meet.FindAsync(id);
            if (meet == null)
            {
                return NotFound();
            }
            return View(meet);
        }

        // POST: Meets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MeetName,MeetCode")] Meet meet)
        {
            if (id != meet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetExists(meet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meet);
        }

        // GET: Meets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meet = await _context.Meet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meet == null)
            {
                return NotFound();
            }

            return View(meet);
        }

        // POST: Meets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meet = await _context.Meet.FindAsync(id);
            _context.Meet.Remove(meet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetExists(int id)
        {
            return _context.Meet.Any(e => e.Id == id);
        }
    }
}
