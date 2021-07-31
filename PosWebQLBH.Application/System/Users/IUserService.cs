using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosWebQLBH.Application.System.Users
{
    //Làm việc với Database
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<ApiResult<PagedResult<UserVm>>> GetUserPaging(GetUserPagingRequest request);

        Task<ApiResult<UserVm>> GetUserById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);
    }
}