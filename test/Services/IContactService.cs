using test.DbModels;

namespace test.Services
{
    public interface IContactService
    {
        void Add(Contact contact);

        Contact GetContactByMail(string mail);
    }
}