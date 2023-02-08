using SE_Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Reservation : IReservationState
    {
        // changed the reservation ID to int (need change in class diagram)
        public int reservationID { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public bool cancelledReservation { get; set; }
        public double reservationCost { get; set; }    

        //need to add this payment made into class diagram
        public bool paymentMade { get; set; }

        private Guest guest;
        public Payment pay { get; set; }

        public Room room { get; set; }

        //reservation state
        public IReservationState reservationState { get; set; }

        public Guest Guest
        {
            get { return guest; }
            set
            {
                if (guest != value)
                {
                    guest = value;
                    value.ReserveHotel(this);
                }
            }
        }
        public Reservation() { }
        public Reservation(int id, DateTime cIn, DateTime cOut, Guest g, Room r)
        {
            reservationID = id;
            checkInDate = cIn;
            checkOutDate = cOut;
            cancelledReservation = false;
            paymentMade = false;
            guest = g;
            room = r;

            double days = (cOut - cIn).TotalDays;
            reservationCost = room.roomCost * days;

            reservationState = new SubmittedState();

            pay = new Payment(this);
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
                room.availability = true;
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
            string state = "";
            if(reservationState is SubmittedState) { state = "Submitted\nPlease make payment before check in"; }
            else if (reservationState is ConfirmedState) { state = "Confirmed\nPlease check in on " + checkInDate.ToShortDateString() + " after 2pm"; }
            else if ( reservationState is CancelledState) { state = "Cancelled"; }
            else if ( reservationState is FulfilledState) { state = "Fulfilled"; }
            else if (reservationState is NoShowState) { state = "No Show"; }
            Console.WriteLine("\n-------- Details --------");
            Console.WriteLine("Reservation ID: " + reservationID);
            Console.WriteLine("Location: " + room.Hotel.hotelAddress);
            Console.WriteLine("Check in date: " + checkInDate.ToShortDateString());
            Console.WriteLine("Check out date: " + checkOutDate.ToShortDateString());
            Console.WriteLine("Reservation Cost: $" + reservationCost);
            Console.WriteLine("Is cancelled: " + cancelledReservation);
            Console.WriteLine("Payment made: "+ paymentMade +"");
            Console.WriteLine("Status: " + state);
        }

        public void SetReservationStatus()
        {
            reservationState.SetReservationStatus();

            if(reservationState is SubmittedState)
            {
                // check if payment has been made
                if (paymentMade == true) 
                {
                    reservationState = new ConfirmedState();
                }
            }
        }
    }
}
