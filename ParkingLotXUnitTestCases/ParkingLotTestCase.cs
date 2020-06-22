using System;
using Xunit;
using Moq;
using ParkingLotProject.Controllers;
using RepositoryLayer.Interface;
using BusinessLayer.Interface;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DBContext;
using RepositoryLayer.Services;
using BusinessLayer.Services;
using Microsoft.Extensions.Configuration;
using CommonLayer.ParkingModel;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;
using CommonLayer;

namespace ParkingLotXUnitTestCases
{
    public class ParkingLotTestCase
    {
        ParkingController parkingController;
        UserController userController;

        private readonly IVehicalParkingDetailsRL _IVehicalParkingDetailsRL;
        private readonly IVehicalParkingDetailsBL _IVehicalParkingDetailsBL;
        private readonly IUserRL _IUserRL;
        private readonly IUserBL _IUserBL;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        

        public static DbContextOptions<ParkingLotDbContext> dbContextOptions { get; }

        public static string connectionString = "Data Source=DESKTOP-IVOPHLI\\SQLEXPRESS;Initial Catalog=MigrationOfParkingLot;Integrated Security=True";

        static ParkingLotTestCase()
        {
            dbContextOptions = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public ParkingLotTestCase()
        {
            var context = new ParkingLotDbContext(dbContextOptions);

            _IVehicalParkingDetailsRL = new VehicalParkingDetailsRL(context);
            _IVehicalParkingDetailsBL = new VehicalParkingDetailsBL(_IVehicalParkingDetailsRL);

            _IUserRL = new UserRL(context);
            _IUserBL = new UserBL(_IUserRL);

            parkingController = new ParkingController(_IVehicalParkingDetailsBL);
            userController = new UserController(_IUserBL, configuration);
        }

        [Fact]
        public void CarParkDetails_ReturnOKResult()
        {

            ParkingLotDetails details = new ParkingLotDetails()
            {
                VehicleOwnerName = "Anurag",
                VehicleNumber = "MH-12-AA-9116",
                VehicalBrand = "Jaguar",
                VehicalColor = "Black",
                DriverName = "Anurag"

            };

            // Act
            var okResult = parkingController.ParkingCarInLot(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void CarParkingDetails_ReturnBadRequest()
        {


            ParkingLotDetails details = new ParkingLotDetails()
            {
                VehicleOwnerName = "Sahil",
                VehicalBrand = "Toyota",
                VehicalColor = "Black",
                DriverName = "Anurag"
            };

            // Act
            var badRequest = parkingController.ParkingCarInLot(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void UnParkDetails_ReturnOKResult()
        {

            VehicalUnpark details = new VehicalUnpark()
            {
                ParkingID = 31

            };

            // Act
            var okResult = parkingController.CarUnPark(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void UnParkDetails_ReturnBadRequest()
        {

            VehicalUnpark details = new VehicalUnpark()
            {
                ParkingID = 99

            };

            // Act
            var badRequest = parkingController.CarUnPark(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void DeleteParkingDetails_ReturnOKResult()
        {
            // Act
            var okResult = parkingController.DeleteCarParkingDetails(33);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void DeleteParkingDetails_ReturnBadRequest()
        {

            // Act
            var badRequest = parkingController.DeleteCarParkingDetails(258);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void UserLogin_ReturnBadRequestDueToEncryptedPassword()
        {
            UserController controller = new UserController(_IUserBL, configuration);

            string password = EncryptedPassword.EncodePasswordToBase64("kartikey@123");

            UserLogin details = new UserLogin()
            {
                UserTypes = "Owner",
                Email = "kartikey@gmail.com",
                Password = password
            };
            
            // Act
            var okResult = userController.LoginUser(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(okResult);
        }

        [Fact]
        public void UserRegistration_ReturnOKResult()
        {

            var details = new UserDetails()
            {
                FirstName = "Garvit",
                LastName = "Juneja",
                Email = "garvit@gmail.com",
                UserType = "Security",
                Password = "garvit@123",
            };

            //Act
            var okResult = userController.RegisterUser(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void UserRegistration_ReturnBadRequest()
        {

            var details = new UserDetails()
            {
                FirstName = "Garvit",
                LastName = "Juneja",
                UserType = "Security",
            };

            //Act
            var badRequest = userController.RegisterUser(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }
    }
}

