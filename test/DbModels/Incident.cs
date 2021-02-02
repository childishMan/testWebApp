using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace test.DbModels
{
    public class Incident
    {
        [Key]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}