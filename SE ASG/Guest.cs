using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Guest
    {
        private string guestName;
        private List<Reservation> reservations;
         
        public Guest()
        {
            reservations = new List<Reservation>();
        }

        public void makeReservation(Reservation r)
        {
            if (!reservations.Contains(r))
            {
                reservations.Add(r);
                r.Guest = this;
            }
        }

        public void cancelReservation()
        {
            bool result = false;

            foreach (Reservation r in reservations)
            {
                Console.WriteLine(r.reservationID);
            }
            Console.WriteLine("Please enter ID of reservation to cancel: ");
            string answer = Console.ReadLine();

            foreach (Reservation r in reservations)
            {
                if (r.reservationID == answer)
                {
                    result = r.setCancelReservation();
                }
            }

            if (result) { Console.WriteLine("Cancellatio Successfull"); }
            else { Console.WriteLine("Cancellation Error"); }

        }
    }
}
