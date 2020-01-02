using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.EntityFrameworkCore.TestData.Migrations
{
    public partial class Created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_Courses_Code",
                        column: x => x.Code,
                        principalTable: "Courses",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                    table.ForeignKey(
                        name: "FK_Logins_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSession",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSession", x => new { x.SessionId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentSession_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentSession_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "UARCH", "Patterns and Practices" },
                    { "UWEBA", "Advanced Web Development" },
                    { "UCORE", "Upgrade to DotNet Core" },
                    { "UDEF", "Domain Driven Design With Entity Framework Core" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Joske", "Vermeulen" },
                    { 2, "Eddy", "Wally" },
                    { 3, "Sam", "Goris" }
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Provider", "StudentId" },
                values: new object[,]
                {
                    { 1, "X", 1 },
                    { 2, "X", 2 },
                    { 3, "X", 3 }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "Code", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, "UARCH", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "UWEBA", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "UCORE", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_StudentId",
                table: "Logins",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Code",
                table: "Sessions",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSession_StudentId",
                table: "StudentSession",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "StudentSession");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
