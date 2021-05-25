using Microsoft.EntityFrameworkCore;

namespace RatingAPI.Models
{
    public class RatingsContext : DbContext
    {
        public RatingsContext(DbContextOptions<RatingsContext> options) : base(options)
        {
        }

        public DbSet<Rating> Ratings { get; set; }
    }
}
