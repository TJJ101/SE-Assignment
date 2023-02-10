using SE_ASG;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    class CancelledState : IReservationState
    {
        public void CancelReservation()
        {
            
        }
        public void MakePayment(Reservation r)
        {
            Console.WriteLine("Can't make payment on cancelled reservation.");
        }
        public void CheckIn(Reservation r)
        {

        }
        public void CheckOut(Reservation r)
        {

        }
    }
}
