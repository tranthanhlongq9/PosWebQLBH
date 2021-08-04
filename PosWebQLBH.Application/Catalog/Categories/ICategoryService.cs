using PosWebQLBH.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        //lấy tất cả
        Task<List<CategoryVm>> GetAll();
    }
}