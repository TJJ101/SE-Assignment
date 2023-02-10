using System;
using SE_ASG;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    public class FulfilledState : IReservationState
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
            Console.WriteLine("\nReservation already fulfilled.\n");
        }
        public void CheckOut(Reservation r)
        {
            Console.WriteLine("\nReservation already fulfilled.\n");
        }
    }
}
