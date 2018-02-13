using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KvMarktApi.Models
{

    public class Association : BaseObject
    {
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public ICollection<Team> Teams { get; set; }

    }
}