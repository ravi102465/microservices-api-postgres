using System.ComponentModel.DataAnnotations.Schema;

namespace Film.Integration.API.Models.DTO
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        [Column("date_prod")]
        public DateTime DateProduce { get; set; }
        public string? Kind { get; set; }

        [Column("len")]
        public int LengthInMinutes { get; set; }
    }
}
