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
    public class Guest : IUser
    {
        // hvnt put in identification number

        // changed ID to int
        public int personalID { get; set; }
        public string email { get; set; }
        public string number { get; set; }
        public double balance { get; set; }
        public string password { get; set; }

        public List<Reservation> reservations;

        public Guest()
        {
            reservations = new List<Reservation>();
        }


        public Guest Login(List<Guest> guestList)
        {
            Guest reuturnedGuest = null;

            Console.WriteLine("\nEnter email: ");
            string email = Console.ReadLine();

            Console.WriteLine("\nEnter paswword: ");
            string password = Console.ReadLine();

            foreach (Guest g in guestList)
            {
                if (email == g.email && password == g.password)
                {
                    reuturnedGuest = g;
                }
            }

            if (reuturnedGuest != null) { Console.WriteLine("\nLogged In"); }
            else { Console.WriteLine("\nEmail or passord is incorrect"); }

            return reuturnedGuest;
        }

        public List<string> Register(List<Guest> guestList)
        {
            bool Exist = false;
            int X;
            Console.WriteLine("\nEnter email: ");
            string email = Console.ReadLine();

            Console.WriteLine("\nPersonal ID: ");
            string Result = Console.ReadLine();
            while (!Int32.TryParse(Result, out X))
            {
                Console.WriteLine("Not a valid number, try again.\nPersonal ID:");

                Result = Console.ReadLine();
            }


            Console.WriteLine("\nEnter Phone Number: ");
            string num = Console.ReadLine();

            Console.WriteLine("\nEnter password: ");
            string password = Console.ReadLine();

            Console.WriteLine("\nRetype passwword: ");
            string password2 = Console.ReadLine();

            foreach (Guest g in guestList)
            {
                if (email == g.email)
                {
                    Exist = true;
                }
            }

            if (Exist == true)
            {
                Console.WriteLine("\nEmail exists in the database");
            }
            if (password != password2)
            {
                Console.WriteLine("\nPassword Mismatch");
            }
            if (email == "" || num == "" || password == "" || password2 == "")
            {
                Console.WriteLine("\nPlease input all the informations required");
            }
            if (Exist == false && password == password2 && email != "" && num != "" && password != "" && password2 != "")
            {
                List<string> guest = new List<string>() { Convert.ToString(X), email, num, "0", password };
                Console.WriteLine("\nAccount successfully registered");
                return guest;
            }
            return null;
        }

        public void ViewDetails()
        {
            Console.WriteLine("\nUser ID: " + personalID + "\nNumber: " + number + "\nBalance: " + balance + "");
        }


        public void Browse(List<Hotel> hotels)
        {
            Console.WriteLine("\nAvailable Hotels:");
            foreach (Hotel h in hotels) { Console.WriteLine(h.hotelID + ") " + h.hotelType); }
            Console.WriteLine("\nPlease select a hotel: ");
            string answer = Console.ReadLine();

            foreach (Hotel h in hotels)
            {
                if (answer == Convert.ToString(h.hotelID))
                {
                    bool roomsAvailable = h.displayDetails();

                    if (roomsAvailable)
                    {
                        Console.WriteLine("\nDo you want to view rooms? (y/n):");
                        answer = Console.ReadLine();

                        if (answer == "y")
                        {
                            h.displayRooms();

                            Console.WriteLine("\nPlease select a room to view details (enter any character to go back): ");
                            answer = Console.ReadLine();
                            Room room = h.getRoom(answer);
                            room.ViewDetails();

                            Console.WriteLine("\nDo you want to make a booking? (y/n):");
                            answer = Console.ReadLine();

                            if (answer == "y")
                            {
                                if (room != null)
                                {                                    
                                    DateTime dateTime;
                                    DateTime checkIn = DateTime.Now;
                                    DateTime checkOut = DateTime.Now;

                                    Console.WriteLine("\nSelect a check in date (dd/mm/yyyy): ");
                                    answer = Console.ReadLine();
                                    if (DateTime.TryParseExact(answer, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                                    {
                                        checkIn = DateTime.ParseExact(answer, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    }
                                    else { Console.WriteLine("\nInvalid Input"); break; }


                                    Console.WriteLine("\nSelect a check out date (dd/mm/yyyy): ");
                                    answer = Console.ReadLine();
                                    if (DateTime.TryParseExact(answer, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                                    {
                                        checkOut = DateTime.ParseExact(answer, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    }
                                    else { Console.WriteLine("\nInvalid Input"); break; }

                                    if (checkIn != DateTime.Now && checkOut != DateTime.Now)
                                    {
                                        int id = reservations.Count();
                                        Reservation newReservation = new Reservation { reservationID = (id += 1), checkInDate = checkIn, checkOutDate = checkOut, reservationCost = room.roomCost, cancelledReservation = false, room = room, paymentMade = false };
                                        ReserveHotel(newReservation);
                                        room.availability = false;
                                        Console.WriteLine("\nReservation made");
                                    }
                                    else { Console.WriteLine("\nError: Unable to create"); }
                                }
                                else { Console.WriteLine("\nError: Unable to create"); }
                            }                                       
                        }
                        break;
                    }
                }
                //else { Console.WriteLine("\nInvalid Input"); }
            }
        }

        public void ReserveHotel(Reservation r)
        {
            if (!reservations.Contains(r))
            {
                reservations.Add(r);
                r.Guest = this;
            }
        }

        public void ViewBookings()
        {
            if (reservations.Count > 0)
            {
                Console.WriteLine("\nReservations:\n---------------------");
                int index = 1;
                foreach (Reservation r in reservations)
                {
                    if (r.cancelledReservation)
                    {
                        Console.WriteLine(index + ") " + "Reservation ID: " + r.reservationID + " (cancelled)");
                    }
                    else { Console.WriteLine(index + ") " + "Reservation ID: " + r.reservationID); }
                    index++;

                }
                Console.WriteLine("\nSelect a Reservation ID to display details:\n---------------------");

                string answer = Console.ReadLine();
                var isNumeric = int.TryParse(answer, out int n);

                if (isNumeric)
                {

                    foreach (Reservation r in reservations)
                    {
                        if (r.reservationID == n)
                        {
                            r.displayDetails();
                        }
                    }
                }

            }
            else { Console.WriteLine("\nNo reservations found"); }
        }

        public void CancelReservation()
        {
            bool result = false;
            bool cacellationAvailable = false;


            if (reservations.Count > 0)
            {
                Console.WriteLine("\nReservations:\n---------------------");
                int index = 1;

                foreach (Reservation r in reservations)
                {
                    if (!r.cancelledReservation)
                    {
                        Console.WriteLine(index + ") " + "Reservation ID: " + r.reservationID);
                        cacellationAvailable = true;
                    }
                    index++;

                }

                if (cacellationAvailable)
                {
                    Console.WriteLine("\nPlease enter ID of reservation to cancel: ");
                    string answer = Console.ReadLine();

                    foreach (Reservation r in reservations)
                    {
                        if (r.reservationID == Convert.ToInt32(answer))
                        {
                            DateTime now = DateTime.Now;
                            if ((r.checkInDate - now).TotalDays >= 2)
                            {
                                result = r.setCancelReservation();
                            }
                        }
                    }

                    if (result) { Console.WriteLine("\nCancellation Successfull"); }
                    else { Console.WriteLine("\nCancellation Error"); }
                }
                else { Console.WriteLine("\nThere are no reservations"); }

            }
        }

        public void MakePayment()
        {

        }
    }
}
