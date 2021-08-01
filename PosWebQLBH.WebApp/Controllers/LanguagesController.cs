using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosWebQLBH.Application.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosWebQLBH.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(
            ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var products = await _languageService.GetAll();
            return Ok(products);
        }
    }
}