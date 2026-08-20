using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourtifyBE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    USERNAME = table.Column<string>(type: "text", nullable: false),
                    PASSWORD = table.Column<string>(type: "text", nullable: false),
                    NAME = table.Column<string>(type: "text", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CourtCategories",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CATEGORY = table.Column<string>(type: "text", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ITEM_NAME = table.Column<string>(type: "text", nullable: false),
                    RENTAL_FEE = table.Column<decimal>(type: "numeric", nullable: false),
                    STOCK = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NAME_VENUE = table.Column<string>(type: "text", nullable: false),
                    ADDRESS = table.Column<string>(type: "text", nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VENUE_ID = table.Column<long>(type: "bigint", nullable: false),
                    COURT_CATEGORY_ID = table.Column<long>(type: "bigint", nullable: false),
                    COURT_NAME = table.Column<string>(type: "text", nullable: false),
                    HOURLY_RATE = table.Column<decimal>(type: "numeric", nullable: false),
                    IS_AVAILABLE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Courts_CourtCategories_COURT_CATEGORY_ID",
                        column: x => x.COURT_CATEGORY_ID,
                        principalTable: "CourtCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courts_Venues_VENUE_ID",
                        column: x => x.VENUE_ID,
                        principalTable: "Venues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BOOKING_CODE = table.Column<string>(type: "text", nullable: false),
                    ADMIN_ID = table.Column<long>(type: "bigint", nullable: false),
                    COURTS_ID = table.Column<long>(type: "bigint", nullable: false),
                    CUSTOMER_NAME = table.Column<string>(type: "text", nullable: false),
                    BOOKING_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    START_TIME = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    END_TIME = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    TOTAL_HOURS = table.Column<int>(type: "integer", nullable: false),
                    BASE_AMOUNT = table.Column<decimal>(type: "numeric", nullable: false),
                    TOTAL_AMOUNT = table.Column<decimal>(type: "numeric", nullable: false),
                    STATUS = table.Column<string>(type: "text", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Admins_ADMIN_ID",
                        column: x => x.ADMIN_ID,
                        principalTable: "Admins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Courts_COURTS_ID",
                        column: x => x.COURTS_ID,
                        principalTable: "Courts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingAddOns",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BOOKING_ID = table.Column<long>(type: "bigint", nullable: false),
                    EQUIPMENT_ID = table.Column<long>(type: "bigint", nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    UNIT_PRICE = table.Column<decimal>(type: "numeric", nullable: false),
                    SUB_TOTAL = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingAddOns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingAddOns_Bookings_BOOKING_ID",
                        column: x => x.BOOKING_ID,
                        principalTable: "Bookings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingAddOns_Equipments_EQUIPMENT_ID",
                        column: x => x.EQUIPMENT_ID,
                        principalTable: "Equipments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentReceipts",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BOOKING_ID = table.Column<long>(type: "bigint", nullable: false),
                    PAYMENT_METHOD = table.Column<string>(type: "text", nullable: false),
                    RECEIPT_NUMBER = table.Column<string>(type: "text", nullable: false),
                    STATUS = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentReceipts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentReceipts_Bookings_BOOKING_ID",
                        column: x => x.BOOKING_ID,
                        principalTable: "Bookings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_USERNAME",
                table: "Admins",
                column: "USERNAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingAddOns_BOOKING_ID",
                table: "BookingAddOns",
                column: "BOOKING_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingAddOns_EQUIPMENT_ID",
                table: "BookingAddOns",
                column: "EQUIPMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ADMIN_ID",
                table: "Bookings",
                column: "ADMIN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_COURTS_ID",
                table: "Bookings",
                column: "COURTS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Courts_COURT_CATEGORY_ID",
                table: "Courts",
                column: "COURT_CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Courts_VENUE_ID",
                table: "Courts",
                column: "VENUE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentReceipts_BOOKING_ID",
                table: "PaymentReceipts",
                column: "BOOKING_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingAddOns");

            migrationBuilder.DropTable(
                name: "PaymentReceipts");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "CourtCategories");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
