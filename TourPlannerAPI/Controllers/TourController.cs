using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using TourPlanner.Models;

namespace TourPlannerAPI.Controllers
{
    [Route("api/tour")]
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

            return Ok(await _context.Tours
                .Include(t => t.TourInfo)
                .Include(t => t.TourLogs)
                .ToListAsync());
        }

        [HttpGet("{tourId}")]
        public async Task<ActionResult<Tour>> Get(int tourId)
        {
            var tour = await _context.Tours
                .Include(t => t.TourInfo)
                .Include(t => t.TourLogs)
                .FirstOrDefaultAsync(t => t.TourId == tourId);

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


        [HttpPost("log")]
        public async Task<ActionResult<TourLog>> AddLog(TourLog log)
        {
            _context.TourLog.Add(log);
            await _context.SaveChangesAsync();

            return Ok(log);
        }

        [HttpPut]
        public async Task<ActionResult<Tour>> UpdateTour(Tour request)
        {
            var tour = await _context.Tours
                .Include(t => t.TourInfo)
                .Include(t => t.TourLogs)
                .FirstOrDefaultAsync(t => t.TourId == request.TourId);
            if (tour == null)
                return BadRequest("tour not found");

            var existingTourInfo = await _context.TourInfo.FindAsync(request.TourInfo?.TourInfoId);
            if (existingTourInfo == null)
                return BadRequest("tour info not found");

            
            existingTourInfo.From = request.TourInfo?.From;
            existingTourInfo.To = request.TourInfo?.To;
            existingTourInfo.Distance = request.TourInfo?.Distance;
            existingTourInfo.Description = request.TourInfo?.Description;
            existingTourInfo.TransportType = request.TourInfo?.TransportType;
            existingTourInfo.EstimatedTime = request.TourInfo?.EstimatedTime;
            tour.TourName = request.TourName;
            tour.TourInfo = existingTourInfo;

            tour.TourLogs = request.TourLogs;


            await _context.SaveChangesAsync();

            return Ok(tour);
        }

        [HttpDelete("{tourId}")]

        public async Task<ActionResult<List<Tour>>> Delete(int tourId)
        {
            var tour = await _context.Tours
                .Include(t => t.TourInfo)
                .FirstOrDefaultAsync(t => t.TourId == tourId);

            if (tour == null)
                return BadRequest("tour not found");

            var tourLogs = await _context.TourLog
                .Where(tl => tl.TourId == tourId)
                .ToListAsync();
            
            _context.TourLog.RemoveRange(tourLogs);

            var tourInfo = tour.TourInfo;

            if(tourInfo != null)
                _context.TourInfo.Remove(tourInfo);

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return Ok(tours);
        }

    }
}
