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
            // This file is for the codes to run the main stuff
            IUser userInterface = new Guest();
            IUser guest = new Guest();
            Guest guestG = new Guest();
            Hotel hotel = new Hotel();
            string answer;

            // Creating hotels
            List<Hotel> hotels = new List<Hotel>()
            {
            };

            // creating a guest
            List<Guest> guests = new List<Guest>
            {
            };

            // Creating rooms for each hotel
            foreach (Hotel h in hotels)
            {
            }


            // Running of the program
            while (true)
            {
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
                            Console.WriteLine("\nPlease choose an option (1 - 8): \n1) View Profile\n2) Browse  \n3) View Reservations\n4) Cancel Reservations\n5) Make Payment\n6) Check In\n7) Check Out\n8) Logout");
                            Console.WriteLine("--------------------------");
                            answer = Console.ReadLine();

                            // View Profile
                            if (answer == "1") { guest.ViewDetails(); }

                            /// Browse Hotel
                            if (answer == "2") { guest.Browse(hotels); }

                            // View Bookings
                            else if (answer == "3") { guest.ViewBookings(); }

                            // Cancel Reservation
                            else if (answer == "4") 
                            {
                                    }
                                        {
                                        }
                                        {
                            }

                            //Make Payment
                            else if (answer == "5")
                            {
                                    }
                                }

                                if (!check) { Console.WriteLine("\nNo reservations that require payment"); }
                                else
                                {
                                    Console.Write("\nEnter ID to make payment: ");
                                    string choice = Console.ReadLine();
                                    Reservation reservation= null;
                                    foreach(Reservation r in guest.reservations)
                                    {
                                        //return r if id matches, breaks foreach loop
                                        if (choice == r.reservationID.ToString()) { reservation = r; break; }
                                    }
                                    if(reservation != null) { reservation.MakePayment(reservation); }
                                    else { Console.WriteLine("\nInvalid Choice.\n"); }
                                }
                            }
                            else if (answer == "6")
                            {
                                }
                            }
                            else if (answer == "7")
                            {
                                        }

                                    }
                                    if (!check) { Console.WriteLine("\nNo reservations to check out"); }
                                    else
                                    {
                                        Console.Write("Enter ID to Check Out: ");
                                        string choice = Console.ReadLine();
                                        Reservation reservation = null;
                                        foreach (Reservation r in guest.reservations)
                                        {
                                            //return r if id matches, breaks foreach loop
                                            if (choice == r.reservationID.ToString()) { reservation = r; break; }
                                        }

                                        // if user input is valid and corresponds with a reservation, checkOut reservation
                                        if (reservation != null) { reservation.CheckOut(reservation); }
                                        else { Console.WriteLine("\nInvalid Choice.\n"); }
                                    }
                                }
                            }
                            else if (answer == "8") { guest = null; break; }
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