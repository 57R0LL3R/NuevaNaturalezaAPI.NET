using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NuevaNaturalezaAPI.NET.Models;

public partial class Les2Context : DbContext
{
    public Les2Context()
    {
    }

    public Les2Context(DbContextOptions<Les2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Dispositivo> Dispositivos { get; set; }

    public virtual DbSet<EstadoDispositivo> EstadoDispositivos { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<FechaMedicion> FechaMedicions { get; set; }

    public virtual DbSet<Impacto> Impactos { get; set; }

    public virtual DbSet<Medicion> Medicions { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<PuntoOptimo> PuntoOptimos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

    public virtual DbSet<TipoDispositivo> TipoDispositivos { get; set; }

    public virtual DbSet<TipoMedicion> TipoMedicions { get; set; }

    public virtual DbSet<TipoNotificacion> TipoNotificacions { get; set; }

    public virtual DbSet<TipoUnidadMedidum> TipoUnidadMedida { get; set; }

    public virtual DbSet<Titulo> Titulos { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioNotificacion> UsuarioNotificacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("PK__Auditori__7FD13FA0AE2A4DCB");

            entity.Property(e => e.IdAuditoria).ValueGeneratedNever();
            entity.Property(e => e.Accion).HasMaxLength(100);
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.TablaAfectada).HasMaxLength(100);
        });

        modelBuilder.Entity<Dispositivo>(entity =>
        {
            entity.HasKey(e => e.IdDispositivo).HasName("PK__Disposit__B1EDB8E48D68F114");

            entity.ToTable("Dispositivo");

            entity.Property(e => e.IdDispositivo).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<EstadoDispositivo>(entity =>
        {
            entity.HasKey(e => e.IdEstadoDispositivo).HasName("PK__EstadoDi__838E47E30DC0EAC6");

            entity.ToTable("EstadoDispositivo");

            entity.Property(e => e.IdEstadoDispositivo).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__Evento__034EFC04547BC945");

            entity.ToTable("Evento");

            entity.Property(e => e.IdEvento).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
        });

        modelBuilder.Entity<FechaMedicion>(entity =>
        {
            entity.HasKey(e => e.IdFechaMedicion).HasName("PK__FechaMed__81946618216F3F95");

            entity.ToTable("FechaMedicion");

            entity.Property(e => e.IdFechaMedicion).ValueGeneratedNever();
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
        });

        modelBuilder.Entity<Impacto>(entity =>
        {
            entity.HasKey(e => e.IdImpacto).HasName("PK__Impacto__5343B38B61402166");

            entity.ToTable("Impacto");

            entity.Property(e => e.IdImpacto).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Medicion>(entity =>
        {
            entity.HasKey(e => e.IdMedicion).HasName("PK__Medicion__0DAF13BAAA818366");

            entity.ToTable("Medicion");

            entity.Property(e => e.IdMedicion).ValueGeneratedNever();
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK__Notifica__F6CA0A8554E5EAF7");

            entity.ToTable("Notificacion");

            entity.Property(e => e.IdNotificacion).ValueGeneratedNever();
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
        });

        modelBuilder.Entity<PuntoOptimo>(entity =>
        {
            entity.HasKey(e => e.IdPuntoOptimo).HasName("PK__PuntoOpt__4DE3AB6ED2BDE8A1");

            entity.ToTable("PuntoOptimo");

            entity.Property(e => e.IdPuntoOptimo).ValueGeneratedNever();
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C35ABA326");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.IdSensor).HasName("PK__Sensor__4E7D4A8C7B8D8A35");

            entity.ToTable("Sensor");

            entity.Property(e => e.IdSensor).ValueGeneratedNever();
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => e.IdSistema).HasName("PK__Sistema__48B026F4834E1DD3");

            entity.ToTable("Sistema");

            entity.Property(e => e.IdSistema).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoDispositivo>(entity =>
        {
            entity.HasKey(e => e.IdTipoDispositivo).HasName("PK__TipoDisp__A9EEE6485BD853C3");

            entity.ToTable("TipoDispositivo");

            entity.Property(e => e.IdTipoDispositivo).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoMedicion>(entity =>
        {
            entity.HasKey(e => e.IdTipoMedicion).HasName("PK__TipoMedi__A6CD50B1916CB39D");

            entity.ToTable("TipoMedicion");

            entity.Property(e => e.IdTipoMedicion).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoNotificacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoNotificacion).HasName("PK__TipoNoti__0ECE0435ED0AF494");

            entity.ToTable("TipoNotificacion");

            entity.Property(e => e.IdTipoNotificacion).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoUnidadMedidum>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<Titulo>(entity =>
        {
            entity.HasKey(e => e.IdTitulo).HasName("PK__Titulo__1E1502D7D286645E");

            entity.ToTable("Titulo");

            entity.Property(e => e.IdTitulo).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedida).HasName("PK__UnidadMe__18F83A93D380FF48");

            entity.Property(e => e.IdUnidadMedida).ValueGeneratedNever();
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Simbolo).HasMaxLength(10);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97C4F709A2");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<UsuarioNotificacion>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioNotificacion).HasName("PK__UsuarioN__14B9EC848F76701D");

            entity.ToTable("UsuarioNotificacion");

            entity.Property(e => e.IdUsuarioNotificacion).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
