using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Business.Configuration
{
    internal class InvitadoCnf : IEntityTypeConfiguration<InvitadoEtd>
    {
        public void Configure(EntityTypeBuilder<InvitadoEtd> builder)
        {
            builder.ToTable("Invitado");

            //Id int	4	no
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            //Nombre  varchar	100	no
            builder.Property(x => x.Nombre).IsUnicode(false);

            //ApellidoPaterno varchar	50	no
            builder.Property(x => x.ApellidoPaterno).IsUnicode(false);

            //ApellidoMaterno varchar	50	no
            builder.Property(x => x.ApellidoMaterno).IsUnicode(false);

            //Escuela varchar	100	no
            builder.Property(x => x.Escuela).IsUnicode(false);

            //CorreoElectronico varchar	100	no
            builder.Property(x => x.CorreoElectronico).IsUnicode(false);

            //FechaRegistro   datetime	8	no
            builder.Property(x => x.FechaRegistro);          
        }
    }
}
