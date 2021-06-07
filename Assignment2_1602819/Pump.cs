using System.Timers;

namespace Assignment2_1602819
{
    /// <summary>
    /// 
    ///     The <c>Pump</c> class.
    ///     Holds the methods needed to create, operate, and maintain the <c>Pump</c> object.
    ///     Holds the members and properties that pertain to the <c>Pump</c> class and object.
    /// 
    /// </summary>
    class Pump
    {
        /// <section>
        /// 
        ///     Initialisation of members with constant values.
        ///         - Rate of fuel the pump can dispense (litres per second).
        /// 
        /// </section>

        private const float SPEED       = 2;

        /// <section>
        /// 
        ///     Declaration of integer memberss.
        ///         - Unique identification number.
        ///         - Number of vehicles pump serviced.
        ///         - ID for the next pump to be created.
        /// 
        /// </section>

        private int pumpID;
        private int servicedVehicles;
        private static int nextPumpID;

        /// <section>
        /// 
        ///     Declaration of floating-point members.
        ///         - Number of litres dispensed.
        ///         - Number of unleaded litres dispensed.
        ///         - Number of diesel litres dispensed.
        ///         - Number of lpg litres dispensed.
        ///         - Cost of litres dispensed.
        ///         - Commission from litres dispensed.
        /// 
        /// </section>

        private float litresDispensed;
        private float unleadedDispensed;
        private float dieselDispensed;
        private float lpgDispensed;
        private float dispensedCost;
        private float dispensedCommission;

        /// <section>
        /// 
        ///     Declaration of string members.
        ///         - Category of fuel remaining.
        /// 
        /// </section>

        private string fuelTimeLeft;

        /// <section>
        /// 
        ///     Initialisation of members with array objects.
        ///         - All fuel remaining categories.
        /// 
        /// </section>

        private static string[] fuelTimeLeftCategories = new string[] { "█████", "████ ", "███  ", "██   ", "█    ", "     " };

        /// <section>
        /// 
        ///     Declaration of object members.
        ///         - Vehicle currently being serviced.
        ///         - Pump currently being used.
        ///         - Timer that elapses every divisional amount from the vehicle fuel time dividied by the number of fuel time categories.
        ///         - Timer that elapses when the vehicle has finished refuelling.
        /// 
        /// </section>

        private Vehicle currentVehicle;
        private Pump currentPump;
        private Timer updateFuelTimer;
        private Timer refuelTimer;







        /// <section>
        ///
        ///     <c>Get</c> and <c>Set</c> accessor methods for the <c>Pump</c> class properties.
        /// 
        /// </section>

        /// <value> Gets the value of the pump speed. </value>
        public static float Speed { get { return SPEED; } }

        /// <value> Gets the value of the pump identifier. </value>
        public int PumpID { get { return pumpID; } }

        /// <value> Gets the value of the number of litres dispensed by the pump. </value>
        public float LitresDispensed { get { return litresDispensed; } }

        /// <value> Gets the value of the litres of unleaded fuel dispensed by the pump. </value>
        public float UnleadedDispensed { get { return unleadedDispensed; } }

        /// <value> Gets the value of the litres of diesel fuel dispensed by the pump. </value>
        public float DieselDispensed { get { return dieselDispensed; } }

        /// <value> Gets the value of the litres of lpg fuel dispensed by the pump. </value>
        public float LpgDispensed { get { return lpgDispensed; } }

        /// <value> Gets the value of the cost of the fuel dispensed by the pump. </value>
        public float DispensedCost { get { return dispensedCost; } }

        /// <value> Gets the value of the commission of the fuel dispensed by the pump. </value>
        public float DispensedCommission { get { return dispensedCommission; } }

        /// <value> Gets the value of the number of vehicles that used the pump. </value>
        public int ServicedVehicles { get {  return servicedVehicles; } }

        /// <value> Gets and sets the object reference of the vehicle using the pump. </value>
        public Vehicle CurrentVehicle { get { return currentVehicle; } set { currentVehicle = value; } }

        /// <value> Gets and sets the object reference of the pump in use. </value>
        public Pump CurrentPump { get { return currentPump; } set { currentPump = value; } }

        /// <value> Gets and sets the visual timer of the time taken for the vehicle to refuel. </value>
        public string FuelTimeLeft { get { return fuelTimeLeft; } set { fuelTimeLeft = value; } }







