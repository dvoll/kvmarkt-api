using System;
using System.ComponentModel.DataAnnotations;

namespace KvMarktApi.Models
{
    public abstract class BaseObject
    {
        [Key]
        public long Id { get; set; }
        // public DateTime Created { get; set; }
        // public DateTime Updated { get; set; }
    }
}