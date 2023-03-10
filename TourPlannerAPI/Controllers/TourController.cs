using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;

namespace TourPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private DataContext _context;

        public TourController(DataContext context)
        {
            _context = context;
        }

        private static List<Tour> tours = new List<Tour>();

        private static bool ToursFilled = false;


      
        [HttpGet]
        public async Task<ActionResult<List<Tour>>> Get()
        {

            return Ok(await _context.Tours.ToListAsync());
        }

        [HttpGet("{tourId}")]
        public async Task<ActionResult<Tour>> Get(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour == null)
                return BadRequest("tour not found");
            return Ok(tour);
        }


        [HttpPost]

        public async Task<ActionResult<List<Tour>>> AddTour(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            return Ok(_context.Tours);
        }

        [HttpPut]
        public async Task<ActionResult<Tour>> UpdateTour(Tour request)
        {
            var tour = await _context.Tours.FindAsync(request.TourId);
            if (tour == null)
                return BadRequest("tour not found");

            tour.TourName = request.TourName;

            await _context.SaveChangesAsync();

            return Ok(tour);
        }

        [HttpDelete("{tourId}")]

        public async Task<ActionResult<List<Tour>>> Delete(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour == null)
                return BadRequest("tour not found");

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return Ok(tours);
        }

    }
}
