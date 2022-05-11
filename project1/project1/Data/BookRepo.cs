using project1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace project1.Data
{
    public class BookRepo : IBook<Books>
    {



        public List<Books> books;
        public AutherRepo authers;
        private readonly DbContex _context;
        public BookRepo(DbContex context)
        {
            _context = context;
        }

      


        public void add(Books entity)
        {
            _context.books.Add(entity);
        }

        public void delete(int id)
        {

            _context.books.Remove(_context.books.SingleOrDefault(a => a.Id == id));
        }

     

        public IList<Books> list()
        {


            return _context.books.ToList();
        }

        public void update(Books entity)
        {
            _context.books.Update(entity);



        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Books find(int id)
        {
            return _context.books.SingleOrDefault(a => a.Id == id);
        }

        public void update(int id, Books entity)
        {
            _context.books.Update(entity);
        }

        public List<Books> Search(string term)
        {
            var result = _context.books.Include(a => a.Auther).Where(b => b.Title.Contains(term) || b.Description.Contains(term) || b.Auther.FullName.Contains(term)).ToList();
            return result;
        }
    }
}
