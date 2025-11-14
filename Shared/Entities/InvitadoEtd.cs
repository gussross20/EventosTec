using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class InvitadoEtd
    {
        //Id int	4	no
        public int Id { get; set; }

        //Nombre  varchar	100	no
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El valor para '{0}' es necesario.")]
        [MaxLength(100, ErrorMessage = "El valor para '{0}' debe tener máximo '{1}' caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        //ApellidoPaterno varchar	50	no
        [Display(Name = "Apellido paterno")]
        [Required(ErrorMessage = "El valor para '{0}' es necesario.")]
        [MaxLength(50, ErrorMessage = "El valor para '{0}' debe tener máximo '{1}' caracteres.")]
        public string ApellidoPaterno { get; set; } = string.Empty;

        //ApellidoMaterno varchar	50	no
        [Display(Name = "Apellido materno")]
        [Required(ErrorMessage = "El valor para '{0}' es necesario.")]
        [MaxLength(50, ErrorMessage = "El valor para '{0}' debe tener máximo '{1}' caracteres.")]
        public string ApellidoMaterno { get; set; } = string.Empty;

        //Escuela varchar	100	no
        [Display(Name = "Escuela")]
        [Required(ErrorMessage = "El valor para '{0}' es necesario.")]
        [MaxLength(100, ErrorMessage = "El valor para '{0}' debe tener máximo '{1}' caracteres.")]
        public string Escuela { get; set; } = string.Empty;

        //CorreoElectronico varchar	100	no
        [Display(Name = "Escuela")]
        [Required(ErrorMessage = "El valor para '{0}' es necesario.")]
        [MaxLength(100, ErrorMessage = "El valor para '{0}' debe tener máximo '{1}' caracteres.")]
        [EmailAddress(ErrorMessage = "El valor para '{0}' no tiene el formato correcto.")]
        public string CorreoElectronico { get; set; } = string.Empty;

        //FechaRegistro   datetime	8	no
        public DateTime FechaRegistro { get; set; }       
    }
}
