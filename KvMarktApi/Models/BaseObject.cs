using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public abstract class BaseObject
    {
        [Key]
        public long Id { get; set; }
        // public DateTime Created { get; set; }
        // public DateTime Updated { get; set; }
    }
}