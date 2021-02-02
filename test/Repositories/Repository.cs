using System;
using Microsoft.EntityFrameworkCore;

namespace test.Repositories
{
    public class Repository:IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public DbSet<T> GetRepository<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}