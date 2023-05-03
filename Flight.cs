using System;
using System.Linq;
using System.Threading;

namespace OopConsoleProjectProgram
{
    internal class Flight
    {
        private string flightNumber;
        private string flightOrigin;
        private string flightDestination;
        private string flightDate;
        private string flightTime;
        private int maxSeats;
        private int numOfbookingsOnTheFlight;

        public Flight(int flightNumber, string flightOrigin, string flightDestination, string flightDate, string flightTime)
        {
            this.flightNumber = FlightNumberToString(flightNumber);
            this.flightOrigin = flightOrigin;
            this.flightDestination = flightDestination;
            this.flightDate = flightDate;
            this.flightTime = flightTime;
            maxSeats = 200;
        }

        public Flight(string flightNumber, string flightOrigin, string flightDestination, string flightDate, string flightTime)
        {
            this.flightNumber = flightNumber;
            this.flightOrigin = flightOrigin;
            this.flightDestination = flightDestination;
            this.flightDate = flightDate;
            this.flightTime = flightTime;
            maxSeats = 200;
        }


        //create random flight numbers
        private static string FlightNumberToString(int flightNumber)
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string Num = "0123456789";
            int count = 0;
            string result = "";
            while (flightNumber != 0)
            {
                if (count % 2 == 0)
                {
                    result += alpha.ToCharArray()[flightNumber % 10];
                }
                else {
                    result += Num.ToCharArray()[flightNumber % 10];
                }
                count++;
                flightNumber /= 10;
            }
            return result;
        }

        public string getFlightNumber()
        {
            return flightNumber;
        }

        public string getFlightOrigin()
        {
            return flightOrigin;
        }

        public string getFlightDestination()
        {
            return flightDestination;
        }

        public string getFlightDate()
        {
            return flightDate;
        }

        public string getFlightTime()
        {
            return flightTime;
        }

        public int getFlightSeats()
        {
            return maxSeats;
        }
        public void decFlightSeats()
        {
            maxSeats--;

        }

        public int getNumOfbookingsOnTheFlight()
        {
            return numOfbookingsOnTheFlight;
        }
        public override string ToString()
        {
            string output = "";
            output += "\n***************************************";
            output += "\nFlight Number: " + flightNumber;
            output += "\nFlight Origin: " + flightOrigin;
            output += "\nFlight Destination: " + flightDestination;
            output += "\nFlight Date: " + flightDate;
            output += "\nFlight Time: " + flightTime;
            output += "\nPlane Capacity: " + maxSeats;
            output += "\n***************************************\n";

            return output;
        }


    }
}
