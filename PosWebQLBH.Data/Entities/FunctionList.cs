using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class FunctionList
    {
        public FunctionList()
        {
            Roles = new HashSet<Role>();
        }

        public string IdFunction { get; set; }
        public string NameFunction { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
