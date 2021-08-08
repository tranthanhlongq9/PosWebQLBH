using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace PosWebQLBH.ViewModels.Catalog.Units
{
    public class UnitUpdateRequest
    {
        [Display(Name = "Mã đơn vị:")]
        public string ID_Unit { get; set; }

        [Display(Name = "Tên đơn vị:")]
        public string Name_Unit { get; set; }

        [Display(Name = "Người tạo:")]
        public string CreatedBy { get; set; }

        [Display(Name = "Cập nhật bởi:")]
        public string UpdatedBy { get; set; }

    }
}
