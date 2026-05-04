using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseHandler.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpenseReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManagerComment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseReports_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseItems_ExpenseReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "ExpenseReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3b7bc618-54dd-4341-8968-891229413753"), "Office" },
                    { new Guid("d33a33db-4d93-4221-bd56-45bc59587b0b"), "Meals" },
                    { new Guid("dc12b61c-d4e5-4eb0-a100-00d9f1dc943e"), "Travel" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "ManagerId", "Name" },
                values: new object[,]
                {
                    { new Guid("5de35a77-07f6-4e0d-b1f8-afe9374ab529"), "Tharun@company.com", null, "Tharun Manager" },
                    { new Guid("03227966-869a-44be-8dfc-0f8f2531eee3"), "Pavan@company.com", new Guid("5de35a77-07f6-4e0d-b1f8-afe9374ab529"), "Pavan Employee" },
                    { new Guid("ae0a2f73-8240-41c2-9d10-baa8e653e30f"), "Naveen@company.com", new Guid("5de35a77-07f6-4e0d-b1f8-afe9374ab529"), "Naveen Employee" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseReports",
                columns: new[] { "Id", "CreatedAt", "EmployeeId", "ManagerComment", "Status", "SubmittedAt" },
                values: new object[] { new Guid("e70be0f3-4967-4cb0-9dd5-7fcb3692f324"), new DateTime(2025, 9, 9, 17, 15, 23, 272, DateTimeKind.Utc).AddTicks(2621), new Guid("ae0a2f73-8240-41c2-9d10-baa8e653e30f"), null, 0, null });

            migrationBuilder.InsertData(
                table: "ExpenseItems",
                columns: new[] { "Id", "Amount", "CategoryId", "Description", "ReportId", "SpendDate" },
                values: new object[] { new Guid("3691b3b5-8147-4656-8036-f7a1228e0d44"), 120m, new Guid("dc12b61c-d4e5-4eb0-a100-00d9f1dc943e"), "Taxi to airport", new Guid("e70be0f3-4967-4cb0-9dd5-7fcb3692f324"), new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_CategoryId",
                table: "ExpenseItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_ReportId",
                table: "ExpenseItems",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseReports_EmployeeId",
                table: "ExpenseReports",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ExpenseReports");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
