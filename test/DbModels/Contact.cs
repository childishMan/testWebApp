using System;

namespace test.DbModels
{
    public class Contact : BaseEntity
    {
        public string Mail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
