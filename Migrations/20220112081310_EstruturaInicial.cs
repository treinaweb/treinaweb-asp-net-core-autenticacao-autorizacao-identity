using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    public partial class EstruturaInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CRM = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdMedico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Medicos_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformacoesComplementaresPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alergias = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MedicamentosEmUso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CirurgiasRealizadas = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacoesComplementaresPaciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacoesComplementaresPaciente_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoramentoPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PressaoArterial = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Temperatura = table.Column<decimal>(type: "DECIMAL(3,1)", nullable: false),
                    SaturacaoOxigenio = table.Column<byte>(type: "TINYINT", nullable: false),
                    FrequenciaCardiaca = table.Column<byte>(type: "TINYINT", nullable: false),
                    DataAfericao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoramentoPaciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoramentoPaciente_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IdMedico",
                table: "Consultas",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IdPaciente",
                table: "Consultas",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_InformacoesComplementaresPaciente_IdPaciente",
                table: "InformacoesComplementaresPaciente",
                column: "IdPaciente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_CRM",
                table: "Medicos",
                column: "CRM",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoPaciente_PacienteId",
                table: "MonitoramentoPaciente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_CPF",
                table: "Pacientes",
                column: "CPF",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "InformacoesComplementaresPaciente");

            migrationBuilder.DropTable(
                name: "MonitoramentoPaciente");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
