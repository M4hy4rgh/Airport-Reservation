using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using static System.Console;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Net;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace OopConsoleProjectProgram
{
    internal class Program
    {
        public static Coordinator coord;

        public static string phoneRegex;
        public static string dateRegex;
        public static string timeRegex;
        public static string flightNumberRegex;
        public static string CustomerIdRegex;
        public static Dictionary<string, Dictionary<string, Action>> menu;

        //----------- Menu -------------
        public static void showMenu(string menuName)
        {
            Clear();
            WriteLine("\nMNSV Airlines Co.");
            WriteLine("\t" + menuName + " Menu");
            WriteLine("Please select an option from the list below:\n");
            int i;
            for (i = 0; i < menu[menuName].Count; i++)
            {
                WriteLine((i + 1) + ") " + menu[menuName].Keys.ToList()[i]);
            }
            WriteLine((i + 1) + ") Exit/Return\n");
        }

        //----------- Main -------------

        //run the program
        public static void runProgram()
        {
            int choice;
            showMenu("Main");

            while ((choice = MenuValidation("Main")) != 4)
            {
                string menuName = menu["Main"].Keys.ToList()[choice - 1];
                showMenu(menuName);
                while ((choice = MenuValidation(menuName)) != menu[menuName].Keys.Count() + 1)
                {
                    menu[menuName][menu[menuName].Keys.ToList()[choice - 1]]();
                    showMenu(menuName);
                }
                Clear();
                showMenu("Main");
            }
        }


        //------------ Customer -------------

        //add Customer
        public static void addCustomer()
        {
            string fName, lName, phoneNum;
            WriteLine("\n*************** Add Customer ***************");
            WriteLine("\nPlease enter the customer's First Name: ");
            fName = inputValidation();

            WriteLine("Please enter the customer's Last Name: ");
            lName = inputValidation();

            WriteLine("please enter the customer's Phone Number: (200-456-7890/+1 223 456 7890, etc...) ");
            phoneNum = ReadLine().Trim();
            
            //check with the phoneRegex
            while (!Regex.IsMatch(phoneNum, phoneRegex))
            {
                WriteLine("Please enter valid customer's Phone Number: ");
                phoneNum = ReadLine().Trim();
            }

            if (coord.AddCustomer(fName, lName, phoneNum))
            {
                //append customer id, fname , lname, and phone number into the file customerInfor.txt
                using (StreamWriter sw = new StreamWriter("../../../files/customerInfo.txt"))
                {
                    Customer[] allCustomers = coord.getCustomerList();
                    sw.WriteLine("Number of Customers: " + coord.getcustomerCount());
                    for (int i = 0; i < coord.getcustomerCount(); i++)
                    {
                        sw.WriteLine(allCustomers[i].getCustomerId() +
                        "," + allCustomers[i].getCustomerFirstName() +
                        "," + allCustomers[i].getCustomerLastName() +
                        "," + allCustomers[i].getCustomerPhoneNumber());
                    }
                }
                WriteLine("Customer added successfully....");
            }
            else
            {
                WriteLine("Customer not added....");
            }
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }

        //remove customer
        public static void removeCustomer()
        {
            int custID;
            WriteLine("\n*************** Remove Customer ***************");
            WriteLine(coord.getAllCustomerIds());
            WriteLine("Please enter the customer's ID: ");
            
            while (!int.TryParse(ReadLine().Trim(), out custID) || !Regex.IsMatch(custID.ToString(), CustomerIdRegex))
            {
                //check with the CustomerIDRegex
                WriteLine("Please enter a valid customer's ID: ");
            }

            if (coord.RemoveCustomer(custID))
            {
                using (StreamWriter sw = new StreamWriter("../../../files/customerInfo.txt"))
                {
                    Customer[] allCustomers = coord.getCustomerList();
                    sw.WriteLine("Number of Customers: " + coord.getcustomerCount());
                    for (int i = 0; i < coord.getcustomerCount(); i++)
                    {
                        sw.WriteLine(allCustomers[i].getCustomerId() +
                        "," + allCustomers[i].getCustomerFirstName() +
                        "," + allCustomers[i].getCustomerLastName() +
                        "," + allCustomers[i].getCustomerPhoneNumber());
                    }
                }
                WriteLine("Customer removed successfully....");
            }
            else
            {
                WriteLine("Customer not removed... The Customer have a Booking / The Customer is not found...");
            }
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }

        //view customers
        public static void viewCustomers()
        {
            Clear();
            WriteLine(coord.ViewCustomers());
            WriteLine("\nPress any key to continue....");
            ReadKey();

        }

        //--------------- Flight ---------------

        //add flight
        public static void addFlight()
        {
            string origin, destination, date, time;
            WriteLine("*************** Add Flight ***************");
            
            WriteLine("Please enter the flight's origin: ");
            origin = inputValidation();
            
            WriteLine("Please enter the flight's destination: ");
            destination = inputValidation();
            
            WriteLine("Please enter the flight's date: (MM/DD/YYYY) ");
            date = ReadLine().Trim();
            //check with the dateRegex
            while (!Regex.IsMatch(date, dateRegex))
            {
                WriteLine("Please enter the flight's date: (MM/DD/YYYY) ");
                date = ReadLine().Trim();
            }

            WriteLine("Please enter the number of time: (e.g 12:00 pm/am) ");
            time = ReadLine().Trim();
            //check with the timeRegex
            while (!Regex.IsMatch(time, timeRegex, RegexOptions.IgnoreCase))
            {
                WriteLine("Please enter the number of time: (e.g 12:00 pm/am) ");
                time = ReadLine().Trim();
            }

            if (coord.AddFlight(origin, destination, date, time))
            {
                WriteLine("Flight added successfully....");

                using (StreamWriter sw = new StreamWriter("../../../files/flightInfo.txt"))
                {
                    Flight[] allFlights = coord.getFlightsList();
                    sw.WriteLine("Number of Flight: " + coord.getFlightCount());
                    for (int i = 0; i < coord.getFlightCount(); i++)
                    {
                        sw.WriteLine(allFlights[i].getFlightNumber() +
                        "," + allFlights[i].getFlightOrigin() +
                        "," + allFlights[i].getFlightDestination() +
                        "," + allFlights[i].getFlightDate() +
                        "," + allFlights[i].getFlightTime());
                    }
                }

            }
            else
            {
                WriteLine("Flight not added....");
            }
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }

        //remove flight
        public static void removeFlight()
        {
            string flightID;
            WriteLine("\n*************** Remove Flight ***************");
            
            WriteLine(coord.getAllFlightNumber() + "\n");
            WriteLine("Please enter the flight's Number: ");
            flightID = ReadLine().Trim().ToUpper();
            //check with the regex
            while (!Regex.IsMatch(flightID, flightNumberRegex, RegexOptions.IgnoreCase))
            {
                WriteLine("Invalid flight number, please enter again: ");
                flightID = ReadLine().Trim().ToUpper();
            }

            if (coord.RemoveFlight(flightID))
            {
                using (StreamWriter sw = new StreamWriter("../../../files/flightInfo.txt"))
                {
                    Flight[] allFlights = coord.getFlightsList();
                    sw.WriteLine("Number of Flight: " + coord.getFlightCount());
                    for (int i = 0; i < coord.getFlightCount(); i++)
                    {
                        sw.WriteLine(allFlights[i].getFlightNumber() +
                        "," + allFlights[i].getFlightOrigin() +
                        "," + allFlights[i].getFlightDestination() +
                        "," + allFlights[i].getFlightDate() +
                        "," + allFlights[i].getFlightTime());
                    }
                }
                WriteLine("Flight removed successfully....");
            }
            else
            {
                WriteLine("Flight not removed... / There are customers on this flight...");
            }
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }

        // view a flight
        public static void viewParticularFlight()
        {
            string flightNumber;
            Clear();
            WriteLine("\n*************** View Flight ***************");
            
            //show the list of all flight numbers
            WriteLine(coord.getAllFlightNumber() + "\n");
            
            WriteLine("\nPlease enter the flight's Number: ");
            flightNumber = ReadLine().Trim().ToUpper();
            // check flightnumber with the flightNumberRegex
            while (!Regex.IsMatch(flightNumber, flightNumberRegex, RegexOptions.IgnoreCase))
            {
                WriteLine("Invalid flight number, please enter again: ");
                flightNumber = ReadLine().Trim().ToUpper();
            }

            WriteLine(coord.ViewParticularFlight(flightNumber));
            WriteLine(coord.GetCustomerListPerFlight(flightNumber));
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }

        //view all flights
        public static void viewAllFlight()
        {
            Clear();
            WriteLine(coord.ViewAllFlights());
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }

        //--------- Booking ----------


        //make booking
        public static void makeBooking()
        {
            int custID;
            string flightID;
            WriteLine("\n*************** Make Booking ***************\n");
            WriteLine(coord.ViewCustomers() + "\n");
            WriteLine("========================================================================================================\n");
            WriteLine(coord.ViewAllFlights());
            WriteLine("\nPlease enter the customer's ID: ");
            while (!int.TryParse(ReadLine().Trim(), out custID) || !Regex.IsMatch(custID.ToString(), CustomerIdRegex))
            {
                //check with the CustomerIDRegex
                WriteLine("Please enter a vlaid customer's ID: ");
            }

            WriteLine("Please enter the flight's Number: ");
            flightID = ReadLine().Trim().ToUpper();

            //check with the flightNumberRegex
            while (!Regex.IsMatch(flightID, flightNumberRegex, RegexOptions.IgnoreCase))
            {
                WriteLine("Invalid flight number, please enter again: ");
                flightID = ReadLine().Trim().ToUpper();
            }

            if (coord.MakeBooking(flightID, custID))
            {
                WriteLine("Booking made successfully....");

                using (StreamWriter sr = new StreamWriter("../../../files/bookingInfo.txt"))
                {
                    Booking[] allBookings = coord.getBookingList();
                    sr.WriteLine("Number of Bookings: " + coord.getBookingCount());
                    for (int i = 0; i < coord.getBookingCount(); i++)
                    {
                        sr.WriteLine(allBookings[i].getDateOfBooking() +
                        "," + allBookings[i].getBookingNumber() +
                        "," + allBookings[i].getFlightnumber() +
                        "," + allBookings[i].getCustomerId());
                    }
                }

            }
            else
            {
                WriteLine("Booking not made....");
            }
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }


        //cancel booking
        public static void cancelBooking()
        {
            int bookingNumber;
            WriteLine("\n*************** Cancel Booking ***************");
            WriteLine(coord.ViewAllBookings());
            WriteLine("\nPlease enter the booking's Number: ");
            while (!int.TryParse(ReadLine().Trim(), out bookingNumber))
            {
                WriteLine("Please enter a valid booking's Number: ");
            }

            if (coord.CancelBooking(bookingNumber))
            {
                using (StreamWriter sr = new StreamWriter("../../../files/bookingInfo.txt"))
                {
                    Booking[] allBookings = coord.getBookingList();
                    sr.WriteLine("Number of Bookings: " + coord.getBookingCount());
                    for (int i = 0; i < coord.getBookingCount(); i++)
                    {
                        sr.WriteLine(allBookings[i].getDateOfBooking() +
                        "," + allBookings[i].getBookingNumber() +
                        "," + allBookings[i].getFlightnumber() +
                        "," + allBookings[i].getCustomerId());
                    }
                }

                WriteLine("Booking cancelled successfully....");
            }
            else
            {
                WriteLine("Booking not cancelled....");
            }
        }

        //view booking
        public static void viewBooking()
        {
            Clear();
            WriteLine(coord.ViewAllBookings());
            WriteLine("\nPress any key to continue....");
            ReadKey();
        }

        //-------------Validation Methods----------------

        //validating the menus
        public static int MenuValidation(string menuName)
        {
            int choice;
            bool validChoice = int.TryParse(ReadLine().Trim(), out choice);
            while (!validChoice || choice < 1 || choice > menu[menuName].Keys.Count() + 1)
            {
                WriteLine("\aInvalid choice. Please try again.");
                //show to message to the user for some seonds
                Thread.Sleep(1000);
                Clear();

                showMenu(menuName);
                validChoice = int.TryParse(ReadLine().Trim(), out choice);
            }
            return choice;
        }

        //input validation for null or empty
        public static string inputValidation()
        {
            string input = ReadLine().Trim();
            while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                WriteLine("\aInvalid input. Please try again:");
                input = ReadLine().Trim();
            }
            return input;
        }

        //--------------------------other functions -----------------------------

        static void Main(string[] args)
        {
            CustomerManager cm = new CustomerManager(10000);
            FlightManager fm = new FlightManager(10000);
            BookingManager bm = new BookingManager(10000);
            coord = new Coordinator(cm, fm, bm);

            menu = new Dictionary<string, Dictionary<string, Action>>
            {
                {"Main", new Dictionary<string, Action> {
                        {"Customer", viewBooking},
                        {"Flight", viewBooking},
                        {"Booking", viewBooking}
                    }
                },
                {"Customer", new Dictionary<string, Action> {
                        {"Add Customer", addCustomer},
                        {"Remove Customer", removeCustomer},
                        {"View Customers", viewCustomers}
                    }
                },
                {"Flight", new Dictionary<string, Action> {
                        {"Add Flight", addFlight},
                        {"Remove Flight", removeFlight},
                        {"View Flights", viewAllFlight},
                        {"View a Particular Flight", viewParticularFlight}
                    }
                },
                {"Booking", new Dictionary<string, Action> {
                        {"Make Booking", makeBooking},
                        {"View Bookings", viewBooking},
                        {"Cancel Bookings", cancelBooking}
                    }
                }
            };

            //------------- Regex -------------
            //add regex for the phone number input as string
            phoneRegex = @"^(\+1)?\s?\(?([2-9]\d{2})\)?([\s-]?)([2-9]\d{2})([\s-]?)(\d{4})$";

            //add regex for the date input as string
            dateRegex = @"^(((0[13-9]|1[012])[-/]?(0[1-9]|[12][0-9]|30)|(0[13578]|1[02])[-/]?31|02[-/]?(0[1-9]|1[0-9]|2[0-8]))[-/]?[0-9]{4}|02[-/]?29[-/]?([0-9]{2}(([2468][048]|[02468][48])|[13579][26])|([13579][26]|[02468][048]|0[0-9]|1[0-6])00))$";

            //add regex for the time input as string
            timeRegex = @"^(1[0-2]|0?[1-9]):([0-5][0-9]) ?([AP][M])$";

            //add regex for the flight number input as string
            flightNumberRegex = @"^[A-Z?a-z]\d[A-Z?a-z]\d$";

            //add regex for the customer Id input
            CustomerIdRegex = @"^[1-9]\d\d+$";


            //---------run the program-----------
            runProgram();
            WriteLine("Thank you for using this program... Goodbye..");
            
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}
