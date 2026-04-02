using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allasinterju.Database.Migrations
{
    /// <inheritdoc />
    public partial class dec09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "timelimitmin",
                table: "testing");

            migrationBuilder.DropColumn(
                name: "timelimitminute",
                table: "devops");

            migrationBuilder.DropColumn(
                name: "timelimitmin",
                table: "algorithm");

            migrationBuilder.RenameColumn(
                name: "osszpont",
                table: "kitoltottkerdoiv",
                newName: "miszazalek");

            migrationBuilder.AlterColumn<string>(
                name: "teststep",
                table: "testingcasestep",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "stepstoreproduce",
                table: "testing",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "requireattachments",
                table: "testing",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "expectedresult",
                table: "testing",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "defaultseverity",
                table: "testing",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "defaultpriority",
                table: "testing",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "actualresult",
                table: "testing",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "testing",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "programmingtestcase",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "programming",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "programming",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "programming",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "szazalek",
                table: "kitoltottkerdoiv",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "kivalasztva",
                table: "kitoltottallas",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "programming",
                table: "kerdoiv",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "validation",
                table: "devopstask",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tool",
                table: "devopsprereq",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "weight",
                table: "devopsevaluation",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "devopsdocumentation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "templatecontent",
                table: "devopsdocumentation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tasktitle",
                table: "devops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "taskdescription",
                table: "devops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "systemrequirements",
                table: "devops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "resourcelimits",
                table: "devops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "platform",
                table: "devops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "architecturedesc",
                table: "devops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "accessrequirements",
                table: "devops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "design",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "szint",
                table: "allaskompetencia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "problemdesc",
                table: "algorithm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "difficulty",
                table: "algorithm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "algorithm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "algorithm",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "k_programming",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    programmingid = table.Column<int>(type: "int", nullable: false),
                    kitoltottkerdoivid = table.Column<int>(type: "int", nullable: false),
                    programkod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_k_programming", x => x.id);
                    table.ForeignKey(
                        name: "FK_k_programming_kitoltottkerdoiv",
                        column: x => x.kitoltottkerdoivid,
                        principalTable: "kitoltottkerdoiv",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_k_programming_programming",
                        column: x => x.programmingid,
                        principalTable: "programming",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "k_tobbi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kitoltottkerdoivid = table.Column<int>(type: "int", nullable: false),
                    algorithmid = table.Column<int>(type: "int", nullable: true),
                    designid = table.Column<int>(type: "int", nullable: true),
                    testingid = table.Column<int>(type: "int", nullable: true),
                    devopsid = table.Column<int>(type: "int", nullable: true),
                    szovegesvalasz = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_k_tobbi", x => x.id);
                    table.ForeignKey(
                        name: "FK_k_tobbi_algorithm",
                        column: x => x.algorithmid,
                        principalTable: "algorithm",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_k_tobbi_design_1",
                        column: x => x.designid,
                        principalTable: "design",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_k_tobbi_devops_2",
                        column: x => x.devopsid,
                        principalTable: "devops",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_k_tobbi_kitoltottkerdoiv",
                        column: x => x.kitoltottkerdoivid,
                        principalTable: "kitoltottkerdoiv",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_k_tobbi_testing_3",
                        column: x => x.testingid,
                        principalTable: "testing",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vegzettseg",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    felhasznaloid = table.Column<int>(type: "int", nullable: false),
                    rovidleiras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hosszuleiras = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vegzettseg", x => x.id);
                    table.ForeignKey(
                        name: "FK_vegzettseg_felhasznalo",
                        column: x => x.felhasznaloid,
                        principalTable: "felhasznalo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "k_programmingtestcase",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    k_programmingid = table.Column<int>(type: "int", nullable: false),
                    programmingtestcaseid = table.Column<int>(type: "int", nullable: false),
                    stdout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stderr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memoria = table.Column<double>(type: "float", nullable: true),
                    futasido = table.Column<int>(type: "int", nullable: true),
                    lefutott = table.Column<bool>(type: "bit", nullable: false),
                    nemfutle = table.Column<bool>(type: "bit", nullable: true),
                    helyes = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_k_programmingtestcase", x => x.id);
                    table.ForeignKey(
                        name: "FK_k_programmingtestcase_k_programming",
                        column: x => x.k_programmingid,
                        principalTable: "k_programming",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_k_programmingtestcase_programmingtestcase",
                        column: x => x.programmingtestcaseid,
                        principalTable: "programmingtestcase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_k_programming_kitoltottkerdoivid",
                table: "k_programming",
                column: "kitoltottkerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_k_programming_programmingid",
                table: "k_programming",
                column: "programmingid");

            migrationBuilder.CreateIndex(
                name: "IX_k_programmingtestcase_k_programmingid",
                table: "k_programmingtestcase",
                column: "k_programmingid");

            migrationBuilder.CreateIndex(
                name: "IX_k_programmingtestcase_programmingtestcaseid",
                table: "k_programmingtestcase",
                column: "programmingtestcaseid");

            migrationBuilder.CreateIndex(
                name: "IX_k_tobbi_algorithmid",
                table: "k_tobbi",
                column: "algorithmid");

            migrationBuilder.CreateIndex(
                name: "IX_k_tobbi_designid",
                table: "k_tobbi",
                column: "designid");

            migrationBuilder.CreateIndex(
                name: "IX_k_tobbi_devopsid",
                table: "k_tobbi",
                column: "devopsid");

            migrationBuilder.CreateIndex(
                name: "IX_k_tobbi_kitoltottkerdoivid",
                table: "k_tobbi",
                column: "kitoltottkerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_k_tobbi_testingid",
                table: "k_tobbi",
                column: "testingid");

            migrationBuilder.CreateIndex(
                name: "IX_vegzettseg_felhasznaloid",
                table: "vegzettseg",
                column: "felhasznaloid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "k_programmingtestcase");

            migrationBuilder.DropTable(
                name: "k_tobbi");

            migrationBuilder.DropTable(
                name: "vegzettseg");

            migrationBuilder.DropTable(
                name: "k_programming");

            migrationBuilder.DropColumn(
                name: "szazalek",
                table: "kitoltottkerdoiv");

            migrationBuilder.DropColumn(
                name: "kivalasztva",
                table: "kitoltottallas");

            migrationBuilder.RenameColumn(
                name: "miszazalek",
                table: "kitoltottkerdoiv",
                newName: "osszpont");

            migrationBuilder.AlterColumn<int>(
                name: "teststep",
                table: "testingcasestep",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "stepstoreproduce",
                table: "testing",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "requireattachments",
                table: "testing",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "expectedresult",
                table: "testing",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "defaultseverity",
                table: "testing",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "defaultpriority",
                table: "testing",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "actualresult",
                table: "testing",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "testing",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "timelimitmin",
                table: "testing",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "programmingtestcase",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "title",
                table: "programming",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "description",
                table: "programming",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "programming",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "programming",
                table: "kerdoiv",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "validation",
                table: "devopstask",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "tool",
                table: "devopsprereq",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "weight",
                table: "devopsevaluation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "title",
                table: "devopsdocumentation",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "templatecontent",
                table: "devopsdocumentation",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "tasktitle",
                table: "devops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "taskdescription",
                table: "devops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "systemrequirements",
                table: "devops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "resourcelimits",
                table: "devops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "platform",
                table: "devops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "architecturedesc",
                table: "devops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "accessrequirements",
                table: "devops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "timelimitminute",
                table: "devops",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "design",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "szint",
                table: "allaskompetencia",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "problemdesc",
                table: "algorithm",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "difficulty",
                table: "algorithm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "algorithm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "algorithm",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "timelimitmin",
                table: "algorithm",
                type: "int",
                nullable: true);
        }
    }
}
