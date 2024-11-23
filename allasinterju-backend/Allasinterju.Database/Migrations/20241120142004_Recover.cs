using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allasinterju.Database.Migrations
{
    /// <inheritdoc />
    public partial class Recover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ceg",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jelszo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cegnev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cegtipus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    leiras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    levelezesicim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kapcsolattarto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kep = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    mobiltelefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kapcsolattartonev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telephely = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ceg", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cegtelephely",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    irsz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telepules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    utcahazszam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cimszoveg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cegid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cegtelephely", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "felhasznalo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vezeteknev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    keresztnev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jelszo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adoszam = table.Column<long>(type: "bigint", nullable: true),
                    anyjaneve = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    szuldat = table.Column<DateTime>(type: "datetime", nullable: true),
                    szulirsz = table.Column<int>(type: "int", nullable: true),
                    szulhely = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dolgozo = table.Column<bool>(type: "bit", nullable: false),
                    allaskereso = table.Column<bool>(type: "bit", nullable: false),
                    cegid = table.Column<int>(type: "int", nullable: true),
                    kep = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    leetcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_felhasznalo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kompetencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kompetencia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "allas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    munkakor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    munkarend = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    leiras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rovidleiras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cegid = table.Column<int>(type: "int", nullable: false),
                    hatarido = table.Column<DateTime>(type: "datetime", nullable: true),
                    kitoltesido = table.Column<TimeOnly>(type: "time", nullable: true),
                    telephelyszoveg = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allas", x => x.id);
                    table.ForeignKey(
                        name: "FK_allas_ceg",
                        column: x => x.cegid,
                        principalTable: "ceg",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "meghivokod",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ervenyesseg = table.Column<DateTime>(type: "datetime", nullable: true),
                    cegid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meghivokod", x => x.id);
                    table.ForeignKey(
                        name: "FK_meghivokod_ceg",
                        column: x => x.cegid,
                        principalTable: "ceg",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "dokumentum",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    leiras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fajlnev = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fajl = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    felhasznaloid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dokumentum", x => x.id);
                    table.ForeignKey(
                        name: "FK_dokumentum_felhasznalo",
                        column: x => x.felhasznaloid,
                        principalTable: "felhasznalo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "felhasznalokompetencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kompetenciaid = table.Column<int>(type: "int", nullable: false),
                    felhasznaloid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_felhasznalokompetencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_felhasznalokompetencia_felhasznalo",
                        column: x => x.felhasznaloid,
                        principalTable: "felhasznalo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_felhasznalokompetencia_kompetencia",
                        column: x => x.kompetenciaid,
                        principalTable: "kompetencia",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "allaskapcsolattarto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    allasid = table.Column<int>(type: "int", nullable: false),
                    kapcsolattartoid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allaskapcsolattarto", x => x.id);
                    table.ForeignKey(
                        name: "FK_allaskapcsolattarto_allas",
                        column: x => x.allasid,
                        principalTable: "allas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_allaskapcsolattarto_felhasznalo",
                        column: x => x.kapcsolattartoid,
                        principalTable: "felhasznalo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "allaskompetencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    allasid = table.Column<int>(type: "int", nullable: false),
                    kompetenciaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allaskompetencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_allaskompetencia_allas",
                        column: x => x.allasid,
                        principalTable: "allas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_allaskompetencia_kompetencia",
                        column: x => x.kompetenciaid,
                        principalTable: "kompetencia",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "allasvizsgalo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    allasid = table.Column<int>(type: "int", nullable: false),
                    felhasznaloid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allasvizsgalo", x => x.id);
                    table.ForeignKey(
                        name: "FK_allasvizsgalo_allas",
                        column: x => x.allasid,
                        principalTable: "allas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_allasvizsgalo_felhasznalo",
                        column: x => x.felhasznaloid,
                        principalTable: "felhasznalo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "kerdoiv",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    kor = table.Column<int>(type: "int", nullable: false),
                    nev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    allasid = table.Column<int>(type: "int", nullable: false),
                    maxpont = table.Column<int>(type: "int", nullable: true),
                    kitoltesperc = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kerdoiv", x => x.id);
                    table.ForeignKey(
                        name: "FK_kerdoiv_allas",
                        column: x => x.allasid,
                        principalTable: "allas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "kitoltottallas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    allaskeresoid = table.Column<int>(type: "int", nullable: false),
                    allasid = table.Column<int>(type: "int", nullable: false),
                    kitolteskezdet = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kitoltottallas", x => x.id);
                    table.ForeignKey(
                        name: "FK_kitoltottallas_allas",
                        column: x => x.allasid,
                        principalTable: "allas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_kitoltottallas_felhasznalo",
                        column: x => x.allaskeresoid,
                        principalTable: "felhasznalo",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "kerdes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    szoveg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    maxpont = table.Column<int>(type: "int", nullable: true),
                    programalapszoveg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    programteszteset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    sorrendkerdes = table.Column<int>(type: "int", nullable: true),
                    programozos = table.Column<bool>(type: "bit", nullable: true),
                    kifejtos = table.Column<bool>(type: "bit", nullable: true),
                    feleletvalasztos = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kerdes", x => x.id);
                    table.ForeignKey(
                        name: "FK_kerdes_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "kitoltottkerdoiv",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    kitoltottallasid = table.Column<int>(type: "int", nullable: false),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    osszpont = table.Column<int>(type: "int", nullable: true),
                    befejezve = table.Column<bool>(type: "bit", nullable: false),
                    tovabbjut = table.Column<bool>(type: "bit", nullable: true),
                    miajanlas = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kitoltottkerdoiv", x => x.id);
                    table.ForeignKey(
                        name: "FK_kitoltottkerdoiv_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_kitoltottkerdoiv_kitoltottallas",
                        column: x => x.kitoltottallasid,
                        principalTable: "kitoltottallas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "allaskerdoiv",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kerdoivid = table.Column<int>(type: "int", nullable: false),
                    kerdesid = table.Column<int>(type: "int", nullable: false),
                    sorszam = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allaskerdes", x => x.id);
                    table.ForeignKey(
                        name: "FK_allaskerdoiv_kerdes",
                        column: x => x.kerdesid,
                        principalTable: "kerdes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_allaskerdoiv_kerdoiv",
                        column: x => x.kerdoivid,
                        principalTable: "kerdoiv",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "teszteset",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    kerdesid = table.Column<int>(type: "int", nullable: false),
                    bemenet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kimenet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teszteset", x => x.id);
                    table.ForeignKey(
                        name: "FK_teszteset_kerdes",
                        column: x => x.kerdesid,
                        principalTable: "kerdes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "valasz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    szoveg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kerdesid = table.Column<int>(type: "int", nullable: false),
                    helyes = table.Column<bool>(type: "bit", nullable: true),
                    pontszam = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_valasz", x => x.id);
                    table.ForeignKey(
                        name: "FK_valasz_kerdes",
                        column: x => x.kerdesid,
                        principalTable: "kerdes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "kitoltottkerdes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kitoltottkerdoivid = table.Column<int>(type: "int", nullable: false),
                    kerdesid = table.Column<int>(type: "int", nullable: false),
                    kitolteskezdet = table.Column<int>(type: "int", nullable: true),
                    elertpont = table.Column<int>(type: "int", nullable: true),
                    szovegesvalasz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valasztosid = table.Column<int>(type: "int", nullable: true),
                    programhelyes = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kitoltottkerdes", x => x.id);
                    table.ForeignKey(
                        name: "FK_kitoltottkerdes_kerdes",
                        column: x => x.kerdesid,
                        principalTable: "kerdes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_kitoltottkerdes_kitoltottallas",
                        column: x => x.kitoltottkerdoivid,
                        principalTable: "kitoltottkerdoiv",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_kitoltottkerdes_valasz",
                        column: x => x.valasztosid,
                        principalTable: "valasz",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "kitoltottvalasz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    szovegesvalasz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    elertpont = table.Column<int>(type: "int", nullable: true),
                    kitoltottkerdesid = table.Column<int>(type: "int", nullable: false),
                    valaszid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kitoltottvalasz", x => x.id);
                    table.ForeignKey(
                        name: "FK_kitoltottvalasz_kitoltottkerdes",
                        column: x => x.kitoltottkerdesid,
                        principalTable: "kitoltottkerdes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_kitoltottvalasz_valasz",
                        column: x => x.valaszid,
                        principalTable: "valasz",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "lefutottteszteset",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    tesztesetid = table.Column<int>(type: "int", nullable: false),
                    kitoltottkerdesid = table.Column<int>(type: "int", nullable: false),
                    kimenet = table.Column<int>(type: "int", nullable: true),
                    helyes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lefutottteszteset", x => x.id);
                    table.ForeignKey(
                        name: "FK_lefutottteszteset_kitoltottkerdes",
                        column: x => x.kitoltottkerdesid,
                        principalTable: "kitoltottkerdes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_lefutottteszteset_teszteset",
                        column: x => x.tesztesetid,
                        principalTable: "teszteset",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_allas_cegid",
                table: "allas",
                column: "cegid");

            migrationBuilder.CreateIndex(
                name: "IX_allaskapcsolattarto_allasid",
                table: "allaskapcsolattarto",
                column: "allasid");

            migrationBuilder.CreateIndex(
                name: "IX_allaskapcsolattarto_kapcsolattartoid",
                table: "allaskapcsolattarto",
                column: "kapcsolattartoid");

            migrationBuilder.CreateIndex(
                name: "IX_allaskerdoiv_kerdesid",
                table: "allaskerdoiv",
                column: "kerdesid");

            migrationBuilder.CreateIndex(
                name: "IX_allaskerdoiv_kerdoivid",
                table: "allaskerdoiv",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_allaskompetencia_allasid",
                table: "allaskompetencia",
                column: "allasid");

            migrationBuilder.CreateIndex(
                name: "IX_allaskompetencia_kompetenciaid",
                table: "allaskompetencia",
                column: "kompetenciaid");

            migrationBuilder.CreateIndex(
                name: "IX_allasvizsgalo_allasid",
                table: "allasvizsgalo",
                column: "allasid");

            migrationBuilder.CreateIndex(
                name: "IX_allasvizsgalo_felhasznaloid",
                table: "allasvizsgalo",
                column: "felhasznaloid");

            migrationBuilder.CreateIndex(
                name: "IX_dokumentum_felhasznaloid",
                table: "dokumentum",
                column: "felhasznaloid");

            migrationBuilder.CreateIndex(
                name: "IX_felhasznalokompetencia_felhasznaloid",
                table: "felhasznalokompetencia",
                column: "felhasznaloid");

            migrationBuilder.CreateIndex(
                name: "IX_felhasznalokompetencia_kompetenciaid",
                table: "felhasznalokompetencia",
                column: "kompetenciaid");

            migrationBuilder.CreateIndex(
                name: "IX_kerdes_kerdoivid",
                table: "kerdes",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_kerdoiv_allasid",
                table: "kerdoiv",
                column: "allasid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottallas_allasid",
                table: "kitoltottallas",
                column: "allasid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottallas_allaskeresoid",
                table: "kitoltottallas",
                column: "allaskeresoid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottkerdes_kerdesid",
                table: "kitoltottkerdes",
                column: "kerdesid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottkerdes_kitoltottkerdoivid",
                table: "kitoltottkerdes",
                column: "kitoltottkerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottkerdes_valasztosid",
                table: "kitoltottkerdes",
                column: "valasztosid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottkerdoiv_kerdoivid",
                table: "kitoltottkerdoiv",
                column: "kerdoivid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottkerdoiv_kitoltottallasid",
                table: "kitoltottkerdoiv",
                column: "kitoltottallasid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottvalasz_kitoltottkerdesid",
                table: "kitoltottvalasz",
                column: "kitoltottkerdesid");

            migrationBuilder.CreateIndex(
                name: "IX_kitoltottvalasz_valaszid",
                table: "kitoltottvalasz",
                column: "valaszid");

            migrationBuilder.CreateIndex(
                name: "IX_lefutottteszteset_kitoltottkerdesid",
                table: "lefutottteszteset",
                column: "kitoltottkerdesid");

            migrationBuilder.CreateIndex(
                name: "IX_lefutottteszteset_tesztesetid",
                table: "lefutottteszteset",
                column: "tesztesetid");

            migrationBuilder.CreateIndex(
                name: "IX_meghivokod_cegid",
                table: "meghivokod",
                column: "cegid");

            migrationBuilder.CreateIndex(
                name: "IX_teszteset_kerdesid",
                table: "teszteset",
                column: "kerdesid");

            migrationBuilder.CreateIndex(
                name: "IX_valasz_kerdesid",
                table: "valasz",
                column: "kerdesid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "allaskapcsolattarto");

            migrationBuilder.DropTable(
                name: "allaskerdoiv");

            migrationBuilder.DropTable(
                name: "allaskompetencia");

            migrationBuilder.DropTable(
                name: "allasvizsgalo");

            migrationBuilder.DropTable(
                name: "cegtelephely");

            migrationBuilder.DropTable(
                name: "dokumentum");

            migrationBuilder.DropTable(
                name: "felhasznalokompetencia");

            migrationBuilder.DropTable(
                name: "kitoltottvalasz");

            migrationBuilder.DropTable(
                name: "lefutottteszteset");

            migrationBuilder.DropTable(
                name: "meghivokod");

            migrationBuilder.DropTable(
                name: "kompetencia");

            migrationBuilder.DropTable(
                name: "kitoltottkerdes");

            migrationBuilder.DropTable(
                name: "teszteset");

            migrationBuilder.DropTable(
                name: "kitoltottkerdoiv");

            migrationBuilder.DropTable(
                name: "valasz");

            migrationBuilder.DropTable(
                name: "kitoltottallas");

            migrationBuilder.DropTable(
                name: "kerdes");

            migrationBuilder.DropTable(
                name: "felhasznalo");

            migrationBuilder.DropTable(
                name: "kerdoiv");

            migrationBuilder.DropTable(
                name: "allas");

            migrationBuilder.DropTable(
                name: "ceg");
        }
    }
}
