using System.ComponentModel.DataAnnotations.Schema;

namespace KvMarktApi.Models {
    [Table("contributor_favoriteSchemes")]
    public class ContributorFavoriteScheme : BaseObject
    {
        [Column("Scheme")]
        public long SchemeId { get; set; }
        [ForeignKey(nameof(SchemeId))]
        public Scheme Scheme { get; set; }

        [Column("Contributor")]
        public long ContributorId { get; set; }
        [ForeignKey(nameof(ContributorId))]
        public Contributor Contributor { get; set; }
    }
}