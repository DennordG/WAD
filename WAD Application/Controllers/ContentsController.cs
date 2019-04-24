using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WAD_Application.Models;
using WAD_Application.Services.Interfaces;

namespace WAD_Application.Controllers
{
	public class ContentsController : Controller
	{
		private readonly IService<Content> _contentService;
		private readonly IService<Message> _messageService;

		public ContentsController(
			IService<Content> contentService,
			IService<Message> messageService)
		{
			_contentService = contentService;
			_messageService = messageService;
		}

		// GET: Contents
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _contentService.All();
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Contents/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var content = await _contentService.All().FirstOrDefaultAsync(m => m.ContentId == id);
			if (content == null)
			{
				return NotFound();
			}

			return View(content);
		}

		// GET: Contents/Create
		public IActionResult Create()
		{
			ViewData["MessageId"] = new SelectList(_messageService.All(), "MessageId", "MessageId");
			return View();
		}

		// POST: Contents/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ContentId,TextContent,ImageContent,MessageId")] Content content)
		{
			if (ModelState.IsValid)
			{
				await _contentService.AddAsync(content);
				return RedirectToAction(nameof(Index));
			}
			ViewData["MessageId"] = new SelectList(_messageService.All(), "MessageId", "MessageId", content.MessageId);
			return View(content);
		}

		// GET: Contents/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var content = await _contentService.FindByIdAsync(id);
			if (content == null)
			{
				return NotFound();
			}
			ViewData["MessageId"] = new SelectList(_messageService.All(), "MessageId", "MessageId", content.MessageId);
			return View(content);
		}

		// POST: Contents/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ContentId,TextContent,ImageContent,MessageId")] Content content)
		{
			if (id != content.ContentId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _contentService.UpdateAsync(content);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ContentExists(content.ContentId))
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
			ViewData["MessageId"] = new SelectList(_messageService.All(), "MessageId", "MessageId", content.MessageId);
			return View(content);
		}

		// GET: Contents/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var content = await _contentService.All().FirstOrDefaultAsync(m => m.ContentId == id);
			if (content == null)
			{
				return NotFound();
			}

			return View(content);
		}

		// POST: Contents/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var content = await _contentService.FindByIdAsync(id);
			await _contentService.DeleteAsync(content);
			return RedirectToAction(nameof(Index));
		}

		private bool ContentExists(int id)
		{
			return _contentService.All().Any(e => e.ContentId == id);
		}
	}
}
