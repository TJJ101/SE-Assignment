using SE_ASG;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    // Start of code done by Tan Jun Jie S10194152D ------------------------------------------------
    public class NoShowState : IReservationState
    {
        public void CancelReservation(Reservation r) { Console.WriteLine("\nReservation already over. Can't cancel.\n"); }
        public void MakePayment(Reservation r) { Console.WriteLine("\nReservation already paid.\n"); }
        public void CheckIn(Reservation r) { Console.WriteLine("\nReservation pass check in time.\n"); }
        public void CheckOut(Reservation r) { Console.WriteLine("\nCan't check out a no show reservations.\n"); }
    }

    // End of code done by Tan Jun Jie S10194152D ------------------------------------------------
}
