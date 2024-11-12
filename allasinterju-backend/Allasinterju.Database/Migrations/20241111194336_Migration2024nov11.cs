using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allasinterju.Database.Migrations
{
    /// <inheritdoc />
    public partial class Migration2024nov11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    kep = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_felhasznalo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kerdes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    szoveg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kitoltesido = table.Column<TimeOnly>(type: "time", nullable: true),
                    maxpont = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kerdes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kompetencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    leiras = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kompetencia", x => x.id);
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
                name: "valasz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    szoveg = table.Column<int>(type: "int", nullable: false),
                    kerdesid = table.Column<int>(type: "int", nullable: false),
                    helyes = table.Column<int>(type: "int", nullable: true),
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
                    telephelyid = table.Column<int>(type: "int", nullable: false),
                    cegid = table.Column<int>(type: "int", nullable: false),
                    hatarido = table.Column<DateTime>(type: "datetime", nullable: true),
                    kitoltesido = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allas", x => x.id);
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
                    nev = table.Column<int>(type: "int", nullable: true),
                    allasid = table.Column<int>(type: "int", nullable: false)
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
                    kitolteskezdet = table.Column<int>(type: "int", nullable: false)
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
                name: "kitoltottkerdes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kitoltottallasid = table.Column<int>(type: "int", nullable: false),
                    kerdesid = table.Column<int>(type: "int", nullable: false),
                    kitolteskezdet = table.Column<int>(type: "int", nullable: true),
                    elertpont = table.Column<int>(type: "int", nullable: true)
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
                        column: x => x.kitoltottallasid,
                        principalTable: "kitoltottallas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "kitoltottvalasz",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    szovegesvalasz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    forrasfajl = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    fajlnev = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    fotelephelyid = table.Column<int>(type: "int", nullable: true),
                    levelezesicim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kapcsolattarto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kep = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    mobiltelefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kapcsolattartonev = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    irsz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telepules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    utcahazszam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cegid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cegtelephely", x => x.id);
                    table.ForeignKey(
                        name: "FK_cegtelephely_ceg",
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

            migrationBuilder.CreateIndex(
                name: "IX_allas_cegid",
                table: "allas",
                column: "cegid");

            migrationBuilder.CreateIndex(
                name: "IX_allas_telephelyid",
                table: "allas",
                column: "telephelyid");

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
                name: "IX_allasvizsgalo_allasid",
                table: "allasvizsgalo",
                column: "allasid");

            migrationBuilder.CreateIndex(
                name: "IX_allasvizsgalo_felhasznaloid",
                table: "allasvizsgalo",
                column: "felhasznaloid");

            migrationBuilder.CreateIndex(
                name: "IX_ceg_fotelephelyid",
                table: "ceg",
                column: "fotelephelyid");

            migrationBuilder.CreateIndex(
                name: "IX_cegtelephely_cegid",
                table: "cegtelephely",
                column: "cegid");

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
                name: "IX_kitoltottkerdes_kitoltottallasid",
                table: "kitoltottkerdes",
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
                name: "IX_meghivokod_cegid",
                table: "meghivokod",
                column: "cegid");

            migrationBuilder.CreateIndex(
                name: "IX_valasz_kerdesid",
                table: "valasz",
                column: "kerdesid");

            migrationBuilder.AddForeignKey(
                name: "FK_allas_ceg",
                table: "allas",
                column: "cegid",
                principalTable: "ceg",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_allas_cegtelephely",
                table: "allas",
                column: "telephelyid",
                principalTable: "cegtelephely",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ceg_cegtelephely",
                table: "ceg",
                column: "fotelephelyid",
                principalTable: "cegtelephely",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cegtelephely_ceg",
                table: "cegtelephely");

            migrationBuilder.DropTable(
                name: "allaskapcsolattarto");

            migrationBuilder.DropTable(
                name: "allaskerdoiv");

            migrationBuilder.DropTable(
                name: "allasvizsgalo");

            migrationBuilder.DropTable(
                name: "dokumentum");

            migrationBuilder.DropTable(
                name: "felhasznalokompetencia");

            migrationBuilder.DropTable(
                name: "kitoltottvalasz");

            migrationBuilder.DropTable(
                name: "meghivokod");

            migrationBuilder.DropTable(
                name: "kerdoiv");

            migrationBuilder.DropTable(
                name: "kompetencia");

            migrationBuilder.DropTable(
                name: "kitoltottkerdes");

            migrationBuilder.DropTable(
                name: "valasz");

            migrationBuilder.DropTable(
                name: "kitoltottallas");

            migrationBuilder.DropTable(
                name: "kerdes");

            migrationBuilder.DropTable(
                name: "allas");

            migrationBuilder.DropTable(
                name: "felhasznalo");

            migrationBuilder.DropTable(
                name: "ceg");

            migrationBuilder.DropTable(
                name: "cegtelephely");
        }
    }
}
