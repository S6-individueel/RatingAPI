using Microsoft.AspNetCore.Mvc;
using RatingAPI.Models;
using RatingAPI.RatingData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {
        private IRatingData _ratingData;

        public RatingController(IRatingData ratingData)
        {
            _ratingData = ratingData;
        }

        [HttpGet]
        public IActionResult GetAllRatings()
        {
            return Ok(_ratingData.GetAllRatings());
        }

        [HttpGet("{id}")]
        public IActionResult GetRatingById(int id)
        {
            var rating = _ratingData.GetRatingById(id);

            if (rating != null)
            {
                return Ok(rating);
            }
            return NotFound($"Rating with id: {id} was not found");
        }

        [HttpPost]
        public IActionResult AddRating(Rating rating)
        {
            _ratingData.AddRating(rating);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + rating.Id, rating);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id)
        {
            var rating = _ratingData.GetRatingById(id);

            if (rating != null)
            {
                _ratingData.DeleteRating(rating);
                return Ok();
            }
            return NotFound($"Rating with id: {id} was not found");
        }

        [HttpPatch("{id}")]
        public IActionResult ModifyRating(int id, Rating rating)
        {
            var existingRating = _ratingData.GetRatingById(id);

            if (existingRating != null)
            {
                rating.Id = existingRating.Id;
                rating.MovieId = existingRating.MovieId;
                rating.UserId = existingRating.UserId;
                _ratingData.ModifyRating(rating);
                return Ok();
            }
            return NotFound($"Rating with id: {id} was not found");
        }

        [HttpGet("movie/{id}")]
        public IActionResult GetRatingsByMovieId(int id)
        {
            var existingMovieRatings = _ratingData.GetRatingsByMovieId(id);

            if (existingMovieRatings != null)
            {
                return Ok(existingMovieRatings);
            }
            return NotFound($"Ratings for the movie with id: {id} was not found");
        }

        [HttpGet("user/{id}")]
        public IActionResult GetRatingsByUserId(Guid id)
        {
            var existingUserRatings = _ratingData.GetRatingsByUserId(id);

            if (existingUserRatings != null)
            {
                return Ok(existingUserRatings);
            }
            return NotFound($"Ratings for the user with id: {id} was not found");
        }
    }
}
