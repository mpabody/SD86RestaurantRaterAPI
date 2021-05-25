using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        [Required]
        [Range(0, 10)]
        public double FoodScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double EnvironmentScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double CleanlinessScore { get; set; }

        public double AverageRating
        {
            get
            {
                return (FoodScore + EnvironmentScore + CleanlinessScore) / 3;
            }
        }
    }
}