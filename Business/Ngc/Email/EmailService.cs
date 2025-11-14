using Business.Ngc.Qr;
using Business.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.IO;
using Domain.Entities;

public class EmailService
{
    #region IoC

    readonly IOptions<EmailSettings> _mailSettings;
    string logoUrl = "https://drive.google.com/uc?export=download&id=1uXwHctY5p8ZTClj97UQAt5AsJSuihb3b";

    public EmailService(IOptions<EmailSettings> mailSettings)
    {
        _mailSettings = mailSettings;
    }

    #endregion

    public void EnviarCorreoConQr(string destinatario, string textoQr, string bodyHtml, string nombreDestinatario = "Usuario")
    {
        try
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress(_mailSettings.Value.DisplayName, _mailSettings.Value.From));
            mensaje.To.Add(new MailboxAddress(nombreDestinatario, destinatario));
            mensaje.Subject = "Registro al día del informático 2025";

            var builder = new BodyBuilder();

            var qrAttachment = GenerateQrCode.Obtener_Qr_PorTexto(textoQr);
            if (qrAttachment != null)
            {
                using (var stream = new MemoryStream())
                {
                    qrAttachment.ContentStream.CopyTo(stream);
                    var mimeType = qrAttachment.ContentType?.MediaType ?? "image/png";
                    builder.Attachments.Add("codigo-qr.png", stream.ToArray(), MimeKit.ContentType.Parse(mimeType));
                }
            }

