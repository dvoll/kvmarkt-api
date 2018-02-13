using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KvMarktApi.Models
{
    [Table("scheme_categories")]
    public class Category : BaseObject
    {

        [Required]
        public string Name { get; set; }

    }

}
