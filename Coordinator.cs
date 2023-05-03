using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace OopConsoleProjectProgram
{
    internal class Coordinator
    {
        private CustomerManager cm;
        private FlightManager fm;
        private BookingManager bm;

        public Coordinator(CustomerManager cm, FlightManager fm, BookingManager bm)
        {
            this.cm = cm;
            this.fm = fm;
            this.bm = bm;

            string[] bookingInfo = File.ReadLines("../../../files/bookingInfo.txt").ToArray();

            for (int i = 0; i < bookingInfo.Length - 1; i++)
            {
                string[] items = bookingInfo[i + 1].Split(',');
                MakeBooking(items[2], int.Parse(items[3]), items[0], int.Parse(items[1]));
            }
            bm.updateInitialId(int.Parse(bookingInfo.Last().Split(',')[1]) + 1);
        }

        //------------------ Customer ---------------------

        //add customer
        public bool AddCustomer(string firstName, string lastName, string phone)
        {
            return cm.AddCustomer(firstName, lastName, phone);
        }

        //remove customer
        public bool RemoveCustomer(int custId)
        {
            if (bm.getNumberOfBookingPerCustomer(custId) == 0)
            {
                return cm.DeleteCustomer(custId);
            }
            return false;
        }

        //view customers
        public string ViewCustomers()
        {
            return cm.viewCustomers();
        }

        //find customer by id
        public Customer FindCustomer(int custId)
        {
            return cm.findCustomer(custId);
        }
        //find customer by name, phone
        public Customer FindCustomer(string firstName, string lastName, string phone)
        {
            return cm.findCustomer(firstName, lastName, phone);
        }

        //get all the customer ids
        public string getAllCustomerIds()
        {
            return cm.getAllCustomerIN();
        }

        //customer list
        public int getcustomerCount()
        {
            return cm.getcustomerCount();
        }

        //get the customer list
        public Customer[] getCustomerList()
        {
            return cm.getCustList();
        }

        //customer list for a particular flight
        public string GetCustomerListPerFlight(string flightNumber)
        {
            return bm.getCustomerListPerFlight(flightNumber);
        }

        //------------------ Fights ---------------------


        //add flight
        public bool AddFlight(string origin, string destination, string date, string time)
        {

            return fm.AddFlight(origin, destination, date, time);
        }

        // remove flight
        public bool RemoveFlight(string flightNumber)
        {
            if (bm.getNumberOfBookingPerFlight(flightNumber) == 0)
            {
                return fm.removeFlight(flightNumber);
            }
            return false;
        }

        //view all flights
        public string ViewAllFlights()
        {
            return fm.viewAllFlights();
        }

        //view particular flight
        public string ViewParticularFlight(string flightNumber)
        {
            return fm.viewFlight(flightNumber);
        }

        //find flight by flight number
        public Flight FindFlight(string flightNumber)
        {
            return fm.findFlight(flightNumber);
        }
        // find flight by origin, destination, date, time
        public Flight FindFlight(string origin, string destination, string date, string time)
        {
            return fm.findFlight(origin, destination, date, time);
        }

        //getting all the flight numbers
        public string getAllFlightNumber()
        {
            return fm.getAllFlightNumber();
        }
        //get the count of flights
        public int getFlightCount()
        {
            return fm.getNumFlight();
        }

        //get the flight list
        public Flight[] getFlightsList()
        {
            return fm.getFlights();
        }



        //------------------ Bookings ---------------------


        //add booking
        public bool MakeBooking(string flightNumber, int customerId, string date = null, int id = -1)
        {
            if (cm.search(customerId) != -1 && fm.search(flightNumber) != -1 && fm.findFlight(flightNumber).getFlightSeats() > 0)
            {
                Customer c = cm.findCustomer(customerId);
                Flight f = fm.findFlight(flightNumber);
                f.decFlightSeats();
                c.incNumOfBookings();
                if (date == null)
                    return bm.MakeBooking(c, f);
                return bm.MakeBooking(c,f, date, id);
               
            }
            return false;
        }

        //view bookings
        public string ViewAllBookings()
        {
            return bm.ViewAllBookings();
        }

        //cancel booking
        public bool CancelBooking(int bookingNumber)
        {
            return bm.cancelBooking(bookingNumber);
        }

        //find booking by customer id and flight number
        public Booking FindBooking(int custId, string flightNumber)
        {
            return bm.findBooking(custId, flightNumber);
        }
        
        //get the list of bookings
        public Booking[] getBookingList()
        {
            return bm.getAllBookings();
        }

        //get count of bookings
        public int getBookingCount()
        {
            return bm.getBookingCount();
        }

    }
}
