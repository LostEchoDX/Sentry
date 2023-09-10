using SentryAPI.Models;

namespace SentryAPI.Repositories
{
    public interface IRepository
    {
        IEnumerable<PoI> GetPoIs();
        PoI GetPoIById(int id);
        void InsertPoI(PoI poi);
        void UpdatePoI(PoI poi);
        void DeletePoI(int id);
        void SaveChanges();
    }
}
