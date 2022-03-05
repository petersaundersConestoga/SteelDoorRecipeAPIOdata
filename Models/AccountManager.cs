using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata.Models
{
    public partial class AccountManager
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public bool? IsLoggedIn { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastLogout { get; set; }
        public TimeSpan? LastActivity { get; set; }

        public virtual Person Person { get; set; } = null!;
    }
}
