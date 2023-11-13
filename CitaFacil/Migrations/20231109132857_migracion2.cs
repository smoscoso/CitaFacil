using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitaFacil.Migrations
{
    /// <inheritdoc />
    public partial class migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Rol_RolId_roles",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_RolId_roles",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "RolId_roles",
                table: "Empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Id_Rol",
                table: "Empresa",
                column: "Id_Rol");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Rol_Id_Rol",
                table: "Empresa",
                column: "Id_Rol",
                principalTable: "Rol",
                principalColumn: "Id_roles",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Rol_Id_Rol",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_Id_Rol",
                table: "Empresa");

            migrationBuilder.AddColumn<int>(
                name: "RolId_roles",
                table: "Empresa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_RolId_roles",
                table: "Empresa",
                column: "RolId_roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Rol_RolId_roles",
                table: "Empresa",
                column: "RolId_roles",
                principalTable: "Rol",
                principalColumn: "Id_roles");
        }
    }
}
