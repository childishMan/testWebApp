using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using test.DbModels;
using test.Repositories;

namespace test.Services
{
    public class AccountService:IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Account> _accountRepository;

        public AccountService(IRepository repository,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _accountRepository = repository.GetRepository<Account>() ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(Account account)
        {
            _accountRepository.Add(account);
            Save();
        }

        public Account GetByName(string name)
        {
            name = name.ToLowerInvariant();

            return _accountRepository
                .Include(a=>a.Contacts)
                .FirstOrDefault(a => a.Name.ToLower() == name);
        }

        private void Save()
        {
            _unitOfWork.SaveChanges();
        }
    }
}