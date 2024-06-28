using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyInfo.Entities.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Birimler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birimler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menuler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QueryStrings = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Css = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ParentMenuId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menuler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menuler_Menuler_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "Menuler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tedarikciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SirketAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VergiNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tedarikciler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gsm = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cinsiyet = table.Column<bool>(type: "bit", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrunKodu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fiyat = table.Column<double>(type: "float", nullable: true),
                    Adet = table.Column<double>(type: "float", nullable: true),
                    NegatifStokCalis = table.Column<bool>(type: "bit", nullable: false),
                    BirimId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urunler_Birimler_BirimId",
                        column: x => x.BirimId,
                        principalTable: "Birimler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    KullanicilarId = table.Column<int>(type: "int", nullable: false),
                    RollerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.KullanicilarId, x.RollerId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roller_RollerId",
                        column: x => x.RollerId,
                        principalTable: "Roller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_KullanicilarId",
                        column: x => x.KullanicilarId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KategoriUrun",
                columns: table => new
                {
                    KategorilerId = table.Column<int>(type: "int", nullable: false),
                    UrunlerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriUrun", x => new { x.KategorilerId, x.UrunlerId });
                    table.ForeignKey(
                        name: "FK_KategoriUrun_Kategoriler_KategorilerId",
                        column: x => x.KategorilerId,
                        principalTable: "Kategoriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategoriUrun_Urunler_UrunlerId",
                        column: x => x.UrunlerId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TedarikciUrun",
                columns: table => new
                {
                    TedarikcilerId = table.Column<int>(type: "int", nullable: false),
                    UrunlerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TedarikciUrun", x => new { x.TedarikcilerId, x.UrunlerId });
                    table.ForeignKey(
                        name: "FK_TedarikciUrun_Tedarikciler_TedarikcilerId",
                        column: x => x.TedarikcilerId,
                        principalTable: "Tedarikciler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TedarikciUrun_Urunler_UrunlerId",
                        column: x => x.UrunlerId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrunFotolari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DataFiles = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunFotolari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrunFotolari_Urunler_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urunler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Birimler",
                columns: new[] { "Id", "BirimAdi", "CreateDate" },
                values: new object[,]
                {
                    { 1, "Adet", new DateTime(2024, 6, 14, 15, 37, 42, 580, DateTimeKind.Local).AddTicks(6343) },
                    { 2, "Cm", new DateTime(2024, 6, 14, 15, 37, 42, 580, DateTimeKind.Local).AddTicks(6345) },
                    { 3, "Gram", new DateTime(2024, 6, 14, 15, 37, 42, 580, DateTimeKind.Local).AddTicks(6347) },
                    { 4, "Miligram", new DateTime(2024, 6, 14, 15, 37, 42, 580, DateTimeKind.Local).AddTicks(6349) }
                });

            migrationBuilder.InsertData(
                table: "Kategoriler",
                columns: new[] { "Id", "Aciklama", "CreateDate", "KategoriAdi" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(432), "Gida" },
                    { 2, null, new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(435), "Tekstil" },
                    { 3, null, new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(437), "Eletronik" },
                    { 4, null, new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(439), "Otomotiv" }
                });

            migrationBuilder.InsertData(
                table: "Menuler",
                columns: new[] { "Id", "ActionName", "Area", "Class", "ControllerName", "CreateDate", "Css", "Description", "Name", "OrderNo", "ParentId", "ParentMenuId", "QueryStrings" },
                values: new object[,]
                {
                    { 1, "", "", null, "", new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(4138), null, "Urun Yonetimi", "Urun Yonetimi", null, null, null, null },
                    { 2, "Index", "Admin", null, "Urun", new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(4162), null, "Urunler", "Urunler", null, 1, null, null },
                    { 3, "Index", "Admin", null, "Kategori", new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(4168), null, "Kategoriler", "Kategoriler", null, 1, null, null },
                    { 4, "KategoriYonet", "Admin", null, "Urun", new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(4173), null, "Urunlere Kategori Ekle", "Urunlere Kategori Ekleme", null, 1, null, null },
                    { 5, "UrunYonet", "Admin", null, "Kategori", new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(4178), null, "Kategorilere Urun Ekle", "Kategorilere Urun Ekleme", null, 1, null, null },
                    { 6, "", "", null, "", new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(4185), null, "Tedarikciler", "Tedarikciler", null, null, null, null },
                    { 7, "Create", "Admin", null, "Tedarikci", new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(4190), null, "Tedarikci Ekle", "Tedarikci Ekle", null, 6, null, null }
                });

            migrationBuilder.InsertData(
                table: "Roller",
                columns: new[] { "Id", "CreateDate", "RoleAdi" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(7782), "Admin" },
                    { 2, new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(7787), "User" },
                    { 3, new DateTime(2024, 6, 14, 15, 37, 42, 581, DateTimeKind.Local).AddTicks(7788), "Satis" }
                });

            migrationBuilder.InsertData(
                table: "Tedarikciler",
                columns: new[] { "Id", "CreateDate", "SirketAdi", "VergiNo" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 14, 15, 37, 42, 582, DateTimeKind.Local).AddTicks(1778), "Abc", "123" },
                    { 2, new DateTime(2024, 6, 14, 15, 37, 42, 582, DateTimeKind.Local).AddTicks(1780), "Asd", "456" },
                    { 3, new DateTime(2024, 6, 14, 15, 37, 42, 582, DateTimeKind.Local).AddTicks(1782), "Qwe", "7789" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Birimler_BirimAdi",
                table: "Birimler",
                column: "BirimAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kategoriler_KategoriAdi",
                table: "Kategoriler",
                column: "KategoriAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KategoriUrun_UrunlerId",
                table: "KategoriUrun",
                column: "UrunlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Menuler_ParentMenuId",
                table: "Menuler",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_RollerId",
                table: "RoleUser",
                column: "RollerId");

            migrationBuilder.CreateIndex(
                name: "IX_Roller_RoleAdi",
                table: "Roller",
                column: "RoleAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tedarikciler_VergiNo",
                table: "Tedarikciler",
                column: "VergiNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TedarikciUrun_UrunlerId",
                table: "TedarikciUrun",
                column: "UrunlerId");

            migrationBuilder.CreateIndex(
                name: "IX_UrunFotolari_Name",
                table: "UrunFotolari",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_UrunFotolari_UrunId",
                table: "UrunFotolari",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_BirimId",
                table: "Urunler",
                column: "BirimId");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_UrunKodu",
                table: "Urunler",
                column: "UrunKodu",
                unique: true,
                filter: "[UrunKodu] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Gsm",
                table: "Users",
                column: "Gsm",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategoriUrun");

            migrationBuilder.DropTable(
                name: "Menuler");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "TedarikciUrun");

            migrationBuilder.DropTable(
                name: "UrunFotolari");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tedarikciler");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Birimler");
        }
    }
}
