using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("scheme_categories")]
    public class Category : BaseObject
    {

        [Required]
        public string Name { get; set; }

    }

}
