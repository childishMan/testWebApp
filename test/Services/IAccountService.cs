using test.DbModels;

namespace test.Services
{
    public interface IAccountService
    {
        void Add(Account account);

        Account GetByName(string name);
    }
}