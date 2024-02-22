using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutritionApp.Data.Data;
using NutritionApp.Data.Repository.IRepository;

namespace NutritionApp.Data.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly ApplicationDbContext _context;
		internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
			dbSet = _context.Set<T>();
        }
        public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public IEnumerable<T> FindAll()
		{
			throw new NotImplementedException();
		}

		public T Get(Expression<Func<T, bool>> filter)
		{
			return dbSet.Where(filter).FirstOrDefault();
		}

		public IEnumerable<T> GetAll()
		{
			return dbSet.ToList();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		
	}
}
