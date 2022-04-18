using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class migrationVacation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "vacation");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "vacation",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Country_pk", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "vacation",
                columns: table => new
                {
                    IdItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsPacked = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Item_pk", x => x.IdItem);
                });

            migrationBuilder.CreateTable(
                name: "UserVacation",
                schema: "vacation",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pk", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "vacation",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CityId_pk", x => x.IdCity);
                    table.ForeignKey(
                        name: "City_Country_pk",
                        column: x => x.IdCountry,
                        principalSchema: "vacation",
                        principalTable: "Country",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suitcase",
                schema: "vacation",
                columns: table => new
                {
                    IdSuitcase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdCity = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Suitcase_pk", x => x.IdSuitcase);
                    table.ForeignKey(
                        name: "Cities",
                        column: x => x.IdCity,
                        principalSchema: "vacation",
                        principalTable: "City",
                        principalColumn: "IdCity",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Users",
                        column: x => x.IdUser,
                        principalSchema: "vacation",
                        principalTable: "UserVacation",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuitcaseItem",
                schema: "vacation",
                columns: table => new
                {
                    IdSuitcase = table.Column<int>(type: "int", nullable: false),
                    IdItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SuitcaseItem_pk", x => new { x.IdItem, x.IdSuitcase });
                    table.ForeignKey(
                        name: "SuitcaseItem_Item",
                        column: x => x.IdItem,
                        principalSchema: "vacation",
                        principalTable: "Item",
                        principalColumn: "IdItem",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "SuitcaseItem_suitcase",
                        column: x => x.IdSuitcase,
                        principalSchema: "vacation",
                        principalTable: "Suitcase",
                        principalColumn: "IdSuitcase",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_IdCountry",
                schema: "vacation",
                table: "City",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Suitcase_IdCity",
                schema: "vacation",
                table: "Suitcase",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Suitcase_IdUser",
                schema: "vacation",
                table: "Suitcase",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_SuitcaseItem_IdSuitcase",
                schema: "vacation",
                table: "SuitcaseItem",
                column: "IdSuitcase");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuitcaseItem",
                schema: "vacation");

            migrationBuilder.DropTable(
                name: "Item",
                schema: "vacation");

            migrationBuilder.DropTable(
                name: "Suitcase",
                schema: "vacation");

            migrationBuilder.DropTable(
                name: "City",
                schema: "vacation");

            migrationBuilder.DropTable(
                name: "UserVacation",
                schema: "vacation");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "vacation");
        }
    }
}
