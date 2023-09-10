using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SentryAPI.Data;
using SentryAPI.Models;

namespace SentryAPI.Pages.PoIs
{
    public class DeleteModel : PageModel
    {
        private readonly SentryAPI.Data.SentryContext _context;

        public DeleteModel(SentryAPI.Data.SentryContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PoI PoI { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PoIs == null)
            {
                return NotFound();
            }

            var poi = await _context.PoIs.FirstOrDefaultAsync(m => m.ID == id);

            if (poi == null)
            {
                return NotFound();
            }
            else 
            {
                PoI = poi;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PoIs == null)
            {
                return NotFound();
            }
            var poi = await _context.PoIs.FindAsync(id);

            if (poi != null)
            {
                PoI = poi;
                _context.PoIs.Remove(PoI);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
