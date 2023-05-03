using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace OopConsoleProjectProgram
{
    internal class CustomerManager 
    {
        private int numCust;
        private int maxCust;
        private Customer[] custList;
        private int custId;


        //constructor
        public CustomerManager(int maxCust)
        {
            numCust = 0;
            custId = 100;
            string[] reader = File.ReadLines("../../../files/customerInfo.txt").ToArray();
            if (reader.Length != 0)
            {
                int lastId = Convert.ToInt32(reader.Last().Split(',')[0]);
                custId = lastId + 1;
            }
            this.maxCust = maxCust;
            custList = new Customer[maxCust];

            for (int i = 0; i < reader.Length - 1; i++)
            {
                string[] items = reader[i + 1].Split(',');
                custList[i] = new Customer(int.Parse(items[0]), items[1], items[2], items[3]);
                numCust++;
            }
        }

        //add customer      
        public bool AddCustomer(string custFName, string custLName, string custPhone)
        {
            if (numCust < maxCust)
            {
                int loc = search(custFName, custLName, custPhone);
                if (loc == -1)
                {
                    custList[numCust] = new Customer(custId++, custFName, custLName, custPhone);
                    numCust++;
                    return true;
                }
            }
            return false;
        }

        //A customer can only be deleted if there are no bookings for that customer.
        public bool DeleteCustomer(int custId)
        {
            int loc = search(custId);
            if (loc != -1)
            {
                custList[loc] = custList[numCust - 1];
                numCust--;
                return true;
            }
            return false;
        }


        //Show all the customers
        public string viewCustomers()
        {
            string output = "\n********** Customer List **********\n";
            for (int i = 0; i < numCust; i++)
            {
                output += custList[i].ToString();
            }
            return output;
        }
        
        public Customer[] getCustList()
        {
            return custList;
        }

        //get customer list
        public int getcustomerCount()
        {
            return numCust;
        }

        //find customer by id
        public Customer findCustomer(int custId)
        {
            int loc = search(custId);
            if (loc != -1)
            {
                return custList[loc];
            }
            return null;
        }

        //find customer by name
        public Customer findCustomer(string custFName, string custLName, string custPhone)
        {
            int loc = search(custFName, custLName, custPhone);
            if (loc != -1)
            {
                return custList[loc];
            }
            return null;
        }

        //get the customer ids
        public string getAllCustomerIN()
        {
            string str = "\n*********** Customer List ***********\n\n";
            
            for (int i = 0; i < numCust; i++)
            {
                str += "ID: " + custList[i].getCustomerId() + " | Name: " + custList[i].getCustomerFirstName() + " " + custList[i].getCustomerLastName() +"\n";
            }
            return str;
        }

        //check if customer exists by full name, and phone number
        public int search(string custFName, string custLName, string custPhone)
        {
            for (int i = 0; i < numCust; i++)
            {
                if (custList[i].getCustomerFirstName() == custFName && custList[i].getCustomerLastName() == custLName && custList[i].getCustomerPhoneNumber() == custPhone)
                {
                    return i;
                }
            }
            return -1;
        }

        //check if customer exists by customer id
        public int search(int custId)
        {
            for (int i = 0; i < numCust; i++)
            {
                if (custList[i].getCustomerId() == custId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
