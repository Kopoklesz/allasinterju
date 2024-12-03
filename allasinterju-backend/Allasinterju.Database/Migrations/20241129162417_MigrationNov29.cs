using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allasinterju.Database.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNov29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kitolteskezdet",
                table: "kitoltottkerdes");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "teszteset",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "kimenet",
                table: "lefutottteszteset",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "helyes",
                table: "lefutottteszteset",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "lefutottteszteset",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "futasido",
                table: "lefutottteszteset",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hibakimenet",
                table: "lefutottteszteset",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "lefutottteszteset",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "kitoltottkerdoiv",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "kitolteskezdet",
                table: "kitoltottkerdoiv",
                type: "datetime",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "kerdoiv",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "algorithm",
                table: "kerdoiv",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "design",
                table: "kerdoiv",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "devops",
                table: "kerdoiv",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "programming",
                table: "kerdoiv",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "testing",
                table: "kerdoiv",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "programnyelv",
                table: "kerdes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "szint",
                table: "felhasznalokompetencia",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "dokumentum",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "allaskompetencia",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "szint",
                table: "allaskompetencia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ajanlas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    allasid = table.Column<int>(type: "int", nullable: false),
                    allaskeresoid = table.Column<int>(type: "int", nullable: false),
                    jelentkezve = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajanlas", x => x.id);
                    table.ForeignKey(
                        name: "FK_ajanlas_allas",
                        column: x => x.allasid,
                        principalTable: "allas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ajanlas_felhasznalo",
                        column: x => x.allaskeresoid,
                        principalTable: "felhasznalo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "algorithm",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timelimitmin = table.Column<int>(type: "int", nullable: true),
                    memory = table.Column<int>(type: "int", nullable: true),
                    problemdesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    inputformat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    outputformat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timecomplexity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    spacecomplexity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    samplesolution = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_algorithm", x => x.id);
                    table.ForeignKey(
                        name: "FK_algorithm_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "design",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    styleguide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deliverables = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_design", x => x.id);
                    table.ForeignKey(
                        name: "FK_design_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devops",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    tasktitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    difficulty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    taskdescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timelimitminute = table.Column<int>(type: "int", nullable: true),
                    platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    systemrequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resourcelimits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    accessrequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    architecturedesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    infraconstraints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    docrequired = table.Column<bool>(type: "bit", nullable: true),
                    docformat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devops", x => x.id);
                    table.ForeignKey(
                        name: "FK_devops_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "programming",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<int>(type: "int", nullable: false),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codetemplate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programming", x => x.id);
                    table.ForeignKey(
                        name: "FK_programming_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testing",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    taskdesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    testingtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timelimitmin = table.Column<int>(type: "int", nullable: false),
                    appurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    os = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    additionalreq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stepstoreproduce = table.Column<int>(type: "int", nullable: true),
                    expectedresult = table.Column<int>(type: "int", nullable: true),
                    actualresult = table.Column<int>(type: "int", nullable: true),
                    defaultseverity = table.Column<int>(type: "int", nullable: true),
                    defaultpriority = table.Column<int>(type: "int", nullable: true),
                    requireattachments = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testing", x => x.id);
                    table.ForeignKey(
                        name: "FK_testing_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "algorithmconstraint",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    algorithmid = table.Column<int>(type: "int", nullable: false),
                    constraint = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_algorithmconstraint", x => x.id);
                    table.ForeignKey(
                        name: "FK_algorithmconstraint_algorithm",
                        column: x => x.algorithmid,
                        principalTable: "algorithm",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "algorithmexample",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    algorithmid = table.Column<int>(type: "int", nullable: false),
                    input = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    output = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    explanation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_algorithmexample", x => x.id);
                    table.ForeignKey(
                        name: "FK_algorithmexample_algorithm",
                        column: x => x.algorithmid,
                        principalTable: "algorithm",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "algorithmhint",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    algorithmid = table.Column<int>(type: "int", nullable: false),
                    hint = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_algorithmhint", x => x.id);
                    table.ForeignKey(
                        name: "FK_algorithmhint_algorithm",
                        column: x => x.algorithmid,
                        principalTable: "algorithm",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "algortihmtestcase",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    algorithmid = table.Column<int>(type: "int", nullable: false),
                    input = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    output = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hidden = table.Column<bool>(type: "bit", nullable: false),
                    points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_algortihmtestcase", x => x.id);
                    table.ForeignKey(
                        name: "FK_algortihmtestcase_algorithm",
                        column: x => x.algorithmid,
                        principalTable: "algorithm",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "designevaluation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    designid = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designevaluation", x => x.id);
                    table.ForeignKey(
                        name: "FK_designevaluation_design",
                        column: x => x.designid,
                        principalTable: "design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "designreference",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    designid = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designreference", x => x.id);
                    table.ForeignKey(
                        name: "FK_designreference_design",
                        column: x => x.designid,
                        principalTable: "design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "designreq",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    designid = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designreq", x => x.id);
                    table.ForeignKey(
                        name: "FK_designreq_design",
                        column: x => x.designid,
                        principalTable: "design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devopscomponent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    devopsid = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    configuration = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devopscomponent", x => x.id);
                    table.ForeignKey(
                        name: "FK_devopscomponent_devops",
                        column: x => x.devopsid,
                        principalTable: "devops",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devopsdeliverable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    devopsid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    acceptance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    format = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devopsdeliverable", x => x.id);
                    table.ForeignKey(
                        name: "FK_devopsdeliverable_devops",
                        column: x => x.devopsid,
                        principalTable: "devops",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devopsdocumentation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    devopsid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<int>(type: "int", nullable: true),
                    templatecontent = table.Column<int>(type: "int", nullable: true),
                    requiredtemplate = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devopsdocumentation", x => x.id);
                    table.ForeignKey(
                        name: "FK_devopsdocumentation_devops",
                        column: x => x.devopsid,
                        principalTable: "devops",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devopsevaluation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    devopsid = table.Column<int>(type: "int", nullable: false),
                    criterion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devopsevaluation", x => x.id);
                    table.ForeignKey(
                        name: "FK_devopsevaluation_devops",
                        column: x => x.devopsid,
                        principalTable: "devops",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devopsprereq",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    devopsid = table.Column<int>(type: "int", nullable: false),
                    tool = table.Column<int>(type: "int", nullable: false),
                    version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    purpose = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devopsprereq", x => x.id);
                    table.ForeignKey(
                        name: "FK_devopsprereq_devops",
                        column: x => x.devopsid,
                        principalTable: "devops",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devopstask",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    devopsid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    validation = table.Column<int>(type: "int", nullable: true),
                    points = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devopstask", x => x.id);
                    table.ForeignKey(
                        name: "FK_devopstask_devops",
                        column: x => x.devopsid,
                        principalTable: "devops",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "programmingtestcase",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    programmingid = table.Column<int>(type: "int", nullable: false),
                    input = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    output = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programmingtestcase", x => x.id);
                    table.ForeignKey(
                        name: "FK_programmingtestcase_programming",
                        column: x => x.programmingid,
                        principalTable: "programming",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testingcase",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    testingid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expectedresult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    testdata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    canbeautomated = table.Column<bool>(type: "bit", nullable: true),
                    points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testingcase", x => x.id);
                    table.ForeignKey(
                        name: "FK_testingcase_testing",
                        column: x => x.testingid,
                        principalTable: "testing",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testingevaluation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    testingid = table.Column<int>(type: "int", nullable: false),
                    criterion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    weight = table.Column<double>(type: "float", nullable: true),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testingevaluation", x => x.id);
                    table.ForeignKey(
                        name: "FK_testingevaluation_testing",
                        column: x => x.testingid,
                        principalTable: "testing",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testingscenario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    testingid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prereq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    priority = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testingscenario", x => x.id);
                    table.ForeignKey(
                        name: "FK_testingscenario_testing",
                        column: x => x.testingid,
                        principalTable: "testing",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testingtool",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    testingid = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    purpose = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testingtool", x => x.id);
                    table.ForeignKey(
                        name: "FK_testingtool_testing",
                        column: x => x.testingid,
                        principalTable: "testing",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "devopstaskimplementation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    devopstaskid = table.Column<int>(type: "int", nullable: false),
                    implementation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devopstaskimplementation", x => x.id);
                    table.ForeignKey(
                        name: "FK_devopstaskimplementation_devopstask",
                        column: x => x.devopstaskid,
                        principalTable: "devopstask",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "testingcasestep",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    testingcaseid = table.Column<int>(type: "int", nullable: false),
                    teststep = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testingcasestep", x => x.id);
                    table.ForeignKey(
                        name: "FK_testingcasestep_testingcase",
                        column: x => x.testingcaseid,
                        principalTable: "testingcase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ajanlas_allasid",
                table: "ajanlas",
                column: "allasid");

            migrationBuilder.CreateIndex(
                name: "IX_ajanlas_allaskeresoid",
                table: "ajanlas",
                column: "allaskeresoid");

            migrationBuilder.CreateIndex(
                name: "IX_algorithm_kerdoivid",
                table: "algorithm",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_algorithmconstraint_algorithmid",
                table: "algorithmconstraint",
                column: "algorithmid");

            migrationBuilder.CreateIndex(
                name: "IX_algorithmexample_algorithmid",
                table: "algorithmexample",
                column: "algorithmid");

            migrationBuilder.CreateIndex(
                name: "IX_algorithmhint_algorithmid",
                table: "algorithmhint",
                column: "algorithmid");

            migrationBuilder.CreateIndex(
                name: "IX_algortihmtestcase_algorithmid",
                table: "algortihmtestcase",
                column: "algorithmid");

            migrationBuilder.CreateIndex(
                name: "IX_design_kerdoivid",
                table: "design",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_designevaluation_designid",
                table: "designevaluation",
                column: "designid");

            migrationBuilder.CreateIndex(
                name: "IX_designreference_designid",
                table: "designreference",
                column: "designid");

            migrationBuilder.CreateIndex(
                name: "IX_designreq_designid",
                table: "designreq",
                column: "designid");

            migrationBuilder.CreateIndex(
                name: "IX_devops_kerdoivid",
                table: "devops",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_devopscomponent_devopsid",
                table: "devopscomponent",
                column: "devopsid");

            migrationBuilder.CreateIndex(
                name: "IX_devopsdeliverable_devopsid",
                table: "devopsdeliverable",
                column: "devopsid");

            migrationBuilder.CreateIndex(
                name: "IX_devopsdocumentation_devopsid",
                table: "devopsdocumentation",
                column: "devopsid");

            migrationBuilder.CreateIndex(
                name: "IX_devopsevaluation_devopsid",
                table: "devopsevaluation",
                column: "devopsid");

            migrationBuilder.CreateIndex(
                name: "IX_devopsprereq_devopsid",
                table: "devopsprereq",
                column: "devopsid");

            migrationBuilder.CreateIndex(
                name: "IX_devopstask_devopsid",
                table: "devopstask",
                column: "devopsid");

            migrationBuilder.CreateIndex(
                name: "IX_devopstaskimplementation_devopstaskid",
                table: "devopstaskimplementation",
                column: "devopstaskid");

            migrationBuilder.CreateIndex(
                name: "IX_programming_kerdoivid",
                table: "programming",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_programmingtestcase_programmingid",
                table: "programmingtestcase",
                column: "programmingid");

            migrationBuilder.CreateIndex(
                name: "IX_testing_kerdoivid",
                table: "testing",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_testingcase_testingid",
                table: "testingcase",
                column: "testingid");

            migrationBuilder.CreateIndex(
                name: "IX_testingcasestep_testingcaseid",
                table: "testingcasestep",
                column: "testingcaseid");

            migrationBuilder.CreateIndex(
                name: "IX_testingevaluation_testingid",
                table: "testingevaluation",
                column: "testingid");

            migrationBuilder.CreateIndex(
                name: "IX_testingscenario_testingid",
                table: "testingscenario",
                column: "testingid");

            migrationBuilder.CreateIndex(
                name: "IX_testingtool_testingid",
                table: "testingtool",
                column: "testingid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ajanlas");

            migrationBuilder.DropTable(
                name: "algorithmconstraint");

            migrationBuilder.DropTable(
                name: "algorithmexample");

            migrationBuilder.DropTable(
                name: "algorithmhint");

            migrationBuilder.DropTable(
                name: "algortihmtestcase");

            migrationBuilder.DropTable(
                name: "designevaluation");

            migrationBuilder.DropTable(
                name: "designreference");

            migrationBuilder.DropTable(
                name: "designreq");

            migrationBuilder.DropTable(
                name: "devopscomponent");

            migrationBuilder.DropTable(
                name: "devopsdeliverable");

            migrationBuilder.DropTable(
                name: "devopsdocumentation");

            migrationBuilder.DropTable(
                name: "devopsevaluation");

            migrationBuilder.DropTable(
                name: "devopsprereq");

            migrationBuilder.DropTable(
                name: "devopstaskimplementation");

            migrationBuilder.DropTable(
                name: "programmingtestcase");

            migrationBuilder.DropTable(
                name: "testingcasestep");

            migrationBuilder.DropTable(
                name: "testingevaluation");

            migrationBuilder.DropTable(
                name: "testingscenario");

            migrationBuilder.DropTable(
                name: "testingtool");

            migrationBuilder.DropTable(
                name: "algorithm");

            migrationBuilder.DropTable(
                name: "design");

            migrationBuilder.DropTable(
                name: "devopstask");

            migrationBuilder.DropTable(
                name: "programming");

            migrationBuilder.DropTable(
                name: "testingcase");

            migrationBuilder.DropTable(
                name: "devops");

            migrationBuilder.DropTable(
                name: "testing");

            migrationBuilder.DropColumn(
                name: "futasido",
                table: "lefutottteszteset");

            migrationBuilder.DropColumn(
                name: "hibakimenet",
                table: "lefutottteszteset");

            migrationBuilder.DropColumn(
                name: "token",
                table: "lefutottteszteset");

            migrationBuilder.DropColumn(
                name: "kitolteskezdet",
                table: "kitoltottkerdoiv");

            migrationBuilder.DropColumn(
                name: "algorithm",
                table: "kerdoiv");

            migrationBuilder.DropColumn(
                name: "design",
                table: "kerdoiv");

            migrationBuilder.DropColumn(
                name: "devops",
                table: "kerdoiv");

            migrationBuilder.DropColumn(
                name: "programming",
                table: "kerdoiv");

            migrationBuilder.DropColumn(
                name: "testing",
                table: "kerdoiv");

            migrationBuilder.DropColumn(
                name: "programnyelv",
                table: "kerdes");

            migrationBuilder.DropColumn(
                name: "szint",
                table: "felhasznalokompetencia");

            migrationBuilder.DropColumn(
                name: "szint",
                table: "allaskompetencia");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "teszteset",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "kimenet",
                table: "lefutottteszteset",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "helyes",
                table: "lefutottteszteset",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "lefutottteszteset",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "kitoltottkerdoiv",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "kitolteskezdet",
                table: "kitoltottkerdes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "kerdoiv",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "dokumentum",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "allaskompetencia",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
