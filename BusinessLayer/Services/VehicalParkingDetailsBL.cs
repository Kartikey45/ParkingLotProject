using BusinessLayer.Interface;
using CommonLayer.ParkingLimitForVehical;
using CommonLayer.ParkingModel;
using CommonLayer.Response;
using RepositoryLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer.Services
{
    public class VehicalParkingDetailsBL : IVehicalParkingDetailsBL
    {
        private readonly IVehicalParkingDetailsRL parkingLotRL;

        private readonly ParkingLotDbContext dataBase;

        ParkingLimit Limit = new ParkingLimit();

        public VehicalParkingDetailsBL(IVehicalParkingDetailsRL _parkingLotRL)
        {
            parkingLotRL = _parkingLotRL;
        }

        //Method to show all parked cars details
        public List<ParkingLotDetails> GetAllParkingCarsDetails()
        {
            try
            {
                var data = parkingLotRL.GetAllParkingCarsDetails();
                // Check IF Data Equal To Null 
                if (data == null)
                {
                    // IF Data Null Throw Exception
                    throw new Exception();
                }
                else
                {
                    // Return Data
                    return data;
                }
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }

        }

        //Method to unpark the car 
        public object CarUnPark(int ParkingID)
        {
            try
            {
                var data = parkingLotRL.CarUnPark(ParkingID);

                // Check IF Data Equal To Null 
                if (data == null)
                {
                    // IF Data Null Throw Exception
                    throw new Exception();
                }
                else
                {
                    // Return Data
                    return data;
                }
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }
        }

        //Method to delete parking details
        public object DeleteCarParkingDetails(int ParkingID)
        {
            try
            {
                var data = parkingLotRL.DeleteCarParkingDetails(ParkingID);
                if (data == null)
                {
                    throw new Exception();
                }
                else
                {
                    return data;
                }


            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);

            }
        }

        /*
        //Method to Add car park details 
        public ParkingLotDetails ParkingCarInLot(ParkingLotDetails details)
        {
            try
            {
                var data = parkingLotRL.ParkingCarInLot(details);
                if(data == null)
                {
                    throw new Exception();
                }
                else
                {
                    return data;
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        */

        public ParkingLotDetails ParkingCarInLot(ParkingInformation Details)
        {
            try
            {
                var data = parkingLotRL.ParkingCarInLot(Details);
                if (data == null)
                {
                    throw new Exception();
                }
                else
                {
                    return data;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //Method to return parking lot status
        public object ParkingLotStatus()
        {
            try
            {
                var data = parkingLotRL.ParkingLotStatus();
                // Check IF Data Equal To Null 
                if (data == null)
                {
                    // IF Data Null Throw Exception
                    throw new Exception();
                }
                else
                {
                    // Return data
                    return data;
                }
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }
        }

        // Get All UnPark Car Details
        public object GetAllUnParkedCarDetail()
        {
            try
            {
                var data = parkingLotRL.GetAllUnParkedCarDetail();
                
                // Check IF Data Equal To Null 
                if (data == null)
                {
                    // IF Data Null Throw Exception
                    throw new Exception();
                }
                else
                {
                    // Return data
                    return data;
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // Get Car Details By Car Number
        public List<ParkingLotDetails> GetCarDetailsByVehicleNumber(string VehicleNumber)
        {
            try
            {

                if(!IsValid(VehicleNumber))
                {
                    throw new Exception();
                }

                var data = parkingLotRL.GetCarDetailsByVehicleNumber(VehicleNumber);
                return data;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // Validations for Parking Atttributes
        public bool IsValid(string input)
        {
            Regex VehicalNumber = new Regex("^[a-z||A-Z]{2}[ ]{1}[0-9]{2}[ ]{1}[a-z||A-Z]{1,2}[ ]{1}[0-9]{4}$");
            Regex VehicalBrand = new Regex("^[A-Z][a-z]*$");
            Regex VehicalColor = new Regex("^[A-Z][a-z]*$");
            Regex ParkingSlot = new Regex("^([A-Z])$");

            return (VehicalNumber.IsMatch(input) || VehicalBrand.IsMatch(input) || VehicalColor.IsMatch(input) || ParkingSlot.IsMatch(input));
        }

        // Get Car Details By Parking Slot
        public List<ParkingLotDetails> GetCarDetailsByParkingSlot(string Slot)
        {
            try
            {

                if (!IsValid(Slot))
                {
                    throw new Exception();
                }
                var data = parkingLotRL.GetCarDetailsByParkingSlot(Slot);
                return data;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // Get Car Details By Car Brand
        public List<ParkingLotDetails> GetCarDetailsByVehicleBrand(string brand)
        {
            try
            {
                if (!IsValid(brand))
                {
                    throw new Exception();
                }
                var data = parkingLotRL.GetCarDetailsByVehicleBrand(brand);
                return data;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // Get All Car Details Of Handicap
        public List<ParkingLotDetails> GetAllCarDetailsOfHandicap()
        {
            try
            {
                var data = parkingLotRL.GetAllCarDetailsOfHandicap();
                if (data == null)
                {
                    throw new Exception();
                }
                else
                {
                    return data;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //Get all car details by color
        public List<ParkingLotDetails> GetAllCarDetailsByColor(string VehicalColor)
        {
            try
            {
                if (!IsValid(VehicalColor))
                {
                    throw new Exception();
                }
                var data = parkingLotRL.GetAllCarDetailsByColor(VehicalColor);
                return data;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

       
    }
}
