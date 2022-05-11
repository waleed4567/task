using project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project1.Data
{
    public class AutherRepo : IBook<Auther>
    {

        public List<Auther> Auth;
        private readonly DbContex _context;

        public AutherRepo(DbContex context)
        {
            _context = context;
        }
        public void add(Auther entity)
        {

            _context.authers.Add(entity);
        }

        public void delete(int id)
        {
            _context.Remove(_context.authers.SingleOrDefault(a => a.id == id));
        }

        public Auther find(int id)
        {
            return _context.authers.SingleOrDefault(a => a.id == id);
        }

        public IList<Auther> list()
        {
            return _context.authers.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void update(int id, Auther entity)
        {

            _context.authers.Update(entity);



        }

        public List<Auther> Search(string term)
        {
            var result = _context.authers.Where(a => a.FullName.Contains(term)).ToList();
            return result;
        }
    }
}
