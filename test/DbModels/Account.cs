using System.Collections.Generic;

namespace test.DbModels
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}