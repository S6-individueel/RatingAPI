using RatingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RatingAPI.RatingData
{
    public class SqlRatingData : IRatingData
    {
        private RatingsContext _ratingsContext;

        public SqlRatingData(RatingsContext ratingsContext)
        {
            _ratingsContext = ratingsContext;
        }

        public Rating AddRating(Rating rating)
        {
            _ratingsContext.Add(rating);
            _ratingsContext.SaveChanges();
            return rating;
        }

        public void DeleteRating(Rating rating)
        {
            _ratingsContext.Remove(rating);
            _ratingsContext.SaveChanges();
        }

        public List<Rating> GetAllRatings()
        {
            return _ratingsContext.Ratings.ToList();
        }

        public Rating ModifyRating(Rating rating)
        {
            var existingRating = _ratingsContext.Ratings.Find(rating.Id);
            if (existingRating != null)
            {
                existingRating.UserRating = rating.UserRating;

                _ratingsContext.Ratings.Update(existingRating);
                _ratingsContext.SaveChanges();
            }
            return rating;
        }

        public List<Rating> GetRatingsByUserId(Guid id)
        {
            List<Rating> ratings = new List<Rating>();
            foreach (var rating in _ratingsContext.Ratings.Where(r => r.UserId == id))
            {
                ratings.Add(new Rating()
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    MovieId = rating.MovieId,
                    UserRating = rating.UserRating
                });
            }
            return ratings;
        }

        public List<Rating> GetRatingsByMovieId(int id)
        {
            List<Rating> ratings = new List<Rating>();
            foreach (var rating in _ratingsContext.Ratings.Where(r => r.MovieId == id))
            {
                ratings.Add(new Rating()
                {
                    Id = rating.Id,
                    UserId = rating.UserId,
                    MovieId = rating.MovieId,
                    UserRating = rating.UserRating
                });
            }
            return ratings;
        }

        public Rating GetRatingById(int id)
        {
            return _ratingsContext.Ratings.Find(id);
        }

        public void DeleteRatings(Guid id)
        {
            foreach (var comment in _ratingsContext.Ratings.Where(r => r.UserId == id))
            {
                _ratingsContext.Remove(comment);
                _ratingsContext.SaveChanges();
            }
        }
    }
}
