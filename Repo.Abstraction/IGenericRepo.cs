using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Abstraction
{
    public interface IGenericRepo<T> where T : class
    {
        IQueryable<T> GetAll();
        void Update(T model);
        void Insert(T model);

    }
}
