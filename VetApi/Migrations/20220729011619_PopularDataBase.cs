using Microsoft.EntityFrameworkCore.Migrations;

namespace VetApi.Migrations
{
    public partial class PopularDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO `vetdb`.`addresses` (`Id`,`PublicPlace`,`StreetName`,`ZipCode`,`Neighborhood`,`Number`,`City`,`State`) VALUES(1, 0, 'Azaleias', '125896300', 'Santaninha', '1', 'Moçoró', 'Ceara')");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`addresses` (`Id`,`PublicPlace`,`StreetName`,`ZipCode`,`Neighborhood`,`Number`,`City`,`State`) VALUES(2, 0, 'Canabrava', '12457899', 'Riachão azul', '25a', 'Guaianazes', 'Sao Paulo')");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`addresses` (`Id`,`PublicPlace`,`StreetName`,`ZipCode`,`Neighborhood`,`Number`,`City`,`State`) VALUES(3, 2, 'Litoranea', '6582031', 'Calhau', '12a', 'Sao Luis', 'Maranhao')");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`addresses` (`Id`,`PublicPlace`,`StreetName`,`ZipCode`,`Neighborhood`,`Number`,`City`,`State`) VALUES(4, 1, 'Pelican', '85963214', 'Silicon Road', '125d', 'Sao paulo', 'Sao Paulo')");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`addresses` (`Id`,`PublicPlace`,`StreetName`,`ZipCode`,`Neighborhood`,`Number`,`City`,`State`) VALUES(5, 2, 'Pianco', '65085620', 'Vila Embratel', '001', 'Sao Luis', 'Maranhao')");

            migrationBuilder.Sql("INSERT INTO `vetdb`.`tutors` (`Id`,`SSN`,`Name`,`BirthDay`,`AddressId`) VALUES(1, '12345678900', 'Joaquin Bezerra', '1987-07-02 00:00:00.000000', 1)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`tutors` (`Id`,`SSN`,`Name`,`BirthDay`,`AddressId`) VALUES(2, '78946512378', 'Clecio Silva', '2000-10-12 00:00:00.000000', 2)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`tutors` (`Id`,`SSN`,`Name`,`BirthDay`,`AddressId`) VALUES(3, '56892314756', 'Moacir Verde', '1960-01-01 00:00:00.000000', 3)");
            
            migrationBuilder.Sql("INSERT INTO `vetdb`.`veterinarians` (`Id`,`CRMV`,`Specialty`,`SSN`,`Name`,`BirthDay`,`AddressId`) VALUES(1, 'C3269', 2, '45678932123', 'Silvino Castro', '1985-12-22 00:00:00.000000', 4)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`veterinarians` (`Id`,`CRMV`,`Specialty`,`SSN`,`Name`,`BirthDay`,`AddressId`) VALUES(2, 'C5897', 0, '75689423615', 'Fernanda Rodrigues', '1999-12-30 00:00:00.000000', 5)");

            migrationBuilder.Sql("INSERT INTO `vetdb`.`animals` (`Id`,`Weigth`,`IdentificationCode`,`Name`,`Sex`,`Species`,`TutorId`,`Breed`) VALUES (1,2.00,'X0001','Pepeto',1,'Canine',1,'Poodle')");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`animals` (`Id`,`Weigth`,`IdentificationCode`,`Name`,`Sex`,`Species`,`TutorId`,`Breed`) VALUES(2, 30.00, 'X0002', 'Pirulito', 1, 'Canine', 2, 'American Pit Bull Terrier')");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`animals` (`Id`,`Weigth`,`IdentificationCode`,`Name`,`Sex`,`Species`,`TutorId`,`Breed`) VALUES(3, 50.00, 'X0003', 'Piquitita', 0, 'Canine', 2, 'Fila Brasileiro')");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`animals` (`Id`,`Weigth`,`IdentificationCode`,`Name`,`Sex`,`Species`,`TutorId`,`Breed`) VALUES(4, 6.00, 'X0004', 'Kratos', 1, 'Canine', 3, 'Alapaha Blue Blood Bulldog')");


            migrationBuilder.Sql("INSERT INTO `vetdb`.`datasoftheday` (`Id`,`Date`,`Weitgth`,`Temperature`) VALUES(2, '2022-07-29 22:06:13.524587', 1.80, 42.00)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`datasoftheday` (`Id`,`Date`,`Weitgth`,`Temperature`) VALUES(3, '2022-07-28 22:07:24.118884', 31.00, 38.00)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`datasoftheday` (`Id`,`Date`,`Weitgth`,`Temperature`) VALUES(5, '2022-07-29 22:09:14.498646', 53.00, 37.00)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`datasoftheday` (`Id`,`Date`,`Weitgth`,`Temperature`) VALUES(6, '2022-07-28 22:11:05.932430', 8.00, 37.00)");

            migrationBuilder.Sql("INSERT INTO `vetdb`.`querys` (`Id`,`Symptoms`,`Comments`,`VeterinarianId`,`DataId`,`AnimalId`) VALUES(1, 'Febre', 'Animal com falta de apetite sem disposição', 1, 2, 1)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`querys` (`Id`,`Symptoms`,`Comments`,`VeterinarianId`,`DataId`,`AnimalId`) VALUES(2, 'Tosse', 'Animal com tosse, mucosa humida', 1, 3, 2)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`querys` (`Id`,`Symptoms`,`Comments`,`VeterinarianId`,`DataId`,`AnimalId`) VALUES(3, 'Pata quebrada', 'Animal tibia esquerda fraturada, cirurgia', 2, 5, 3)");
            migrationBuilder.Sql("INSERT INTO `vetdb`.`querys` (`Id`,`Symptoms`,`Comments`,`VeterinarianId`,`DataId`,`AnimalId`) VALUES(4, 'Vomitos ao comer', 'Animal engoliu objeto, cirurgia para retirar', 2, 6, 4)");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adminIDAPI00213554856Pqwus",
                column: "ConcurrencyStamp",
                value: "3704a4d8-6ad3-466a-b0c0-ac4aa06e1712");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "regular123aosdm123bJASNd",
                column: "ConcurrencyStamp",
                value: "8cc1aaed-f6f7-4532-917d-55005c13ebbb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "vet125sasD31asdADA516as2da6A",
                column: "ConcurrencyStamp",
                value: "1000117d-ca0f-4005-9a75-8bf086b2772c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "adminIDAPI00213554856Pqwus",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b0c2356-183c-4582-907b-475b6adb61a8", "AQAAAAEAACcQAAAAEKM/nYwOSbSXwtk8LwNeITVB1l3XDNHywsW+hlUIGaIg4EsiVNpiIavnUj7p7d5FlA==", "2b4dc08a-f260-4a2e-a664-f409e37ea526" });
        }
    }
}
