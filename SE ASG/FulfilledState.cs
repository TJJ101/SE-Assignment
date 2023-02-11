using System;
using SE_ASG;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    public class FulfilledState : IReservationState
    {
        // Fulfilled reservations can't perform any of the operations
        public void CancelReservation(Reservation r) { Console.WriteLine("\nReservation already fulfilled.\n"); }
        public void MakePayment(Reservation r) { Console.WriteLine("\nReservation already paid.\n"); }
        public void CheckIn(Reservation r) { Console.WriteLine("\nReservation already fulfilled.\n"); }
        public void CheckOut(Reservation r) { Console.WriteLine("\nReservation already fulfilled.\n"); }
    }
}
