using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.ViewModels.Catalog.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Units
{
    public class UnitService : IUnitService
    {
        //khai báo
        private readonly DbQLBHContext _context;

        //gán vào constructor
        public UnitService(DbQLBHContext context)
        {
            _context = context;
        }

        public async Task<List<UnitVmodel>> GetAll()
        {
            var query = from u in _context.Units
                        select new { u };
            return await query.Select(x => new UnitVmodel()
            {
                IdUnit = x.u.IdUnit,
                NameUnit = x.u.NameUnit
            }).ToListAsync();
        }
    }
}