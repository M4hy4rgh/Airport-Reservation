using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OopConsoleProjectProgram
{
    internal class FlightManager
    {
        private int NumFlight;
        private int MaxFlight;
        private int FlightNumber;
        private Flight[] flights;

        //constructor
        public FlightManager(int maxFlight)
        {
            MaxFlight = maxFlight;
            flights = new Flight[MaxFlight];
            NumFlight = 0;
            FlightNumber = 1000;

            string[] flightLines = File.ReadLines("../../../files/flightInfo.txt").ToArray();
            if (flightLines.Length != 0)
            {
                int lastId = FlightNumberToInt(flightLines.Last().Split(',')[0]);
                FlightNumber = lastId + 1;
            }

            for (int i = 0; i < flightLines.Length - 1; i++)
            {
                string[] items = flightLines[i + 1].Split(',');
                flights[i] = new Flight(items[0], items[1], items[2], items[3], items[4]);
                NumFlight++;
            }
        }


        //write the reverse of above function.
        private static int FlightNumberToInt(string flightNumber)
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string Num = "0123456789";
            int count = 0;
            int result = 0;
            char[] flightNumChar = flightNumber.ToCharArray();
            for (int i = flightNumChar.Length - 1; i >= 0; i--)
            {
                result += count++ % 2 == 1 ? alpha.IndexOf(flightNumChar[i]) : Num.IndexOf(flightNumChar[i]);
                result *= 10;
            }
            return result / 10;
        }

        //addflight
        public bool AddFlight(string flightOrigin, string flightDestination, string flightDate, string flightTime)
        {
            if (NumFlight < MaxFlight)
            {
                int loc = search(flightOrigin, flightDestination, flightDate, flightTime);
                if (loc == -1)
                {
                    flights[NumFlight] = new Flight(FlightNumber++, flightOrigin, flightDestination, flightDate, flightTime);
                    NumFlight++;
                    return true;
                }
            }
            return false;
        }

        //remove flight
        public bool removeFlight(string flightNumber)
        {
            int loc = search(flightNumber);
            if (loc != -1)
            {
                flights[loc] = flights[NumFlight - 1];
                NumFlight--;
                return true;
            }
            return false;
        }

        //view all Flights
        public string viewAllFlights()
        {
            string str = "\n*********** Flight List ***********\n";
            for (int i = 0; i < NumFlight; i++)
            {
                str += flights[i].ToString();
            }
            return str;
        }

        //view a particular flight using flight number
        public string viewFlight(string flightNumber)
        {
            int loc = search(flightNumber);
            if (loc != -1)
            {
                return flights[loc].ToString();
            }
            return "Flight not found";
        }

        //get number of flights
        public int getNumFlight()
        {
            return NumFlight;
        }

        //get the flights
        public Flight[] getFlights()
        {
            return flights;
        }

        //find flight with flightnumber
        public Flight findFlight(string flightNumber)
        {
            int loc = search(flightNumber);
            if (loc != -1)
            {
                return flights[loc];
            }
            return null;
        }
        
        // find flight with origin, destination, date and time
        public Flight findFlight(string flightOrigin, string flightDestination, string flightDate, string flightTime)
        {
            int loc = search(flightOrigin, flightDestination, flightDate, flightTime);
            if (loc != -1)
            {
                return flights[loc];
            }
            return null;
        }

        //get the flight numbers
        public string getAllFlightNumber()
        {
            string str = "*********** Flight List ***********\n";
            for (int i = 0; i < NumFlight; i++)
            {
                str += "Flight Number: " + flights[i].getFlightNumber() + "\n";
            }
            return str;
        }


        //check if flight exists by origin, destination, date and time
        public int search(string flightOrigin, string flightDestination, string flightDate, string flightTime)
        {
            for (int i = 0; i < NumFlight; i++)
            {
                if (flights[i].getFlightOrigin() == flightOrigin && flights[i].getFlightDestination() == flightDestination && flights[i].getFlightDate() == flightDate && flights[i].getFlightTime() == flightTime)
                {
                    return i;
                }
            }
            return -1;
        }

        //check if flight exists by flight number
        public int search(string flightNumber)
        {
            for (int i = 0; i < NumFlight; i++)
            {
                if (flights[i].getFlightNumber() == flightNumber)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
