﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AlertToCareAPI.Migrations
{
    public partial class AlertToCareAPIICUDatabaseEntitiesIcuContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beds",
                columns: table => new
                {
                    BedId = table.Column<string>(nullable: false),
                    IcuId = table.Column<string>(nullable: true),
                    BedStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.BedId);
                });

            migrationBuilder.CreateTable(
                name: "ICUs",
                columns: table => new
                {
                    IcuId = table.Column<string>(nullable: false),
                    Layout = table.Column<string>(nullable: true),
                    BedsCount = table.Column<int>(nullable: false),
                    BedIdPrefix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICUs", x => x.IcuId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<string>(nullable: false),
                    PatientName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    ContactNo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BedId = table.Column<string>(nullable: true),
                    ICUId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_ICUs_ICUId",
                        column: x => x.ICUId,
                        principalTable: "ICUs",
                        principalColumn: "IcuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientAddress",
                columns: table => new
                {
                    PatientId = table.Column<string>(nullable: false),
                    HouseNo = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Pincode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAddress", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_PatientAddress_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vitals",
                columns: table => new
                {
                    PatientId = table.Column<string>(nullable: false),
                    Bpm = table.Column<float>(nullable: false),
                    Spo2 = table.Column<float>(nullable: false),
                    RespRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitals", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Vitals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ICUId",
                table: "Patients",
                column: "ICUId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "PatientAddress");

            migrationBuilder.DropTable(
                name: "Vitals");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ICUs");
        }
    }
}