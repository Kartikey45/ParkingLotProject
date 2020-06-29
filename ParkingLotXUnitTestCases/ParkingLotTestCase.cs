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
using System.ComponentModel;

namespace ParkingLotXUnitTestCases
{
    public class ParkingLotTestCase
    {
        //Instance of controllers
        ParkingController parkingController;
        UserController userController;

        //Instances of classes of business and repository layers
        private readonly IVehicalParkingDetailsRL _IVehicalParkingDetailsRL;
        private readonly IVehicalParkingDetailsBL _IVehicalParkingDetailsBL;
        private readonly IUserRL _IUserRL;
        private readonly IUserBL _IUserBL;

        //Instance of Configuration 
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        
        // Instance of DbContextOptions
        public static DbContextOptions<ParkingLotDbContext> dbContextOptions { get; }

        //Connection String
        public static string connectionString = "Data Source=DESKTOP-IVOPHLI\\SQLEXPRESS;Initial Catalog=MigrationOfParkingLot;Integrated Security=True";
        //public static string connectionString = "ParkingLotDBConnection";

        //Constructor
        static ParkingLotTestCase()
        {
            dbContextOptions = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        //Constructor
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

        //Parking vehical returns ok result
        [Fact]
        public void ParkingCarInLot_ReturnsOkResult()
        {

            ParkingLotDetails details = new ParkingLotDetails()
            {
                VehicleOwnerName = "Abhijit",
                VehicleNumber = "MP 67 MB 2817",
                VehicalBrand = "Honda",
                VehicalColor = "Black",
                DriverName = "Harshit",
                //ParkingUserCategory = "Handicap"

            };

            // Act
            var okResult = parkingController.ParkingCarInLot(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //Parking vehical returns bad request
        [Fact]
        public void ParkingCarInLot_ReturnsBadRequest()
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

        //Unpark vehical returns ok result
        [Fact]
        public void CarUnPark_ReturnOKResult()
        {

            VehicalUnpark details = new VehicalUnpark()
            {
                ParkingID = 50

            };

            // Act
            var okResult = parkingController.CarUnPark(47);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //Unpark a vehical return bad request
        [Fact]
        public void CarUnPark_ReturnBadRequest()
        {

            VehicalUnpark details = new VehicalUnpark()
            {
                ParkingID = 99

            };

            // Act
            var badRequest = parkingController.CarUnPark(258);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        //Delete parking details  by id returns ok result
        [Fact]
        public void DeleteParkingDetails_ReturnOKResult()
        {
            // Act
            var okResult = parkingController.DeleteCarParkingDetails(50);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //Delete parking details  by id returns bad request
        [Fact]
        public void DeleteParkingDetails_ReturnBadRequest()
        {

            // Act
            var badRequest = parkingController.DeleteCarParkingDetails(258);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        //user login returns bad request due to Encrypted password
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

        //User registration returns ok result 
        [Fact]
        public void UserRegistration_ReturnOKResult()
        {

            var details = new UserDetails()
            {
                FirstName = "Garvit",
                LastName = "Kumar",
                Email = "garvit@gmail.com",
                UserType = "Security",
                Password = "garvit@123",
            };

            //Act
            var okResult = userController.RegisterUser(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //user registration returns bad request
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

        //Get all user details returns ok result
        [Fact]
        public void GetAllUserDetailsReturnsOkResult()
        {
            //Act
            var okResult = userController.GetAllUserDetails();

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //delete user details by id returns ok result
        [Fact]
        public void DeleteUserDetails_ReturnsOkResult()
        {
            // Act
            var okResult = userController.DeleteUserRecord(19);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //delete user details by id return bad request
        [Fact]
        public void DeleteUserDetails_ReturnsBadRequest()
        {
            // Act
            var badRequest = userController.DeleteUserRecord(0);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        //Get all parking details returns ok result
        [Fact]
        public void GetAllParkingDetails_ReturnsOkResult()
        {
            //Act
            var okResult = parkingController.GetAllParkingCarsDetails();

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //Get all unparked details returns ok result
        [Fact]
        public void GetAllUnparkedCarsDetails_ReturnsOkResult()
        {
            // Act
            var okResult = parkingController.GetAllUnParkedCarDetail();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //get details by vehical number returns ok result
        [Fact]
        public void GetCarDetailsByVehicleNumber_ReturnsOkResult()
        {
            //Act
            var okResult = parkingController.GetCarDetailsByVehicleNumber("MP 67 MB 3071");

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //get details by vehical number returns bad request
        [Fact]
        public void GetCarDetailsByVehicleNumber_ReturnsBadRequest()
        {
            //Act
            var badresult = parkingController.GetCarDetailsByVehicleNumber("");

            //Assert
            Assert.IsType<BadRequestObjectResult>(badresult);
        }

        //get details by parking slot returns ok result
        [Fact]
        public void GetCarDetailsByParkingSlot_ReturnsOkResult()
        {
            //Act
            var okResult = parkingController.GetCarDetailsByParkingSlot("D");

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //get details by parking slot returns bad request
        [Fact]
        public void GetCarDetailsByParkingSlot_ReturnsBadResult()
        {
            //Act
            var badResult = parkingController.GetCarDetailsByParkingSlot("");

            //Assert
            Assert.IsType<BadRequestObjectResult>(badResult);
        }

        //get details by vehical brand returns ok result
        [Fact]
        public void GetCarDetailsByVehicleBrand_ReturnsOkResult()
        {
            //Act
            var okResult = parkingController.GetCarDetailsByVehicleBrand("Bmw");

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //get details by vehical brand returns bad request
        [Fact]
        public void GetCarDetailsByVehicleBrand_ReturnsBadRequest()
        {
            //Act
            var badRequest = parkingController.GetCarDetailsByVehicleBrand("");

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        //get car details of handicap users returns ok result
        [Fact]
        public void GetAllCarDetailsOfHandicap_ReturnsOkResult()
        {
            //Act
            var okResult = parkingController.GetAllCarDetailsOfHandicap();

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //get car details by its color returns ok result
        [Fact]
        public void GetAllCarDetailsByColor_ReturnsOkResult()
        {
            //Act
            var okResult = parkingController.GetAllCarDetailsByColor("Blue");

            //Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        //get car details by its color returns bad request
        [Fact]
        public void GetAllCarDetailsByColor_ReturnsBadRequest()
        {
            //Act
            var badRequest = parkingController.GetAllCarDetailsByColor("");

            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }
    }
}

