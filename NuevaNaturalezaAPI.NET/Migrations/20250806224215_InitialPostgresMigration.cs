using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoDispositivo",
                columns: table => new
                {
                    IdEstadoDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadoDi__838E47E3AA25D2AD", x => x.IdEstadoDispositivo);
                });

            migrationBuilder.CreateTable(
                name: "FechaMedicion",
                columns: table => new
                {
                    IdFechaMedicion = table.Column<Guid>(type: "uuid", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FechaMed__8194661818A24598", x => x.IdFechaMedicion);
                });

            migrationBuilder.CreateTable(
                name: "Impacto",
                columns: table => new
                {
                    IdImpacto = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Impacto__5343B38B5C9E169E", x => x.IdImpacto);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    IdMarca = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Marca__4076A887D69EBEE8", x => x.IdMarca);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__2A49584CA78F1000", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Sistema",
                columns: table => new
                {
                    IdSistema = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sistema__48B026F42391412D", x => x.IdSistema);
                });

            migrationBuilder.CreateTable(
                name: "TipoDispositivo",
                columns: table => new
                {
                    IdTipoDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoDisp__A9EEE648DDB96E69", x => x.IdTipoDispositivo);
                });

            migrationBuilder.CreateTable(
                name: "TipoMedicion",
                columns: table => new
                {
                    IdTipoMedicion = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoMedi__A6CD50B1C1C59359", x => x.IdTipoMedicion);
                });

            migrationBuilder.CreateTable(
                name: "TipoNotificacion",
                columns: table => new
                {
                    IdTipoNotificacion = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoNoti__0ECE04358C2BE008", x => x.IdTipoNotificacion);
                });

            migrationBuilder.CreateTable(
                name: "Titulo",
                columns: table => new
                {
                    IdTitulo = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Titulo__1E1502D74A588033", x => x.IdTitulo);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    IdUnidadMedida = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UnidadMe__18F83A93642ECCF9", x => x.IdUnidadMedida);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Clave = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Cedula = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IdRol = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__5B65BF9720640986", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK__Usuario__IdRol__6D0D32F4",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Dispositivo",
                columns: table => new
                {
                    IdDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    IdTipoDispositivo = table.Column<Guid>(type: "uuid", nullable: true),
                    IdSistema = table.Column<Guid>(type: "uuid", nullable: true),
                    IdMarca = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Disposit__B1EDB8E474C7CBCF", x => x.IdDispositivo);
                    table.ForeignKey(
                        name: "FK__Dispositi__IdMar__534D60F1",
                        column: x => x.IdMarca,
                        principalTable: "Marca",
                        principalColumn: "IdMarca");
                    table.ForeignKey(
                        name: "FK__Dispositi__IdSis__52593CB8",
                        column: x => x.IdSistema,
                        principalTable: "Sistema",
                        principalColumn: "IdSistema");
                    table.ForeignKey(
                        name: "FK__Dispositi__IdTip__5165187F",
                        column: x => x.IdTipoDispositivo,
                        principalTable: "TipoDispositivo",
                        principalColumn: "IdTipoDispositivo");
                });

            migrationBuilder.CreateTable(
                name: "Notificacion",
                columns: table => new
                {
                    IdNotificacion = table.Column<Guid>(type: "uuid", nullable: false),
                    IdTitulo = table.Column<Guid>(type: "uuid", nullable: false),
                    IdTipoNotificacion = table.Column<Guid>(type: "uuid", nullable: false),
                    Mensaje = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Enlace = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__F6CA0A85A05D6A01", x => x.IdNotificacion);
                    table.ForeignKey(
                        name: "FK__Notificac__IdTip__693CA210",
                        column: x => x.IdTipoNotificacion,
                        principalTable: "TipoNotificacion",
                        principalColumn: "IdTipoNotificacion");
                    table.ForeignKey(
                        name: "FK__Notificac__IdTit__68487DD7",
                        column: x => x.IdTitulo,
                        principalTable: "Titulo",
                        principalColumn: "IdTitulo");
                });

            migrationBuilder.CreateTable(
                name: "TipoM_UnidadM",
                columns: table => new
                {
                    IdTipoM_UnidadM = table.Column<Guid>(type: "uuid", nullable: false),
                    IdTipoMedicion = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUnidadMedida = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoM_Un__F93B7E8DADF2DCAB", x => x.IdTipoM_UnidadM);
                    table.ForeignKey(
                        name: "FK__TipoM_Uni__IdTip__02FC7413",
                        column: x => x.IdTipoMedicion,
                        principalTable: "TipoMedicion",
                        principalColumn: "IdTipoMedicion");
                    table.ForeignKey(
                        name: "FK__TipoM_Uni__IdUni__02084FDA",
                        column: x => x.IdUnidadMedida,
                        principalTable: "UnidadMedida",
                        principalColumn: "IdUnidadMedida");
                });

            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    IdAuditoria = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    Accion = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Observacion = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Auditori__7FD13FA0A61CE084", x => x.IdAuditoria);
                    table.ForeignKey(
                        name: "FK__Auditoria__IdDis__7F2BE32F",
                        column: x => x.IdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo");
                    table.ForeignKey(
                        name: "FK__Auditoria__IdUsu__7E37BEF6",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    IdEvento = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    IdImpacto = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSistema = table.Column<Guid>(type: "uuid", nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Evento__034EFC0405B295B9", x => x.IdEvento);
                    table.ForeignKey(
                        name: "FK__Evento__IdDispos__70DDC3D8",
                        column: x => x.IdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo");
                    table.ForeignKey(
                        name: "FK__Evento__IdImpact__71D1E811",
                        column: x => x.IdImpacto,
                        principalTable: "Impacto",
                        principalColumn: "IdImpacto");
                    table.ForeignKey(
                        name: "FK__Evento__IdSistem__6FE99F9F",
                        column: x => x.IdSistema,
                        principalTable: "Sistema",
                        principalColumn: "IdSistema");
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    IdSensor = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    IdTipoMedicion = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUnidadMedida = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sensor__4E7D4A8CAE317C27", x => x.IdSensor);
                    table.ForeignKey(
                        name: "FK__Sensor__IdDispos__5CD6CB2B",
                        column: x => x.IdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo");
                    table.ForeignKey(
                        name: "FK__Sensor__IdTipoMe__5DCAEF64",
                        column: x => x.IdTipoMedicion,
                        principalTable: "TipoMedicion",
                        principalColumn: "IdTipoMedicion");
                    table.ForeignKey(
                        name: "FK__Sensor__IdUnidad__5EBF139D",
                        column: x => x.IdUnidadMedida,
                        principalTable: "UnidadMedida",
                        principalColumn: "IdUnidadMedida");
                });

            migrationBuilder.CreateTable(
                name: "Medicion",
                columns: table => new
                {
                    IdMedicion = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSensor = table.Column<Guid>(type: "uuid", nullable: false),
                    IdFechaMedicion = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUnidadMedida = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEstadoDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medicion__0DAF13BA8526E345", x => x.IdMedicion);
                    table.ForeignKey(
                        name: "FK__Medicion__IdEsta__778AC167",
                        column: x => x.IdEstadoDispositivo,
                        principalTable: "EstadoDispositivo",
                        principalColumn: "IdEstadoDispositivo");
                    table.ForeignKey(
                        name: "FK__Medicion__IdFech__75A278F5",
                        column: x => x.IdFechaMedicion,
                        principalTable: "FechaMedicion",
                        principalColumn: "IdFechaMedicion");
                    table.ForeignKey(
                        name: "FK__Medicion__IdSens__74AE54BC",
                        column: x => x.IdSensor,
                        principalTable: "Sensor",
                        principalColumn: "IdSensor");
                    table.ForeignKey(
                        name: "FK__Medicion__IdUnid__76969D2E",
                        column: x => x.IdUnidadMedida,
                        principalTable: "UnidadMedida",
                        principalColumn: "IdUnidadMedida");
                });

            migrationBuilder.CreateTable(
                name: "PuntoOptimo",
                columns: table => new
                {
                    IdPuntoOptimo = table.Column<Guid>(type: "uuid", nullable: false),
                    IdSensor = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUnidadMedida = table.Column<Guid>(type: "uuid", nullable: false),
                    ValorMin = table.Column<double>(type: "double precision", nullable: false),
                    ValorMax = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PuntoOpt__4DE3AB6EA6EAB830", x => x.IdPuntoOptimo);
                    table.ForeignKey(
                        name: "FK__PuntoOpti__IdSen__619B8048",
                        column: x => x.IdSensor,
                        principalTable: "Sensor",
                        principalColumn: "IdSensor");
                    table.ForeignKey(
                        name: "FK__PuntoOpti__IdUni__628FA481",
                        column: x => x.IdUnidadMedida,
                        principalTable: "UnidadMedida",
                        principalColumn: "IdUnidadMedida");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_IdDispositivo",
                table: "Auditoria",
                column: "IdDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_IdUsuario",
                table: "Auditoria",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivo_IdMarca",
                table: "Dispositivo",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivo_IdSistema",
                table: "Dispositivo",
                column: "IdSistema");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivo_IdTipoDispositivo",
                table: "Dispositivo",
                column: "IdTipoDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdDispositivo",
                table: "Evento",
                column: "IdDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdImpacto",
                table: "Evento",
                column: "IdImpacto");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdSistema",
                table: "Evento",
                column: "IdSistema");

            migrationBuilder.CreateIndex(
                name: "IX_Medicion_IdEstadoDispositivo",
                table: "Medicion",
                column: "IdEstadoDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Medicion_IdFechaMedicion",
                table: "Medicion",
                column: "IdFechaMedicion");

            migrationBuilder.CreateIndex(
                name: "IX_Medicion_IdSensor",
                table: "Medicion",
                column: "IdSensor");

            migrationBuilder.CreateIndex(
                name: "IX_Medicion_IdUnidadMedida",
                table: "Medicion",
                column: "IdUnidadMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_IdTipoNotificacion",
                table: "Notificacion",
                column: "IdTipoNotificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacion_IdTitulo",
                table: "Notificacion",
                column: "IdTitulo");

            migrationBuilder.CreateIndex(
                name: "IX_PuntoOptimo_IdSensor",
                table: "PuntoOptimo",
                column: "IdSensor");

            migrationBuilder.CreateIndex(
                name: "IX_PuntoOptimo_IdUnidadMedida",
                table: "PuntoOptimo",
                column: "IdUnidadMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_IdDispositivo",
                table: "Sensor",
                column: "IdDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_IdTipoMedicion",
                table: "Sensor",
                column: "IdTipoMedicion");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_IdUnidadMedida",
                table: "Sensor",
                column: "IdUnidadMedida");

            migrationBuilder.CreateIndex(
                name: "IX_TipoM_UnidadM_IdTipoMedicion",
                table: "TipoM_UnidadM",
                column: "IdTipoMedicion");

            migrationBuilder.CreateIndex(
                name: "IX_TipoM_UnidadM_IdUnidadMedida",
                table: "TipoM_UnidadM",
                column: "IdUnidadMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdRol",
                table: "Usuario",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Medicion");

            migrationBuilder.DropTable(
                name: "Notificacion");

            migrationBuilder.DropTable(
                name: "PuntoOptimo");

            migrationBuilder.DropTable(
                name: "TipoM_UnidadM");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Impacto");

            migrationBuilder.DropTable(
                name: "EstadoDispositivo");

            migrationBuilder.DropTable(
                name: "FechaMedicion");

            migrationBuilder.DropTable(
                name: "TipoNotificacion");

            migrationBuilder.DropTable(
                name: "Titulo");

            migrationBuilder.DropTable(
                name: "Sensor");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Dispositivo");

            migrationBuilder.DropTable(
                name: "TipoMedicion");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Sistema");

            migrationBuilder.DropTable(
                name: "TipoDispositivo");
        }
    }
}
