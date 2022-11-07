using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiRestaurante2.Migrations
{
    public partial class Relacion_PlatilloAcompañamiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "PlatilloAcompañamiento",
                 columns: table => new
                 {
                     Id = table.Column<int>(type: "int", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     PlatillosId = table.Column<int>(type: "int", nullable: false),
                     AcompañamientoId = table.Column<int>(type: "int", nullable: false),
                     Orden = table.Column<int>(type: "int", nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_PlatilloAcompañamiento", x => x.Id);
                     table.ForeignKey(
                         name: "FK_PlatilloAcompañamiento_Acompañamientos_AcompañamientoId",
                         column: x => x.AcompañamientoId,
                         principalTable: "Acompañamientos",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                     table.ForeignKey(
                         name: "FK_PlatilloAcompañamiento_Platillos_PlatillosId",
                         column: x => x.PlatillosId,
                         principalTable: "Platillos",
                         principalColumn: "Id");
                 });

            migrationBuilder.CreateIndex(
                name: "IX_PlatilloAcompañamiento_AcompañamientoId",
                table: "PlatilloAcompañamiento",
                column: "AcompañamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatilloAcompañamiento_PlatillosId",
                table: "PlatilloAcompañamiento",
                column: "PlatillosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatilloAcompañamiento");
        }
    }
}
