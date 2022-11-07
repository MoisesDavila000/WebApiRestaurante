using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiRestaurante2.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acompañamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acompañamientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platillos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Descricpion = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platillos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Entrada = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Salida = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                });

            /*migrationBuilder.CreateTable(
                name: "PlatilloAcompañamiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatilloId = table.Column<int>(type: "int", nullable: false),
                    AcompañamientoId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    PlatillosId = table.Column<int>(type: "int", nullable: true)
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
            */
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Matricula = table.Column<int>(type: "int", nullable: false),
                    TurnosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleados_Turnos_TurnosId",
                        column: x => x.TurnosId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_TurnosId",
                table: "Empleados",
                column: "TurnosId");

            /*migrationBuilder.CreateIndex(
                name: "IX_PlatilloAcompañamiento_AcompañamientoId",
                table: "PlatilloAcompañamiento",
                column: "AcompañamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlatilloAcompañamiento_PlatillosId",
                table: "PlatilloAcompañamiento",
                column: "PlatillosId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");

            /*migrationBuilder.DropTable(
                name: "PlatilloAcompañamiento");*/

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Acompañamientos");

            migrationBuilder.DropTable(
                name: "Platillos");
        }
    }
}
