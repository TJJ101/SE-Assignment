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
        public double revervationCost { get; set; }
        public bool cancelledReservation { get; set; }
        public double reservationCost { get; set; }    

        //need to add this payment made into class diagram
        public bool paymentMade { get; set; }

        public bool checkedIn { get; set; }
        public bool checkedOut { get; set; }
        private Guest guest;
        public Payment pay { get; set; }


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

        {


                {
        }
                {
            }
        }
        public void CheckOut(Reservation r)
        {
            reservationState.CheckOut(r);

            {
        }
    }
}
