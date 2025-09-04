using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NuevaNaturalezaAPI.NET.Models.DB;

public partial class NuevaNatuContext : DbContext
{
    public NuevaNatuContext()
    {
    }

    public NuevaNatuContext(DbContextOptions<NuevaNatuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RecuperarContrasena> RecuperarContrasena { get; set; }
    public virtual DbSet<Actuador> Actuador { get; set; }
    public virtual DbSet<AccionAct> AccionAct { get; set; }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Dispositivo> Dispositivos { get; set; }

    public virtual DbSet<EstadoDispositivo> EstadoDispositivos { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<FechaMedicion> FechaMedicions { get; set; }

    public virtual DbSet<Impacto> Impactos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Medicion> Medicions { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<PuntoOptimo> PuntoOptimos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

    public virtual DbSet<TipoDispositivo> TipoDispositivos { get; set; }

    public virtual DbSet<TipoMUnidadM> TipoMUnidadMs { get; set; }

    public virtual DbSet<TipoMedicion> TipoMedicions { get; set; }

    public virtual DbSet<TipoNotificacion> TipoNotificacions { get; set; }

    public virtual DbSet<Titulo> Titulos { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccionAct>(entity =>
        {
            entity.HasKey(e => e.IdAccionAct);

            entity.Property(e => e.Accion)
                .HasMaxLength(200);
        });

        modelBuilder.Entity<RecuperarContrasena>(entity =>
        {
            entity.HasKey(e => e.IdRecuperarContrasena);

            entity.Property(e => e.Fecha).HasColumnType("timestamp");
            entity.Property(e => e.Correo)
                .HasMaxLength(200);
        }
        );

        modelBuilder.Entity<Actuador>(entity =>
        {
            entity.HasKey(e => e.IdActuador);
            entity.HasOne(d => d.IdDispositivoNavigation).WithMany(d => d.Actuadores).HasForeignKey(d => d.IdDispositivo);
            entity.HasOne(d => d.AccionAct).WithMany(d => d.Actuadores).HasForeignKey(d => d.IdAccionAct);
        }
        );
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("PK__Auditori__7FD13FA0A61CE084");

            entity.Property(e => e.IdAuditoria).ValueGeneratedNever();

            entity.HasOne(e => e.IdAccionNavigation)
                .WithMany(d=>d.Auditoria)
                .HasForeignKey(e=>e.IdAccion)
                .IsRequired(false);

            entity.Property( e => e.Estado).IsRequired(false);

            entity.Property(e => e.Fecha).HasColumnType("timestamp");
            entity.Property(e => e.Observacion)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDispositivoNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.IdDispositivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__IdDis__7F2BE32F");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__IdUsu__7E37BEF6");
        });

        modelBuilder.Entity<Dispositivo>(entity =>
        {
            entity.HasKey(e => e.IdDispositivo).HasName("PK__Disposit__B1EDB8E474C7CBCF");

            entity.ToTable("Dispositivo");

            entity.Property(e => e.IdDispositivo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Sn)
                .HasMaxLength(100)
                .HasColumnName("SN");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__Dispositi__IdMar__534D60F1");

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.IdSistema)
                .HasConstraintName("FK__Dispositi__IdSis__52593CB8");

            entity.HasOne(d => d.IdTipoDispositivoNavigation).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.IdTipoDispositivo)
                .HasConstraintName("FK__Dispositi__IdTip__5165187F");

            entity.HasOne(d => d.IdEstadoDispositivoNavigation)
            .WithMany(d => d.Dispositivos).HasForeignKey(d => d.IdEstadoDispositivo);
        });

        modelBuilder.Entity<EstadoDispositivo>(entity =>
        {
            entity.HasKey(e => e.IdEstadoDispositivo).HasName("PK__EstadoDi__838E47E3AA25D2AD");

            entity.ToTable("EstadoDispositivo");

            entity.Property(e => e.IdEstadoDispositivo).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__Evento__034EFC0405B295B9");
            
            entity.ToTable("Evento");

            entity.Property(e => e.IdEvento).ValueGeneratedNever();
            entity.Property(e => e.FechaEvento).HasColumnType("timestamp");

            entity.HasOne(d => d.IdDispositivoNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdDispositivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evento__IdDispos__70DDC3D8");

            entity.HasOne(d => d.IdImpactoNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdImpacto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evento__IdImpact__71D1E811");

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evento__IdSistem__6FE99F9F");
        });

        modelBuilder.Entity<FechaMedicion>(entity =>
        {

            entity.Property(e => e.Fecha)
                  .HasColumnType("timestamp without time zone")
                  .HasDefaultValueSql("now()");
            entity.HasKey(e => e.IdFechaMedicion).HasName("PK__FechaMed__8194661818A24598");

            entity.ToTable("FechaMedicion");

            entity.Property(e => e.IdFechaMedicion).ValueGeneratedNever();
        });

        modelBuilder.Entity<Impacto>(entity =>
        {
            entity.HasKey(e => e.IdImpacto).HasName("PK__Impacto__5343B38B5C9E169E");

            entity.ToTable("Impacto");

            entity.Property(e => e.IdImpacto).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A887D69EBEE8");

            entity.ToTable("Marca");

            entity.Property(e => e.IdMarca).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Medicion>(entity =>
        {
            entity.HasKey(e => e.IdMedicion).HasName("PK__Medicion__0DAF13BA8526E345");

            entity.ToTable("Medicion");

            entity.Property(e => e.IdMedicion).ValueGeneratedNever();

            entity.HasOne(d => d.IdFechaMedicionNavigation).WithMany(p => p.Medicions)
                .HasForeignKey(d => d.IdFechaMedicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicion__IdFech__75A278F5");

            entity.HasOne(d => d.IdSensorNavigation).WithMany(p => p.Medicions)
                .HasForeignKey(d => d.IdSensor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicion__IdSens__74AE54BC");

            entity.HasOne(d => d.IdTipoMUnidadMNavigation).WithMany(p => p.Medicions)
                .HasForeignKey(d => d.IdTipoMUnidadM)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medicion__IdUnid__76969D2E");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK__Notifica__F6CA0A85A05D6A01");

            entity.ToTable("Notificacion");

            entity.Property(e => e.IdNotificacion).ValueGeneratedNever();
            entity.Property(e => e.Enlace).HasMaxLength(255);
            entity.Property(e => e.Mensaje).HasMaxLength(255);

            entity.HasOne(d => d.IdTipoNotificacionNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdTipoNotificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__IdTip__693CA210");

            entity.HasOne(d => d.IdTituloNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdTitulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__IdTit__68487DD7");
        });

        modelBuilder.Entity<PuntoOptimo>(entity =>
        {
            entity.HasKey(e => e.IdPuntoOptimo).HasName("PK__PuntoOpt__4DE3AB6EA6EAB830");

            entity.ToTable("PuntoOptimo");

            entity.Property(e => e.IdPuntoOptimo).ValueGeneratedNever();

            entity.HasOne(d => d.IdSensorNavigation).WithMany(p => p.PuntoOptimos)
                .HasForeignKey(d => d.IdSensor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PuntoOpti__IdSen__619B8048");

            entity.HasOne(d => d.IdTipoMUnidadMNavigation).WithMany(p => p.PuntoOptimos)
                .HasForeignKey(d => d.IdTipoMUnidadM)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PuntoOpti__IdUni__628FA481");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CA78F1000");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.IdSensor).HasName("PK__Sensor__4E7D4A8CAE317C27");

            entity.ToTable("Sensor");

            entity.Property(e => e.IdSensor).ValueGeneratedNever();

            entity.HasOne(d => d.IdDispositivoNavigation).WithMany(p => p.Sensors)
                .HasForeignKey(d => d.IdDispositivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sensor__IdDispos__5CD6CB2B");

            entity.HasOne(d => d.IdTipoMUnidadMNavigation).WithMany(p => p.Sensors)
                .HasForeignKey(d => d.IdTipoMUnidadM)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => e.IdSistema).HasName("PK__Sistema__48B026F42391412D");

            entity.ToTable("Sistema");

            entity.Property(e => e.IdSistema).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoDispositivo>(entity =>
        {
            entity.HasKey(e => e.IdTipoDispositivo).HasName("PK__TipoDisp__A9EEE648DDB96E69");

            entity.ToTable("TipoDispositivo");

            entity.Property(e => e.IdTipoDispositivo).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoMUnidadM>(entity =>
        {
            entity.HasKey(e => e.IdTipoMUnidadM).HasName("PK__TipoM_Un__F93B7E8DADF2DCAB");

            entity.ToTable("TipoM_UnidadM");

            entity.Property(e => e.IdTipoMUnidadM)
                .ValueGeneratedNever()
                .HasColumnName("IdTipoM_UnidadM");

            entity.HasOne(d => d.IdTipoMedicionNavigation).WithMany(p => p.TipoMUnidadMs)
                .HasForeignKey(d => d.IdTipoMedicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TipoM_Uni__IdTip__02FC7413");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.TipoMUnidadMs)
                .HasForeignKey(d => d.IdUnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TipoM_Uni__IdUni__02084FDA");
        });

        modelBuilder.Entity<TipoMedicion>(entity =>
        {
            entity.HasKey(e => e.IdTipoMedicion).HasName("PK__TipoMedi__A6CD50B1C1C59359");

            entity.ToTable("TipoMedicion");

            entity.Property(e => e.IdTipoMedicion).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoNotificacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoNotificacion).HasName("PK__TipoNoti__0ECE04358C2BE008");

            entity.ToTable("TipoNotificacion");

            entity.Property(e => e.IdTipoNotificacion).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Titulo>(entity =>
        {
            entity.HasKey(e => e.IdTitulo).HasName("PK__Titulo__1E1502D74A588033");

            entity.ToTable("Titulo");

            entity.Property(e => e.IdTitulo).ValueGeneratedNever();
            entity.Property(e => e.Titulo1)
                .HasMaxLength(100)
                .HasColumnName("Titulo");
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedida).HasName("PK__UnidadMe__18F83A93642ECCF9");

            entity.Property(e => e.IdUnidadMedida).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9720640986");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.Cedula).HasMaxLength(20);
            entity.Property(e => e.Clave).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__6D0D32F4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