        /// <summary>
        /// 
        ///     <c>Instance Constructor</c> for the <c>Pump</c> class. Creates a <c>Pump</c> object.
        /// 
        /// </summary>
        public Pump()
        {
            // Increment the pump ID.
            pumpID = nextPumpID++;

            // Set the visual timer for refuelling to the empty category.
            fuelTimeLeft = fuelTimeLeftCategories[fuelTimeLeftCategories.Length - 1];
        }







        /// <summary>
        /// 
        ///     Will identify if the pump is free of a vehicle.
        /// 
        /// </summary>
        /// <returns> Returns a <c>true</c> or <c>false</c> value for whether the pump is available or not. </returns>
        public bool IsAvailable()
        {
            // If there is no vehicle at the pump, return true.
            return currentVehicle == null;
        }







        /// <summary>
        /// 
        ///     Will identify if the pump is blocked by a previous pump in the same lane.
        /// 
        /// </summary>
        /// <param name="currentPump"> An integer index of the pump being checked. </param>
        /// <param name="first"> An integer index of the first pump in the lane. </param>
        /// <returns> Returns a <c>true</c> or <c>false</c> value for whether the pump is blocked or not. </returns>
        public bool IsBlocked(int currentPump, int first)
        {
            // Check if the pump is the first in the lane.
            if (currentPump == first)
            {
                // The first pump in the lane can't be blocked by another pump.
                return false;
            }

            // Loop through the pumps before the current pump in the lane
            for (int i = 1; i <= (currentPump - first); i++)
            {
                // Check if the first pump is unavailable.
                if (!Station.forecourt[first].IsAvailable())
                {
                    // Current pump is blocked.
                    return true;
                }

                // Check if the pump before the current pump in the lane is unavailable.
                if (!Station.forecourt[first + i].IsAvailable())
                {
                    // Current pump is blocked.
                    return true;
                }


            }
            // No pumps are blocking the current pump.
            return false;
        }







        /// <summary>
        /// 
        ///     A timer that updates the visual refuelling timer for the pump.
        /// 
        /// </summary>
        /// <param name="v"> A <c>Vehicle</c> object of the vehicle refuelling. </param>
        /// <param name="p"> A <c>Pump</c> object of the pump being used. </param>
        public void UpdateFuelTimer(Vehicle v, Pump p)
        {
            // Localise members.
            CurrentVehicle = v;
            CurrentPump = p;

            // Create a new instance of the timer object.
            updateFuelTimer = new Timer();

            // Divide the time taken to fuel with the number of categories (5) to get the divisional sections for each update of the visual timer. Timer loops.
            updateFuelTimer.Interval = (currentVehicle.FuelTime / (fuelTimeLeftCategories.Length - 1));
            updateFuelTimer.AutoReset = true;
            updateFuelTimer.Elapsed += TimeRemainingStageUpdater;
            updateFuelTimer.Enabled = true;
            updateFuelTimer.Start();

        }







        /// <summary>
        /// 
        ///     Updates the visual refuelling timer to the next stage.
        /// 
        /// </summary>
        /// <param name="sender"> Reference to the event invoker object. This was the <c>updateFuelTimer</c> <c>Timer</c> object. </param>
        /// <param name="e"> Information about the <c>updateFuelTimer</c> timer elapsing. </param>
        private void TimeRemainingStageUpdater(object sender, ElapsedEventArgs e)
        {
            // Check which stage the visual timer is at, update it to the next stage.
            if (currentPump.FuelTimeLeft == "█    ")
            {
                currentPump.FuelTimeLeft = "██   ";
            }
            else if (currentPump.FuelTimeLeft == "██   ")
            {
                currentPump.FuelTimeLeft = "███  ";
            }
            else if (currentPump.FuelTimeLeft == "███  ")
            {
                currentPump.FuelTimeLeft = "████ ";
            }
            else if (currentPump.FuelTimeLeft == "████ ")
            {
                currentPump.FuelTimeLeft = "█████";
            }
            else if (currentPump.FuelTimeLeft == "█████")
            {
                currentPump.FuelTimeLeft = "     ";
                
                // Last category reached and reset, stop the timer and free up memory.
                updateFuelTimer.Stop();
                updateFuelTimer.Dispose();
            }
        }







        /// <summary>
        /// 
        ///     A timer that holds the vehicle at the pump for the duration of the time the vehicle needs to refuel.
        /// 
        /// </summary>
        /// <param name="v"> A <c>Vehicle</c> object of the vehicle refuelling. </param>
        /// <param name="p"> A <c>Pump</c> object of the pump being used. </param>
        public void FillUp(Vehicle v, Pump p)
        {
            // Localise members.
            currentVehicle = v;
            currentPump = p;

            // Create a new instance of the timer object.
            refuelTimer = new Timer();

            // Timer will take the length of the fuel time to elapse. Timer doesn't loop.
            refuelTimer.Interval = currentVehicle.FuelTime;
            refuelTimer.AutoReset = false;
            refuelTimer.Elapsed += VehicleFinished;
            refuelTimer.Enabled = true;
            refuelTimer.Start();
        }







