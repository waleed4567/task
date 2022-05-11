using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project1.Models
{
    public class Auther
    {
        public int id { get; set; }


        public String FullName { get; set; }
        public ICollection<Books> books { get; set; } = new List<Books>();
    }
}
