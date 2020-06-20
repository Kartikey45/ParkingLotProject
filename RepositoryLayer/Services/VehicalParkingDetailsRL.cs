using CommonLayer.ParkingLimitForVehical;
using CommonLayer.ParkingModel;
using RepositoryLayer.DBContext;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class VehicalParkingDetailsRL : IVehicalParkingDetailsRL
    {
        ParkingLotDbContext dataBase;

        ParkingLimit Limit = new ParkingLimit();

        public VehicalParkingDetailsRL(ParkingLotDbContext _dataBase)
        {
            dataBase = _dataBase;
        }

        public ParkingLotDetails ParkingCarInLot(ParkingLotDetails details)
        {
            var condition = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.Status == "Park").Count();

            if(!condition.Equals(10))
            {
                try
                {
                    // Conditions 
                    bool condition1 = dataBase.ParkingLotDetails.Any(parkingDetails => parkingDetails.VehicleNumber == details.VehicleNumber);
                    var condition2 = (from parkingDetails in dataBase.ParkingLotDetails where parkingDetails.VehicleNumber == details.VehicleNumber select parkingDetails.Status == "UnPark").LastOrDefault();
                    // Check Same Data Available Or Not By Vehicale Number
                    if (!condition1)
                    {

                        details.ParkingDate = DateTime.Now;
                        details.Status = "Park";
                        details.ParkingSlot = checkValidParkingSlot(details.ParkingSlot);
                        details.ParkingUserCategory = ParkingCategory(details.ParkingUserCategory);
                        if (details.ParkingUserCategory == "Handicap")
                        {
                            details.ParkingSlot = checkPakingSlotForHandicap(details.ParkingSlot);
                        }
                        dataBase.Add(details);
                        dataBase.SaveChanges();
                        // Return Data
                        return details;
                    }
                    else if (condition2)  // Check Second Condition The Data Avaliable With UnPark And Last Data Of That Vehical Number
                    {
                        // Current Date And Time
                        details.ParkingDate = DateTime.Now;
                        details.Status = "Park";
                        dataBase.Add(details);
                        dataBase.SaveChanges();
                        // Return Data
                        return details;
                    }
                    else
                    {
                        // If Data Avaliable With Park Status Return This Message
                        //  return details.Vehicle_Number + " This Car Data Available With Park Status";
                        throw new Exception("Already Park " + details.VehicleNumber + " This Car Number");
                    }
                }
                catch(Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
            else
            {
                throw new Exception("ParkingLot Is Full");
            }
        }

        public object ParkingLotStatus()
        {
            try
            {
                // Count How Many Car Is Park
                return dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.Status == "Park").Count();
                // Reurn Count
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }
        }

        public string checkValidParkingSlot(string parkingSlot)
        {
            var condition = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "A" && parkingDetails.Status == "Park").Count();
            var condition1 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "B" && parkingDetails.Status == "Park").Count();
            var condition2 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "C" && parkingDetails.Status == "Park").Count();
            var condition3 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "D" && parkingDetails.Status == "Park").Count();
            if (condition2 != Limit.ParkingSlotA)
            {
                return parkingSlot = "A";
            }
            else if (condition1 != Limit.ParkingSlotB)
            {
                return parkingSlot = "B";
            }
            else if (condition != Limit.ParkingSlotC)
            {
                return parkingSlot = "C";
            }
            else if (condition3 != Limit.ParkingSlotD)
            {
                return parkingSlot = "D";
            }
            else
            {
                throw new Exception("Parking Is Full");
            }
        }

        public string checkPakingSlotForHandicap(string parkingSlot)
        {
            var condition = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "A" && parkingDetails.Status == "Park").Count();
            var condition1 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "B" && parkingDetails.Status == "Park").Count();
            var condition2 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "C" && parkingDetails.Status == "Park").Count();
            var condition3 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "D" && parkingDetails.Status == "Park").Count();
            if (condition != Limit.ParkingSlotA)
            {
                return parkingSlot = "A";
            }
            else
            if (condition1 != Limit.ParkingSlotB)
            {
                return parkingSlot = "B";
            }
            else if (condition2 != Limit.ParkingSlotC)
            {
                return parkingSlot = "C";
            }
            else if (condition3 != Limit.ParkingSlotD)
            {
                return parkingSlot = "D";
            }
            else
            {
                throw new Exception("Parking Is Full");
            }
        }

        public string ParkingCategory(string category)
        {
            var condition = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "A" && parkingDetails.Status == "Park" && parkingDetails.ParkingUserCategory == "Handicap").Count();
            if (category == "Handicap")
            {
                // Return Data
                return category = "Handicap";
            }
            else
            {
                return category = "Normal";
            }
        }

        public object DeleteCarParkingDetails(int ParkingID)
        {
            try
            {
                //Find the Car Parking  For Specific Receipt Number
                var details = dataBase.ParkingLotDetails.FirstOrDefault(x => x.ParkingID == ParkingID);

                if (details != null)
                {
                    //Delete
                    dataBase.ParkingLotDetails.Remove(details);

                    //Commit the transaction
                    dataBase.SaveChanges();
                    return "Data Deleted Successfully";
                }
                else
                {
                    // If Data Not Found
                    //   return ReceiptNumber + " This Receipt Number Not Found";
                    throw new Exception();
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
