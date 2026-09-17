using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePacienteIdFromNotaClinica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasClinicas_Pacientes_PacienteId",
                table: "NotasClinicas");

            migrationBuilder.DropIndex(
                name: "IX_NotasClinicas_PacienteId",
                table: "NotasClinicas");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "NotasClinicas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "NotasClinicas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NotasClinicas_PacienteId",
                table: "NotasClinicas",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasClinicas_Pacientes_PacienteId",
                table: "NotasClinicas",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
