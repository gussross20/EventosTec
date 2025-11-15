using Domain.Interfaces;
using Service.Srv;

namespace Evento_Tecnologico_Web2025
{
    public static class IoC
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAlumnoItf, AlumnoSrv>();
            services.AddScoped<IInvitadoItf, InvitadoSrv>();
            services.AddScoped<IGeneralItf, GeneralSrv>();

            return services;
        }
    }
}
