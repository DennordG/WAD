using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Data;
using WAD_Application.Models;

namespace WAD_Application.Controllers
{
    public class UserConversationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserConversationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserConversations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserConversations.Include(u => u.Conversation).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserConversations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConversation = await _context.UserConversations
                .Include(u => u.Conversation)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserConversationId == id);
            if (userConversation == null)
            {
                return NotFound();
            }

            return View(userConversation);
        }

        // GET: UserConversations/Create
        public IActionResult Create()
        {
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserConversations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserConversationId,UserId,ConversationId")] UserConversation userConversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userConversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", userConversation.ConversationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userConversation.UserId);
            return View(userConversation);
        }

        // GET: UserConversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConversation = await _context.UserConversations.FindAsync(id);
            if (userConversation == null)
            {
                return NotFound();
            }
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", userConversation.ConversationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userConversation.UserId);
            return View(userConversation);
        }

        // POST: UserConversations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserConversationId,UserId,ConversationId")] UserConversation userConversation)
        {
            if (id != userConversation.UserConversationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userConversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserConversationExists(userConversation.UserConversationId))
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
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", userConversation.ConversationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userConversation.UserId);
            return View(userConversation);
        }

        // GET: UserConversations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConversation = await _context.UserConversations
                .Include(u => u.Conversation)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserConversationId == id);
            if (userConversation == null)
            {
                return NotFound();
            }

            return View(userConversation);
        }

        // POST: UserConversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userConversation = await _context.UserConversations.FindAsync(id);
            _context.UserConversations.Remove(userConversation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserConversationExists(int id)
        {
            return _context.UserConversations.Any(e => e.UserConversationId == id);
        }
    }
}
