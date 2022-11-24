using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikkiDBBlib.Migrations
{
    public partial class AddDeleteBehaviorRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Stadt_SID",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Stadt_SID",
                table: "Person",
                column: "SID",
                principalTable: "Stadt",
                principalColumn: "SID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Stadt_SID",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Stadt_SID",
                table: "Person",
                column: "SID",
                principalTable: "Stadt",
                principalColumn: "SID",
                onDelete: ReferentialAction.Cascade);
        }
        // Cascade Löschen heißt, das er nicht auf abhängigkeiten in einer anderen Tabelle prüft
        // Sollte ich eine Stadt löschen, die schon einer Person zugewiesen wurde, so wird die Person auch
        // nicht mehr angezeigt
        // onDelete: ReferentialAction.Cascade);   Aus diesem Grunde führen wir eine neue Migration durch, damit
        // dieser Fehler vermieden wird.
    }
}
