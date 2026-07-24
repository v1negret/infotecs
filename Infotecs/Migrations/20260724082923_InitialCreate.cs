using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infotecs.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUid = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTimeDelta = table.Column<int>(type: "integer", nullable: false),
                    MinDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AvgExecutionTime = table.Column<double>(type: "double precision", nullable: false),
                    AvgValue = table.Column<decimal>(type: "numeric", nullable: false),
                    MedianValue = table.Column<decimal>(type: "numeric", nullable: false),
                    MinValue = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Results_Files_FileUid",
                        column: x => x.FileUid,
                        principalTable: "Files",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutionTime = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Values_Files_FileUid",
                        column: x => x.FileUid,
                        principalTable: "Files",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_FileUid",
                table: "Results",
                column: "FileUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Values_FileUid",
                table: "Values",
                column: "FileUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
