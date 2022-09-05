using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class ChaveEstrangeiraMonitoramento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitoramentoPaciente_Pacientes_PacienteId",
                table: "MonitoramentoPaciente");

            migrationBuilder.DropIndex(
                name: "IX_MonitoramentoPaciente_PacienteId",
                table: "MonitoramentoPaciente");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "MonitoramentoPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoPaciente_IdPaciente",
                table: "MonitoramentoPaciente",
                column: "IdPaciente");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoramentoPaciente_Pacientes_IdPaciente",
                table: "MonitoramentoPaciente",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitoramentoPaciente_Pacientes_IdPaciente",
                table: "MonitoramentoPaciente");

            migrationBuilder.DropIndex(
                name: "IX_MonitoramentoPaciente_IdPaciente",
                table: "MonitoramentoPaciente");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "MonitoramentoPaciente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoPaciente_PacienteId",
                table: "MonitoramentoPaciente",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoramentoPaciente_Pacientes_PacienteId",
                table: "MonitoramentoPaciente",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
