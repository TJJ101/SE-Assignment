using SE_ASG;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    public class ConfirmedState : IReservationState
    {
        public void CancelReservation(Reservation r)
        {
            DateTime today = DateTime.Now;

            if ((r.checkInDate - today).TotalDays >= 2)
            {
                // refund the amount that guest paid for reservation
                r.Guest.balance += r.pay.AmountPaid;
                r.room.availability = true;
                Console.WriteLine("\nReservation successfully cancelled\n");

                // Text if refund
                if(r.pay.AmountPaid > 0){ Console.WriteLine("$" + r.pay.AmountPaid + " has been refunded to your balance.\n");}
            }
            else { Console.WriteLine("\nCancellation error"); }
        }

        //Payment already paid for Confirmed Reservations
        public void MakePayment(Reservation r) { Console.WriteLine("Reservation already paid."); }
        public void CheckIn(Reservation r)
        {
            //DateTime today = DateTime.Now;

            //for checking against today's date
            DateTime today = DateTime.Parse("2/5/2023 3:00:00 PM");

            //for checking against today's time
            DateTime checkDate = DateTime.Parse(r.checkInDate.ToShortDateString() + " 02:00:00 PM");

            if (r.checkedIn) { Console.WriteLine("Already checked in."); }

            // checks that today is on the same date as check in date and that its after 2pm
            else if (r.checkInDate.Date == today.Date && today.TimeOfDay > checkDate.TimeOfDay)
            {
                r.checkedIn = true;
                Console.WriteLine("\nChecked In!\n");
            }
            else { Console.WriteLine("\nCan't Check in."); }
        }
        public void CheckOut(Reservation r)
        {
            //DateTime today = DateTime.Now;

            //for checking against today's date
            DateTime today = DateTime.Parse("10/5/2023 9:00:00 AM");
            //for checking against today's time
            DateTime checkDate = DateTime.Parse(r.checkInDate.ToShortDateString() + " 12:00:00 PM");

            if (!r.checkedIn) { Console.WriteLine("Can't check out as you haven't checked in yet."); }

            // checks that today is on the same date as check out date and that its before 12pm
            else if (r.checkOutDate == today.Date && today.TimeOfDay < checkDate.TimeOfDay)
            {
                r.checkedOut = true;
                Console.WriteLine("\nChecked Out!\n");
            }
            else { Console.WriteLine("\nCan't check out.\n"); }
        }
    }
}
