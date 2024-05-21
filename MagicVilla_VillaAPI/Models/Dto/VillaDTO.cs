using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int id { get; set; }
        [Required]
        [MaxLength(30)]
        public string name { get; set; }
        public string details { get; set; }
        [Required]
        public double rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string imageUrl { get; set; }
        public string amenity { get; set; }
    }
}
