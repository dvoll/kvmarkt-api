using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models {

    public class Team : BaseObject
    { 
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        [Column("Association")]
        public long AssociationId { get; set; }
        [ForeignKey(nameof(AssociationId))]
        public Association Association { get; set; }


        public ICollection<ContributorTeam> ContributorTeams { get; set; }

    }
}