using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models
{

    public class Contributor : BaseObject
    {

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Column("Association")]
        public long AssociationId { get; set; }
        [ForeignKey(nameof(AssociationId))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Association Association { get; set; }


        public  ICollection<ContributorFavoriteScheme> ContributorFavoriteSchemes { get; set; }

        public  ICollection<ContributorTeam> ContributorTeams { get; set; }
    }

}
