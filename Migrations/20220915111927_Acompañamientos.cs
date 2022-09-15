using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiRestaurante.Migrations
{
    public partial class Acompañamientos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acompañamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlatilloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acompañamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acompañamientos_Platillos_PlatilloId",
                        column: x => x.PlatilloId,
                        principalTable: "Platillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acompañamientos_PlatilloId",
                table: "Acompañamientos",
                column: "PlatilloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acompañamientos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Platillos");
        }
    }
}
