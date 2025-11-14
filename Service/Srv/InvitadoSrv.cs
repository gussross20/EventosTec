using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces;
using System.Reflection;

namespace Service.Srv
{
    public class InvitadoSrv : IInvitadoItf
    {
        const string _constWebApiRoute = "api/InvitadoWebApi/";

        #region IOC

        private readonly IHttpClientFactoryService _httpClientFactoryService;

        public InvitadoSrv(IHttpClientFactoryService httpClientFactoryService)
        {
            _httpClientFactoryService = httpClientFactoryService;
        }

        #endregion

        public Task<ProcesoDto<bool>> Registrar_Invitado(InvitadoEtd invitado)
        {
            _httpClientFactoryService.Metodo = _constWebApiRoute + MethodBase.GetCurrentMethod()?.Name ?? "";
            _httpClientFactoryService.Parametros = new Dictionary<string, object>
            {
                { "invitado", invitado},
            };
            var result = _httpClientFactoryService.InvocarPostAsJsonAsync<ProcesoDto<bool>>();
            return result;
        }
    }
}
