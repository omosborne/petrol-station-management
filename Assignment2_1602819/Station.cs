using System;
using System.Collections.Generic;
using System.Timers;

namespace Assignment2_1602819
{
    /// <summary>
    /// 
    ///     The <c>Station</c> class.
    ///     Holds the methods needed to create, operate, and maintain the <c>Vehicle</c> objects and <c>Pump</c> objects.
    ///     Holds the members and properties that pertain to the <c>Station</c> class.
    /// 
    /// </summary>
    class Station
    {
        /// <section>
        /// 
        ///     Initialisation of members with constant values.
        ///         - Maximum number of vehicles that can be waiting in the queue (vehicles).
        ///         - Maximum number of pumps in the forecourt (pumps).
        ///         - Maximum number of pumps in a lane (pumps).
        ///         - Lower bracket for randomised vehicle creation (milliseconds).
        ///         - Upper bracket for randomised vehicle creation (exclusive, +1) (milliseconds).
        ///         - Commission percentage for station attendants (%).
        ///         - Cost per unleaded litre rate (£).
        ///         - Cost per diesel litres rate (£).
        ///         - Cost per lpg litres rate (£).
        ///         - Name of the fuel station.
        /// 
        /// </section>

        private const int QUEUE_MAX                 = 5;
        private const int PUMP_LIMIT                = 9;
        private const int PUMPS_PER_LANE_LIMIT      = 3;
        private const int RANDOM_VEHICLE_LOWER      = 1500;
        private const int RANDOM_VEHICLE_UPPER      = 2201;
        private const float COMMISSION              = 0.01f;
        private const float UNLEADED_RATE           = 1.02f;
        private const float DIESEL_RATE             = 1.32f;
        private const float LPG_RATE                = 1.17f;
        private const string STATION_NAME           = "Petrol Truly Unlimited Ltd";

        /// <section>
        /// 
        ///     Declaration of integer members.
        ///         - Total number of vehicles being serviced.
        ///         - Total number of lanes on the forcourt.
        ///         - Total number of vehicles serviced by each pump.
        ///         - Total number of vehicles that left the queue before refuelling.
        /// 
        /// </section>

