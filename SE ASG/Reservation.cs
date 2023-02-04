using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Reservation
    {
        public string reservationID { get; set; }
        private DateTime checkInDate { get; set; }
        private DateTime checkOutDate { get; set; }
        private double revervationCost { get; set; }
        private bool cancelledReservation { get; set; }
        private double reservationCost { get; set; }    
        private Guest guest;

        public Guest Guest
        {
            set
            {
                if (guest != value)
                {
                    guest = value;
                    value.makeReservation(this);
                }
            }
        }


        public bool setCancelReservation()
        {
            Console.WriteLine("Are you sure you want to cancel? (y/n)");
            string answer = Console.ReadLine();

            DateTime now = DateTime.Now;

            if (answer == "y" && (checkInDate - now).TotalDays >= 2 && cancelledReservation == false)
            {
                cancelledReservation = true;
                return true;
            }
            else
            {
                return false;
            }

            // on successful transaction return credits back to customer 
            // display successful/unsuccessful cancel message
        }
    }
}
