using System;
using System.Security.Cryptography.X509Certificates;

namespace SE_ASG
{
    class Run
    {
        static void Main(string[] args)
        {
            // This file is for the codes to run the main stuff

            // Creating hotels
            List<Hotel> hotels = new List<Hotel>();
            hotels.Add(new Hotel{ hotelType = "luxury", avaliableRooms = 1, allowVoucher = true, hotelAddress = "Q street"});
            hotels.Add(new Hotel { hotelType = "themed", avaliableRooms = 1, allowVoucher = true, hotelAddress = "W street" });
            hotels.Add(new Hotel { hotelType = "family-friendly", avaliableRooms = 1, allowVoucher = true, hotelAddress = "E street" });
            hotels.Add(new Hotel { hotelType = "city", avaliableRooms = 1, allowVoucher = false, hotelAddress = "R street" });
            hotels.Add(new Hotel { hotelType = "budget hotel", avaliableRooms = 1, allowVoucher = false, hotelAddress = "T street" });
    
            System.Console.WriteLine(hotels.Count);
            string answer = Console.ReadLine();
            Console.WriteLine(answer);
        }
    }
}