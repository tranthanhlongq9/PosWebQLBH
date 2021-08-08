using PosWebQLBH.ViewModels.Catalog.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    public interface IUnitApiClient
    {
        Task<List<UnitVmodel>> GetAll();
    }
}