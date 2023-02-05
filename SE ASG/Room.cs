using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Room
    {
        private Hotel hotel;

        public Hotel Hotel
        {
            set
            {
                if (hotel != value)
                {
                    hotel = value;
                    value.AddRoom(this);
                }
            }
        }
    }
}
