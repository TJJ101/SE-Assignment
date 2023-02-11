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
            string leaveRatingCheck;
            int guestHotelRatingInt;
            string guestHotelRatingString; //Guest's rating when parsed to string.
            string leaveReviewCheck;

            // Creating hotels
            List<Hotel> hotels = new List<Hotel>()
            {
                new Hotel("1", "luxury", true, "Q Street"),
                new Hotel("2", "themed", true, "W Street"),
                new Hotel("3", "family-friendly", true, "E Street"),
                new Hotel("4", "city", true, "R Street"),
                new Hotel("5", "themed", true, "T Street")
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
                            Console.WriteLine("\nPlease choose an option (1 - 8): \n1) View Profile \n2) Browse  \n3) View Reservations\n4) Cancel Reservations\n5) Make Payment\n6) Check In\n7) Check Out\n8) Logout");
                            Console.WriteLine("--------------------------");
                            answer = Console.ReadLine();

                            if (answer == "1") { guest.ViewDetails(); }

                            /// Browse Hotel
                            if (answer == "2") { guest.Browse(hotels); }

                            // View Bookings
                            else if (answer == "3") { guest.ViewBookings(); }

                            // Cancel Reservation
                            else if (answer == "4")
                            {

                                if (guest.reservations.Count == 0) { Console.WriteLine("\nThere are no reservations"); }
                                else
                                {
                                    bool check = false;
                                    foreach (Reservation r in guest.reservations)
                                    {
                                        if (r.reservationState is ConfirmedState)
                                        {
                                            if (!check) { check = true; Console.WriteLine("\n----------------------------Reservations----------------------------"); }
                                            Console.WriteLine("ID: " + r.reservationID + " |Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
                                        }
                                    }

                                    if (!check) { Console.WriteLine("\nNo reservations applicable for cancellation.\n"); }
                                    else
                                    {
                                        Console.Write("\nPlease enter ID of reservation to cancel: ");
                                        answer = Console.ReadLine();
                                        Reservation reservation = null;
                                        foreach (Reservation r in guest.reservations)
                                        {
                                            if (answer == r.reservationID.ToString()) { reservation = r; break; }
                                        }

                                        if (reservation != null)
                                        {
                                            Console.Write("Are you sure you want to cancel? (Y/N): ");
                                            answer = Console.ReadLine().ToLower();

                                            if (answer == "y") { reservation.CancelReservation(reservation); }
                                            else if (answer == "n") { Console.WriteLine("\nCancellation process cancelled.\n"); }
                                            else { Console.WriteLine("\nInvalid Input.\n"); }
                                        }
                                        else { Console.WriteLine("\nInvalid Input\n"); }
                                    }
                                }
                            }

                            //Make Payment
                            else if (answer == "5")
                            {
                                bool check = false;
                                foreach (Reservation r in guest.reservations)
                                {
                                    if (r.reservationState is SubmittedState)
                                    {
                                        if (!check) { check = true; Console.WriteLine("\n----------------------------Reservations----------------------------"); }
                                        Console.WriteLine("ID: " + r.reservationID + " |Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
                                    }
                                }

                                if (!check) { Console.WriteLine("\nNo reservations that require payment"); }
                                else
                                {
                                    Console.Write("\nEnter ID to make payment: ");
                                    string choice = Console.ReadLine();
                                    Reservation reservation = null;
                                    foreach (Reservation r in guest.reservations)
                                    {
                                        //return r if id matches, breaks foreach loop
                                        if (choice == r.reservationID.ToString()) { reservation = r; break; }
                                    }
                                    if (reservation != null) { reservation.MakePayment(reservation); }
                                    else { Console.WriteLine("\nInvalid Choice.\n"); }
                                }
                            }

                            // Check In Reservation
                            else if (answer == "6")
                            {
                                if (guest.reservations.Count == 0) { Console.WriteLine("\nYou currently have no reservations."); }
                                else
                                {
                                    bool check = false;
                                    foreach (Reservation r in guest.reservations)
                                    {
                                        if (r.reservationState is ConfirmedState)
                                        {
                                            //print header only on first finding a reservation that matches
                                            if (!check) { check = true; Console.WriteLine("\n----------------------------Reservations----------------------------"); }
                                            Console.WriteLine("ID: " + r.reservationID + " | Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
                                        }
                                    }
                                    if (!check) { Console.WriteLine("\nNo reservations to check in"); }
                                    else
                                    {
                                        Console.Write("\nEnter ID to Check In: ");
                                        string choice = Console.ReadLine();
                                        Reservation reservation = null;
                                        foreach (Reservation r in guest.reservations)
                                        {
                                            //return r if id matches, breaks foreach loop
                                            if (choice == r.reservationID.ToString()) { reservation = r; break; }
                                        }
                                        // if user input is valid and corresponds with a reservation, checkIn reservation
                                        if (reservation != null) { reservation.CheckIn(reservation); }
                                        else { Console.WriteLine("\nInvalid Choice.\n"); }
                                    }
                                }
                            }

                            // Check out reservation
                            else if (answer == "7")
                            {
                                if (guest.reservations.Count == 0) { Console.WriteLine("\nYou currently have no reservations."); }
                                else
                                {
                                    bool check = false;
                                    foreach (Reservation r in guest.reservations)
                                    {
                                        if (r.reservationState is ConfirmedState)
                                        {
                                            //print header only on first finding a reservation that matches
                                            if (!check) { check = true; Console.WriteLine("\n--------------------------- Reservations ---------------------------"); }
                                            Console.WriteLine("ID: " + r.reservationID + " | Hotel: " + r.room.Hotel.hotelType + " | Room: " + r.room.roomType + " | Cost: $" + r.reservationCost + " | " + r.checkInDate.ToShortDateString() + " - " + r.checkOutDate.ToShortDateString());
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
                                        if (reservation != null) {
                                            reservation.CheckOut(reservation);
                                            if (reservation.checkedOut)
                                            {
                                                // Start of S10194048D Daryl Chong Teck Yuan's code to begin interface to get users to leave a rating and or review------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                                                //Attaching admin as an observer to the hotel object stated above.
                                                reservation.room.Hotel.Attach(new Admin());

                                                Console.WriteLine("We thank you for using BookHoliStay, would you like to leave a rating of the hotel that you stayed in? (y/n)");
                                                leaveRatingCheck = Console.ReadLine();

                                                //Check to see if guest wants to leave a rating, will continue looping if they enter anything other than y or n.
                                                while (!leaveRatingCheck.Equals("y") && !leaveRatingCheck.Equals("n"))
                                                {
                                                    Console.WriteLine("");
                                                    Console.WriteLine("Sorry, you have entered an invalid response, please input y if you would like to leave a rating, or n if you do not wish to leave a rating.");
                                                    leaveRatingCheck = Console.ReadLine();
                                                }
                                                if (leaveRatingCheck == "y")
                                                {
                                                    Console.WriteLine("");
                                                    Console.WriteLine("How much would you rate your stay from 1 to 5?");
                                                    guestHotelRatingString = Console.ReadLine();

                                                    //Will loop until user inputs a number in a range between 1-5.
                                                    while (!guestHotelRatingString.Equals("1") && !guestHotelRatingString.Equals("2") && !guestHotelRatingString.Equals("3") && !guestHotelRatingString.Equals("4") && !guestHotelRatingString.Equals("5"))
                                                    {
                                                        Console.WriteLine("");
                                                        Console.WriteLine("Sorry, you have entered an invalid response, please input a rating between 1-5 thank you.");
                                                        guestHotelRatingString = Console.ReadLine();
                                                    }
                                                    guestHotelRatingInt = int.Parse(guestHotelRatingString);

                                                    //Checking to see if guest have left a valid rating aka 1-5
                                                    if (guestHotelRatingInt != 0)
                                                    {
                                                        Console.WriteLine("");
                                                        Console.WriteLine("Would you like to leave a short comment regarding your stay experience? (y/n)");
                                                        leaveReviewCheck = Console.ReadLine();
                                                        //Check to see if guest wants to leave a rating, will continue looping if they enter anything other than y or n.
                                                        while (!leaveReviewCheck.Equals("y") && !leaveReviewCheck.Equals("n"))
                                                        {
                                                            Console.WriteLine("");
                                                            Console.WriteLine("Sorry, you have entered an invalid response, please input y if you would like to leave a review, or n if you do not wish to leave a rating.");
                                                            leaveReviewCheck = Console.ReadLine();
                                                        }
                                                        if (leaveReviewCheck == "y")
                                                        {
                                                            Console.WriteLine("");
                                                            Console.WriteLine("Please write out a short review of your stay experience be it good or bad.");
                                                            string guestHotelReview = Console.ReadLine();

                                                            Console.WriteLine("");
                                                            Console.WriteLine("Review successfully submitted");
                                                            Console.WriteLine("Thank you for taking the time to leave a rating and a review! Your actions are appreciated by the BookHoliStay team!");
                                                            //Updates the rating and review details to observers.
                                                            reservation.room.Hotel.UpdateReview(guestHotelReview, guestHotelRatingInt);
                                                        }
                                                        if (leaveReviewCheck == "n")
                                                        {
                                                            //Updates the rating details to observers.
                                                            Console.WriteLine("");
                                                            Console.WriteLine("Review successfully submitted");
                                                            Console.WriteLine("Thank you for taking the time to leave a rating! Your actions are appreciated by the BookHoliStay team!");
                                                            reservation.room.Hotel.UpdateRatings(guestHotelRatingInt);
                                                        }
                                                    }
                                                }
                                                //If user decides not to leave a rating.
                                                else if (leaveRatingCheck == "n")
                                                {
                                                    Console.WriteLine("");
                                                    Console.WriteLine("Understood. We hope to see you again!");
                                                }
                                                //End of S10194048D Daryl Chong Teck Yuan's code to begin interface to get users to leave a rating and or review------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                            }
                                        }
                                        else { Console.WriteLine("\nInvalid Choice.\n"); }
                                    }
                                }
                            }

                            // Log out
                            else if (answer == "8") { guest = null; break; }
                        }
                    }
                    else { Console.WriteLine("Login Error"); }
                }

                // Register
                else if (answer == "2")
                {
                    Guest guestG = new Guest();
                    List<string> newguest = new List<string>();
                    newguest = guestG.Register(guests);
                    if (newguest == null) { continue; }
                    else
                    {
                        var test = new Guest { personalID = int.Parse(newguest[0]), email = newguest[1], number = newguest[2], balance = int.Parse(newguest[3]), password = newguest[4] };
                        guests.Add(test);
                    }
                }
            }
        }
    }
}