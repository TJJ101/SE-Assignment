using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Admin : IUser
    {
        private string name;
        private string password;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public void InformHotel()
        {
            // to be implemented
        }

        public void Update(int rating, string review, string id)
        {
            //id is hotelId
            // to be implemented
        }

        public Guest Login(List<Guest> guestList) { return null; }

        // I think might also need browse feature (for search and shit)
        public void Browse(List<Hotel> hotels) { }

        // Need to add this to class diagram
        public void ViewBookings() { }

        // Need to add this to class diagram
        public void CancelReservation() { }

        // add this to class diageam
        public void MakePayment(Reservation r) { }

        public void ViewDetails()
        {
            Console.WriteLine("\nName: " + name +"");
        }
    }
}
