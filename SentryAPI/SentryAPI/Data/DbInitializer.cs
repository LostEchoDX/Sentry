using SentryAPI.Models;
using SentryAPI.Repositories;

namespace SentryAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IRepository repository)
        {
            // Look for any students.
            if (repository.GetPoIs().Any())
            {
                return;   // DB has been seeded
            }

            var pois = new PoI[]
            {
                new PoI{drone_id="a", _class="land",f_id="friend",picture="a", latitude="48.84616278031766", longitude="2.334603986455451"},
            new PoI{drone_id="a", _class="air",f_id="hostile",picture="a", latitude="48.84616278031766", longitude="2.334603986455451"},
            new PoI{drone_id="a", _class="sea",f_id="neutral",picture="a", latitude="48.84616278031766", longitude="2.334603986455451"},
            new PoI{drone_id="a", _class="civilian",f_id="friend",picture="a", latitude="48.84616278031766", longitude="2.334603986455451"},
            };

            repository.GetPoIs().ToList().AddRange(pois);
            repository.SaveChanges();
        }
    }
}