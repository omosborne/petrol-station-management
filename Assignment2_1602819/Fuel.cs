using System;

namespace Assignment2_1602819
{
    class Fuel
    {
        private string type;

        private static string[] fuels = new string[] { "UNLDEADED", "DIESEL", "LPG" };

        public Fuel(string type)
        {
            this.type = type;
        }
    }
}
