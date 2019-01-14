using System.ComponentModel.DataAnnotations.Schema;

namespace KvMarktApi.Models
{
    [Table("scheme_selectedPlaces")]
    public class SchemeSelectedPlace : BaseObject
    {
        [Column("Scheme")]
        public long SchemeId { get; set; }
        [ForeignKey(nameof(SchemeId))]
        public Scheme Scheme { get; set; }

        [Column("Place")]
        public long PlaceId { get; set; }
        [ForeignKey(nameof(PlaceId))]
        public Place Place { get; set; }
    }
}