            builder.HtmlBody = bodyHtml;
            mensaje.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Value.Host, _mailSettings.Value.Port, _mailSettings.Value.UseSSL);

            if (_mailSettings.Value.UseStartTls)
            {
                smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp.Authenticate(_mailSettings.Value.UserName, _mailSettings.Value.Password);
            }

            smtp.Send(mensaje);
            smtp.Disconnect(true);

            Console.WriteLine("Correo enviado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalle del error: {ex.InnerException.Message}");
            }
        }
    }

    public string Obtener_Body_ParaAlumno(AlumnoEtd alumno)
    {
        return @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            <html dir=""ltr"" lang=""es"">
              <head>
                <meta content=""text/html; charset=UTF-8"" http-equiv=""Content-Type"" />
                <meta name=""x-apple-disable-message-reformatting"" />
              </head>
              <body
                style='background-color:#fff;font-family:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,Oxygen-Sans,Ubuntu,Cantarell,""Helvetica Neue"",sans-serif'>
                <div
                  style=""display:none;overflow:hidden;line-height:1px;opacity:0;max-height:0;max-width:0"">
                  Tu código QR está listo para usar
                </div>
                <table
                  align=""center""
                  width=""100%""
                  border=""0""
                  cellpadding=""0""
                  cellspacing=""0""
                  role=""presentation""
                  style=""max-width:37.5em"">
                  <tbody>
                    <tr style=""width:100%"">
                      <td>
                        <table
                          align=""center""
                          width=""100%""
                          border=""0""
                          cellpadding=""0""
                          cellspacing=""0""
                          role=""presentation""
                          style=""padding:30px 20px"">
                          <tbody>
                            <tr>
                              <td align=""center"">
                                <img src=""" + logoUrl + @""" alt=""TecSerdan"" style=""max-width:200px;height:auto;"" />
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <table
                          align=""center""
                          width=""100%""
                          border=""0""
                          cellpadding=""0""
                          cellspacing=""0""
                          role=""presentation""
                          style=""border:1px solid rgb(0,0,0, 0.1);border-radius:3px;overflow:hidden"">
                          <tbody>
                            <tr>
                              <td>
                                <table
                                  align=""center""
                                  width=""100%""
                                  border=""0""
                                  cellpadding=""0""
                                  cellspacing=""0""
                                  role=""presentation"">
                                  <tbody style=""width:100%"">
                                    <tr style=""width:100%"">
                                      <td style=""background-color:#4154f1;height:10px;width:100%""></td>
                                    </tr>
                                  </tbody>
                                </table>
                                <table
                                  align=""center""
                                  width=""100%""
                                  border=""0""
                                  cellpadding=""0""
                                  cellspacing=""0""
                                  role=""presentation""
                                  style=""padding:20px;padding-bottom:0"">
                                  <tbody style=""width:100%"">
                                    <tr style=""width:100%"">
                                      <td data-id=""__react-email-column"">
                                        <h1
                                          style=""font-size:32px;font-weight:bold;text-align:center"">
                                          Hola " + alumno.Nombre + @",
                                        </h1>
                                        <h2
                                          style=""font-size:26px;font-weight:bold;text-align:center;color:#4154f1"">
                                          Tu registro ha sido exitoso!!!.
                                        </h2>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-bottom:16px;margin-top:16px"">
                                          Gracias por registrarte al evento del día del día del informático en el instituto tecnologico superior de ciudad Serdan.
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Fecha de registro: </b>" + (alumno.FechaRegistro?.ToString("dd/MM/yyyy HH:mm") ?? "No disponible") + @"
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Número de control: </b>" + alumno.NumeroControl + @"
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Nombre completo: </b>" + alumno.Nombre + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + @"
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Carrera: </b>" + alumno.Carrera + @"
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Para utilizar tu código QR: </b>
                                        </p>
                                        <ol style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <li>Descarga el archivo adjunto</li>
                                          <li>Imprímelo o guárdalo en tu dispositivo</li>
                                          <li>Escanéalo el día del evento</li>
                                        </ol>                                
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>                        
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <p
                          style=""font-size:12px;line-height:24px;text-align:center;color:rgb(0,0,0, 0.7);margin-bottom:16px;margin-top:16px"">
                          © " + DateTime.Now.Year + @" | Ingeniería informática
                        </p>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </body>
            </html>";
    }

    public string Obtener_Body_ParaInvitado(InvitadoEtd invitado)
    {
        return @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            <html dir=""ltr"" lang=""es"">
              <head>
                <meta content=""text/html; charset=UTF-8"" http-equiv=""Content-Type"" />
                <meta name=""x-apple-disable-message-reformatting"" />
              </head>
              <body
                style='background-color:#fff;font-family:-apple-system,BlinkMacSystemFont,""Segoe UI"",Roboto,Oxygen-Sans,Ubuntu,Cantarell,""Helvetica Neue"",sans-serif'>
                <div
                  style=""display:none;overflow:hidden;line-height:1px;opacity:0;max-height:0;max-width:0"">
                  Tu código QR está listo para usar
                </div>
                <table
                  align=""center""
                  width=""100%""
                  border=""0""
                  cellpadding=""0""
                  cellspacing=""0""
                  role=""presentation""
                  style=""max-width:37.5em"">
                  <tbody>
                    <tr style=""width:100%"">
                      <td>
                        <table
                          align=""center""
                          width=""100%""
                          border=""0""
                          cellpadding=""0""
                          cellspacing=""0""
                          role=""presentation""
                          style=""padding:30px 20px"">
                          <tbody>
                            <tr>
                              <td align=""center"">                              
                                <img src=""" + logoUrl + @""" alt=""TecSerdan"" style=""max-width:200px;height:auto;"" />
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <table
                          align=""center""
                          width=""100%""
                          border=""0""
                          cellpadding=""0""
                          cellspacing=""0""
                          role=""presentation""
                          style=""border:1px solid rgb(0,0,0, 0.1);border-radius:3px;overflow:hidden"">
                          <tbody>
                            <tr>
                              <td>
                                <table
                                  align=""center""
                                  width=""100%""
                                  border=""0""
                                  cellpadding=""0""
                                  cellspacing=""0""
                                  role=""presentation"">
                                  <tbody style=""width:100%"">
                                    <tr style=""width:100%"">
                                      <td style=""background-color:#4154f1;height:10px;width:100%""></td>
                                    </tr>
                                  </tbody>
                                </table>
                                <table
                                  align=""center""
                                  width=""100%""
                                  border=""0""
                                  cellpadding=""0""
                                  cellspacing=""0""
                                  role=""presentation""
                                  style=""padding:20px;padding-bottom:0"">
                                  <tbody style=""width:100%"">
                                    <tr style=""width:100%"">
                                      <td data-id=""__react-email-column"">
                                        <h1
                                          style=""font-size:32px;font-weight:bold;text-align:center"">
                                          Hola " + invitado.Nombre.ToUpper() + @",
                                        </h1>
                                        <h2
                                          style=""font-size:26px;font-weight:bold;text-align:center;color:#4154f1"">
                                          Tu registro ha sido exitoso!!!.
                                        </h2>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-bottom:16px;margin-top:16px"">
                                          Gracias por registrarte al evento del día del informático en el instituto tecnologico superior de ciudad Serdan.
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Fecha de registro: </b>" + (invitado.FechaRegistro.ToString("dd/MM/yyyy HH:mm")) + @"
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Nombre completo: </b>" + invitado.Nombre.ToUpper() + " " + invitado.ApellidoPaterno.ToUpper() + " " + invitado.ApellidoMaterno.ToUpper() + @"
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Escuela: </b>" + invitado.Escuela.ToUpper() + @"
                                        </p>
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Correo electrónico: </b>" + invitado.CorreoElectronico + @"
                                        </p>                                       
                                        <p
                                          style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <b>Para utilizar tu código QR: </b>
                                        </p>
                                        <ol style=""font-size:16px;line-height:24px;margin-top:-5px;margin-bottom:16px"">
                                          <li>Descarga el archivo adjunto</li>
                                          <li>Imprímelo o guárdalo en tu dispositivo</li>
                                          <li>Escanéalo el día del evento</li>
                                        </ol>                                
                                      </td>
                                    </tr>
                                  </tbody>
                                </table>                        
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <p
                          style=""font-size:12px;line-height:24px;text-align:center;color:rgb(0,0,0, 0.7);margin-bottom:16px;margin-top:16px"">
                          © " + DateTime.Now.Year + @" | Ingeniería informática
                        </p>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </body>
            </html>";
    }
}