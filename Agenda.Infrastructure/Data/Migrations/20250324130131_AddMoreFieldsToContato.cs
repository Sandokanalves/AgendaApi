using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreFieldsToContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adicionar novas colunas à tabela existente "Contatos"
            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Contatos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Contatos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "Contatos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelefoneComercial",
                table: "Contatos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remover as colunas adicionadas em caso de rollback
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "TelefoneComercial",
                table: "Contatos");
        }
    }
}
