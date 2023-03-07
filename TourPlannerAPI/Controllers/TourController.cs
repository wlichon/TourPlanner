using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TourPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {

        private static List<Tour> tours = new List<Tour>();

        private static bool ToursFilled = false;
        static private void FillTours()
        {

            for (int i = 0; i < 5; i++)
            {
                tours.Add(new Tour
                {
                    TourId = i,
                    TourName = "Andreaspark"
                });

            }

            for (int i = 0; i < 5; i++)
            {
                tours.Add(new Tour
                {
                    TourId = i + 5,
                    TourName = "Schoenbrunn"
                });

            }

            for (int i = 0; i < 5; i++)
            {
                tours.Add(new Tour
                {
                    TourId = i + 10,
                    TourName = "Kahlenberg"
                });

            }

            ToursFilled = true;
        }

        public TourController() : base()
        {
            if (!ToursFilled)
                FillTours();
        }

        [HttpGet]
        public async Task<ActionResult<List<Tour>>> Get()
        {
            return Ok(tours);
        }

        [HttpGet("{tourId}")]
        public async Task<ActionResult<Tour>> Get(int tourId)
        {
            var tour = tours.Find(tour => tour.TourId == tourId);
            if (tour == null)
                return BadRequest("tour not found");
            return Ok(tour);
        }


        [HttpPost]

        public async Task<ActionResult<List<Tour>>> AddTour(Tour tour)
        {
            tours.Add(tour);
            return Ok(tours);
        }

        [HttpPut]
        public async Task<ActionResult<List<Tour>>> UpdateTour(Tour request)
        {
            var tour = tours.Find(tour => tour.TourId == request.TourId);
            if (tour == null)
                return BadRequest("tour not found");

            tour.TourName = request.TourName;

            return Ok(tour);
        }

        [HttpDelete("{tourId}")]

        public async Task<ActionResult<List<Tour>>> Delete(int tourId)
        {
            var tour = tours.Find(tour => tour.TourId == tourId);
            if (tour == null)
                return BadRequest("tour not found");

            tours.Remove(tour);
            return Ok(tours);
        }

    }
}
