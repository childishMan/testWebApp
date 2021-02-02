using Microsoft.EntityFrameworkCore;

namespace test.Repositories
{
    public interface IRepository
    {
        DbSet<T> GetRepository<T>() where T : class;
    }
}