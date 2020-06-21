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
    }
}
