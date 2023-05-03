using System;

namespace OopConsoleProjectProgram
{
    internal class Booking
    {
        private string dateOfBooking;
        private int bookingNumber;
        private Customer customer;
        private Flight flight;


        //constructor
        public Booking(int bookingNumber, Customer customer, Flight flight, string dateOfBooking = null)
        {
            this.bookingNumber = bookingNumber;
            this.customer = customer;
            this.flight = flight;
            this.dateOfBooking = dateOfBooking;
            if (dateOfBooking == null)
                this.dateOfBooking = DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt");
            
        }

        //getters
        public string getDateOfBooking()
        {
            return dateOfBooking;
        }

        public int getBookingNumber()
        {
            return bookingNumber;
        }

        public string getFlightnumber()
        {
            return flight.getFlightNumber();
        }

        public int getCustomerId()
        {
            return customer.getCustomerId();
        }


        //toString()
        public override string ToString()
        {
            return "Date of booking: " + dateOfBooking 
                + " | Booking number: " + bookingNumber 
                + " | Flight Number: " + flight.getFlightNumber()
                + " | Customer name: " + customer.getCustomerFirstName() + " " + customer.getCustomerLastName();
        }


    }
}
