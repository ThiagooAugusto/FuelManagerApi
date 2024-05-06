using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelManagerApi.Migrations
{
    /// <inheritdoc />
    public partial class M03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Enum>(
        name: "Perfil",
        table: "Usuarios",
        type: "int",
        nullable: false,
        defaultValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
