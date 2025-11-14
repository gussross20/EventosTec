using Domain.Interfaces;

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

    }    
}