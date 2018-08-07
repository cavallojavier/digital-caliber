using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace digital.caliber.model.Migrations
{
    public partial class measures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    PatientName = table.Column<string>(maxLength: 255, nullable: false),
                    HcNumber = table.Column<string>(maxLength: 50, nullable: false),
                    DateMeasure = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measures_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasuresMouth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeasureId = table.Column<int>(nullable: false),
                    LeftSuperiorIncisive = table.Column<decimal>(nullable: false),
                    RightSuperiorIncisive = table.Column<decimal>(nullable: false),
                    LeftSuperiorCanine = table.Column<decimal>(nullable: false),
                    RightSuperiorCanine = table.Column<decimal>(nullable: false),
                    LeftSuperiorPremolar = table.Column<decimal>(nullable: false),
                    RightSuperiorPremolar = table.Column<decimal>(nullable: false),
                    LeftInferiorIncisive = table.Column<decimal>(nullable: false),
                    RightInferiorIncisive = table.Column<decimal>(nullable: false),
                    LeftInferiorCanine = table.Column<decimal>(nullable: false),
                    RightInferiorCanine = table.Column<decimal>(nullable: false),
                    LeftInferiorPremolar = table.Column<decimal>(nullable: false),
                    RightInferiorPremolar = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuresMouth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasuresMouth_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasuresTeeths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeasureId = table.Column<int>(nullable: false),
                    Tooth11 = table.Column<decimal>(nullable: false),
                    Tooth12 = table.Column<decimal>(nullable: false),
                    Tooth13 = table.Column<decimal>(nullable: false),
                    Tooth14 = table.Column<decimal>(nullable: false),
                    Tooth15 = table.Column<decimal>(nullable: false),
                    Tooth16 = table.Column<decimal>(nullable: false),
                    Tooth17 = table.Column<decimal>(nullable: false),
                    Tooth21 = table.Column<decimal>(nullable: false),
                    Tooth22 = table.Column<decimal>(nullable: false),
                    Tooth23 = table.Column<decimal>(nullable: false),
                    Tooth24 = table.Column<decimal>(nullable: false),
                    Tooth25 = table.Column<decimal>(nullable: false),
                    Tooth26 = table.Column<decimal>(nullable: false),
                    Tooth27 = table.Column<decimal>(nullable: false),
                    Tooth31 = table.Column<decimal>(nullable: false),
                    Tooth32 = table.Column<decimal>(nullable: false),
                    Tooth33 = table.Column<decimal>(nullable: false),
                    Tooth34 = table.Column<decimal>(nullable: false),
                    Tooth35 = table.Column<decimal>(nullable: false),
                    Tooth36 = table.Column<decimal>(nullable: false),
                    Tooth37 = table.Column<decimal>(nullable: false),
                    Tooth41 = table.Column<decimal>(nullable: false),
                    Tooth42 = table.Column<decimal>(nullable: false),
                    Tooth43 = table.Column<decimal>(nullable: false),
                    Tooth44 = table.Column<decimal>(nullable: false),
                    Tooth45 = table.Column<decimal>(nullable: false),
                    Tooth46 = table.Column<decimal>(nullable: false),
                    Tooth47 = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuresTeeths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasuresTeeths_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measures_UserId",
                table: "Measures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuresMouth_MeasureId",
                table: "MeasuresMouth",
                column: "MeasureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasuresTeeths_MeasureId",
                table: "MeasuresTeeths",
                column: "MeasureId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasuresMouth");

            migrationBuilder.DropTable(
                name: "MeasuresTeeths");

            migrationBuilder.DropTable(
                name: "Measures");
        }
    }
}
