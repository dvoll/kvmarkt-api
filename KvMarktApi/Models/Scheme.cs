using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KvMarktApi.Models
{

    // [Table("Schemes")]
    public class Scheme : BaseObject
    {


        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }


        [Column("Author")]
        public long? AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Contributor Author { get; set; }

        // [Column("Place")]
        // public long PlaceId { get; set; }
        // [ForeignKey(nameof(PlaceId))]
        // public Place Place { get; set; }

        // [Column("Place2")]
        // public long? Place2Id { get; set; }
        // [ForeignKey(nameof(Place2Id))]
        // public Place Place2 { get; set; }

        // [Column("Place3")]
        // public long? Place3Id { get; set; }
        // [ForeignKey(nameof(Place3Id))]
        // public Place Place3 { get; set; }


        [Column("Category")]
        public long CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }


        [Column("age_start")]
        public int AgeStart { get; set; }
        [Column("age_end")]
        public int AgeEnd { get; set; }


        public ICollection<ContributorFavoriteScheme> ContributorFavoriteSchemes { get; set; }

        public ICollection<SchemeSelectedPlace> SchemeSelectedPlaces { get; set; }

        // public string Status { get; set; }
    }

    public class SchemeDto
    {

        public SchemeDto() { }
        public SchemeDto(Scheme x)
        {
            this.Id = x.Id;
            Title = x.Title;
            Description = x.Description;
            Content = x.Content;
            Author = x.AuthorId;
            // Places = x.SchemeSelectedPlaces != null ? x.SchemeSelectedPlaces.Where(sp => sp.Scheme.Id == x.Id).Select(sp => sp.Place.Id).ToList() : new List<long>();
            // Place = x.PlaceId;
            AgeStart = x.AgeStart;
            AgeEnd = x.AgeEnd;
        }
        public SchemeDto(SchemeDto x)
        {
            this.Id = x.Id;
            Title = x.Title;
            Description = x.Description;
            Content = x.Content;
            Author = x.Author;
            Places = x.Places;
            // Place = x.Place;
            AgeStart = x.AgeStart;
            AgeEnd = x.AgeEnd;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        // public long? Place { get; set; }
        // public string PlaceName { get; set; }
        // public long? Place2 { get; set; }
        // public string Place2Name { get; set; }
        // public long? Place3 { get; set; }
        // public string Place3Name { get; set; }

        public long? Category { get; set; }
        public string CategoryName { get; set; }


        public int AgeStart { get; set; }
        public int AgeEnd { get; set; }

        public long? Author { get; set; }
        public string AuthorName { get; set; }
        public bool IsFavorite { get; set; }

        public IEnumerable<long> Places { get; set; }
        // public IEnumerable<long> Fans { get; set; }
    }

}

