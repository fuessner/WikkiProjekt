using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikkiDBBlib.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stadt",
                columns: table => new
                {
                    SID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadt", x => x.SID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PVorname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PInfiziert = table.Column<bool>(type: "bit", nullable: false),
                    PTestAbgeschlosen = table.Column<bool>(type: "bit", nullable: false),
                    PBild = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PID);
                    table.ForeignKey(
                        name: "FK_Person_Stadt_SID",
                        column: x => x.SID,
                        principalTable: "Stadt",
                        principalColumn: "SID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_SID",
                table: "Person",
                column: "SID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Stadt");
        }
    }
}
