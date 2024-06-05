using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoWater.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incidentes",
                columns: table => new
                {
                    Id_Incidente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Id_Embarcacao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Data = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tipo_Poluicao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Severidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes", x => x.Id_Incidente);
                });

            migrationBuilder.CreateTable(
                name: "Proprietarios",
                columns: table => new
                {
                    Id_Proprietario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietarios", x => x.Id_Proprietario);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosPoluicao",
                columns: table => new
                {
                    Id_Registro = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Id_Embarcacao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Data = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Hora = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tipo_Poluicao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Quantidade_Poluida = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Testemunhas = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosPoluicao", x => x.Id_Registro);
                });

            migrationBuilder.CreateTable(
                name: "Sensores",
                columns: table => new
                {
                    Id_Sensor = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Data_Instalacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.Id_Sensor);
                });

            migrationBuilder.CreateTable(
                name: "Embarcacoes",
                columns: table => new
                {
                    Id_Embarcacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Bandeira = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Capacidade = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Ano_Fabricação = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_Proprietario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Embarcacoes", x => x.Id_Embarcacao);
                    table.ForeignKey(
                        name: "FK_Embarcacoes_Proprietarios_Id_Proprietario",
                        column: x => x.Id_Proprietario,
                        principalTable: "Proprietarios",
                        principalColumn: "Id_Proprietario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monitoramentos",
                columns: table => new
                {
                    Id_Monitoramento = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Id_Embarcacao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_Sensor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Data = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Hora = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Nivel_Poluicao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitoramentos", x => x.Id_Monitoramento);
                    table.ForeignKey(
                        name: "FK_Monitoramentos_Embarcacoes_Id_Embarcacao",
                        column: x => x.Id_Embarcacao,
                        principalTable: "Embarcacoes",
                        principalColumn: "Id_Embarcacao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Monitoramentos_Sensores_Id_Sensor",
                        column: x => x.Id_Sensor,
                        principalTable: "Sensores",
                        principalColumn: "Id_Sensor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Embarcacoes_Id_Proprietario",
                table: "Embarcacoes",
                column: "Id_Proprietario");

            migrationBuilder.CreateIndex(
                name: "IX_Monitoramentos_Id_Embarcacao",
                table: "Monitoramentos",
                column: "Id_Embarcacao");

            migrationBuilder.CreateIndex(
                name: "IX_Monitoramentos_Id_Sensor",
                table: "Monitoramentos",
                column: "Id_Sensor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidentes");

            migrationBuilder.DropTable(
                name: "Monitoramentos");

            migrationBuilder.DropTable(
                name: "RegistrosPoluicao");

            migrationBuilder.DropTable(
                name: "Embarcacoes");

            migrationBuilder.DropTable(
                name: "Sensores");

            migrationBuilder.DropTable(
                name: "Proprietarios");
        }
    }
}
