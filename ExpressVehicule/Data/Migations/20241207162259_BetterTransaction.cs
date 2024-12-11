using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Data.Migations
{
    /// <inheritdoc />
    public partial class BetterTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vehicules_TransactionAchatId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vehicules_TransactionVenteId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionAchatId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionVenteId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionAchatId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "TransactionVenteId",
                table: "Transactions",
                newName: "VehiculeLinkedId");

            migrationBuilder.AddColumn<int>(
                name: "TransactionVenteId",
                table: "Vehicules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicules_TransactionVenteId",
                table: "Vehicules",
                column: "TransactionVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_VehiculeLinkedId",
                table: "Transactions",
                column: "VehiculeLinkedId",
                unique: true,
                filter: "[VehiculeLinkedId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vehicules_VehiculeLinkedId",
                table: "Transactions",
                column: "VehiculeLinkedId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicules_Transactions_TransactionVenteId",
                table: "Vehicules",
                column: "TransactionVenteId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vehicules_VehiculeLinkedId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicules_Transactions_TransactionVenteId",
                table: "Vehicules");

            migrationBuilder.DropIndex(
                name: "IX_Vehicules_TransactionVenteId",
                table: "Vehicules");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_VehiculeLinkedId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionVenteId",
                table: "Vehicules");

            migrationBuilder.RenameColumn(
                name: "VehiculeLinkedId",
                table: "Transactions",
                newName: "TransactionVenteId");

            migrationBuilder.AddColumn<int>(
                name: "TransactionAchatId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionAchatId",
                table: "Transactions",
                column: "TransactionAchatId",
                unique: true,
                filter: "[TransactionAchatId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionVenteId",
                table: "Transactions",
                column: "TransactionVenteId",
                unique: true,
                filter: "[TransactionVenteId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vehicules_TransactionAchatId",
                table: "Transactions",
                column: "TransactionAchatId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vehicules_TransactionVenteId",
                table: "Transactions",
                column: "TransactionVenteId",
                principalTable: "Vehicules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
