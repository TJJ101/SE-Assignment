using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Guest : User
    {
        private string personalID { get; set; }
        private string email { get; set; }
        private string number { get; set; }
        public double balance { get; set; }
        private string password { get; set; }

        private List<Reservation> reservations;
         
        public Guest()
        {
            reservations = new List<Reservation>();
        }

        public void Login()
        {
            Console.WriteLine("Logged In");
        }

        public void Browse()
        {

        }

        public void ReserveHotel(Reservation r)
        {
            if (!reservations.Contains(r))
            {
                reservations.Add(r);
                r.Guest = this;
            }
        }

        public void CancelReservation()
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

            if (result) { Console.WriteLine("Cancellation Successfull"); }
            else { Console.WriteLine("Cancellation Error"); }

        }
    }
}
