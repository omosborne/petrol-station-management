using System;

namespace Assignment2_1602819
{
    /// <summary>
    /// 
    ///     The <c>Display</c> class.
    ///     Holds the methods needed to draw the program.
    ///     Holds the members and properties that pertain to the <c>Display</c> class.
    /// 
    /// </summary>
    class Display
    {
        /// <section>
        /// 
        ///     Initialisation of members with constant values.
        ///         - Default colour of all regular text on the console.
        ///         - Colour of title text on the console.
        ///         - Title of the queue section.
        ///         - Title of the forecourt section.
        ///         - Title of the counter section.
        ///         - Display of the loading screen section.
        /// 
        /// </section>

        private const ConsoleColor DEFAULT_COLOUR       = ConsoleColor.White;
        private const ConsoleColor TITLE_COLOUR         = ConsoleColor.Yellow;
        private const string QUEUE_SECTION              = "========== QUEUE ==========";
        private const string FORECOURT_SECTION          = "======== FORECOURT ========";
        private const string COUNTER_SECTION            = "========= COUNTERS =========";
        private const string LOADING_SECTION            = "LOADING...";







        /// <section>
        /// 
        ///     <c>Get</c> and <c>Set</c> accessor methods for the <c>Station</c> class properties.
        /// 
        /// </section>

        /// <value> Gets the value of the default colour of text used on the console. </value>
        public static ConsoleColor DefaultColour { get { return DEFAULT_COLOUR; } }







        /// <summary>
        /// 
        ///     Draws the menu to the console. This is the first screen the user sees.
        /// 
        /// </summary>
        public static void DrawMenu()
        {
            DrawGap(7);
            Console.ForegroundColor = TITLE_COLOUR;
            Console.WriteLine(CenterText(Station.Name.ToUpper()));
            Console.ForegroundColor = DEFAULT_COLOUR;
            DrawGap(3);
            Console.WriteLine(CenterText("ENTER FULLSCREEN FOR THE BEST EXPERIENCE"));
            DrawGap(2);
            Console.WriteLine(CenterText("PRESS ANY KEY TO CONTINUE"));
            Console.ReadKey();
            Console.Clear();
            DrawGap(11);
            Console.WriteLine(CenterText(LOADING_SECTION));
        }







        /// <summary>
        /// 
        ///     Draws the queue section to the console. This is displayed at the top above the queue.
        /// 
        /// </summary>
        public static void DrawQueue()
        {
            // Declare members.
            Vehicle v;

            // Display section title.
            Console.ForegroundColor = TITLE_COLOUR;
            Console.WriteLine(CenterText(QUEUE_SECTION));
            Console.ForegroundColor = DEFAULT_COLOUR;

            // Displays a small queue identifier to tell how many vehicles are in the queue along with the queue max.
            Console.WriteLine(CenterText("[" + Station.queue.Count + "/" + Station.QueueMax + "]"));
            Console.WriteLine();

            // Go through the queue and display each vehicle.
            for (int i = 0; i < Station.queue.Count; i++)
            {
                v = Station.queue[i];
                Console.Write("| {0} {1} {2}. {3} {4} |\t", v.VehicleID, v.DriverName, v.DriverSurname, v.VehicleType, v.FuelType);
            }
            DrawGap(2);
        }







