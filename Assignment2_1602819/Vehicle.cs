using System;
using System.Timers;

namespace Assignment2_1602819
{
    /// <summary>
    /// 
    ///     The <c>Vehicle</c> class.
    ///     Holds the methods needed to create, operate, and maintain the <c>Vehicle</c> object.
    ///     Holds the members and properties that pertain to the <c>Vehicle</c> class and object.
    /// 
    /// </summary>
    class Vehicle
    {
        /// <section>
        /// 
        ///     Initialisation of members with constant values.
        ///         - Lower bracket for ransomised queue leaving (milliseconds).
        ///         - Upper bracket for ransomised queue leaving (exclusive, +1) (milliseconds).
        ///         - Maximum number of litres a car can hold in its fuel tank (litres).
        ///         - Maximum number of litres a van can hold in its fuel tank (litres).
        ///         - Maximum number of litres a hgv can hold in its fuel tank (litres).
        /// 
        /// </section>

        private const int LEAVE_QUEUE_LOWER     = 1000;
        private const int LEAVE_QUEUE_UPPER     = 2001;
        private const int CAR_MAX_FUEL          = 40;
        private const int VAN_MAX_FUEL          = 80;
        private const int HGV_MAX_FUEL          = 150;

        /// <section>
        /// 
        ///     Declaration of integer members.
        ///         - Unique identification number.
        ///         - Maximum number of litres the vehicle can hold in its fuel tank.
        ///         - ID for the next vehicle to be created.
        /// 
        /// </section>

        private int vehicleID;
        private int tankMax;
        private static int nextVehicleID;

        /// <section>
        /// 
        ///     Declaration of floating-point members.
        ///         - Time taken for the vehicle to refuel to the maximum tank. The calculation is: (tankMax - tankCurrent) / pumpSpeed.
        ///         - Maximum time before the vehicle leaves the queue before refuelling.
        ///         - Number of litres currently in the vehicle fuel tank.
        ///         - Number of litres the vehicle needs to reach the maximum fuel tank.
        /// 
        /// </section>

        private float fuelTime;
        private float failToServiceTime;
        private float tankCurrent;
        private float fuelNeeded;

        /// <section>
        /// 
        ///     Declaration of string members.
        ///         - First name of the driver.
        ///         - Last name of the driver.
        ///         - Type of fuel the vehicle uses.
        ///         - Classification of vehicle.
        /// 
        /// </section>

        private string driverName;
        private string driverSurname;
        private string fuelType;
        private string vehicleType;

        /// <section>
        /// 
        ///     Initialisation of members with array objects.
        ///         - List containing classifications of vehicles.
        ///         - List containing types of fuel a car can use.
        ///         - List containing types of fuel a van can use.
        ///         - List containing types of fuel a hgv can use.
        ///         - Pool of first names for the driver. These names were randomly generated on 13/05/20 at: http://listofrandomnames.com/index.cfm?generated.
        ///         - Pool of Last name initials for the driver.
        /// 
        /// </section>

        private static string[] vehicleTypes = new string[] { "CAR", "VAN", "HGV" };
        private static string[] carFuels = new string[] { "UNLEADED", "DIESEL", "LPG" };
        private static string[] vanFuels = new string[] { "DIESEL", "LPG" };
        private static string[] hgvFuels = new string[] { "DIESEL" };
        private static string[] names = new string[] { "Glenn", "Julio", "Emilio", "Ted", "Daryl", "Jamaal", "Romeo", "Charlie", "Jack", "Omar", "Alaine", "Moriah", "Evelynn", "Brittaney", "Adelle", "Wynell", "Fumiko", "Cecile", "Viola", "Lizzie" };
        private static string[] surnames = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        /// <section>
        /// 
        ///     Declaration of object members.
        ///         - Vehicle that left the queue before refuelling.
        ///         - Timer that elapses when the vahicles maximum time before leaving the queue is reached.
        /// 
        /// </section>

        private Vehicle currentFailed;
        private Timer failToServiceTimer;







        /// <section>
        ///
        ///     <c>Get</c> and <c>Set</c> accessor methods for the <c>Vehicle</c> class properties.
        /// 
        /// </section>

        /// <value> Gets the value of the vehicle identifier. </value>
        public int VehicleID { get { return vehicleID; } }

        /// <value> Gets the value of the type of vehicle. </value>
        public string VehicleType { get { return vehicleType; } }

