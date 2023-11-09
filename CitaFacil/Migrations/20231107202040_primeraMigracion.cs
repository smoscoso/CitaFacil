using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitaFacil.Migrations
{
    /// <inheritdoc />
    public partial class primeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cita", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id_Estado);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id_Pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    Metodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id_Pago);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id_roles = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id_roles);
                });

            migrationBuilder.CreateTable(
                name: "Suscripcion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Pago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscripcion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id_Empresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ubicacion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    plan = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    fecha_Registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Id_Rol = table.Column<int>(type: "int", nullable: false),
                    RolId_roles = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id_Empresa);
                    table.ForeignKey(
                        name: "FK_Empresa_Rol_RolId_roles",
                        column: x => x.RolId_roles,
                        principalTable: "Rol",
                        principalColumn: "Id_roles");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    contraseña = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Id_Rol = table.Column<int>(type: "int", nullable: false),
                    Id_EstadoUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id_Usuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Estado_Id_EstadoUsuario",
                        column: x => x.Id_EstadoUsuario,
                        principalTable: "Estado",
                        principalColumn: "Id_Estado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_Id_Rol",
                        column: x => x.Id_Rol,
                        principalTable: "Rol",
                        principalColumn: "Id_roles",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_RolId_roles",
                table: "Empresa",
                column: "RolId_roles");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Id_EstadoUsuario",
                table: "Usuario",
                column: "Id_EstadoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Id_Rol",
                table: "Usuario",
                column: "Id_Rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Suscripcion");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
