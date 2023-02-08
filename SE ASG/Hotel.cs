using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Hotel
    {
        // missing average rating

        // added hotel id from class diagram
        public int hotelID { get; set; }
        public string hotelType { get; set; }

        // can remove available room from class diagram
        public int avaliableRooms { get; set; }
        public bool allowVoucher { get; set; }
        public string hotelAddress { get; set; }

        public List<Room> rooms;

        public Hotel()
        {
            rooms = new List<Room>();
        }

        public void AddRoom(Room room)
        {
            if (!rooms.Contains(room))
            {
                rooms.Add(room);
                room.Hotel = this;
            }
        }

        // Need to add this operation into the class diagram
        public bool displayDetails()
        {
            avaliableRooms = 0;
            foreach (Room r in rooms)
            {
                if (r.availability) { avaliableRooms++; } 
            }

            if (avaliableRooms > 0)
            {
                Console.WriteLine("\nHotel type: " + hotelType + "\nAvailable Rooms: " + avaliableRooms + "\nAllows Vouchers: " + Convert.ToString(allowVoucher) + "\nAddress: " + hotelAddress + "");
                return true;
            }
            else 
            {
                Console.WriteLine("\nThere are currently no rooms available at this hotel");
                return false; 
            }
        }

        // add this into class diagram
        public void displayRooms()
        {
            Console.WriteLine("\n-------- Rooms --------");
            foreach (Room r in rooms)
            {
                Console.WriteLine("\n1) Room Number: "+ r.roomNumber +" Availability: "+ r.availability +"");
            }
        }

        // add this into class diagram
        public Room getRoom(string number)
        {
            int num = Convert.ToInt32(number);
            foreach (Room r in rooms)
            {
                if (num == r.roomNumber)
                {
                    return r;
                }
            }
            return null;
        }
    }
}
