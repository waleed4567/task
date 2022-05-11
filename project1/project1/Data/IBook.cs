using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace project1.Data
{
    public interface IBook<TEntity>
    {


        IList<TEntity> list();

        TEntity find(int id);

        void add(TEntity entity);

        void update(int id, TEntity entity);
        void delete(int id);
        bool SaveChanges();
        List<TEntity> Search(string term);

    }
}

