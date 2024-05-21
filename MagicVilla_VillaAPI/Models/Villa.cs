using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public double rate { get; set; }
        public int sqft { get; set; }
        public int occupancy { get; set; }
        public string imageUrl { get; set; }
        public string amenity { get; set; }
        public DateTime createDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
