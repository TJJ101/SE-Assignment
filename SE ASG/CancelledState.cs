using SE_ASG;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    class CancelledState : IReservationState
    {
        public void CancelReservation(Reservation r)
        {
            
        }
        // Cancelled State can't make payment
        public void MakePayment(Reservation r) { Console.WriteLine("Can't make payment on cancelled reservation."); }

        // Cancelled State can't check in
        public void CheckIn(Reservation r) { Console.WriteLine("Can't check in on a cancelled reservation"); }

        // Cancelled State can't check out
        public void CheckOut(Reservation r) { Console.WriteLine("Can't check out on a cancelled reservation"); }
    }
}
