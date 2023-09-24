using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitaFacil.Migrations
{
    /// <inheritdoc />
    public partial class primera_Migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id_Estado);
                });

            migrationBuilder.CreateTable(
                name: "Registrar_Cliente",
                columns: table => new
                {
                    Correo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Primer_Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Segundo_Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Primer_Apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Segundo_Apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefono_Fijo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    passsword = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrar_Cliente", x => x.Correo);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id_Rol);
                });

            migrationBuilder.CreateTable(
                name: "Registrar_Negocio",
                columns: table => new
                {
                    Correo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telefono = table.Column<int>(type: "int", maxLength: 80, nullable: false),
                    Telefono_Fijo = table.Column<int>(type: "int", maxLength: 80, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: false),
                    passsword = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Id_Rol = table.Column<int>(type: "int", nullable: false),
                    RolId_Rol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrar_Negocio", x => x.Correo);
                    table.ForeignKey(
                        name: "FK_Registrar_Negocio_Roles_RolId_Rol",
                        column: x => x.RolId_Rol,
                        principalTable: "Roles",
                        principalColumn: "Id_Rol");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrar_Negocio_RolId_Rol",
                table: "Registrar_Negocio",
                column: "RolId_Rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Registrar_Cliente");

            migrationBuilder.DropTable(
                name: "Registrar_Negocio");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
