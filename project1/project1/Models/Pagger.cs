using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project1.Models
{
    public class Pagger
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public Pagger()
        {

        }
        public Pagger(int totalItems, int page, int pagesize = 10)
        {
            int taotalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pagesize);
            int currentPage = page;
            int startPage = currentPage - 5;
            int endPage = currentPage + 4;
            if(startPage <=0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if(endPage > taotalPages)
            {
                endPage = taotalPages;
                if(endPage >10)
                {
                    startPage = endPage - 9;
                }
            }
            TotalPage = taotalPages;
            CurrentPage = currentPage;
            PageSize = pagesize;
            TotalItems = totalItems;
            StartPage = startPage;
            EndPage = endPage;


        }
        
    }
}
