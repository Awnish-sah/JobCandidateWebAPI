using Domain;
using Microsoft.EntityFrameworkCore;
using Repo.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public void Update(T model)
        {
            _dbSet.Update(model);

        }

        public void Insert(T model)
        {
            _dbSet.Add(model);
        }
    }
}
