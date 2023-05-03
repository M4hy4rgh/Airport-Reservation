using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace OopConsoleProjectProgram
{
    internal class BookingManager
    {
        private int numBookings;
        private int maxBookings;
        private Booking[] bookings;
        private int bookingNumber;

        public BookingManager(int maxBookings)
        {
            this.maxBookings = maxBookings;
            bookings = new Booking[maxBookings];
            numBookings = 0;
            bookingNumber = 1;
        }

        public void updateInitialId(int id)
        {
            bookingNumber = id;
        }
        public Booking[] getAllBookings()
        {
            return bookings;
        }

        public int getBookingCount()
        {
            return numBookings;
        }

        //check if booking exists by customer id and flight number
        private int search(int custId, string flightNumber)
        {
            for (int i = 0; i < numBookings; i++)
            {
                if (bookings[i].getCustomerId() == custId && bookings[i].getFlightnumber() == flightNumber)
                {
                    return i;
                }
            }
            return -1;
        }
        private int search(int bookingNumber)
        {
            for (int i = 0; i < numBookings; i++)
            {
                if (bookings[i].getBookingNumber() == bookingNumber)
                {
                    return i;
                }
            }
            return -1;
        }

        //make booking method that get the customer id anf the flight number as parameters then assign the customer to the flight
        public bool MakeBooking(Customer c, Flight f, string date = null, int id = -1)
        {
            if (numBookings < maxBookings)
            {
                if (date == null)
                    bookings[numBookings] = new Booking(bookingNumber++, c, f);
                else
                    bookings[numBookings] = new Booking(id, c, f, date);
                numBookings++;
                return true;
                
            }
            return false;
        }

        //customer numbers of bookings
        public int getNumberOfBookingPerCustomer(int custId)
        {
            int count = 0;
            for (int i = 0; i < numBookings; i++)
            {
                if (bookings[i].getCustomerId() == custId)
                {
                    count++;
                }
            }
            return count;
        }

        //get the numbers of bookings on a flight
        public int getNumberOfBookingPerFlight(string flightNumber)
        {
            int count = 0;
            for (int i = 0; i < numBookings; i++)
            {
                if (bookings[i].getFlightnumber() == flightNumber)
                {
                    count++;
                }
            }
            return count;
        }

        //getting the customer list of a flight/ Print the list of customers on a flight
        public string getCustomerListPerFlight(string flightNumber)
        {
            string str = "\n*********** Customer List ***********\n";
            bool found = false;
            for (int i = 0; i < numBookings; i++)
            {
                if (bookings[i].getFlightnumber() == flightNumber)
                {
                    found = true;
                    str += "Customer Id: " + bookings[i].getCustomerId().ToString() + "\n";
                }
              
            }
            if (!found)
            {
                str += "\nNo customer found on this flight\n";
            }
            return str;
        }

        public Booking findBooking(int custId, string flightNumber)
        {
            int loc = search(custId, flightNumber);
            if (loc != -1)
            {
                return bookings[loc];
            }
            return null;
        }


        //toString()
        public string ViewAllBookings()
        {
             
            string str = "\n********** View Booking **********\n";
            for (int i = 0; i < numBookings; i++)
            {
                str += bookings[i] + "\n";
            }
            return str;
        }

        public bool cancelBooking(int bookingNumber)
        {
            int loc = search(bookingNumber);
            if (loc != -1)
            {
                for (int i = loc; i < numBookings - 1; i++)
                {
                    bookings[i] = bookings[i + 1];
                }
                numBookings--;
                return true;
            }
            return false;
        }

    }
}
