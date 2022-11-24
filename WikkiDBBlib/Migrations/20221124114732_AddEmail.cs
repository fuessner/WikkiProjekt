using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikkiDBBlib.Migrations
{
    public partial class AddEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PEmail",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PEmail",
                table: "Person");
        }
    }
}
