using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoMVC.Migrations
{
    public partial class MaestrosDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maestros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Edad = table.Column<int>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Domicilio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maestros", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maestros");
        }
    }
}
