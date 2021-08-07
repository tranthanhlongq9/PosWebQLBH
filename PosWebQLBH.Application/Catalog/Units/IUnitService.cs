using PosWebQLBH.ViewModels.Catalog.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Units
{
    public interface IUnitService
    {
        //lấy tất cả
        Task<List<UnitVmodel>> GetAll();
    }
}