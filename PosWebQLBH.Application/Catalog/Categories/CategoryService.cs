using Microsoft.EntityFrameworkCore;
using PosWebQLBH.Data.Entities;
using PosWebQLBH.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        //khai báo
        private readonly DbQLBHContext _context;

        //gán vào constructor
        public CategoryService(DbQLBHContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryVm>> GetAll() //sau này thêm ngôn ngữ thì thêm 1 biến (string languageId)
        {
            var query = from c in _context.Categories
                        //join p in _context.Products on c.IdCategory equals p.IdCategory

                        //where c.LanguageId == languageId //sau này thêm language sẽ cần
                        select new { c };

            return await query.Select(x => new CategoryVm()
            {
                IdCate = x.c.IdCategory,
                NameCate = x.c.NameCategory
            }).ToListAsync();
        }
    }
}