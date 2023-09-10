using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SentryAPI.Data;
using SentryAPI.Models;

namespace SentryAPI.Pages.PoIs
{
    public class EditModel : PageModel
    {
        private readonly SentryAPI.Data.SentryContext _context;

        public EditModel(SentryAPI.Data.SentryContext context)
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

            var poi =  await _context.PoIs.FirstOrDefaultAsync(m => m.ID == id);
            if (poi == null)
            {
                return NotFound();
            }
            PoI = poi;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Entry(PoI).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoIExists(PoI.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PoIExists(int id)
        {
          return (_context.PoIs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
