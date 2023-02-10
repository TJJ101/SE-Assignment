using SE_ASG;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    public class NoShowState : IReservationState
    {
        public void CancelReservation()
        {
            
        }
        public void MakePayment(Reservation r)
        {
            Console.WriteLine("Reservation already paid.");
        }
        public void CheckIn(Reservation r)
        {
            Console.WriteLine();
        }
        public void CheckOut(Reservation r)
        {
            Console.WriteLine();
        }
    }
}
