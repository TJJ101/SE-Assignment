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

        // added hotel id
        public int hotelID { get; set; }
        public string hotelType { get; set; }
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
        public void displayDetails()
        {
            Console.WriteLine("\nHotel type: " + hotelType + "\nAvailable Rooms: " + avaliableRooms + "\nAllows Vouchers: " + Convert.ToString(allowVoucher) + "\nAddress: " + hotelAddress + "");
        }
    }
}
