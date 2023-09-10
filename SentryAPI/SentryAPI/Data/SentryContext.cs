using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SentryAPI.Models;

namespace SentryAPI.Data
{
    public class SentryContext : DbContext
    {
        public SentryContext(DbContextOptions<SentryContext> options) : base(options)
        {
        }

        public DbSet<PoI> PoIs { get; set; } = default!;
    }
}
