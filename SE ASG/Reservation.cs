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
        public int reservationID { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public double reservationCost { get; set; }    
        public bool paymentMade { get; set; }

        public bool checkedIn { get; set; }
        public bool checkedOut { get; set; }
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
            paymentMade = false;
            guest = g;
            room = r;

            double days = (cOut - cIn).TotalDays;
            reservationCost = room.roomCost * days;

            reservationState = new SubmittedState();

            pay = new Payment(this);
        }

        // Display Details of Reservation
        public void DisplayDetails()
        {
            string state = ""; string inTxt = ""; string outTxt;
            if(reservationState is SubmittedState) { state = "Submitted\nPlease make payment before check in"; }
            else if (reservationState is ConfirmedState) { state = "Confirmed\nPlease check in on " + checkInDate.ToShortDateString() + " after 2pm"; }
            else if ( reservationState is CancelledState) { state = "Cancelled"; }
            else if ( reservationState is FulfilledState) { state = "Fulfilled"; }
            else if (reservationState is NoShowState) { state = "No Show"; }

            if(checkedIn) { inTxt = " (Checked In)"; } else { inTxt = " (Not Checked In)"; }
            if(checkedOut) { outTxt = " (Checked Out)"; } else { outTxt = " (Not Checked Out)"; }
            Console.WriteLine("\n-------- Details --------");
            Console.WriteLine("Reservation ID: " + reservationID);
            Console.WriteLine("Location: " + room.Hotel.hotelAddress);
            Console.WriteLine("Check in date: " + checkInDate.ToShortDateString() + inTxt);
            Console.WriteLine("Check out date: " + checkOutDate.ToShortDateString() + outTxt);
            Console.WriteLine("Reservation Cost: $" + reservationCost);
            Console.WriteLine("Payment made: "+ paymentMade);
            Console.WriteLine("Status: " + state);
        }

        public void CancelReservation(Reservation r)
        {
            reservationState.CancelReservation(r);
            if(reservationState is ConfirmedState)
            {
                reservationState = new CancelledState();
            }
        }

        public void MakePayment(Reservation r)
        {
            reservationState.MakePayment(r);

            if(reservationState is SubmittedState)
            {
                if(this.pay.AmountPaid == this.reservationCost && this.paymentMade)
                {
                    reservationState = new ConfirmedState();
                }
            }
        }

        public void CheckIn(Reservation r)
        {
            reservationState.CheckIn(r);

            if(reservationState is ConfirmedState)
            {
                if(!checkedIn && DateTime.Now.Date > checkInDate.Date)
                {
                    reservationState = new NoShowState();
                }
            }
        }
        public void CheckOut(Reservation r)
        {
            reservationState.CheckOut(r);

            if (reservationState is ConfirmedState)
            {
                if(checkedIn && checkedOut) { reservationState = new FulfilledState(); }
            }
        }
    }
}
