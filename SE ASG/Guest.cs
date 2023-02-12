using SE_Assignment;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    // Start of code done by Darius ---------------------------------------------------------------
    public class Guest : IUser
    {
        public int personalID { get; set; }
        public string email { get; set; }
        public string number { get; set; }
        public double balance { get; set; }
        public string password { get; set; }

        public List<Reservation> reservations;
         
        public Guest() { }
        public Guest(int id, string e, string num, double bal, string pass)
        {
            personalID = id;
            email = e;
            number = num;
            balance = bal;
            password = pass;

            reservations = new List<Reservation>();
        }

        public int Login(string email, string pass, List<IUser> userList)
        {
            Guest returnedGuest = null;
            int id = -1;

            // Search userlist
            for (int i = 0; i < userList.Count(); i++){

                // if user is Guest
                if (userList[i] is Guest)
                {
                    Guest g = (Guest)userList[i];

                    // Check email and password
                    if (email == g.email && pass == g.password) 
                    { 
                        returnedGuest = g; 
                        break; 
                    }
                }
            }
            if (returnedGuest != null) { Console.WriteLine("\nLogged In"); id = returnedGuest.personalID; }
            else { Console.WriteLine("\nEmail or passord is incorrect"); }

            return id;
        }

        // Start of code done by Elgin ---------------------------------------------------
        public List<string> Register(List<IUser> userList)
        {
            bool exist = false;
            int x;
            Console.Write("\nEnter email: ");
            string email = Console.ReadLine();

            Console.Write("\nPersonal ID: ");
            string result = Console.ReadLine();
            while (!Int32.TryParse(result, out x))
            {
                Console.Write("Not a valid number, try again.\nPersonal ID: ");
                result = Console.ReadLine();
            }


            Console.Write("\nEnter Phone Number: ");
            string num = Console.ReadLine();

            Console.Write("\nEnter password: ");
            string password = Console.ReadLine();

            Console.Write("\nRetype passwword: ");
            string password2 = Console.ReadLine();

            for(int i = 0; i < userList.Count(); i++)
            {
                if (userList[i] is Guest)
                {
                    Guest g = (Guest)userList[i];
                    if (email == g.email)
                    {
                        exist = true;
                        break;
                    }
                }
            }

            // Email already exist
            if (exist) { Console.WriteLine("\nEmail exists in the database"); }

            // Password mismatch
            if (password != password2) { Console.WriteLine("\nPassword Mismatch"); }

            // Empty Input
            if (email == "" || num == "" || password == "" || password2 == "") { Console.WriteLine("\nPlease input all the informations required"); }

            // Validate all fields, if pass, create List<string> of newly created guest
            if (!exist && password == password2 && email != "" && num != "" && password != "" && password2 != "")
            {
                List<string> guest = new List<string>() { Convert.ToString(x), email, num, "0", password };
                Console.WriteLine("\nAccount successfully registered");
                return guest;
            }
            return null;
        }
        // End of code done by Elgin ---------------------------------------------------------------

        public void ViewDetails()
        {
            Console.WriteLine("\n--------- Profile ---------");
            Console.WriteLine("Personal ID: "+ personalID +"\nEmail: "+ email +"\nBalance: "+ balance +"");
        }

        public void Browse(List<Hotel> hotels)
        {
            Console.WriteLine("\nAvailable Hotels:");
            foreach (Hotel h in hotels) { Console.WriteLine(h.hotelID + ") " + h.hotelType); }
            Console.Write("\nPlease select a hotel: ");
            string answer = Console.ReadLine();

            foreach (Hotel h in hotels)
            {
                if (answer == Convert.ToString(h.hotelID))
                {
                    h.DisplayDetails();
                    if (h.availableRooms > 0)
                    {
                        Console.Write("\nDo you want to view rooms? (Y/N): ");
                        answer = Console.ReadLine().ToLower();

                        if (answer == "y")
                        {                        
                            Console.WriteLine();
                            h.DisplayRooms();
                            Console.WriteLine("\nPlease select a room: ");
                            answer = Console.ReadLine();
                            Room room = h.GetRoom(answer);

                            // Start of code done by Tan Jun Jie S10194152D ------------------------------------------------
                            if (room != null)
                            {
                                DateTime dateTime;
                                DateTime today = DateTime.Now;
                                DateTime checkIn = today;
                                DateTime checkOut = today;

                                Console.Write("\nSelect a check in date (d/m/yyyy): ");
                                answer = Console.ReadLine();
                                if (DateTime.TryParseExact(answer + " " + today.ToLongTimeString(), "d/M/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                                {
                                    checkIn = DateTime.ParseExact(answer + " " + today.ToLongTimeString(), "d/M/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                }
                                else { Console.WriteLine("\nInvalid Input"); break; }


                                Console.Write("Select a check out date (d/m/yyyy): ");
                                answer = Console.ReadLine();
                                if (DateTime.TryParseExact(answer + " " + today.ToLongTimeString(), "d/M/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                                {
                                    checkOut = DateTime.ParseExact(answer + " " + today.ToLongTimeString(),"d/M/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                }
                                else { Console.WriteLine("\nInvalid Input"); break; }

                                // Check that checkin & checkout date is greater than today and check out date is later than check in
                                if (checkIn.Date > today.Date && checkOut.Date > today.Date && checkOut.Date > checkIn.Date)
                                {
                                    Console.WriteLine("\n--------Reservation Details--------------");
                                    Console.WriteLine("Check-in Date: " + checkIn.ToShortDateString());
                                    Console.WriteLine("Check-out Date: " + checkOut.ToShortDateString());
                                    Console.WriteLine("Days reserved: " + (checkOut - checkIn).TotalDays.ToString());
                                    Console.WriteLine("Current Cost for reservation: $" + (room.roomCost * (checkOut - checkIn).TotalDays));
                                    Console.WriteLine("---------------------------------------------");
                                    Console.Write("Details Correct? (Y/N): ");
                                    answer = Console.ReadLine().ToLower();

                                    if (answer == "y")
                                    {
                                        Reservation newReservation = new Reservation(reservations.Count() + 1, checkIn, checkOut, this, room);
                                        ReserveRoom(newReservation);
                                        room.availability = false;
                                        Console.WriteLine("\nReservation made");

                                        Console.Write("Make Payment? (Y/N): ");
                                        answer = Console.ReadLine().ToLower();
                                        if (answer == "y") { newReservation.MakePayment(newReservation); }
                                        else if(answer == "n") { Console.WriteLine("\nPlease pay within 2 days or reservation will be nulled.\n"); break;  }
                                        else { Console.WriteLine("Invalid Input\n"); break; }
                                    }
                                    // No to Make Payment
                                    else if (answer == "n") { break; }
                                    //Invalid Inpt
                                    else { Console.WriteLine("Invalid Input\n"); break; }
                                }
                                // check in and check out date doesnt fit criteria
                                else { Console.WriteLine("\nError: Unable to create"); }
                            }
                            // End of code done by Tan Jun Jie S10194152D ------------------------------------------------
                            else { Console.WriteLine("\nError: Unable to create"); }
                        }
                        break;
                    }                   
                }
                //else { Console.WriteLine("\nInvalid Input"); }
            }
        }

        public void ReserveRoom(Reservation r)
        {
            if (!reservations.Contains(r))
            {
                reservations.Add(r);
                r.Guest = this;
                r.room.UpdateAvailability(false);
            }
        }

        // Need to add this operation in class diagram
        public void ViewBookings()
        {
            if (reservations.Count > 0)
            {
                Console.WriteLine("\nReservations:\n-----------------------------------------------------------------");
                int index = 1;
                foreach (Reservation r in reservations)
                {
                    if (r.reservationState is CancelledState)
                    {
                        Console.WriteLine("ID: " + r.reservationID + " |Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString() + " (cancelled)");
                    }
                    else 
                    {
                        Console.WriteLine("ID: " + r.reservationID + " |Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
                    }

                }

                Console.Write("-----------------------------------------------------------------\nSelect a Reservation ID to display details: ");
                string answer = Console.ReadLine();
                var isNumeric = int.TryParse(answer, out int n);

                if (isNumeric)
                {

                    foreach (Reservation r in reservations)
                    {
                        if (r.reservationID == n)
                        {
                            r.DisplayDetails();
                        }                       
                    }
                }

            }
            else { Console.WriteLine("\nNo reservations found"); }
        }
    }

    // End of code done by Darius ------------------------------------
}
