using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.AdminApp.Models;
using PosWebQLBH.AdminApp.Services;
using PosWebQLBH.Utilities.Constants;
using System.Threading.Tasks;

namespace PosWebQLBH.AdminApp.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;

        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiClient = languageApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiClient.GetAll();
            var navigationVm = new NavigationViewModel()
            {
                CurrentLanguageId = HttpContext
                                        .Session
                                        .GetString(SystemConstants.AppSettings.DefaultLanguageId),

                Languages = languages.ResultObj
            };

            return View("Default", navigationVm);
        }
    }
}