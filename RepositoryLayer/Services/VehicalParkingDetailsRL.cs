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

        //Method to Add parking details
        public ParkingLotDetails ParkingCarInLot(ParkingLotDetails details)
        {
            var condition = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.Status == "Park").Count();

            //check if parking lot is full or empty
            if(!condition.Equals(Limit.TotalParkingLimit))
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
                        throw new Exception(details.VehicleNumber + "  Vehical Number already parked in the Lot ");
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

        //Method to Unpark the car
        public object CarUnPark(VehicalUnpark details)
        {
            try
            {

                bool condition1 = dataBase.VehicleUnpark.Any(parkingDetails => parkingDetails.ParkingID == details.ParkingID);
                if (!condition1)
                {
                    // Quary For Calculating Total Time
                    double total = dataBase.ParkingLotDetails
                        .Where(p => p.ParkingID == details.ParkingID)
                        .Select(i => (DateTime.Now.Subtract(i.ParkingDate).TotalMinutes)).Sum();

                    // Current Date Time
                    details.UnParkDate = DateTime.Now;

                    // Total Amount Calculating With Total Time
                    details.TotalAmount = total * 10;

                    // Total Time In Minutes
                    details.TotalTime = total;

                    // Status
                    details.Status = "UnPark";

                    // Finding Data With Receipt Number
                    var Status = dataBase.ParkingLotDetails.Find(details.ParkingID);

                    // Changing Status Park To UnPark
                    Status.Status = "UnPark";

                    // Undate Changing Status
                    dataBase.ParkingLotDetails.Update(Status);

                    // Adding Data In VehicleUnPark Table
                    dataBase.Add(details);

                    // Save Both Tables (VehicleUnPark And ParkingDetail)
                    dataBase.SaveChanges();

                    // Quary For Return Data
                    var data = (from parkingDetails in dataBase.ParkingLotDetails
                                where parkingDetails.ParkingID == details.ParkingID
                                from q in dataBase.VehicleUnpark
                                select new
                                {
                                    parkingDetails.ParkingID,
                                    parkingDetails.VehicleOwnerName,
                                    parkingDetails.VehicalBrand,
                                    parkingDetails.VehicalColor,
                                    parkingDetails.DriverName,
                                    parkingDetails.ParkingSlot,
                                    parkingDetails.VehicleNumber,
                                    parkingDetails.ParkingDate,
                                    details.UnParkDate,
                                    details.TotalTime,
                                    details.TotalAmount

                                }).FirstOrDefault();

                    // Return Data
                    return data;
                }
                else
                {
                    return (details.ParkingID + "  Parking Id Already UnParked");
                }

            }
            catch(Exception)
            {
                throw new Exception("Parking ID not found");
            }
        }


        //Method to delete parking details
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

        //Method to return Parking lot status
        public object ParkingLotStatus()
        {
            try
            {
                // Count and how Many Cars are Parked
                return dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.Status == "Park").Count();
                
            }
            catch (Exception e)
            {
                // Exception
                throw new Exception(e.Message);
            }
        }

        //Method to check empty parking slot
        public string checkValidParkingSlot(string parkingSlot)
        {
            var condition = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "A" && parkingDetails.Status == "Park").Count();
            var condition1 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "B" && parkingDetails.Status == "Park").Count();
            var condition2 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "C" && parkingDetails.Status == "Park").Count();
            var condition3 = dataBase.ParkingLotDetails.Where(parkingDetails => parkingDetails.ParkingSlot == "D" && parkingDetails.Status == "Park").Count();
            if (condition != Limit.ParkingSlotA)
            {
                return parkingSlot = "A";
            }
            else if (condition1 != Limit.ParkingSlotB)
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

        //Method to check slot for handicap person
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

        //method to add handicap or normal person in database
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

    }
}
