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
    public class IndexModel : PageModel
    {
        private readonly SentryAPI.Data.SentryContext _context;

        public IndexModel(SentryAPI.Data.SentryContext context)
        {
            _context = context;
        }

        public IList<PoI> PoI { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PoIs != null)
            {
                PoI = await _context.PoIs.ToListAsync();
            }
        }
    }
}
