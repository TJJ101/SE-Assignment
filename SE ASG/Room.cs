using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Room
    {
        // changed room number to string
        public int roomNumber { get; set; }
        public string roomType { get; set; }
        public double roomCost { get; set; }
        public int maxGuest { get; set; }
        public bool availability { get; set; }
        public List<string> facilities { get; set; }
        
            
        private Hotel hotel;

        public Hotel Hotel
        {
            get { return hotel; }
            set
            {
                if (hotel != value)
                {
                    hotel = value;
                    value.AddRoom(this);
                }
            }
        }

        public Room() { }

        public Room(int num, string type, double cost, int max, bool avail)
        {
        }
    }
}