        /// <summary>
        /// 
        ///     Draws the forecourt section to the console. This is displayed below the queue.
        /// 
        /// </summary>
        public static void DrawForecourt()
        {
            // Declare members.
            Pump p;

            // Display section title.
            Console.ForegroundColor = TITLE_COLOUR;
            Console.WriteLine(CenterText(FORECOURT_SECTION));
            Console.ForegroundColor = DEFAULT_COLOUR;
            Console.WriteLine();

            // Go through pumps, displaying each one.
            for (int i = 0; i < Station.PumpLimit; i++)
            {
                p = Station.forecourt[i];

                Console.Write("| -{0}- | ", p.PumpID + 1);

                // If no vehicle is using the pump, display free.
                if (p.IsAvailable())
                {
                    Console.Write("[");
                    Console.Write(" ");
                    Console.Write("] ");
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("FREE");
                    Console.ForegroundColor = DEFAULT_COLOUR;
                    Console.Write("]");
                } 

                // If a vehicle is using the pump, display the vehicle and fuel type.
                else if (!p.IsAvailable())
                {
                    Console.Write("[");

                    /// <exception cref="ArgumentNullException"> Thrown with unfortunate timings where a vehicle has been assigned to a pump but just left the queue,
                    ///                                          or the vehicle was assigned to another pump at the same time as being assigned to this pump. Thus, no
                    ///                                          vehicle object reference to display the fuel type. </exception>
                    try
                    {
                        Console.Write("{0}", p.CurrentVehicle.FuelType.Substring(0, 1));
                    }
                    catch (Exception)
                    {
                        // As there is actually no vehicle assigned to this, set it to empty.
                        Console.Write("[");
                        Console.Write(" ");
                        Console.Write("] ");
                    }
                    Console.Write("] ");
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    /// <exception cref="ArgumentNullException"> Thrown with unfortunate timings where a vehicle has been assigned to a pump but just left the queue,
                    ///                                          or the vehicle was assigned to another pump at the same time as being assigned to this pump. Thus, no
                    ///                                          vehicle object reference to display the vehicle type. </exception>
                    try
                    {
                        Console.Write("{0} ", p.CurrentVehicle.VehicleType);
                    }
                    catch (Exception)
                    {
                        // As there is actually no vehicle assigned to this, set it to free.
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("FREE");
                        Console.ForegroundColor = DEFAULT_COLOUR;
                    }
                    Console.ForegroundColor = DEFAULT_COLOUR;
                    Console.Write("]");
                }

                // Draw the visual refuelling timer and determine the stage colour.
                Console.Write(" FUEL: [");
                if (p.FuelTimeLeft == "     ")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}", p.FuelTimeLeft);
                    Console.ForegroundColor = DEFAULT_COLOUR;

                }
                else if (p.FuelTimeLeft == "█    ")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}", p.FuelTimeLeft);
                    Console.ForegroundColor = DEFAULT_COLOUR;

                }
                else if (p.FuelTimeLeft == "██   ")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}", p.FuelTimeLeft);
                    Console.ForegroundColor = DEFAULT_COLOUR;

                }
                else if (p.FuelTimeLeft == "███  ")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("{0}", p.FuelTimeLeft);
                    Console.ForegroundColor = DEFAULT_COLOUR;

                }
                else if (p.FuelTimeLeft == "████ ")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}", p.FuelTimeLeft);
                    Console.ForegroundColor = DEFAULT_COLOUR;

                }
                else if (p.FuelTimeLeft == "█████")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}", p.FuelTimeLeft);
                    Console.ForegroundColor = DEFAULT_COLOUR;

                }
                Console.Write("]");

                // Tab across to create a gap between pumps.
                Console.Write("\t\t");

                // If the pump is the last one in the lane, move down to the next line to start a new lane.
                if (i % Station.PumpsPerLaneLimit == Station.PumpsPerLaneLimit - 1) { Console.WriteLine(); Console.WriteLine(); }


            }
            DrawGap(2);
        }







        /// <summary>
        /// 
        ///     Draws the counters section to the console. This is displayed at the bottom below the forecourt.
        /// 
        /// </summary>
        public static void DrawCounters()
        {
            // Declare members.
            Pump p;
            Transaction t;

            // Display section title.
            Console.ForegroundColor = TITLE_COLOUR;
            Console.WriteLine(CenterText(COUNTER_SECTION));
            Console.ForegroundColor = DEFAULT_COLOUR;
            Console.WriteLine();

            // Display the station details, fuel rates.
            Console.WriteLine(CenterText("SALE! Low prices here!"));
            Console.WriteLine(CenterText("Unleaded: £" + Station.UnleadedRate + " -- Diesel: £" + Station.DieselRate + " -- LPG: £" + Station.LpgRate));
            Console.WriteLine();

            // Go through each pump and display the counter details in a table-like format.
            for (int i = 0; i < Station.PumpLimit; i++)
            {
                p = Station.forecourt[i];
                Console.WriteLine("| Pump {0} | T. Fuel: {1} Ls\t| Unleaded: {2} Ls\t| Diesel: {3} Ls  \t| LPG: {4} Ls\t| Cost: £{5}      \t| Com: £{6}\t| Serviced: {7}",
                                  p.PumpID + 1, p.LitresDispensed, p.UnleadedDispensed, p.DieselDispensed, p.LpgDispensed, p.DispensedCost.ToString("n2"), p.DispensedCommission.ToString("n2"), p.ServicedVehicles);
            }
            Console.WriteLine();

            // Display the counter details for the total values.
            Console.WriteLine("| Total  | T. Fuel: {0} Ls\t| Unleaded: {1} Ls\t| Diesel: {2} Ls  \t| LPG: {3} Ls\t| Cost: £{4}      \t| Com: £{5}\t| Serviced: {6}",
                                  Station.TotalLitresDispensed, Station.TotalUnleadedDispensed, Station.TotalDieselDispensed, Station.TotalLpgDispensed, Station.TotalDispensedCost.ToString("n2"),
                                  Station.TotalDispensedCommission.ToString("n2"), Station.TotalServicedVehicles);
            DrawGap(2);

            // Display the latest transaction, only if there is at least one transaction.
            if (!(Station.transactions.Count == 0))
            {
                t = Station.transactions[Station.transactions.Count - 1];
                Console.WriteLine("Last transaction: #{0} -- Pump {1} | ID: {2} {3} {4}. | {5} | {6} {7} Ls | Cost: £{8} | Com: £{9}",
                                t.TransactionID + 1, t.TransactionPumpID + 1, t.TransactionVehicleID, t.TransactionDriverName, t.TransactionDriverSurname, t.TransactionVehicleType, t.TransactionVehicleFuelType,
                                t.TransactionFuelDispensed, t.TransactionCost.ToString("n2"), t.TransactionCommission.ToString("n2"));
                Console.WriteLine("For the full transactions list: Assignment2_1602819\\bin\\Debug\\netcoreapp2.1\\{0}", Transaction.FileName);
            }
            DrawGap(2);

            // Display number of pumps in use and failed vehicle counters.
            Console.WriteLine("Pumps in use: {0}", Station.VehiclesFuelling);
            Console.WriteLine("Vehicles left before fuelling: {0}", Station.TotalUnservicedVehicles);
        }







        /// <summary>
        /// 
        ///     Ease-of-use tools below.
        /// 
        /// </summary>







        /// <summary>
        /// 
        ///     Places specified text center of the console output line.
        ///     The line of code used in this method to center text was taken from the user "Dayan" from the following website on 14/05/20:
        ///         - https://stackoverflow.com/questions/21917203/how-do-i-center-text-in-a-console-application/21917650.
        /// 
        /// </summary>
        /// <param name="text"> A string of text to be centered. </param>
        /// <returns> A formatted string in the middle of the console. </returns>
        private static string CenterText(string text)
        {
            // Reformat the text depending on the console width and text length.
            return String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text);
        }







        /// <summary>
        /// 
        ///     Sends a specified number of <c>WriteLine</c>s to the console to create a gap.
        /// 
        /// </summary>
        /// <param name="numberOfLines"> An integer of the number of blank lines to display. </param>
        private static void DrawGap(int numberOfLines)
        {
            // Go through the size of gap wanted and write a new line.
            for (int i = 0; i < numberOfLines; i++)
            {
                Console.WriteLine();
            }
        }
    }
}
