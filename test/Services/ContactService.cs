using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using test.DbModels;
using test.Repositories;

namespace test.Services
{
    public class ContactService:IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Contact> _contactRepository;

        public ContactService(IRepository repository,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _contactRepository = repository?.GetRepository<Contact>() ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Add(Contact contact)
        {
            _contactRepository.Add(contact);
            Save();
        }

        public Contact GetContactByMail(string mail)
        {
            mail = mail.ToLowerInvariant();
            return _contactRepository.FirstOrDefault(c => c.Mail.ToLower() == mail);
        }

        private void Save()
        {
            _unitOfWork.SaveChanges();
        }
    }
}