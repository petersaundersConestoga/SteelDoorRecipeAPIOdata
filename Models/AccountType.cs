using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; }
    }
}
