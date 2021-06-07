using System;
using System.Timers;

namespace Assignment2_1602819
{
    /// <summary>
    /// 
    ///     The main <c>Program</c> class.
    ///     Holds the methods needed to run the program.
    ///     Holds the members and properties that pertain to the <c>Program</c> class.
    /// 
    /// </summary>
    class Program
    {
        /// <section>
        /// 
        ///     Initialisation of members with constant values.
        ///         - Refresh rate of the console screen display (milliseconds).
        /// 
        /// </section>

        private const int REFRESH_RATE      = 1500;

        /// <section>
        /// 
        ///     Declaration of object members.
        ///         - Timer that elapses when the screen refresh rate has been reached.
        /// 
        /// </section>

        private static Timer refreshScreenTimer;







        /// <summary>
        /// 
        ///     Runs the program. Creates a timer that refreshes the console display.
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {   
            // Display and set starting values.
            Console.Title = Station.Name;
            Console.ForegroundColor = Display.DefaultColour;

            // Initialise first methods to begin program.
            Display.DrawMenu();
            Station.StartObjectCreators();

            // Create a new instance of the timer object.
            refreshScreenTimer = new Timer();

            // Timer will elapse every screen refresh rate reached. Screen will refresh every 1.5 seconds. Timer loops.
            refreshScreenTimer.Interval = REFRESH_RATE;
            refreshScreenTimer.AutoReset = true;
            refreshScreenTimer.Elapsed += RefreshConsole;
            refreshScreenTimer.Enabled = true;
            refreshScreenTimer.Start();

            // Prevent program from ending.
            Console.ReadLine();
        }







        /// <summary>
        /// 
        ///     Wipes the console display and redraws the program to the console display again after 1.5 seconds.
        /// 
        /// </summary>
        /// <param name="sender"> Reference to the event invoker object. This was the <c>refreshScreenTimer</c> <c>Timer</c> object. </param>
        /// <param name="e"> Information about the <c>refreshScreenTimer</c> timer elapsing. </param>
        static void RefreshConsole(object sender, ElapsedEventArgs e)
        {
            // Clear the console and redisplay the program. Assign a queued vehicle to a pump at least once every refresh.
            Console.Clear();
            Station.LocatePump();
            Display.DrawQueue();
            Display.DrawForecourt();
            Display.DrawCounters();

        }
    }
}
