using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace U2U.EntityFrameworkCore.TestData.Migrations
{
    public partial class Refectoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Students_StudentId",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Courses_Code",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSession_Sessions_SessionId",
                table: "StudentSession");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSession_Students_StudentId",
                table: "StudentSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_Code",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logins",
                table: "Logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Code",
                keyValue: "UDEF");

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "LoginId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "LoginId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "LoginId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Code",
                keyValue: "UARCH");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Code",
                keyValue: "UCORE");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Code",
                keyValue: "UWEBA");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Logins");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Students",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sessions",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Logins",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Courses",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Courses",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logins",
                table: "Logins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "UARCH", "Patterns and Practices" },
                    { 2, "UWEBA", "Advanced Web Development" },
                    { 3, "UCORE", "Upgrade to DotNet Core" },
                    { 4, "UDEF", "Domain Driven Design With Entity Framework Core" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Joske", "Vermeulen" },
                    { 2, "Eddy", "Wally" },
                    { 3, "Sam", "Goris" }
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "Provider", "StudentId" },
                values: new object[,]
                {
                    { 1, "X", 1 },
                    { 2, "X", 2 },
                    { 3, "X", 3 }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "CourseId", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 3, new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 3, new DateTime(2020, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "StudentSession",
                columns: new[] { "SessionId", "StudentId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CourseId",
                table: "Sessions",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Students_StudentId",
                table: "Logins",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Courses_CourseId",
                table: "Sessions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSession_Sessions_SessionId",
                table: "StudentSession",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSession_Students_StudentId",
                table: "StudentSession",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Students_StudentId",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Courses_CourseId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSession_Sessions_SessionId",
                table: "StudentSession");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSession_Students_StudentId",
                table: "StudentSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_CourseId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logins",
                table: "Logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StudentSession",
                keyColumns: new[] { "SessionId", "StudentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentSession",
                keyColumns: new[] { "SessionId", "StudentId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "StudentSession",
                keyColumns: new[] { "SessionId", "StudentId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "StudentSession",
                keyColumns: new[] { "SessionId", "StudentId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "StudentSession",
                keyColumns: new[] { "SessionId", "StudentId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "StudentSession",
                keyColumns: new[] { "SessionId", "StudentId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Sessions",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "Logins",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Courses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "SessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logins",
                table: "Logins",
                column: "LoginId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Code");

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "Code", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, "UARCH", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "UWEBA", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "UCORE", new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                values: new object[] { 1, "X", 1 });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Provider", "StudentId" },
                values: new object[] { 2, "X", 2 });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Provider", "StudentId" },
                values: new object[] { 3, "X", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Code",
                table: "Sessions",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Students_StudentId",
                table: "Logins",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Courses_Code",
                table: "Sessions",
                column: "Code",
                principalTable: "Courses",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSession_Sessions_SessionId",
                table: "StudentSession",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSession_Students_StudentId",
                table: "StudentSession",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
