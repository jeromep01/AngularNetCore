using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaseStudies.Core.Geography
{
    public class Country
    {
        public Country()
        {

        }

        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ISO2 { get; set; }

        public string ISO3 { get; set; }

        public List<City> Cities { get; set; }
    }
}
