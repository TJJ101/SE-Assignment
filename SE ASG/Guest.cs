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
                                DateTime checkIn = DateTime.Now;
                                DateTime checkOut = DateTime.Now;
                                DateTime today = DateTime.Now;

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

                                if (checkIn != DateTime.Now && checkOut != DateTime.Now && checkOut.Date > checkIn.Date)
                                {
                                    Console.WriteLine("\n" + checkIn.ToString() + " - " + checkOut.ToString() + "\n");

                                    int id = reservations.Count();
                                    Console.WriteLine("\n\n--------Reservation Details--------------");
                                    Console.WriteLine("Check-in Date: " + checkIn.ToShortDateString());
                                    Console.WriteLine("Check-out Date: " + checkOut.ToShortDateString());
                                    Console.WriteLine("Days reserved: " + (checkOut - checkIn).TotalDays.ToString());
                                    Console.WriteLine("Current Cost for reservation: $" + (room.roomCost * (checkOut - checkIn).TotalDays));
                                    Console.WriteLine("---------------------------------------------");
                                    Console.Write("Details Correct? (Y/N): ");
                                    answer = Console.ReadLine().ToLower();

                                    if (answer == "y")
                                    {
                                        Reservation newReservation = new Reservation(id += 1, checkIn, checkOut, this, room);
                                        ReserveHotel(newReservation);
                                        room.availability = false;

                                        Console.Write("Make Payment? (Y/N): ");
                                        answer = Console.ReadLine().ToLower();
                                        if (answer == "y")
                                        {
                                            newReservation.MakePayment(newReservation);
                                        }
                                        Console.WriteLine("\nReservation made");
                                    }
                                    else if (answer == "n") { break; }
                                    else { Console.WriteLine("Invalid Input"); break; }
                                    
                                }
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

        public void CancelReservation()
        {
            //bool result = false;
            //bool cancellationAvailable = false;


            //if (reservations.Count > 0)
            //{
            //    Console.WriteLine("\nReservations:\n---------------------");
            //    int index = 1;

            //    foreach (Reservation r in reservations)
            //    {
            //        if (!r.cancelledReservation)
            //        {
            //            Console.WriteLine(index + ") " + "Reservation ID: " + r.reservationID);
            //            cancellationAvailable = true;
            //        }
                    
            //    }

            //    if (cancellationAvailable)
            //    {
            //        Console.WriteLine("\nPlease enter ID of reservation to cancel: ");
            //        string answer = Console.ReadLine();

            //        foreach (Reservation r in reservations)
            //        {
            //            if (r.reservationID == Convert.ToInt32(answer))
            //            {
            //                result = r.SetCancelReservation();
            //            }
            //        }

            //        if (result) { Console.WriteLine("\nCancellation Successfull"); }
            //        else { Console.WriteLine("\nCancellation Error"); }
            //    }
            //    else { Console.WriteLine("\nThere are no reservations"); }

            //}
            //else { Console.WriteLine("\nThere are no reservations"); }
        }

        //public void MakePayment(Reservation r)
        //{
        //    Console.WriteLine("\n------------Payment-----------");
        //    Console.WriteLine("Current Balance: $" + balance);
        //    Console.WriteLine("Payment Amount: $" + r.reservationCost);
        //    if (balance - r.reservationCost <= 0)
        //    {
        //        Console.WriteLine("Currently you have insufficient balance.\n");
        //        Console.WriteLine("You have 2 options:");
        //        Console.WriteLine("1) Do you wish to pay $" + balance + " and pay the rest later");
        //        Console.WriteLine("2) Pay later");
        //        Console.WriteLine("-----------------");
        //        string answer = Console.ReadLine();

        //        if (answer == "1")
        //        {
        //            Console.Write("Confirm Payment? (Y/N): ");
        //            answer = Console.ReadLine().ToLower();
        //            if (answer == "y")
        //            {
        //                r.pay.AmountPaid = balance;
        //                balance = 0;
        //                Console.WriteLine("\nRemaining payment cost: $" + (r.reservationCost - r.pay.AmountPaid));
        //                Console.WriteLine("Please Top up balance and pay the remainder before your check in\n");
        //            }
        //            else { Console.WriteLine("Exiting payment\n"); }
        //        }
        //        else { Console.WriteLine("Exiting payment\n"); }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Remainder: $" + (balance - r.reservationCost));
        //        Console.Write("Confirm payment? (Y/N): ");
        //        string answer = Console.ReadLine().ToLower();
        //        if (answer == "y")
        //        {
        //            r.pay.AmountPaid = r.reservationCost;
        //            balance -= r.reservationCost;
        //            r.paymentMade = true;
        //            Console.WriteLine("Payment successfully made!");
        //            r.SetReservationStatus();
        //        }
        //    }
        //}
    }
}
