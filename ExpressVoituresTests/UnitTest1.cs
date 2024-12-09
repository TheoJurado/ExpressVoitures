using Microsoft.EntityFrameworkCore;
using Moq;
using ExpressVoitures.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Resources;
using System.Globalization;
using Xunit.Sdk;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using ExpressVoitures.Models.Services;
using ExpressVoitures.Models.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpressVoituresTests
{
    public class UnitTest1
    {
        private DbContextOptions<ApplicationDbContext> CreateSqlDatabaseOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("TestDatabaseP5")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }

        [Fact]
        public async void AddAndDeletAnnonceFromDataBase_ShouldRemoveAnnonceFromDB()
        {
            //Arrange
            var options = CreateSqlDatabaseOptions();
            ApplicationDbContext _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted();//delete data base befor creat if to make sure it was clear
            _context.Database.EnsureCreated();//creat database
            var _voitureService = new VoitureService(_context);


            var carTest = new Vehicule
            {
                CodeVin = "test",
                Statut = StatutVehicule.EnVente,
                Annee = 2010,
                Marque = "Marque",
                Model = "Model",
                Finition = "Finition",
                Reparations = new List<Reparation>(),
                TransactionAchat = new Transaction(),
                TransactionVente = null,
            };

            var newAnnonce = new Annonce
            {
                DateDispoVente = new DateOnly(2024, 10, 15),
                Description = "Annonce test",
                Photo = null,
                Price = 12000,
                VehiculeId = 1,
                Vehicule = carTest
            };

            //Act1 : add product to DB
            _context.Annonces.Add(newAnnonce);
            await _context.SaveChangesAsync();

            //Assert 1 : confirm the addition
            Assert.Equal(1, await _context.Annonces.CountAsync());
            var annonce = await _context.Annonces.FirstAsync();
            Assert.Equal("Annonce test", annonce.Description);
            Assert.Equal(1, annonce.Id);


            //Act2 : delet annonce
            _voitureService.DeleteAnnonce(1);
            await _context.SaveChangesAsync();

            //Assert 2 : make sure the annonce is no longuer in DB
            Assert.Equal(0,await _context.Annonces.CountAsync());

            //delet DB
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Theory]
        [InlineData("", "DateDispoVente cannot be null or invalid.")]
        [InlineData("lundi", "DateDispoVente cannot be null or invalid.")]
        [InlineData("31", "DateDispoVente cannot be null or invalid.")]
        [InlineData(null, "DateDispoVente cannot be null or invalid.")]
        public void TestInvalidDate_ShouldReturnInvalidError(string inputDate, string expectedResult)
        {
            //Arrange
            Vehicule vehicule = new Vehicule();


            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                DateOnly? parsedDate = null;
                if (DateOnly.TryParse(inputDate, out DateOnly result))
                    parsedDate = result;

                if (parsedDate == null)
                {
                    throw new InvalidOperationException("DateDispoVente cannot be null or invalid.");
                }

                Annonce annonce = new Annonce
                {
                    DateDispoVente = parsedDate.Value,
                    Description = "",
                    Photo = null,
                    Price = 10,
                    VehiculeId = 1,
                    Vehicule = vehicule
                };
            });
            Assert.Equal(expectedResult, exception.Message); // Validate the exception message
        }
    }
}