using Microsoft.EntityFrameworkCore.Migrations;

namespace AruNyilvantartas.Data.Migrations
{
    public partial class elso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Elnevezes = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    Kategoria = table.Column<string>(type: "nvarchar(60)", nullable: true),
                    CsomagolasiEgyseg = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    DarabSzam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aru", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aru");
        }
    }
}
