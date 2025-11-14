using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces;
using System.Reflection;

namespace Service.Srv
{
    public class AlumnoSrv : IAlumnoItf
    {
        const string _constWebApiRoute = "api/AlumnoWebApi/";

        #region IOC

        private readonly IHttpClientFactoryService _httpClientFactoryService;

        public AlumnoSrv(IHttpClientFactoryService httpClientFactoryService)
        {
            _httpClientFactoryService = httpClientFactoryService;
        }

        #endregion

        public Task<AlumnoEtd> Obtener_Alumno_Detalle(string numeroControl)
        {
            _httpClientFactoryService.Metodo = _constWebApiRoute + MethodBase.GetCurrentMethod()?.Name ?? "";
            _httpClientFactoryService.Parametros = new Dictionary<string, object>
            {
                { "numeroControl", numeroControl},
            };
            var result = _httpClientFactoryService.InvocarGetAsync<AlumnoEtd>();
            return result;
        }

        public Task<ProcesoDto<bool>> Registrar_Alumno(AlumnoEtd alumno)
        {
            _httpClientFactoryService.Metodo = _constWebApiRoute + MethodBase.GetCurrentMethod()?.Name ?? "";
            _httpClientFactoryService.Parametros = new Dictionary<string, object>
            {
                { "alumno", alumno},
            };
            var result = _httpClientFactoryService.InvocarPostAsJsonAsync<ProcesoDto<bool>>();
            return result;
        }
    }
}
