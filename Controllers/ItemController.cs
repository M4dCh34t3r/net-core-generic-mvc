using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GenericCrudMVC.Models;

namespace GenericCrudMVC.Controllers {
    public class ItemController : Controller {
		private readonly ItemDbContext _context;

		public ItemController(ItemDbContext context) {
			_context = context;
		}
		public async Task<IActionResult> Index() {
			return View(await _context.Items.ToListAsync());
		}

		public IActionResult Create() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Name,Description")] Item item) {
			_context.Add(item);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

        public async Task<IActionResult> Delete(int? ID) {
            if (ID == null) {
                return NotFound();
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ID == ID);
            if (item == null) {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ID) {
            var item = await _context.Items.FindAsync(ID);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? ID) {
			if (ID == null) {
				return NotFound();
			}

			var item = await _context.Items.FindAsync(ID);
			if (item == null) {
				return NotFound();
			}
			return View(item);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int ID, [Bind("ID,Name,Description")] Item item) {
			if (ID != item.ID) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					_context.Update(item);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException) {
					if (ItemExists(item.ID)) {
						throw;
					}
					else {
						return NotFound();
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(item);
		}

		private bool ItemExists(int ID) {
			return _context.Items.Any(e => e.ID == ID);
		}
	}
}
