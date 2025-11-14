using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeneralWebApiController : ControllerBase
    {
        #region IOC

        private readonly IGeneralItf _generalNgc;
        public GeneralWebApiController(IGeneralItf generalNgc)
        {
            _generalNgc = generalNgc;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Obtener_Lst_Elemento_Taller()
        {
            var result = await _generalNgc.Obtener_Lst_Elemento_Taller();
            return Ok(result);
        }
    }
}
