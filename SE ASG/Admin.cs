using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Admin : IUser, IObserver
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

        //Concrete update method to update administrators of review and rating updates. By S10194048D Daryl Chong Teck Yuan
        public void Update(int rating, string review, string hotelid)
        {
            Console.WriteLine("");
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            Console.WriteLine("Administrator's POV:");
            Console.WriteLine("A Customer has left a review for hotelID " + hotelid + ":");
            Console.WriteLine("Rating: " + rating);
            Console.WriteLine("Review: " + review);
            Console.WriteLine("+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
        }

        public Guest Login(List<Guest> guestList) { return null; }

        // I think might also need browse feature (for search and shit)
        public void Browse(List<Hotel> hotels) { }

        // Need to add this to class diagram
        public void ViewBookings() { }

        // Need to add this to class diagram
        public void CancelReservation() { }

        // add this to class diageam
        public void ViewDetails() { }

    }
}
