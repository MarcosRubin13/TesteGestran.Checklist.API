using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteGestran.Checklist.Infra.Migrations
{
    public partial class NovosCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataFinalizacao",
                table: "Checklists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Motorista",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Checklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFinalizacao",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "Motorista",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "Placa",
                table: "Checklists");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Checklists");
        }
    }
}
