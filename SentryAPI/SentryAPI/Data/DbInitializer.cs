using SentryAPI.Models;

namespace SentryAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SentryContext context)
        {
            // Look for any students.
            if (context.PoI.Any())
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

            context.PoI.AddRange(pois);
            context.SaveChanges();
        }
    }
}