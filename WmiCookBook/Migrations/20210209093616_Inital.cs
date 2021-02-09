using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WmiCookBook.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "IsFeatured", "Name" },
                values: new object[,]
                {
                    { 1, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/makaron-z-kurczakiem-i-sosem-smietanowym.jpg", true, "Makarony" },
                    { 2, "https://s3.przepisy.pl/przepisy3ii/img/variants/535x0/dania_weganskie476337.jpg", true, "Wegetariańskie" },
                    { 3, "https://s3.przepisy.pl/przepisy3ii/img/variants/535x0/dania_glowne491942.jpg", true, "Dania główne" },
                    { 4, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/nalesniki-z-kurczakiem-i-pieczarkami-2.jpg", true, "Przekąski" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", new byte[] { 9, 101, 110, 36, 232, 181, 219, 209, 210, 202, 72, 11, 99, 62, 106, 8, 198, 143, 233, 184, 116, 128, 210, 75, 160, 78, 83, 145, 111, 16, 42, 122, 125, 198, 198, 219, 232, 106, 82, 154, 41, 233, 83, 63, 241, 121, 117, 216, 185, 218, 233, 27, 193, 135, 10, 111, 150, 10, 82, 102, 224, 4, 70, 72 }, new byte[] { 235, 9, 67, 92, 127, 147, 42, 48, 42, 147, 223, 252, 15, 54, 59, 214, 80, 77, 208, 77, 223, 46, 220, 118, 122, 47, 151, 224, 112, 218, 18, 44, 41, 64, 92, 255, 130, 127, 204, 182, 60, 83, 178, 115, 171, 237, 31, 32, 169, 134, 145, 51, 166, 33, 115, 86, 93, 109, 98, 155, 31, 97, 32, 228, 33, 164, 40, 103, 201, 151, 164, 202, 54, 253, 91, 142, 170, 232, 240, 100, 206, 79, 108, 27, 108, 2, 162, 162, 189, 62, 41, 4, 57, 220, 222, 241, 136, 143, 10, 191, 0, 145, 234, 222, 243, 52, 207, 189, 115, 40, 64, 193, 57, 94, 67, 193, 153, 187, 82, 246, 103, 73, 220, 236, 99, 89, 199, 9 } },
                    { 2, "user@gmail.com", new byte[] { 9, 101, 110, 36, 232, 181, 219, 209, 210, 202, 72, 11, 99, 62, 106, 8, 198, 143, 233, 184, 116, 128, 210, 75, 160, 78, 83, 145, 111, 16, 42, 122, 125, 198, 198, 219, 232, 106, 82, 154, 41, 233, 83, 63, 241, 121, 117, 216, 185, 218, 233, 27, 193, 135, 10, 111, 150, 10, 82, 102, 224, 4, 70, 72 }, new byte[] { 235, 9, 67, 92, 127, 147, 42, 48, 42, 147, 223, 252, 15, 54, 59, 214, 80, 77, 208, 77, 223, 46, 220, 118, 122, 47, 151, 224, 112, 218, 18, 44, 41, 64, 92, 255, 130, 127, 204, 182, 60, 83, 178, 115, 171, 237, 31, 32, 169, 134, 145, 51, 166, 33, 115, 86, 93, 109, 98, 155, 31, 97, 32, 228, 33, 164, 40, 103, 201, 151, 164, 202, 54, 253, 91, 142, 170, 232, 240, 100, 206, 79, 108, 27, 108, 2, 162, 162, 189, 62, 41, 4, 57, 220, 222, 241, 136, 143, 10, 191, 0, 145, 234, 222, 243, 52, 207, 189, 115, 40, 64, 193, 57, 94, 67, 193, 153, 187, 82, 246, 103, 73, 220, 236, 99, 89, 199, 9 } }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Difficulty", "Image", "IsAccepted", "IsFeatured", "Name", "Time" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 2, 9, 10, 36, 15, 760, DateTimeKind.Local).AddTicks(740), 2, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/lasagne-z-sosem-bolognese.jpg", true, true, "Lasagne", 60 },
                    { 2, 1, new DateTime(2021, 2, 9, 10, 36, 15, 762, DateTimeKind.Local).AddTicks(5657), 1, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/makaron-z-kurczakiem-i-sosem-smietanowym.jpg", true, true, "Makaron z kurczakiem i sosem śmietanowym", 45 },
                    { 3, 1, new DateTime(2021, 2, 9, 10, 36, 15, 762, DateTimeKind.Local).AddTicks(5705), 1, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/super-szybkie-tagliatelle-z-kurczakiem658590.jpg", true, true, "Super szybkie tagliatelle z kurczakiem", 25 },
                    { 4, 2, new DateTime(2021, 2, 9, 10, 36, 15, 762, DateTimeKind.Local).AddTicks(5723), 1, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/frittata-z-dynia-i-serem-feta.jpg", true, true, "Pieczona dynia z fetą i oliwkami", 40 },
                    { 5, 3, new DateTime(2021, 2, 9, 10, 36, 15, 762, DateTimeKind.Local).AddTicks(5735), 1, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/roladki-po-bolonsku543812.jpg", true, true, "Roladki po bolońsku", 45 },
                    { 6, 3, new DateTime(2021, 2, 9, 10, 36, 15, 762, DateTimeKind.Local).AddTicks(5748), 1, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/curry-z-kurczaka.jpg", true, true, "Kurczak curry", 45 },
                    { 7, 4, new DateTime(2021, 2, 9, 10, 36, 15, 762, DateTimeKind.Local).AddTicks(5760), 1, "https://s3.przepisy.pl/przepisy3ii/img/variants/800x0/nalesniki-z-kurczakiem-i-pieczarkami-2.jpg", true, true, "Naleśniki z kurczakiem i pieczarkami", 45 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, "Czosnek ząbki", "2 sztuki", 1 },
                    { 40, "Pierś z kurczaka ", "3 sztuki", 5 },
                    { 63, "Filet z kurczaka ", "300g", 7 },
                    { 64, "Pieczarki", "300g", 7 },
                    { 39, "Zioła", "Do smaku", 4 },
                    { 38, "Sok z cytryny", "2 łyżeczki", 4 },
                    { 37, "Oliwa", "3 łyżki", 4 },
                    { 36, "Oliwki drylowane", "100g", 4 },
                    { 41, "Szpinak rozmrożony i odciśnięty z soku ", "450g", 5 },
                    { 35, "Ser feta ", "200g", 4 },
                    { 65, "Menu ze Smakiem Sos pieczarkowy z cebulką Knorr", "1 opakowanie", 7 },
                    { 66, "Cebula dymka", "2 sztuki", 7 },
                    { 67, "Szczypiorek", "1 pęczek", 7 },
                    { 68, "Sól", "Do zmaku", 7 },
                    { 33, "Olej", "2 łyżki", 3 },
                    { 32, "Starty ser mozzarella", "4 łyżki", 3 },
                    { 56, "Puszka mleka kokosowego ", "1 sztuka", 6 },
                    { 34, "Dynia", "Pół średniej", 4 },
                    { 30, "Śmietana 18%", "150ml", 3 },
                    { 42, "Naturalnie smaczne Spaghetti Bolognese Knorr ", "1 opakowanie", 5 },
                    { 44, "Pomidory pelatti ", "1 słoik", 5 },
                    { 55, "Średnie cebule ", "2 sztuki", 6 },
                    { 54, "Przyprawa do złotego kurczaka Knorr ", "1 łyżka", 6 },
                    { 53, "Podwójna pierś z kurczaka", "1 sztuka", 6 },
                    { 58, "Pasta curry ", "1 łyżka", 6 },
                    { 59, "Curry Knorr ", "2 łyżki", 6 },
                    { 60, "Śmietana 18% ", "1 łyżka", 6 },
                    { 61, "Limonka", "1 sztuka", 6 },
                    { 43, "Mozzarella", "100g", 5 },
                    { 62, "Olej do smażenia", "Wedle uznania", 6 },
                    { 51, "Śmietana 18% ", "100ml", 5 },
                    { 50, "Orzechy nerkowca ", "100g", 5 },
                    { 49, "Ząbki czosnku ", "2 sztuki", 5 },
                    { 48, "Gałka muszkatołowa z Indonezji Knorr", "Szczypta", 5 },
                    { 47, "Suszone pomidory ", "50g", 5 },
                    { 46, "Cebula", "1 sztuka", 5 },
                    { 45, "Chilli", "1 sztuka", 5 },
                    { 52, "Margaryna", "3 łyżki", 5 },
                    { 29, "Pierś z kurczaka", "1 sztuka", 3 },
                    { 31, "Woda", "100ml", 3 },
                    { 27, "Makaron tagiatelle", "300g", 3 },
                    { 75, "Woda gazowana", "50ml", 7 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 76, "Olej", "1 łyżeczka", 7 },
                    { 77, "Sól", "Do smaku", 7 },
                    { 14, "Woda", "400ml", 1 },
                    { 13, "Oliwa", "3 łyżki", 1 },
                    { 12, "Gałka muszkatołowa z Indonezji Knorr ", "1 szczypta", 1 },
                    { 11, "Ser żółty ", "150g", 1 },
                    { 28, "Naturalnie smaczne Tagiatelle z kurczakiem Knorr", "1 opakowanie", 3 },
                    { 10, "Masło", "60g", 1 },
                    { 8, "Mleko", "500ml", 1 },
                    { 7, "Makaron lasagne ", "1 opakowanie", 1 },
                    { 6, "Koncentrat pomidorowy", "1 łyżka", 1 },
                    { 5, "Cebula", "2 sztuki", 1 },
                    { 4, "Czerwone wino ", "150ml", 1 },
                    { 3, "Mięso mielone wołowe ", "500g", 1 },
                    { 2, "Fix Lasagne Knorr ", "1 opakowanie", 1 },
                    { 9, "Mąka", "3 łyżki", 1 },
                    { 15, "Filety z piersi kurczaka ", "500g", 2 },
                    { 74, "Jajko", "1 sztuka", 7 },
                    { 22, "Śmietana 18%", "200ml", 2 },
                    { 69, "Pieprz czarny z Wietnamu mielony Knorr", "1 szczypta", 7 },
                    { 70, "Olej do smażenia", "Wedle uznania", 7 },
                    { 71, "Ząbki czosnku ", "2 sztuki", 7 },
                    { 72, "Mąka", "250g", 7 },
                    { 73, "Mleko", "250ml", 7 },
                    { 26, "Starty parmezan lub inny żółty ser", "100g", 2 },
                    { 25, "Mała natka pietruszki", "1 pęczek", 2 },
                    { 24, "Olej do smażenia", "80ml", 2 },
                    { 23, "Mąka pszenna", "2 łyżki", 2 },
                    { 21, "Woda", "300ml", 2 },
                    { 57, "Papryczka chilli ", "1 sztuka", 6 },
                    { 19, "Cebula", "1 sztuka", 2 },
                    { 18, "Pieczarki", "300g", 2 },
                    { 17, "Makaron penne ", "300g", 2 },
                    { 16, "Rosół z kury Knorr", "1 sztuka", 2 },
                    { 20, "Delikatna przyprawa uniwersalna Knorr ", "1 łyżeczka", 2 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Description", "RecipeId" },
                values: new object[,]
                {
                    { 31, "Pokrój cebule i chilli, po czym podsmaż je. Dodaj kurczaka i ponownie podsmaż.", 6 },
                    { 29, "Kurczaka pokrój w paski i obficie posyp curry oraz Przyprawą do złotego kurczaka Knorr. ", 6 },
                    { 36, "Pieczarki umyj i pokrój w plastry. Cebulę, czosnek i szczypior posiekaj.", 7 },
                    { 35, "Kurczaka umyj i pokrój w paski, oprósz solą i pieprzem.", 7 },
                    { 34, "Składniki na ciasto naleśnikowe połącz za pomocą miksera na gładką masę. Smaż cienkie naleśniki na złocisty kolor z obu stron.", 7 },
                    { 33, "momentu, gdy sos się zredukuje i odrobinę zgęstnieje. Podawaj z ryżem.", 6 },
                    { 32, "Następnie dodaj mleko kokosowe, śmietanę, pastę curry i sok z połówki limonki. Gotuj do ", 6 }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "Description", "RecipeId" },
                values: new object[,]
                {
                    { 30, "Pozostaw go na 20 minut w lodówce.", 6 },
                    { 19, "Posmaruj dynię łyżeczką oliwy.", 4 },
                    { 27, "Roladki obsmaż z obu stron na rozgrzanym tłuszczu, przełóż je na bok, na tę samą patelnie wlej pomidory z puszki oraz ¼ szklanki wody. Dodaj 1 opakowanie Knorr Naturalnie smaczne.", 5 },
                    { 1, "Cebule i czosnek pokrój w drobną kostkę, wrzuć na rozgrzaną oliwę. Jak tylko się przesmażą dodaj mielone mięso i smaż powoli.", 1 },
                    { 2, "Wlej wino i poczekaj, aż odparuje.", 1 },
                    { 3, "Fix Knorr wymieszaj w 400 mililitrach wody i wlej do mięsa. Całość duś powoli około 20 minut, mięso powinno być miękkie, a sos gęsty.", 1 },
                    { 4, "Przygotuj sos beszamel. W rondelku rozpuść masło i wsyp mąkę. Mieszaj, aż mąka połączy się z tłuszczem.", 1 },
                    { 5, "Wlej mleko i energicznie mieszaj tak, aby rozbić wszystkie grudki. Sos zagotuj, dopraw szczyptą soli oraz gałką muszkatołową. Na końcu dodaj do gorącego sosu połowę startego sera (najlepszy będzie o wyrazistym smaku). Mieszaj, aż ser się roztopi.", 1 },
                    { 6, "Najlepiej w kwadratowym lub prostokątnym naczyniu żaroodpornym układaj lasagne. Najpierw posmaruj dno sosem beszamel, następnie układaj surowy makaron tak, aby płaty pokryły dno i lekko zachodziły jeden za drugi.", 1 },
                    { 7, "Następnie ponownie posmaruj makaron sosem beszamel. Później nałóż warstwę mięsa, następnie połóż drugą warstwę makaronu i beszamelu. Czynność powtarzaj do wyczerpania mięsa. Ostatnią warstwę przykryj makaronem i polej po wierzchu obficie pozostałym beszamelem. Na końcu posyp serem i piecz w piekarniku rozgrzanym do 180 °C przez 30 minut.", 1 },
                    { 8, "Ugotuj makaron al dente.", 2 },
                    { 9, "Piersi pokrój w kostkę 1x1 cm, cebulę w plastry, a pieczarki w ćwiartki.", 2 },
                    { 10, "Mięso dopraw do smaku przyprawą warzywną oraz pieprzem, oprósz w mące, obsmaż na patelni na rozgrzanym tłuszczu na złoty kolor. Dodaj cebulę i pieczarki. Całość smaż, aż zupełnie odparują soki z mięsa i grzybów.", 2 },
                    { 11, "Dodaj śmietanę i 300 ml zimnej wody. Całość gotuj około 5 minut, aż sos zgęstnieje, a następnie dopraw kostką Rosołu z kury Knorr, dodaj ugotowany makaron i posiekaną natkę pietruszki. Całość dokładnie wymieszaj.", 2 },
                    { 12, "Makaron podawaj posypany startym serem.", 2 },
                    { 28, "Całość dokładnie wymieszaj i zagotuj. Do sosu włóż na powrót roladki duś pod przykryciem około 15 minut aż mięso dojdzie środku.", 5 },
                    { 13, "Pierś kurczaka pokrój w kostkę 1x1 cm. Na patelni rozgrzej olej i usmaż pokrojone mięso.", 3 },
                    { 15, "Makaron ugotuj al dente we wrzącej lekko osolonej wodzie i odcedź. Ugotowany makaron przełóż na patelnię i wymieszaj z sosem.", 3 },
                    { 16, "Gotowy makaron wyłóż na talerze i posyp startym serem.", 3 },
                    { 17, "Umyj dynię, odsącz nasienie i pokrój w plastry.", 4 },
                    { 18, "Wyłóż blachę papierem do pieczenia, ułóż na niej cząstki dyni.", 4 },
                    { 37, "Przesmaż na patelni kurczaka, następnie dodaj cebulę i czosnek. Po 1 minucie dorzuć pieczarki.", 7 },
                    { 20, "Wstaw do piekarnika rozgrzanego do 200C i piecz przez około 30 minut (należy dynię raz obrócić). Można też upiec dynię na grillu.", 4 },
                    { 21, "Przygotuj sos: oliwa, sos z cytryny i zioła.", 4 },
                    { 22, "Posyp dynię fetą.", 4 },
                    { 23, "Na patelni na rozgrzanym tłuszczu podsmaż posiekany czosnek cebulkę i papryczkę chili, dodaj szpinak całość smaż chwilę. Dodaj śmietanę i pokruszone orzeszki nerkowca.", 5 },
                    { 24, "Jeśli uważasz za konieczne dopraw do smaku i odstaw na bok do ostygnięcia. Gdy farsz będzie już zimny dodaj do niego pokrojone w drobna kostkę suszone pomidory i ser mozzarella.", 5 },
                    { 25, "Na foli ułóż piersi z kurczaka przykryj drugą warstwa foli i delikatnie uderzając tłuczkiem rozbij mięso na cienki płat. Na mięsie ułóż porcję szpinaku.", 5 },
                    { 26, "Brzegi mięsa delikatnie zawiń do środka całość zroluj zabezpieczając roladki wykałaczkami przed rozwinięciem się.", 5 },
                    { 14, "W miseczce wymieszaj zawartość opakowania Knorr z wodą i śmietaną, wlej na patelnię i zagotuj.", 3 },
                    { 38, "Podsmażaj wszystko razem 5 minut. Dodaj śmietanę i sos Knorr. Gotuj na małym ogniu, aż całość zgęstnieje. Podawaj naleśniki posmarowane ciepłym sosem, złożone w trójkąty i posypane szczypiorkiem.", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RecipeId",
                table: "Steps",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
