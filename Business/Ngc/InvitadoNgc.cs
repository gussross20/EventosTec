using Data;
using Domain.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Business.Ngc
{
    public class InvitadoNgc : IInvitadoItf
    {
        #region IOC

        private readonly IEfRepository _efRpstry;
        private readonly EmailService _emailService;

        public InvitadoNgc(
            IEfRepository efRpstry, 
            EmailService emailService)
        {
            _efRpstry = efRpstry;
            _emailService = emailService;
        }

        #endregion

        public async Task<ProcesoDto<bool>> Registrar_Invitado(InvitadoEtd invitado)
        {
            var proceso = new ProcesoDto<bool>();
            proceso.Resultado = true;

            try
            {
                #region VALIDAR EMAIL

                var hayMismoEmail = _efRpstry.Queryanle<InvitadoEtd>()
                    .Any(i => i.CorreoElectronico == invitado.CorreoElectronico);

                if (hayMismoEmail)
                {
                    proceso.Resultado = false;
                    proceso.Mensaje = "Ya hay un correo en el sistema.";
                    return proceso;
                }

                #endregion

                _efRpstry.BeginTransaction();

                #region AGREGAR Invitado

                invitado.FechaRegistro = DateTime.Now;

                _efRpstry.Add(invitado);
                await _efRpstry.SaveChangesAsync();

                #endregion

                #region ENVIAR EMAIL

                var invitadoParaCorreo_Etd = await _efRpstry.Queryanle<InvitadoEtd>()
                    .Where(i => i.Id == invitado.Id)
                    .SingleAsync();

                var bodyCorreo = _emailService.Obtener_Body_ParaInvitado(invitadoParaCorreo_Etd!);
                _emailService.EnviarCorreoConQr(invitado.CorreoElectronico, invitado.Id.ToString(), bodyCorreo);

                #endregion

                await _efRpstry.CommitAsync()!;

                proceso.Resultado = true;
                proceso.Mensaje = "Se ha registrado el invitado para el evento";
            }
            catch
            {
                await _efRpstry.RollbackAsync()!;
                proceso.Resultado = false;
                proceso.Mensaje = "Ocurrió un error al registrar el invitado al evento.";
                throw;
            }
            finally
            {
                _efRpstry.CloseTransaction();
            }

            return proceso;
        }        
    }
}
