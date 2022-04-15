using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteelDoorRecipeAPIOdata
{
    public partial class Person
    {
        public Person()
        {
            AccountManagers = new HashSet<AccountManager>();
            PersonReviews = new HashSet<PersonReview>();
            Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool? EmailUpdates { get; set; }
        public bool? EmailNewsletter { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int FailedLoginCount { get; set; }
        public string About { get; set; } = null!;
        public string Image { get; set; } = null!;
        public byte[]? File { get; set; }

        public virtual AccountType AccountType { get; set; } = null!;
        public virtual ICollection<AccountManager> AccountManagers { get; set; }
        public virtual ICollection<PersonReview> PersonReviews { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
