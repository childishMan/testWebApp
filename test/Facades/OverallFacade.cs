using System;
using System.Collections.Generic;
using System.Linq;
using test.DbModels;
using test.Exceptions;
using test.Models;
using test.Services;

namespace test.Facades
{
    public class OverallFacade : IOverallFacade
    {
        private readonly IAccountService _accountService;
        private readonly IContactService _contactService;
        private readonly IIncidentService _incidentService;

        public OverallFacade(IAccountService accountService, IContactService contactService, IIncidentService incidentService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
            _incidentService = incidentService ?? throw new ArgumentNullException(nameof(incidentService));
        }


        public void AddAccount(AddAccountModel model)
        {
            if (_accountService.GetByName(model.Name) != null)
            {
                throw new AccountExist();
            }

            var contact = _contactService.GetContactByMail(model.ContactMail);

            if (contact == null)
            {
                throw new ContactNotFound();
            }

            var newAccount = new Account()
            {
                Name = model.Name,
                Contacts = new List<Contact>() {contact}
            };

            _accountService.Add(newAccount);
        }

        public void AddContact(AddContactModel model)
        {
            if (_contactService.GetContactByMail(model.Mail) != null)
            {
                throw new ContactExist();
            }

            var newContact = new Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mail = model.Mail
            };

            _contactService.Add(newContact);
        }

        public void AddIncident(AddIncidentModel model)
        {
            var account = _accountService.GetByName(model.AccountName);

            if (account == null)
            {
                throw new AccountNotFound();
            }

            var contact = _contactService.GetContactByMail(model.Mail);

            if (contact == null)
            {
                contact = new Contact()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Mail = model.Mail
                };
            }
            else
            {
                contact.FirstName = model.FirstName;
                contact.LastName = model.LastName;
            }

            if (!account.Contacts.Any(c => c.Mail == contact.Mail))
            {
                account.Contacts.Add(contact);
            }

            var newIncident = new Incident()
            {
                Description = model.Description,
                Accounts = new List<Account>() {account}
            };

            _incidentService.Add(newIncident);
        }



        public IEnumerable<KeyValuePair<string, string>> ValidateAccount(AddAccountModel model)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrWhiteSpace(model.ContactMail))
            {
                errors.Add(new KeyValuePair<string, string>("Mail","Mail can't be empty"));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                errors.Add(new KeyValuePair<string, string>("Name", "Name can't be empty"));
            }

            return errors;
        }
       
        public IEnumerable<KeyValuePair<string, string>> ValidateContact(AddContactModel model)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                errors.Add(new KeyValuePair<string, string>("FirstName","FirstName can't be empty"));
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                errors.Add(new KeyValuePair<string, string>("LastName", "LastName can't be empty"));
            }

            if (string.IsNullOrWhiteSpace(model.Mail))
            {
                errors.Add(new KeyValuePair<string, string>("Mail", "Mail can't be empty"));
            }

            return errors;
        }
        
        public IEnumerable<KeyValuePair<string, string>> ValidateIncident(AddIncidentModel model)
        {
            var errors = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrWhiteSpace(model.Mail))
            {
                errors.Add(new KeyValuePair<string, string>("Mail", "Mail can't be empty"));
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                errors.Add(new KeyValuePair<string, string>("FirstName", "Mail can't be empty"));
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                errors.Add(new KeyValuePair<string, string>("LastName", "Mail can't be empty"));
            }

            if (string.IsNullOrWhiteSpace(model.AccountName))
            {
                errors.Add(new KeyValuePair<string, string>("AccountName", "Mail can't be empty"));
            }

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                errors.Add(new KeyValuePair<string, string>("Description", "Mail can't be empty"));
            }

            return errors;
        }
    }
}