using System;
using System.ComponentModel.DataAnnotations;

namespace BestTestPlayers.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Name should be of minimum length 1 and maximum length 60", MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth"),DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Type of player is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Country Name is required")]
        public string Country { get; set; }

        [Display(Name = "Highest Score")]
        [Range(50, 50000, ErrorMessage = "Batsman should have scored at leat 50. ")]
        public int HighestScore { get; set; }

        [Required(ErrorMessage = "Average is required")]
        [RegularExpression("[4-9]{1}[0-9]{1}[.][0-9]{2}", ErrorMessage = "Average should be at least 40  and follow this format 40.34")]
        public string Average { get; set; }

        [Display(Name = "Matches")]
        [Required(ErrorMessage = "Number of matches is required")]
        [Range(50, int.MaxValue, ErrorMessage = "Number of matches should be at least 50")]
        public int Matches { get; set; }
    }
}