        /// <value> Gets the value of the first name of the driver. </value>
        public string DriverName { get { return driverName; } }

        /// <value> Gets the value of the last name of the driver. </value>
        public string DriverSurname { get { return driverSurname; } }

        /// <value> Gets the value of the time before the vehicle leaves the queue without refuelling. </value>
        public float FailToServiceTime { get { return failToServiceTime; } }

        /// <value> Gets the value of the fuel the vehicle needs to reach a maximum tank. </value>
        public float FuelNeeded { get { return fuelNeeded; } }

        /// <value> Gets the value of the type of fuel the vehicle uses. </value>
        public string FuelType { get { return fuelType; } }

        /// <value> Gets the value of the time it takes for the vehicle to refuel to a maximum tank. </value>
        public float FuelTime { get { return fuelTime; } }







        /// <summary>
        /// 
        ///     <c>Instance Constructor</c> for the <c>Vehicle</c> class. Creates a <c>Vehicle</c> object.
        /// 
        /// </summary>
        public Vehicle()
        {
            // Create a new instance of the random object.
            var r = new Random();

            // Increment the vehicle ID.
            vehicleID = nextVehicleID++;

            // Randomly set values.
            vehicleType = vehicleTypes[r.Next(0, vehicleTypes.Length)];
            driverName = names[r.Next(0, names.Length)];
            driverSurname = surnames[r.Next(0, surnames.Length)];
            failToServiceTime = r.Next(LEAVE_QUEUE_LOWER, LEAVE_QUEUE_UPPER);
            
            // Give the vehicle specific type-related values.
            if (vehicleType == "CAR")
            {
                // Set car-related values.
                tankMax = CAR_MAX_FUEL;
                fuelType = carFuels[r.Next(0,carFuels.Length)];
                tankCurrent = r.Next(1, (tankMax / 4));
                fuelNeeded = tankMax - tankCurrent;
                fuelTime = (fuelNeeded  / Pump.Speed) * 1000;
            }
            if (vehicleType == "VAN")
            {
                // Set van-related values.
                tankMax = VAN_MAX_FUEL;
                fuelType = vanFuels[r.Next(0, vanFuels.Length)];
                tankCurrent = r.Next(1, (tankMax / 4));
                fuelNeeded = tankMax - tankCurrent;
                fuelTime = (fuelNeeded / Pump.Speed) * 1000;
            }
            if (vehicleType == "HGV")
            {
                // Set hgv-related values.
                tankMax = HGV_MAX_FUEL;
                fuelType = hgvFuels[r.Next(0, hgvFuels.Length)];
                tankCurrent = r.Next(1, (tankMax / 4));
                fuelNeeded = tankMax - tankCurrent;
                fuelTime = (fuelNeeded / Pump.Speed) * 1000;
            }

        }







        /// <summary>
        /// 
        ///     A timer that removes the vehicle from the queue before refuelling if it has been waiting too long.
        /// 
        /// </summary>
        /// <param name="v"> A <c>Vehicle</c> object of the vehicle waiting in the queue. </param>
        public void FailToServiceTimer(Vehicle v)
        {
            // Localise member.
            currentFailed = v;

            // Create a new instance of the timer object.
            failToServiceTimer = new Timer();

            // Timer will elapsed once it meets the failt to service timer (randomly set). Timer doesn't loop.
            failToServiceTimer.Interval = currentFailed.FailToServiceTime;
            failToServiceTimer.AutoReset = false;
            failToServiceTimer.Elapsed += RemoveVehicle;
            failToServiceTimer.Enabled = true;
            failToServiceTimer.Start();

        }







        /// <summary>
        /// 
        ///     Removes a vehicle from the queue. Deletes the object via a lack of references.
        /// 
        /// </summary>
        /// <param name="sender"> Reference to the event invoker object. This was the <c>failToServiceTimer</c> <c>Timer</c> object. </param>
        /// <param name="e"> Information about the <c>failToServiceTimer</c> timer elapsing. </param>
        private void RemoveVehicle(object sender, ElapsedEventArgs e)
        {
            // Don't remove from an empty queue.
            if (Station.queue.Count == 0) { return; }

            // Remove the vehicle from the queue and decrement counter.
            Station.queue.Remove(currentFailed);
            Station.TotalUnservicedVehicles++;
        }
    }
}
