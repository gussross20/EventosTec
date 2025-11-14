using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlumnoWebApiController : ControllerBase
    {
        #region IOC

        private readonly IAlumnoItf _alumnoNgc;
        public AlumnoWebApiController(IAlumnoItf alumnoNgc)
        {
            _alumnoNgc = alumnoNgc;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Obtener_Alumno_Detalle(string numeroControl)
        {
            var result = await _alumnoNgc.Obtener_Alumno_Detalle(numeroControl);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar_Alumno([FromBody] Dictionary<string, object> parametros)
        {
            var alumnoEtd = JsonConvert.DeserializeObject<AlumnoEtd>(parametros["alumno"].ToString() ?? string.Empty);
            var result = await _alumnoNgc.Registrar_Alumno(alumnoEtd!);
            return Ok(result);
        }
    }
}
