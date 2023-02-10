using SE_Assignment;
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace SE_ASG
{
    class Run
    {
        static void Main(string[] args)
        {
            string answer;

            // Creating hotels
            List<Hotel> hotels = new List<Hotel>()
            {
                new Hotel(1, "luxury", true, "Q Street"),
                new Hotel(2, "themed", true, "W Street"),
                new Hotel(3, "family-friendly", true, "E Street"),
                new Hotel(4, "city", true, "R Street"),
                new Hotel(5, "themed", true, "T Street")
            };

            // creating a guest
            List<Guest> guests = new List<Guest>
            {
                new Guest(1, "user1", "64323224", 10000, "pass1234"),
                new Guest(2, "user2", "98765432", 500, "pass2345"),
                new Guest(3, "user3", "97654321", 100, "pass3456")
            };

            // Creating rooms for each hotel
            foreach (Hotel h in hotels)
            {
                h.AddRoom(new Room(1, "Big", 20, 3, true));
                h.AddRoom(new Room(2, "Medium", 15, 2, true));
                h.AddRoom(new Room(3, "Small", 10, 1, true));
            }


            // Running of the program
            while (true)
            {
                Console.WriteLine(DateTime.Now.ToString());
                Console.WriteLine("\nWhat do you want to do (1 - 2): \n1) Login  \n2) Register");
                Console.WriteLine("------------------");
                answer = Console.ReadLine();

                if (answer == "1")
                {
                    Guest guest = new Guest();
                    guest = guest.Login(guests);

                    // changed login to return true (need change in class diagram). Might need to change login to return a guest so that we can use it to create resevations and stuff
                    if (guest != null)
                    {
                        while (true)
                        {
                            // probably need to check for user type as well
                            Console.WriteLine("\nPlease choose an option (1 - 5): \n1) Browse  \n2) View Reservations\n3) Cancel Reservations\n4) Make Payment\n5) Check In\n6) Check Out\n7) Logout");
                            Console.WriteLine("--------------------------");
                            answer = Console.ReadLine();
                            
                            /// Browse Hotel
                            if (answer == "1")
                            {
                                guest.Browse(hotels);
                            }

                            // View Bookings
                            else if (answer == "2")
                            {
                                guest.ViewBookings();
                            }

                            // Cancel Reservation
                            else if (answer == "3")
                            {
                                guest.CancelReservation();
                            }

                            //Make Payment
                            else if (answer == "4")
                            {
                                int count = 1;
                                foreach (Reservation r in guest.reservations)
                                {
                                    if (r.reservationState is SubmittedState)
                                    {
                                        if (count == 1) { Console.WriteLine("\n----------------------------Reservations----------------------------"); }
                                        Console.WriteLine("ID: " + r.reservationID + " |Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
                                    }
                                    count++;
                                }

                                if (count == 1) { Console.WriteLine("\nNo reservations that require payment"); }
                                else
                                {
                                    Console.Write("\nEnter ID to make payment: ");
                                    string choice = Console.ReadLine();
                                    Reservation reservation= null;
                                    foreach(Reservation r in guest.reservations)
                                    {
                                        if(choice == r.reservationID.ToString())
                                        {
                                            reservation = r;
                                            break;
                                        }
                                    }
                                    if(reservation != null) { reservation.MakePayment(reservation); }
                                    else { Console.WriteLine("\nInvalid Choice.\n"); }

                                }
                            }
                            else if (answer == "5")
                            {
                                if(guest.reservations.Count == 0) { Console.WriteLine("\nYou currently have no reservations."); }

                                int count = 1;
                                foreach(Reservation r in guest.reservations)
                                {
                                    if(r.reservationState is ConfirmedState)
                                    {
                                        if(count == 1) { Console.WriteLine("\n----------------------------Reservations----------------------------"); }
                                        Console.WriteLine("ID: " + r.reservationID + " |Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
                                    }
                                    count++;
                                }
                                if (count == 1) { Console.WriteLine("\nNo reservations that require payment"); }
                                else
                                {
                                    Console.Write("Enter ID to Check In: ");
                                    string choice = Console.ReadLine();
                                    Reservation reservation = null;
                                    foreach(Reservation r in guest.reservations)
                                    {
                                        if(choice == r.reservationID.ToString())
                                        {
                                            reservation = r; 
                                            break;
                                        }
                                    }
                                    if (reservation != null) { reservation.CheckIn(reservation); }
                                    else { Console.WriteLine("\nInvalid Choice.\n"); }
                                }
                            }
                            else if (answer == "6")
                            {
                                if (guest.reservations.Count == 0) { Console.WriteLine("\nYou currently have no reservations."); }

                                int count = 1;
                                foreach (Reservation r in guest.reservations)
                                {
                                    if (r.reservationState is ConfirmedState)
                                    {
                                        if (count == 1) { Console.WriteLine("\n----------------------------Reservations----------------------------"); }
                                        Console.WriteLine("ID: " + r.reservationID + " |Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
                                    }
                                    count++;
                                }
                                if (count == 1) { Console.WriteLine("\nNo reservations that require payment"); }
                                else
                                {
                                    Console.Write("Enter ID to Check Out: ");
                                    string choice = Console.ReadLine();
                                    Reservation reservation = null;
                                    foreach (Reservation r in guest.reservations)
                                    {
                                        if (choice == r.reservationID.ToString())
                                        {
                                            reservation = r;
                                            break;
                                        }
                                    }
                                    if (reservation != null) { reservation.CheckOut(reservation); }
                                    else { Console.WriteLine("\nInvalid Choice.\n"); }
                                }
                            }
                            else if (answer == "7")
                            {
                                guest = null;
                                break;
                            }

                        }

                    }
                    else { Console.WriteLine("Login Error"); }
                }

                else if (answer == "2")
                {

                }
            }         

        }
    }
}