using System.ComponentModel.DataAnnotations;
using static NationalParkApi.Models.Trail;

namespace NationalParkApi.Models.DTOs
{
    public class TrailDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        public string Elevation { get; set; }
        public  DifficultyType Difficulty { get; set; }

        public int NationalParkId {  get; set; }
        public NationalPark NationalPark { get; set; }
    }
}
