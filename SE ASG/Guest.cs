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

            string email = Console.ReadLine();

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

        public void ViewDetails()
        {
            Console.WriteLine("\n------------ Details ------------");
            Console.WriteLine("Personal ID: "+ personalID +"\nEmail: "+ email +"\nBalance: "+ balance +"");
        }

        public void Browse(List<Hotel> hotels)
        {
            Console.WriteLine("\nAvailable Hotels:");
            foreach (Hotel h in hotels) { Console.WriteLine(h.hotelID + ") " + h.hotelType); }
            string answer = Console.ReadLine();

            foreach (Hotel h in hotels)
            {
                if (answer == Convert.ToString(h.hotelID))
                {
                    {

                        if (answer == "y")
                        {
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

                                answer = Console.ReadLine();
                                {
                                }
                                else { Console.WriteLine("\nInvalid Input"); break; }


                                answer = Console.ReadLine();
                                {
                                }
                                else { Console.WriteLine("\nInvalid Input"); break; }

                                    {
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
                int index = 1;
                foreach (Reservation r in reservations)
                {
                    if (r.cancelledReservation)
                    {
                    }
                    else { Console.WriteLine(index + ") " + "Reservation ID: " + r.reservationID); }
                    index++;

                }
                Console.WriteLine("\nSelect a Reservation ID to display details:\n---------------------");

                Console.Write("-----------------------------------------------------------------\nSelect a Reservation ID to display details: ");
                string answer = Console.ReadLine();
                var isNumeric = int.TryParse(answer, out int n);

                if (isNumeric)
                {

                    foreach (Reservation r in reservations)
                    {
                        if (r.reservationID == n)
                        {
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
