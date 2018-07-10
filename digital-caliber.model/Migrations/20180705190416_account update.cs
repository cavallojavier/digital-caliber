using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace digital.caliber.model.Migrations
{
    public partial class accountupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoltonPreviousRef");

            migrationBuilder.DropTable(
                name: "BoltonTotalRef");

            migrationBuilder.DropTable(
                name: "MoyersRef");

            migrationBuilder.DropTable(
                name: "PontRef");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoltonPreviousRef",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value1 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value2 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value3 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoltonPreviousRef", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoltonTotalRef",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value1 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value2 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value3 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoltonTotalRef", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoyersRef",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value1 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value2 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value3 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value4 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoyersRef", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PontRef",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value1 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false),
                    Value2 = table.Column<decimal>(type: "decimal(5, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontRef", x => x.Id);
                });
        }
    }
}
