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

        public Guest Login(List<Guest> guestList)
        {
            Guest reuturnedGuest = null;

            Console.Write("\nEnter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter paswword: ");
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
                    if (h.avaliableRooms > 0)
                    {
                        Console.Write("\nDo you want to view rooms? (Y/N): ");
                        answer = Console.ReadLine().ToLower();

                        if (answer == "y")
                        {
                            Console.WriteLine();
                            h.DisplayRooms();
                            Console.WriteLine("\nPlease select a room: ");
                            answer = Console.ReadLine();
                            Room room = h.getRoom(answer);

                            if (room != null)
                            {
                                DateTime dateTime;
                                DateTime today = DateTime.Now;
                                DateTime checkIn = today;
                                DateTime checkOut = today;

                                Console.Write("\nSelect a check in date (dd/mm/yyyy): ");
                                answer = Console.ReadLine();
                                if (DateTime.TryParseExact(answer + " " + today.ToLongTimeString(), "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                                {
                                    checkIn = DateTime.ParseExact(answer + " " + today.ToLongTimeString(), "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
                                }
                                else { Console.WriteLine("\nInvalid Input"); break; }


                                Console.Write("Select a check out date (dd/mm/yyyy): ");
                                answer = Console.ReadLine();
                                if (DateTime.TryParseExact(answer + " " + today.ToLongTimeString(), "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                                {
                                    checkOut = DateTime.ParseExact(answer + " " + today.ToLongTimeString(),"dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
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
                                        ReserveHotel(newReservation);
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
                            else { Console.WriteLine("\nError: Unable to create"); }
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

        // Need to add this operation in class diagram
        public void ViewBookings()
        {
            if (reservations.Count > 0)
            {
                Console.WriteLine("\nReservations:\n-----------------------------------------------------------------");
                int index = 1;
                foreach (Reservation r in reservations)
                {
                    if (r.cancelledReservation)
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
}
