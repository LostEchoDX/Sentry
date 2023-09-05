using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SentryAPI.Data;
using SentryAPI.Models;

namespace SentryAPI.Pages.PoIs
{
    public class CreateModel : PageModel
    {
        private readonly SentryAPI.Data.SentryContext _context;

        public CreateModel(SentryAPI.Data.SentryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PoI PoI { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.PoI == null || PoI == null)
            {
                return Page();
            }

            _context.PoI.Add(PoI);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
