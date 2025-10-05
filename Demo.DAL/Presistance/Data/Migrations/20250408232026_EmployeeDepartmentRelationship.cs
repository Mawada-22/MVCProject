using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DAL.Presistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDepartmentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HiringDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<int>(
                name: "Departmentid",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Departmentid",
                table: "Employees",
                column: "Departmentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Departmentid",
                table: "Employees",
                column: "Departmentid",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Departmentid",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Departmentid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Departmentid",
                table: "Employees");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "HiringDate",
                table: "Employees",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
