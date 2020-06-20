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

        public ParkingLotDetails ParkingCarInLot(ParkingLotDetails details)
        {
            try
            {
                var data = parkingLotRL.ParkingCarInLot(details);
                var condition = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "A").Count();
                var condition1 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "B").Count();
                var condition2 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "C").Count();
                var condition3 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "D").Count();
                if (condition != Limit.ParkingSlotA)
                {
                    // Return Data
                    details.ParkingSlot = "A";
                    return data;

                }
                else if (condition1 != Limit.ParkingSlotB)
                {
                    details.ParkingSlot = "B";
                    return data;
                }
                else if (condition2 != Limit.ParkingSlotC)
                {
                    details.ParkingSlot = "C";
                    return data;
                }
                else if (condition3 != Limit.ParkingSlotD)
                {
                    details.ParkingSlot = "D";
                    return data;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

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
