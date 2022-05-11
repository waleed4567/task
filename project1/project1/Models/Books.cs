using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project1.Models
{
    public class Books
    {
        [Key]

        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(5)]

        public String Title { get; set; }

        [Required]
        [MaxLength(125)]
        [MinLength(5)]

        public String Description { get; set; }
        public int autherId { get; set; }

        public Auther Auther { get; set; }
        public String Image { get; set; }
    }
}
