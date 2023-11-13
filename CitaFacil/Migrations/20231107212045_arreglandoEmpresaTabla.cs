using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitaFacil.Migrations
{
    /// <inheritdoc />
    public partial class arreglandoEmpresaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Empresa");

            migrationBuilder.AddColumn<string>(
                name: "Celular",
                table: "Empresa",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id_EstadoEmpresa",
                table: "Empresa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Empresa",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Id_EstadoEmpresa",
                table: "Empresa",
                column: "Id_EstadoEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_Estado_Id_EstadoEmpresa",
                table: "Empresa",
                column: "Id_EstadoEmpresa",
                principalTable: "Estado",
                principalColumn: "Id_Estado",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_Estado_Id_EstadoEmpresa",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_Id_EstadoEmpresa",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Celular",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Id_EstadoEmpresa",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Empresa");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
