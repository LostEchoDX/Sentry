using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SentryAPI.Models;
using System.Text.Json;
using SentryAPI.Data;
using SentryAPI.Selenium;
using Microsoft.EntityFrameworkCore;
using SentryAPI.Repositories;

namespace SentryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SentryController : Controller
    {
        private List<string> keys = new List<string>() { "drone_id", "_class", "f_id", "picture", "latitude", "longitude"};
        private IRepository _poiRepository;

        public SentryController(IRepository poiRepository)
        {
            _poiRepository = poiRepository;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create(string json)
        {
            try
            {
                if (json == null)
                {
                    return BadRequest();
                }
                PoI poi = JsonSerializer.Deserialize<PoI>(json);
                //List<object> values = new List<object>() { poi.drone_id, poi._class, poi.f_id, poi.picture, 
                //    poi.latitude, poi.longitude};
                //SQL.CreateEntryLite("SPI", keys, values);
                _poiRepository.InsertPoI(poi);
                _poiRepository.SaveChanges();
                //return RedirectToAction(nameof(Index));
                return CreatedAtAction(nameof(GetLine), new { id = poi.ID }, poi);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: PoIController/Get
        [HttpGet("pois")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                //string json = SQL.ReadTableLite("SPI");
                //PoI[] poi = JsonSerializer.Deserialize<PoI[]>(json);
                //TestScript.ChromeSession();
                var records = _poiRepository.GetPoIs().ToList();

                if (records == null)
                {
                    return NotFound();
                }

                return Ok(records);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: PoIController/Get
        [HttpGet("pois/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLine(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                //string json = SQL.ReadLineLite("SPI", id);
                //PoI[] poi = JsonSerializer.Deserialize<PoI[]>(json);
                var record = _poiRepository.GetPoIById(id);

                if (record == null)
                {
                    return NotFound();
                }

                return Ok(record);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("pois/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                //string json = SQL.ReadLineLite("SPI", id);
                //PoI[] poi = JsonSerializer.Deserialize<PoI[]>(json);

                _poiRepository.DeletePoI(id);
                _poiRepository.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("pois/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, string json)
        {
            try
            {
                if (id == null || json == null)
                {
                    return NotFound();
                }

                //string json = SQL.ReadLineLite("SPI", id);
                //PoI[] poi = JsonSerializer.Deserialize<PoI[]>(json);

                var poiToUpdate = _poiRepository.GetPoIs().FirstOrDefault(m => m.ID == id);

                if (poiToUpdate == null)
                {
                    return NotFound();
                }

                PoI poi = JsonSerializer.Deserialize<PoI>(json);
                poiToUpdate.drone_id = poi.drone_id;
                poiToUpdate._class = poi._class;
                poiToUpdate.f_id = poi.f_id;
                poiToUpdate.picture = poi.picture;
                poiToUpdate.latitude = poi.latitude;
                poiToUpdate.longitude = poi.longitude;
                _poiRepository.UpdatePoI(poiToUpdate);
                _poiRepository.SaveChanges();
                return Ok(poi);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
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
