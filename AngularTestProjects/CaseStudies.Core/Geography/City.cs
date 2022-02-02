using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudies.Core.Geography
{
    public class City
    {
        public City()
        {

        }

        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Name_ASCII { get; set; }

        [Column(TypeName = "decimal(7,4)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(7,4)")]
        public decimal Longitude { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
