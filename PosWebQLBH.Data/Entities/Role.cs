using System;
using System.Collections.Generic;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public string IdRole { get; set; }
        public string IdFunction { get; set; }
        public string NameRole { get; set; }
        public string FeatureList { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual FunctionList IdFunctionNavigation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
