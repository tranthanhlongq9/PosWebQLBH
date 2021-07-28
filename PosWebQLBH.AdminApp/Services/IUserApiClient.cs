using PosWebQLBH.ViewModels.Common;
using PosWebQLBH.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Services
{
    //này sẽ làm việc với API
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);

        Task<PagedResult<UserVm>> GetUsersPagings(GetUserPagingRequest request);
    }
}