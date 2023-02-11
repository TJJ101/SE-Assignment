using SE_ASG;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SE_Assignment
{
    public class SubmittedState : IReservationState
    {
        public void CancelReservation(Reservation r) { Console.WriteLine("Can't cancel a submitted reservation."); }
        public void MakePayment(Reservation r)
        {
            Console.WriteLine("\n------------Payment-----------");
            Console.WriteLine("Current Balance: $" + r.Guest.balance);
            Console.WriteLine("Payment Amount: $" + r.reservationCost);
            if (r.Guest.balance - r.reservationCost <= 0)
            {
                Console.WriteLine("Currently you have insufficient balance.");
                Console.WriteLine("Please top up and pay within 2 days or your reservation will be nulled.\n");
            }
            else
            {
                Console.WriteLine("Remainder: $" + (r.Guest.balance - r.reservationCost));
                Console.Write("Confirm payment? (Y/N): ");
                string answer = Console.ReadLine().ToLower();
                if (answer == "y")
                {
                    r.pay.AmountPaid = r.reservationCost;
                    r.Guest.balance -= r.reservationCost;
                    r.paymentMade = true;
                    Console.WriteLine("Payment successfully made!");
                }
                else if (answer == "n") { Console.WriteLine("Payment Cancelled"); }
                else { Console.WriteLine("Invalid Input."); }
            }
        }
        public void CheckIn(Reservation r) { Console.WriteLine("Please make payment before checking in."); }
        public void CheckOut(Reservation r) { Console.WriteLine("Please make payment before checking out"); }
    }
}
