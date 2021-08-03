using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly IConfiguration _config;
        private readonly DbQLBHContext _context;

        public LanguageService(DbQLBHContext context,
            IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public async Task<ApiResult<List<LanguageVm>>> GetAll()
        {
            var languages = await _context.Languages.Select(x => new LanguageVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new ApiSuccessResult<List<LanguageVm>>(languages);
        }
    }
}