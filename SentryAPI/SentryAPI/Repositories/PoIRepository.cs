using Microsoft.EntityFrameworkCore;
using SentryAPI.Data;
using SentryAPI.Models;

namespace SentryAPI.Repositories
{
    public class PoIRepository : IRepository
    {
        private SentryContext _context;
        public PoIRepository(SentryContext context)
        {
            _context = context;
        }
        public void DeletePoI(int id)
        {
            PoI poi = _context.PoIs.FirstOrDefault(m => m.ID == id);
            _context.PoIs.Remove(poi);
        }
        public PoI GetPoIById(int id)
        {
            return _context.PoIs.FirstOrDefault(m => m.ID == id);
        }
        public IEnumerable<PoI> GetPoIs()
        {
            return _context.PoIs.ToList();
        }
        public void InsertPoI(PoI poi)
        {
            _context.PoIs.Add(poi);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void UpdatePoI(PoI poi)
        {
            _context.Entry(poi).State = EntityState.Modified;
        }
    }
}
