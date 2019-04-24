using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Models;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Controllers
{
	public class UserConversationsController : Controller
	{
		private readonly IService<UserConversation> _userConversationsService;
		private readonly IService<User> _userService;
		private readonly IService<Conversation> _conversationService;

		public UserConversationsController(
			IService<UserConversation> userConversationsService,
			IService<User> userService,
			IService<Conversation> conversationService)
		{
			_userConversationsService = userConversationsService;
			_userService = userService;
			_conversationService = conversationService;
		}

		// GET: UserConversations
		public async Task<IActionResult> Index()
		{
			var userConversations = await _userConversationsService.All().ToListAsync();
			return View(userConversations);
		}

		// GET: UserConversations/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userConversation = await _userConversationsService.All().FirstOrDefaultAsync(m => m.UserConversationId == id);
			if (userConversation == null)
			{
				return NotFound();
			}

			return View(userConversation);
		}

		// GET: UserConversations/Create
		public IActionResult Create()
		{
			ViewData["ConversationId"] = new SelectList(_conversationService.All(), "ConversationId", "ConversationId");
			ViewData["UserId"] = new SelectList(_userService.All(), "UserId", "UserId");
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
				await _userConversationsService.AddAsync(userConversation);
				return RedirectToAction(nameof(Index));
			}
			ViewData["ConversationId"] = new SelectList(_conversationService.All(), "ConversationId", "ConversationId", userConversation.ConversationId);
			ViewData["UserId"] = new SelectList(_userService.All(), "UserId", "UserId", userConversation.UserId);
			return View(userConversation);
		}

		// GET: UserConversations/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userConversation = await _userConversationsService.FindByIdAsync(id);
			if (userConversation == null)
			{
				return NotFound();
			}

			ViewData["ConversationId"] = new SelectList(_conversationService.All(), "ConversationId", "ConversationId", userConversation.ConversationId);
			ViewData["UserId"] = new SelectList(_userService.All(), "UserId", "UserId", userConversation.UserId);
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
					await _userConversationsService.UpdateAsync(userConversation);
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
			ViewData["ConversationId"] = new SelectList(_conversationService.All(), "ConversationId", "ConversationId", userConversation.ConversationId);
			ViewData["UserId"] = new SelectList(_userService.All(), "UserId", "UserId", userConversation.UserId);
			return View(userConversation);
		}

		// GET: UserConversations/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userConversation = await _userConversationsService.All().FirstOrDefaultAsync(m => m.UserConversationId == id);
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
			var userConversation = await _userConversationsService.FindByIdAsync(id);
			await _userConversationsService.DeleteAsync(userConversation);
			return RedirectToAction(nameof(Index));
		}

		private bool UserConversationExists(int id)
		{
			return _userConversationsService.All().Any(e => e.UserConversationId == id);
		}
	}
}
