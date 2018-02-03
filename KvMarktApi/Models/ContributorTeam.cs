using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("team_contributorOfTeam")]
    public class ContributorTeam : BaseObject
    {
        [Column("Team")]
        public long TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }

        [Column("Contributor")]
        public long ContributorId { get; set; }
        [ForeignKey(nameof(ContributorId))]
        public Contributor Contributor { get; set; }
    }
}