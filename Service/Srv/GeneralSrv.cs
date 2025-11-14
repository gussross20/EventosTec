using Domain.Dto;
using Domain.Interfaces;
using System.Reflection;

namespace Service.Srv
{
    public class GeneralSrv : IGeneralItf
    {
        const string _constWebApiRoute = "api/GeneralWebApi/";

        #region IOC

        private readonly IHttpClientFactoryService _httpClientFactoryService;

        public GeneralSrv(IHttpClientFactoryService httpClientFactoryService)
        {
            _httpClientFactoryService = httpClientFactoryService;
        }

        #endregion        

        public Task<List<ElementoDto<byte>>> Obtener_Lst_Elemento_Taller()
        {
            _httpClientFactoryService.Metodo = _constWebApiRoute + MethodBase.GetCurrentMethod()?.Name ?? "";
            var result = _httpClientFactoryService.InvocarGetAsync<List<ElementoDto<byte>>>();
            return result;
        }
    }    
}
