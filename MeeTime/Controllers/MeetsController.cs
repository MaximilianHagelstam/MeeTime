using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeeTime.Data;
using MeeTime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View("Index", await _context.Meet.Where(j => j.CurrentUserId == User.Identity.Name).ToListAsync());
        }

        // POST: Meets/ShowSearchResults
        [Authorize]
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        { 
            return View("Index", await _context.Meet.Where( j => j.MeetName.Contains(SearchPhrase)).ToListAsync());
        }


        // GET: Meets/Details/5
        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Meets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MeetName,MeetCode,CurrentUserId")] Meet meet)
        {
            if (ModelState.IsValid)
            {
                meet.CurrentUserId = User.Identity.Name;
                _context.Add(meet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meet);
        }

        // GET: Meets/Edit/5
        [Authorize]
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
        [Authorize]
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
                    meet.CurrentUserId = User.Identity.Name;
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
        [Authorize]
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
        [Authorize]
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
