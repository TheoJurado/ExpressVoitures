using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Models.Entities;

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

            context.Vehicules.AddRange(
                new Vehicule
                {
                    //Id = 1,
                    Annee = 2019,
                    Marque = "Mazda",
                    Model = "Miata",
                    Finition = "LE",
                },
                new Vehicule
                {
                    //Id = 2,
                    Annee = 2007,
                    Marque = "Jeep",
                    Model = "Liberty",
                    Finition = "Sport",
                },
                new Vehicule
                {
                    //Id = 3,
                    Annee = 2007,
                    Marque = "Renault",
                    Model = "Scénic",
                    Finition = "TCe",
                }
            );
            context.SaveChanges();

            #region transactions
            Transaction transaction1 = new Transaction
            {
                CodeVin = "B38CX23",
                DateAchat = new DateOnly(2022, 01, 07),
                PrixAchat = 1800,
                DateDispoVente = new DateOnly(2022, 04, 07),
                PrixVente = 9900,
                DateVente = new DateOnly(2022, 04, 08),
                Description = "",
                Photo = new byte[] { },
                VehiculeId = 1,
                //Reparations = { reparation1 }
            };
            Transaction transaction2 = new Transaction
            {
                CodeVin = "TX84TP32",
                DateAchat = new DateOnly(2022, 04, 02),
                PrixAchat = 4500,
                DateDispoVente = new DateOnly(2022, 04, 07),
                PrixVente = 5350,
                DateVente = new DateOnly(2022, 04, 09),
                Description = "",
                Photo = new byte[] { },
                VehiculeId = 2,
                //Reparations = { reparation2 }
            };
            Transaction transaction3 = new Transaction
            {
                CodeVin = "R41P243",
                DateAchat = new DateOnly(2022, 04, 04),
                PrixAchat = 1800,
                DateDispoVente = new DateOnly(2022, 04, 08),
                PrixVente = 2990,
                Description = "",
                Photo = new byte[] { },
                VehiculeId = 3,
                //Reparations = { reparation3, reparation4 }
            };
            context.Transactions.AddRange(
                 transaction1,
                 transaction2,
                 transaction3
            );
            context.SaveChanges();
            #endregion

            #region reparation
            Reparation reparation1 = new Reparation
            {
                //Id = 1,
                Type = "Restauration complète",
                Prix = 7600,
                TransactionId = transaction1.Id
            };
            Reparation reparation2 = new Reparation
            {
                //Id = 2,
                Type = "Roulements des roues avant",
                Prix = 350,
                TransactionId = transaction2.Id
            };
            Reparation reparation3 = new Reparation
            {
                //Id = 3,
                Type = "Radiateur",
                Prix = 345,
                TransactionId = transaction3.Id
            };
            Reparation reparation4 = new Reparation
            {
                //Id = 4,
                Type = "freins",
                Prix = 345,
                TransactionId = transaction3.Id
            };

            context.Reparations.AddRange(
                 reparation1,
                 reparation2,
                 reparation3,
                 reparation4
            );
            context.SaveChanges();
            #endregion

            transaction1.Reparations.Add(reparation1);
            transaction2.Reparations.Add(reparation2);
            transaction3.Reparations.Add(reparation3);
            transaction3.Reparations.Add(reparation4);
            context.SaveChanges();
        }
    }
}
