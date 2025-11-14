using Data;
using Domain.Interfaces;

namespace Business.Ngc
{
    public class GeneralNgc : IGeneralItf
    {
        #region IOC

        private readonly IEfRepository _efRpstry;

        public GeneralNgc(IEfRepository efRpstry)
        {
            _efRpstry = efRpstry;
        }

        #endregion


    }
}
