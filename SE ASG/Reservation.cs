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
        // changed the reservation ID to int (need change in class diagram)
        public int reservationID { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public double revervationCost { get; set; }
        public bool cancelledReservation { get; set; }
        public double reservationCost { get; set; }    

        //need to add this payment made into class diagram
        public bool paymentMade { get; set; }

        private Guest guest;

        //need add this into the class diagram
        public Room room;

        public Guest Guest
        {
            set
            {
                if (guest != value)
                {
                    guest = value;
                    value.ReserveHotel(this);
                }
            }
        }

        public bool SetCancelReservation()
        {
            Console.WriteLine("Are you sure you want to cancel? (y/n)");
            string answer = Console.ReadLine();

            DateTime now = DateTime.Now;

            if (answer == "y" && (checkInDate - now).TotalDays >= 2 && cancelledReservation == false)
            {
                cancelledReservation = true;
                guest.balance += reservationCost;
                room.UpdateAvailability(true);
                return true;
            }
            else
            {
                return false;
            }
        }

        // add this into class diagram
        public void DisplayDetails()
        {
            Console.WriteLine("\n-------- Details --------\nRervation ID: "+ reservationID +"\nCheck in date: "+ checkInDate +"\nCheck out date: "+ checkOutDate +"\nIs cancelled: "+ cancelledReservation +"\nPayment made: "+ paymentMade +"");
        }
    }
}
