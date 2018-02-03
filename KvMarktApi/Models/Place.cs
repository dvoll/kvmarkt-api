using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models {

    [Table("scheme_places")]
    public class Place : BaseObject
    { 
        public string Name { get; set; }
    }
}