using SE_ASG;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    public class ConfirmedState : IReservationState
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
            DateTime today = DateTime.Now;
            DateTime checkDate = DateTime.Parse(r.checkInDate.ToShortDateString() + " 02:00:00 PM");

            if (r.checkedIn) { Console.WriteLine("Already checked in."); }
            else if (r.checkInDate.Date == today.Date && today.TimeOfDay > checkDate.TimeOfDay)
            {
                r.checkedIn = true;
            }
            else { Console.WriteLine("\nCan't Check in."); }
        }
        public void CheckOut(Reservation r)
        {
            DateTime today = DateTime.Now;
            DateTime checkDate = DateTime.Parse(r.checkInDate.ToShortDateString() + " 12:00:00 PM");

            if (!r.checkedIn) { Console.WriteLine("Can't check out as you haven't checked in yet."); }
            else if (r.checkOutDate == today.Date && today.TimeOfDay < checkDate.TimeOfDay)
            {
                r.checkedOut = true;
            }
            else { Console.WriteLine("\nCan't check out.\n"); }
        }
    }
}
