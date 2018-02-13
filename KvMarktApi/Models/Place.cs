using System.ComponentModel.DataAnnotations.Schema;

namespace KvMarktApi.Models {

    [Table("scheme_places")]
    public class Place : BaseObject
    { 
        public string Name { get; set; }
    }
}