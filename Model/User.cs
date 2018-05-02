using System;
using System.ComponentModel.DataAnnotations;

namespace MWTest.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Userame { get; set; }
    }
}
