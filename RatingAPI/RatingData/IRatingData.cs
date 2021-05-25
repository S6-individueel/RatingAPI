using RatingAPI.Models;
using System;
using System.Collections.Generic;

namespace RatingAPI.RatingData
{
    public interface IRatingData
    {
        List<Rating> GetAllRatings();

        Rating GetRatingById(int id);

        List<Rating> GetRatingsByUserId(Guid id);

        List<Rating> GetRatingsByMovieId(int id);

        Rating AddRating(Rating rating);

        void DeleteRating(Rating rating);

        Rating ModifyRating(Rating rating);
    }
}
