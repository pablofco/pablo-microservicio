using System;
using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable IDE0240 // Quitar directiva redundante que admite un valor NULL
#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GtMotive.Estimate.Microservice.Infrastructure.Migrations
#pragma warning restore IDE0240 // Quitar directiva redundante que admite un valor NULL
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validar argumentos de métodos públicos
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });
#pragma warning restore CA1062 // Validar argumentos de métodos públicos

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    ParameterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreciPerDay = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    YearsToNoActiveVehicle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.ParameterId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Ports = table.Column<int>(type: "int", nullable: false),
                    AdquisitionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });

            migrationBuilder.CreateTable(
                name: "Rentings",
                columns: table => new
                {
                    RentingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DateEndReal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceReal = table.Column<double>(type: "float", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentings", x => x.RentingId);
                    table.ForeignKey(
                        name: "FK_Rentings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

#pragma warning disable IDE0300 // Simplificar la inicialización de la recopilación
#pragma warning disable CA1861 // Evitar matrices constantes como argumentos
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "BirthDate", "Document", "DocumentType", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1993, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "44714852M", 1, "Garcia", "Pablo" },
                    { 2, null, "44693254L", 2, "Garcia", "Andrea" },
                    { 3, null, "44965142V", 1, "Garcia", "Pablo Antonio" }
                });
#pragma warning restore CA1861 // Evitar matrices constantes como argumentos
#pragma warning restore IDE0300 // Simplificar la inicialización de la recopilación

#pragma warning disable IDE0300 // Simplificar la inicialización de la recopilación
#pragma warning disable CA1861 // Evitar matrices constantes como argumentos
            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "ParameterId", "PreciPerDay", "YearsToNoActiveVehicle" },
                values: new object[] { 1, 40m, 5 });
#pragma warning restore CA1861 // Evitar matrices constantes como argumentos
#pragma warning restore IDE0300 // Simplificar la inicialización de la recopilación

#pragma warning disable IDE0300 // Simplificar la inicialización de la recopilación
#pragma warning disable CA1861 // Evitar matrices constantes como argumentos
            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "Active", "AdquisitionDate", "Color", "NumberId", "Ports" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2015, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "VH001", 3 },
                    { 2, true, new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "VH002", 5 }
                });
#pragma warning restore CA1861 // Evitar matrices constantes como argumentos
#pragma warning restore IDE0300 // Simplificar la inicialización de la recopilación

#pragma warning disable IDE0300 // Simplificar la inicialización de la recopilación
#pragma warning disable CA1861 // Evitar matrices constantes como argumentos
            migrationBuilder.InsertData(
                table: "Rentings",
                columns: new[] { "RentingId", "CustomerId", "DateEnd", "DateEndReal", "DateStart", "Price", "PriceReal", "VehicleId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 240.0, 240.0, 1 },
                    { 2, 2, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.0, null, 2 },
                    { 3, 3, new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 160.0, 200.0, 2 }
                });
#pragma warning restore CA1861 // Evitar matrices constantes como argumentos
#pragma warning restore IDE0300 // Simplificar la inicialización de la recopilación

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Document",
                table: "Customers",
                column: "Document",
                unique: true,
                filter: "[Document] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Rentings_CustomerId",
                table: "Rentings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentings_VehicleId",
                table: "Rentings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_NumberId",
                table: "Vehicles",
                column: "NumberId",
                unique: true,
                filter: "[NumberId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Validar argumentos de métodos públicos
            migrationBuilder.DropTable(
                name: "Parameters");
#pragma warning restore CA1062 // Validar argumentos de métodos públicos

            migrationBuilder.DropTable(
                name: "Rentings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
