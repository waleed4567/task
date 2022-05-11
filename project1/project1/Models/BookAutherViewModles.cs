using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace project1.Models
{
    public class BookAutherViewModles
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(5)]

        public String Title { get; set; }



        [Required]
        [MaxLength(125)]
        [MinLength(5)]

        public String Description { get; set; }

        public int AutherId { get; set; }


        public List<Auther> Authers { get; set; }
        [NotMapped]

        public IFormFile File { get; set; }

        public String image { get; set; }
    }
}
