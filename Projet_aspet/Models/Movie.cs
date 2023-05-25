using Projet_aspet.data;
using Projet_aspet.data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Projet_aspet.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public string cinemaName { get; set; }
        public string ProducedName { get; set; }
    }
}
