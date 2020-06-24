using BusinessLayer.Interface;
using CommonLayer.ParkingLimitForVehical;
using CommonLayer.ParkingModel;
using RepositoryLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public object CarUnPark(VehicalUnpark details)
        {
            try
            {
                var data = parkingLotRL.CarUnPark(details);

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

        // Method to delete Unpark car details
        public object DeleteUnparkHistory(int UnparkVehicalID)
        {
            try
            {
                var data = parkingLotRL.DeleteUnparkHistory(UnparkVehicalID);
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
        public object GetCarDetailsByVehicleNumber(string VehicleNumber)
        {
            try
            {
                var data = parkingLotRL.GetCarDetailsByVehicleNumber(VehicleNumber);
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

        // Get Car Details By Parking Slot
        public object GetCarDetailsByParkingSlot(string Slot)
        {
            try
            {
                var data = parkingLotRL.GetCarDetailsByParkingSlot(Slot);
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

        // Get Car Details By Car Brand
        public object GetCarDetailsByVehicleBrand(string brand)
        {
            try
            {
                var data = parkingLotRL.GetCarDetailsByVehicleBrand(brand);
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

        // Get All Car Details Of Handicap
        public object GetAllCarDetailsOfHandicap()
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
    }
}
