using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatingAPI.Models
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int UserRating { get; set; }
    }
}
