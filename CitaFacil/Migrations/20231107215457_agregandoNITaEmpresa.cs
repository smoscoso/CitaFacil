using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitaFacil.Migrations
{
    /// <inheritdoc />
    public partial class agregandoNITaEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NIT",
                table: "Empresa",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NIT",
                table: "Empresa");
        }
    }
}
