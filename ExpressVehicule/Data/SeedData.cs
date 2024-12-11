using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpressVoitures.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Vehicules.Any() && context.Transactions.Any() && context.Vehicules.Any())
            {
                return;
            }/**/

            Vehicule vehicule1 = new Vehicule
            {
                CodeVin = "B38CX23",
                Annee = 2019,
                Marque = "Mazda",
                Model = "Miata",
                Finition = "LE",
            };
            Vehicule vehicule2 = new Vehicule
            {
                CodeVin = "TX84TP32",
                Annee = 2007,
                Marque = "Jeep",
                Model = "Liberty",
                Finition = "Sport",
            };
            Vehicule vehicule3 = new Vehicule
            {
                CodeVin = "R41P243",
                Annee = 2007,
                Marque = "Renault",
                Model = "Scénic",
                Finition = "TCe",
            };

            context.Vehicules.AddRange(
                vehicule1,
                vehicule2,
                vehicule3
            );
            context.SaveChanges();

            #region transactions
            Transaction transaction1Achat = new Transaction
            {
                Date = new DateOnly(2022, 01, 07),
                Price = 1800,
                Type = TransactionType.Buy,
            };
            Transaction transaction1Vente = new Transaction
            {
                Date = new DateOnly(2022, 04, 08),
                Price = 9900,
                Type = TransactionType.Sell,
            };

            Transaction transaction2Achat = new Transaction
            {
                Date = new DateOnly(2022, 04, 02),
                Price = 4500,
                Type = TransactionType.Buy,
            };
            Transaction transaction2Vente = new Transaction
            {
                Date = new DateOnly(2022, 04, 09),
                Price = 5350,
                Type = TransactionType.Sell,
            };

            Transaction transaction3Achat = new Transaction
            {
                Date = new DateOnly(2022, 04, 04),
                Price = 1800,
                Type = TransactionType.Buy,
            };
            Transaction transaction3Vente = new Transaction
            {
                Date = new DateOnly(2022, 04, 08),
                Price = 2990,
                Type = TransactionType.Sell,
            };
            context.Transactions.AddRange(
                transaction1Achat,
                transaction1Vente,
                transaction2Achat,
                transaction2Vente,
                transaction3Achat,
                transaction3Vente
            );
            context.SaveChanges();

            vehicule1.TransactionAchat = transaction1Achat;
            vehicule2.TransactionAchat = transaction2Achat;
            vehicule3.TransactionAchat = transaction3Achat;

            context.Vehicules.UpdateRange(
                vehicule1,
                vehicule2,
                vehicule3
            );
            context.SaveChanges();
            #endregion

            #region reparation
            Reparation reparation1 = new Reparation
            {
                //Id = 1,
                Type = "Restauration complète",
                Prix = 7600,
                VehiculeId = vehicule1.Id
            };
            Reparation reparation2 = new Reparation
            {
                //Id = 2,
                Type = "Roulements des roues avant",
                Prix = 350,
                VehiculeId = vehicule2.Id
            };
            Reparation reparation3 = new Reparation
            {
                //Id = 3,
                Type = "Radiateur",
                Prix = 345,
                VehiculeId = vehicule3.Id
            };
            Reparation reparation4 = new Reparation
            {
                //Id = 4,
                Type = "freins",
                Prix = 345,
                VehiculeId = vehicule3.Id
            };

            context.Reparations.AddRange(
                 reparation1,
                 reparation2,
                 reparation3,
                 reparation4
            );
            context.SaveChanges();
            #endregion

            vehicule1.Reparations.Add(reparation1);
            vehicule2.Reparations.Add(reparation2);
            vehicule3.Reparations.Add(reparation3);
            vehicule3.Reparations.Add(reparation4);
            context.SaveChanges();


            Annonce annonce1 = new Annonce
            {
                DateDispoVente = new DateOnly(2022, 04, 07),
                Description = "",
                Photo = new byte[] { },
                Price = transaction1Vente.Price,
                VehiculeId = vehicule1.Id,
                Vehicule = vehicule1
            };
            Annonce annonce2 = new Annonce
            {
                DateDispoVente = new DateOnly(2022, 04, 07),
                Description = "",
                Photo = new byte[] { },
                Price = transaction2Vente.Price,
                VehiculeId = vehicule2.Id,
                Vehicule = vehicule2
            };
            Annonce annonce3 = new Annonce
            {
                DateDispoVente = new DateOnly(2022, 04, 08),
                Description = "",
                Photo = new byte[] { },
                Price = transaction3Vente.Price,
                VehiculeId = vehicule3.Id,
                Vehicule = vehicule3
            };
            context.Annonces.AddRange(
                annonce1,
                annonce2,
                annonce3
            );
            context.SaveChanges();
        }
    }
}
