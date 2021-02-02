using System.Collections.Generic;
using test.Models;

namespace test.Facades
{
    public interface IOverallFacade
    {
        void AddAccount(AddAccountModel model);

        void AddContact(AddContactModel model);

        void AddIncident(AddIncidentModel model);

        IEnumerable<KeyValuePair<string, string>> ValidateAccount(AddAccountModel model);

        IEnumerable<KeyValuePair<string, string>> ValidateContact(AddContactModel model);

        IEnumerable<KeyValuePair<string, string>> ValidateIncident(AddIncidentModel model);
    }
}