        /// <summary>
        /// 
        ///     Checks the fuel type, then calculates the following:
        ///         - Litres of fuel dispensed for unleaded, diesel, lpg, and total.
        ///         - Cost from the amount of fuel dispensed.
        ///         - Commission from the cost.
        ///     Requests the transaction to be recorded to file.
        ///     Increases the number of vehicles serviced by the pump and total counters by one.
        ///     Removes the vehicle from the pump and decreases the number of vehicles fuelling counter by one.
        /// 
        /// </summary>
        /// <param name="sender"> Reference to the event invoker object. This was the <c>refuelTimer</c> <c>Timer</c> object. </param>
        /// <param name="e"> Information about the <c>refuelTimer</c> timer elapsing. </param>
        private void VehicleFinished(object sender, ElapsedEventArgs e)
        {
            // Check which type of fuel the vehicle uses.
            if (currentVehicle.FuelType == "UNLEADED")
            { 
                // Calculate unleaded values.
                currentPump.unleadedDispensed += currentVehicle.FuelNeeded;
                Station.TotalUnleadedDispensed += currentVehicle.FuelNeeded;
                currentPump.dispensedCost += currentVehicle.FuelNeeded * Station.UnleadedRate;
                currentPump.dispensedCommission += ((currentVehicle.FuelNeeded * Station.UnleadedRate) * Station.Commission);
                RecordTransaction(currentVehicle, currentPump, (currentVehicle.FuelNeeded * Station.UnleadedRate));
            }
            else if (currentVehicle.FuelType == "DIESEL")
            {
                // Calculate diesel values.
                currentPump.dieselDispensed += currentVehicle.FuelNeeded;
                Station.TotalDieselDispensed += currentVehicle.FuelNeeded;
                currentPump.dispensedCost += currentVehicle.FuelNeeded * Station.DieselRate;
                currentPump.dispensedCommission += ((currentVehicle.FuelNeeded * Station.DieselRate) * Station.Commission);
                RecordTransaction(currentVehicle, currentPump, currentVehicle.FuelNeeded * Station.DieselRate);
            }
            else if (currentVehicle.FuelType == "LPG")
            {
                // Calculate lpg values.
                currentPump.lpgDispensed += currentVehicle.FuelNeeded;
                Station.TotalLpgDispensed += currentVehicle.FuelNeeded;
                currentPump.dispensedCost += currentVehicle.FuelNeeded * Station.LpgRate;
                currentPump.dispensedCommission += ((currentVehicle.FuelNeeded * Station.LpgRate) * Station.Commission);
                RecordTransaction(currentVehicle, currentPump, currentVehicle.FuelNeeded * Station.LpgRate);
            }
            // Calculate total values.
            currentPump.litresDispensed = currentPump.unleadedDispensed + currentPump.dieselDispensed + currentPump.lpgDispensed;
            Station.TotalLitresDispensed = Station.TotalUnleadedDispensed + Station.TotalDieselDispensed + Station.TotalLpgDispensed;
            Station.TotalDispensedCost = (Station.TotalUnleadedDispensed * Station.UnleadedRate) + (Station.TotalDieselDispensed* Station.DieselRate) + (Station.TotalLpgDispensed* Station.LpgRate);
            Station.TotalDispensedCommission = Station.TotalDispensedCost * Station.Commission;

            // Increment counters.
            currentPump.servicedVehicles++;
            Station.TotalServicedVehicles++;

            // Remove vehicle reference.
            currentVehicle = null;

            // Decrement counter.
            Station.VehiclesFuelling--;
        }







        /// <summary>
        /// 
        ///     Creates a new <c>Transaction</c> object. Records the transaction information to file.
        /// 
        /// </summary>
        /// <param name="v"> A <c>Vehicle</c> object of the vehicle that finished refuelling. </param>
        /// <param name="p"> A <c>Pump</c> object of the pump that was used. </param>
        /// <param name="cost"> A floating-point cost from the vehicle that refuelled. </param>
        private void RecordTransaction(Vehicle v, Pump p, float cost)
        {
            // Create new transaction object, add it to list, archive details in a file.
            Transaction t = new Transaction(v, p, cost);
            Station.transactions.Add(t);
            Transaction.StoreTransaction(t);
        }
    }
}
