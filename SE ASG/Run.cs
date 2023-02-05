using System;
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
            Hotel hotel = new Hotel();
            string answer;

            // Creating hotels
            List<Hotel> hotels = new List<Hotel>
            {
                new Hotel { hotelID = 1, hotelType = "luxury", avaliableRooms = 1, allowVoucher = true, hotelAddress = "Q street" },
                new Hotel { hotelID = 2, hotelType = "themed", avaliableRooms = 1, allowVoucher = true, hotelAddress = "W street" },
                new Hotel { hotelID = 3, hotelType = "family-friendly", avaliableRooms = 1, allowVoucher = true, hotelAddress = "E street" },
                new Hotel { hotelID = 4, hotelType = "city", avaliableRooms = 1, allowVoucher = false, hotelAddress = "R street" },
                new Hotel { hotelID = 5, hotelType = "budget hotel", avaliableRooms = 1, allowVoucher = false, hotelAddress = "T street" }
            };

            
            while (true){
                Console.WriteLine("\nWhat do you want to do (1 - 2): \n1) Login  \n2) Register");
                answer = Console.ReadLine();

                if (answer == "1")
                {
                    // changed login to return true (need change in class diagram)
                    if (userInterface.Login()){

                        // probably need to check for user type as well
                        Console.WriteLine("\nPlease choose an option (1 - 2): \n1) Browse  \n2) View Bookings");
                        answer = Console.ReadLine();
                        if (answer == "1")
                        {
                            guest.Browse(hotels);
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