using System;
using System.IO;
using System.Timers;

namespace Assignment2_1602819
{
    /// <summary>
    /// 
    ///     The <c>Transaction</c> class.
    ///     Holds the methods needed to create and archive <c>Transaction</c> objects.
    ///     Holds the members and properties that pertain to the <c>Transaction</c> class and object.
    /// 
    /// </summary>
    class Transaction
    {
        /// <section>
        /// 
        ///     Initialisation of members with constant values.
        ///         - Lower bracket for randomised delay of concurrent transactions (milliseconds).
        ///         - Upper bracket for randomised delay of concurrent transactions (exclusive, +1) (milliseconds).
        ///         - Name of the file name to locate and store transactions.
        /// 
        /// </section>

        private const int TRANSACTION_DELAY_LOWER       = 500;
        private const int TRANSACTION_DELAY_UPPER       = 1001;
        private const string FILE_NAME                  = "Transactions.csv";

        /// <section>
        /// 
        ///     Declaration of integer members.
        ///         - Unique identification number.
        ///         - Vehicle ID for transaction.
        ///         - Pump ID for transaction.
        ///         - ID for the next transaction to be created.
        /// 
        /// </section>

        private int transactionID;
        private int transactionVehicleID;
        private int transactionPumpID;
        private static int nextTransactionID;

        /// <section>
        /// 
        ///     Declaration of floating-point members.
        ///         - Number of litres of fuel dispensed for the transaction.
        ///         - Cost from the number of litres of fuel dispensed for the transaction.
        ///         - Commission from cost of the number of litres of fuel dispensed for the transaction.
        /// 
        /// </section>

        private float transactionFuelDispensed;
        private float transactionCost;
        private float transactionCommission;

        /// <section>
        /// 
        ///     Declaration of string members.
        ///         - Classification of vehicle for the transaction.
        ///         - First name of the driver for the transaction.
        ///         - Last name of the driver for the transaction.
        ///         - Type of fuel vehicle uses for the transaction.
        /// 
        /// </section>

        private string transactionVehicleType;
        private string transactionDriverName;
        private string transactionDriverSurname;
        private string transactionVehicleFuelType;

        /// <section>
        /// 
        ///     Declaration of object members.
        ///         - Transaction currently trying to save to file.
        ///         - Time that elapses randomly to reattempt to save transaction to file.
        /// 
        /// </section>

        private static Transaction currentTransaction;
        private static Timer delayTransactionTimer;







        /// <section>
        ///
        ///     <c>Get</c> and <c>Set</c> accessor methods for the <c>Transaction</c> class properties.
        /// 
        /// </section>

        /// <value> Gets the value of the transaction identifier. </value>
        public int TransactionID { get { return transactionID; } }

        /// <value> Gets the value of the type of vehicle used for the transaction. </value>
        public string TransactionVehicleType { get { return transactionVehicleType; } }

        /// <value> Gets the value of the vehicle identifier for the transaction. </value>
        public int TransactionVehicleID { get { return transactionVehicleID; } }

        /// <value> Gets the value of the first name of the driver for the transaction. </value>
        public string TransactionDriverName { get { return transactionDriverName; } }

        /// <value> Gets the value of the last name of the driver for the transaction. </value>
        public string TransactionDriverSurname { get { return transactionDriverSurname; } }

        /// <value> Gets the value of the type of fuel the vehicle used for the transaction. </value>
        public string TransactionVehicleFuelType { get { return transactionVehicleFuelType; } }

        /// <value> Gets the value of the litres of fuel dispensed for the transaction. </value>
        public float TransactionFuelDispensed { get { return transactionFuelDispensed; } }

        /// <value> Gets the value of the pump identifier for the transaction. </value>
        public int TransactionPumpID { get { return transactionPumpID; } }

        /// <value> Gets the value of the cost for the transaction. </value>
        public float TransactionCost { get { return transactionCost; } }

        /// <value> Gets the value of the commission for the transaction. </value>
        public float TransactionCommission { get { return transactionCommission; } }

        /// <value> Gets the value of the file name to locate and store the transactions. </value>
        public static string FileName { get { return FILE_NAME; } }







        /// <summary>
        /// 
        ///     <c>Instance Constructor</c> for the <c>Transaction</c> class. Creates a <c>Transaction</c> object.
        /// 
        /// </summary>
        /// <param name="v"> A <c>Vehicle</c> object of the vehicle that finished refuelling. </param>
        /// <param name="p"> A <c>Pump</c> object of the pump that was used. </param>
        /// <param name="cost"> A floating-point cost from the vehicle that refuelled. </param>
        public Transaction(Vehicle v, Pump p, float cost)
        {
            // Increment the transaction ID.
            transactionID = nextTransactionID++;

            // Set detail values.
            transactionVehicleType = v.VehicleType;
            transactionFuelDispensed = v.FuelNeeded;
            transactionPumpID = p.PumpID;
            transactionCost = cost;
            transactionCommission = cost * Station.Commission;
            transactionVehicleID = v.VehicleID;
            transactionDriverName = v.DriverName;
            transactionDriverSurname = v.DriverSurname;
            transactionVehicleFuelType = v.FuelType;
        }







        /// <summary>
        /// 
        ///     Archives the transaction for persistence.
        /// 
        /// </summary>
        /// <param name="t"> A <c>Transaction</c> object to store its parameters in a csv file. </param>
        public static void StoreTransaction(Transaction t)
        {
            /// <exception cref="IOException"> Thrown when multiple stream writer instances attempt to write transactions to file concurrently.
            ///                                The transaction can't be saved to a file already opened by another instance. </exception>
            try
            {
                // Create a new instance of the streamwriter object. Set the file name and let it appened to the file.
                using (StreamWriter transactionFile = new StreamWriter(FILE_NAME, true))
                {
                    // Allocate which details to store in the cvs file.
                    transactionFile.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                                    t.TransactionID + 1, t.TransactionPumpID + 1, t.TransactionVehicleID, t.TransactionDriverName, t.TransactionDriverSurname, t.TransactionVehicleType, t.TransactionVehicleFuelType,
                                    t.TransactionFuelDispensed, t.TransactionCost.ToString("n2"), t.TransactionCommission.ToString("n2"));
                }
            }
            catch (IOException)
            {
                // Localise members.
                currentTransaction = t;

                // Create a new instance of the random object.
                var r = new Random();

                // Create a new instance of the timer object.
                delayTransactionTimer = new Timer();

                // Timer runs randomly before reattempting to save transaction to file again. Timer doesn't loop. 
                delayTransactionTimer.Interval = r.Next(TRANSACTION_DELAY_LOWER, TRANSACTION_DELAY_UPPER);
                delayTransactionTimer.AutoReset = false;
                delayTransactionTimer.Elapsed += DelayTransaction;
                delayTransactionTimer.Enabled = true;
                delayTransactionTimer.Start();
                
            }
        }







        /// <summary>
        /// 
        ///     Delays the archiving of the transaction as a result of the file being opened by another instance.
        /// 
        /// </summary>
        /// <param name="sender"> Reference to the event invoker object. This was the <c>delayTransactionTimer</c> <c>Timer</c> object. </param>
        /// <param name="e"> Information about the <c>delayTransactionTimer</c> timer elapsing. </param>
        private static void DelayTransaction(object sender, ElapsedEventArgs e)
        {
            // Reattempt to save transaction to file. This is a recursive loop until the transaction saves successfully.
            StoreTransaction(currentTransaction);
        }
    }
}
