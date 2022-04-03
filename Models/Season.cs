using System;
using System.Collections.Generic;

namespace SteelDoorRecipeAPIOdata
{
    public partial class Season
    {
        public Season()
        {
            SeasonLists = new HashSet<SeasonList>();
        }

        public int Id { get; set; }
        public string SeasonName { get; set; } = null!;

        public virtual ICollection<SeasonList> SeasonLists { get; set; }
    }
}
