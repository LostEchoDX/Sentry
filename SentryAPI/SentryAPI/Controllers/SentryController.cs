using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SentryAPI.Models;
using System.Text.Json;
using SentryAPI.Data;
using SentryAPI.Selenium;

namespace SentryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SentryController : Controller
    {
        private List<string> keys = new List<string>() { "drone_id", "_class", "f_id", "picture", "latitude", "longitude"};
        private SentryContext _context;

        public SentryController(SentryContext context)
        {
            _context = context;
        }

        // GET: PoIController
        [HttpGet("map")]
        public ActionResult Index()
        {
            return View();
        }

        //// GET: PoIController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PoIController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: PoIController/Create
        [HttpPost("pois")]
        public async void Create(string json)
        {
            try
            {
                PoI poi = JsonSerializer.Deserialize<PoI>(json);
                List<object> values = new List<object>() { poi.drone_id, poi._class, poi.f_id, poi.picture, 
                    poi.latitude, poi.longitude};
                //SQL.CreateEntryLite("SPI", keys, values);
                _context.Add(poi);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                //return View();
            }
        }

        // GET: PoIController/Get
        [HttpGet("pois")]
        public List<PoI> Get()
        {
            try
            {
                //string json = SQL.ReadTableLite("SPI");
                //PoI[] poi = JsonSerializer.Deserialize<PoI[]>(json);
                TestScript.ChromeSession();
                return _context.PoI.ToList(); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // GET: PoIController/Get
        [HttpGet("pois/{id}")]
        public PoI GetLine(int id)
        {
            try
            {
                //string json = SQL.ReadLineLite("SPI", id);
                //PoI[] poi = JsonSerializer.Deserialize<PoI[]>(json);
                return _context.PoI.FirstOrDefault(m => m.ID == id);
            }
            catch
            {
                return null;
            }
        }

        // GET: PoIController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PoIController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PoIController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PoIController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
