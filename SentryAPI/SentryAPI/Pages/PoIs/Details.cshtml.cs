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
    public class DetailsModel : PageModel
    {
        private readonly SentryAPI.Data.SentryContext _context;

        public DetailsModel(SentryAPI.Data.SentryContext context)
        {
            _context = context;
        }

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
    }
}
