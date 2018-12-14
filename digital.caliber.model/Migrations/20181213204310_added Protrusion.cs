using Microsoft.EntityFrameworkCore.Migrations;

namespace digital.caliber.model.Migrations
{
    public partial class addedProtrusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProtIncisiveInf",
                table: "MeasuresTeeths",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProtIncisiveSup",
                table: "MeasuresTeeths",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProtIncisiveInf",
                table: "MeasuresTeeths");

            migrationBuilder.DropColumn(
                name: "ProtIncisiveSup",
                table: "MeasuresTeeths");
        }
    }
}
