using SE_ASG;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE_Assignment
{
    public class SubmittedState : IReservationState
    {
        public void CancelReservation()
        {
            
        }
        public void MakePayment(Reservation r)
        {
            Console.WriteLine("\n------------Payment-----------");
            Console.WriteLine("Current Balance: $" + r.Guest.balance);
            Console.WriteLine("Payment Amount: $" + r.reservationCost);
            if (r.Guest.balance - r.reservationCost <= 0)
            {
                Console.WriteLine("Currently you have insufficient balance.\n");
                Console.WriteLine("You have 2 options:");
                Console.WriteLine("1) Do you wish to pay $" + r.Guest.balance + " and pay the rest later");
                Console.WriteLine("2) Pay later");
                Console.WriteLine("-----------------");
                string answer = Console.ReadLine();

                if (answer == "1")
                {
                    Console.Write("Confirm Payment? (Y/N): ");
                    answer = Console.ReadLine().ToLower();
                    if (answer == "y")
                    {
                        r.pay.AmountPaid = r.Guest.balance;
                        r.Guest.balance = 0;
                        Console.WriteLine("\nRemaining payment cost: $" + (r.reservationCost - r.pay.AmountPaid));
                        Console.WriteLine("Please Top up balance and pay the remainder before your check in\n");
                    }
                    else { Console.WriteLine("Exiting payment\n"); }
                }
                else { Console.WriteLine("Exiting payment\n"); }
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
            }
        }
        public void CheckIn(Reservation r)
        {
            Console.WriteLine("Please make payment before checking in.");
        }
        public void CheckOut(Reservation r) 
        {
            Console.WriteLine("Please make payment before checking out");
        }
    }
}
