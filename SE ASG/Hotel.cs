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
    }
}
