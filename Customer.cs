using System;

namespace OopConsoleProjectProgram
{
    internal class Customer
    {
        private int customerId;
        private string customerFirstName;
        private string customerLastName;
        private string customerPhoneNumber;
        private int numberOfBookings;
        public Customer(int customerId ,string customerFirstName, string customerLastName, string customerPhoneNumber)
        {
            this.customerId = customerId;
            this.customerFirstName = customerFirstName;
            this.customerLastName = customerLastName;
            this.customerPhoneNumber = customerPhoneNumber;
            numberOfBookings = 0;
        }

        public int getCustomerId()
        {
            return customerId;
        }

        public string getCustomerFirstName()
        {
            return customerFirstName;
        }

        public string getCustomerLastName()
        {
            return customerLastName;
        }

        public string getCustomerPhoneNumber()
        {
            return customerPhoneNumber;
        }
        public void incNumOfBookings(){
            numberOfBookings++;
        }


        public override string ToString()
        {
            string output = "";
            output += "\n***************************************";
            output += "\nCustomer ID: " + customerId;
            output += "\nCustomer First Name: " + customerFirstName;
            output += "\nCustomer Last Name: " + customerLastName;
            output += "\nCustomer Phone Number: " + customerPhoneNumber;
            output += "\nNumber of Bookings: " + numberOfBookings;
            output += "\n***************************************\n";

            return output;
        }


    }
}