        private static int vehiclesFuelling;
        private static int lanes = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(PUMP_LIMIT) / Convert.ToDouble(PUMPS_PER_LANE_LIMIT)));
        private static int totalServicedVehicles;
        private static int totalUnservicedVehicles;

        /// <section>
        /// 
        ///     Declaration of floating-point members.
        ///     - Total number of litres dispensed by each pump.
        ///     - Total number of unleaded litres dispensed by each pump.
        ///     - Total number of diesel litres dispensed by each pump.
        ///     - Total number of lpg litres dispensed by each pump.
        ///     - Total cost of litres dispensed by each pump.
        ///     - Total commission from cost of litres dispensed by each pump.
        /// 
        /// </section>

        private static float totalLitresDispensed;
        private static float totalUnleadedDispensed;
        private static float totalDieselDispensed;
        private static float totalLpgDispensed;
        private static float totalDispensedCost;
        private static float totalDispensedCommission;

        /// <section>
        /// 
        ///     Initialisation of object members with list objects.
        ///         - List of vehicles in the queue.
        ///         - List of pumps on the forecourt.
        ///         - List of transactions from serviced vehicles.
        /// 
        /// </section>

        public static List<Vehicle> queue = new List<Vehicle>();
        public static List<Pump> forecourt = new List<Pump>();
        public static List<Transaction> transactions = new List<Transaction>();

        /// <section>
        /// 
        ///     Declaration of object members.
        ///     - Timer that elapses randomly to create a vehicle.
        /// 
        /// </section>

        private static Timer randomVehicleTimer;







        /// <section>
        ///
        ///     <c>Get</c> and <c>Set</c> accessor methods for the <c>Station</c> class properties.
        /// 
        /// </section>

        /// <value> Gets the value of the maximum vahicles allowed in the queue. </value>
        public static int QueueMax { get { return QUEUE_MAX; } }

        /// <value> Gets the value of the commission percentage for station attendants. </value>
        public static float Commission { get { return COMMISSION; } }

        /// <value> Gets the value of the name of the station. </value>
        public static string Name { get { return STATION_NAME; } }

        /// <value> Gets the value of the rate for unleaded fuel per litre. </value>
        public static float UnleadedRate { get { return UNLEADED_RATE; } }

        /// <value> Gets the value of the rate for diesel fuel per litre. </value>
        public static float DieselRate { get { return DIESEL_RATE; } }

        /// <value> Gets the value of the rate for lpg fuel per litre. </value>
        public static float LpgRate { get { return LPG_RATE; } }

        /// <value> Gets the value of the maximum number of pumps in the station forecourt. </value>
        public static int PumpLimit { get { return PUMP_LIMIT; } }

        /// <value> Gets the value of the maximum number of pumps in a lane. </value>
        public static int PumpsPerLaneLimit { get { return PUMPS_PER_LANE_LIMIT; } }

        /// <value> Gets and sets the value of the total number of vehicles refuelling. </value>
        public static int VehiclesFuelling { get { return vehiclesFuelling; } set { vehiclesFuelling = value; } }

        /// <value> Gets and sets the value of the total number of litres dispensed by all pumps. </value>
        public static float TotalLitresDispensed { get { return totalLitresDispensed; } set { totalLitresDispensed = value; } }

        /// <value> Gets and sets the value of the total number of unleaded litres dispensed by all pumps. </value>
        public static float TotalUnleadedDispensed { get { return totalUnleadedDispensed; } set { totalUnleadedDispensed = value; } }

        /// <value> Gets and sets the value of the total number of diesel litres dispensed by all pumps. </value>
        public static float TotalDieselDispensed { get { return totalDieselDispensed; } set { totalDieselDispensed = value; } }

        /// <value> Gets and sets the value of the total number of lpg litres dispensed by all pumps. </value>
        public static float TotalLpgDispensed { get { return totalLpgDispensed; } set { totalLpgDispensed = value; } }

        /// <value> Gets and sets the value of the total costs from the number of litres dispensed by all pumps. </value>
        public static float TotalDispensedCost { get { return totalDispensedCost; } set { totalDispensedCost = value; } }

        /// <value> Gets and sets the value of the total commission from the total cost of the number of litres dispensed by all pumps. </value>
        public static float TotalDispensedCommission { get { return totalDispensedCommission; } set { totalDispensedCommission = value; } }

        /// <value> Gets and sets the value of the total number of vehicles services by all pumps. </value>
        public static int TotalServicedVehicles { get { return totalServicedVehicles; } set { totalServicedVehicles = value; } }

        /// <value> Gets and sets the value of the total number of vehicles that left the queue before refuelling. </value>
        public static int TotalUnservicedVehicles { get { return totalUnservicedVehicles; } set { totalUnservicedVehicles = value; } }







        /// <summary>
        /// 
        ///     Initialises the program by creating <c>Vehicle</c> objects and <c>Pump</c> objects.
        /// 
        /// </summary>
        public static void StartObjectCreators()
        {
            // Call local object creator methods.
            PumpCreator();
            RandomisedVehicleCreator();
        }







        /// <summary>
        /// 
        ///     A timer that randomly waits before creating a <c>Vehicle</c> object.
        /// 
        /// </summary>
        private static void RandomisedVehicleCreator()
        {
            // Create a new instance of the random object.
            var r = new Random();

            // Create a new instance of the timer object.
            randomVehicleTimer = new Timer();

            // Timer elapses randomly. Timer loops.
            randomVehicleTimer.Interval = r.Next(RANDOM_VEHICLE_LOWER, RANDOM_VEHICLE_UPPER);
            randomVehicleTimer.AutoReset = true;
            randomVehicleTimer.Elapsed += VehicleCreator;
            randomVehicleTimer.Enabled = true;
            randomVehicleTimer.Start();

        }







        /// <summary>
        /// 
        ///     Creates a <c>Vehicle</c> object and adds it to the queue.
        /// 
        /// </summary>
        /// <param name="sender"> Reference to the event invoker object. This was the <c>randomVehicleTimer</c> <c>Timer</c> object. </param>
        /// <param name="e"> Information about the <c>randomVehicleTimer</c> timer elapsing. </param>
        private static void VehicleCreator(object sender, ElapsedEventArgs e)
        {
            // Create a new instance of the random object.
            var r = new Random();

            // Update the timer interval to a new random number.
            randomVehicleTimer.Interval = r.Next(RANDOM_VEHICLE_LOWER, RANDOM_VEHICLE_UPPER);

            // Don't create a new vehicle if the queue is at peak.
            if (queue.Count == QUEUE_MAX) { return; }

            // Create a new vehicle object, add it to the queue.
            Vehicle v = new Vehicle();
            queue.Add(v);

            // Start the timer to randomly leave the queue before refuelling.
            v.FailToServiceTimer(v);
        }







        /// <summary>
        /// 
        ///     Creates a <c>Pump</c> object and adds it to the forecourt.
        /// 
        /// </summary>
        private static void PumpCreator()
        {
            // Create the maximum number of pumps
            for (int i = 0; i < PumpLimit; i++)
            {
                // Create the pump object and add it to the forecourt.
                Pump p = new Pump();
                forecourt.Add(p);
            }

        }







        /// <summary>
        /// 
        ///     Identifies which pump is free to use for a vehicle at the front of the queue.
        ///     Dynamically calculates the number lanes, which pump is first in the lane, and which pump is last in the lane.
        /// 
        /// </summary>
        public static void LocatePump()
        {
            // Declare members.
            Vehicle v;
            Pump p;

            // Don't assign a non-existent vehicle to a pump (initial check).
            if (queue.Count == 0) { return; }

            // Go through each lane.
            for (int i = 1; i <= lanes; i++)
            {
                int pumpsPerLane = PUMPS_PER_LANE_LIMIT;

                // If the current lane is the last lane.
                if (i == lanes)
                {
                    // Get the number of pumps in the last lane (unusual pumps, example: 10 pumps, 4 per lane, leaves 2 pumps on the last lane).
                    pumpsPerLane = PumpLimit % PUMPS_PER_LANE_LIMIT;

                    // Check if the last lane has maximum pumps.
                    if (pumpsPerLane == 0) pumpsPerLane = PUMPS_PER_LANE_LIMIT;
                }

                // Determine last and first pumps on the lane.
                int last = (i * PUMPS_PER_LANE_LIMIT) - 1 - (PUMPS_PER_LANE_LIMIT - pumpsPerLane);
                int first = last - (pumpsPerLane - 1);

                // Go through each pump in the lane.
                for (int j = last; j >= first; j--)
                {
                    int currentPump = j;
                    p = forecourt[currentPump];

                    // Check if the pump is first and if it's available.
                    if (currentPump == first)
                    {
                        if (p.IsAvailable())
                        {
                            // Remove the first vehicle in the queue.
                            v = queue[0];
                            queue.RemoveAt(0);

                            // Increment counter.
                            VehiclesFuelling++;

                            // Give the pump a vehicle.
                            p.FuelTimeLeft = "█    ";
                            p.FillUp(v, p);
                            p.UpdateFuelTimer(v, p);
                        }
                    }

                    // Check if the pump is not blocked by previous pumps.
                    else if (!p.IsBlocked(currentPump, first))
                    {
                        // Remove the first vehicle in the queue.
                        v = queue[0];
                        queue.RemoveAt(0);

                        // Increment counter.
                        VehiclesFuelling++;

                        // Give the pump a vehicle.
                        p.FuelTimeLeft = "█    ";
                        p.FillUp(v, p);
                        p.UpdateFuelTimer(v, p);
                    }

                    // Don't assign a non-existent vehicle to a pump (double check!).
                    if (queue.Count == 0) { break; }
                }

                // Don't assign a non-existent vehicle to a pump (triple check!!).
                if (queue.Count == 0) { break; }
            }
        }
    }
}
