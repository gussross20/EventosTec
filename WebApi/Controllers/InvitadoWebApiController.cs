using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvitadoWebApiController : ControllerBase
    {
        #region IOC

        private readonly IInvitadoItf _invitadoNgc;

        public InvitadoWebApiController(IInvitadoItf invitadoNgc)
        {
            _invitadoNgc = invitadoNgc;
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> Registrar_Invitado([FromBody] Dictionary<string, object> parametros)
        {
            var invitadoEtd = JsonConvert.DeserializeObject<InvitadoEtd>(parametros["invitado"].ToString() ?? string.Empty);
            var result = await _invitadoNgc.Registrar_Invitado(invitadoEtd!);
            return Ok(result);
        }

    }
}
