using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Admin : IUser, IObserver
    {
        private int adminId;
        private string name;
        private string email;
        private string password;

        public int AdminId
        {
            get { return adminId; }
            set { adminId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public Admin() { }

        public Admin(int id, string n, string e, string pass)
        {
            adminId = id;
            name = n;
            email = e;
            password = pass;
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

        public int Login(string email, string pass, List<IUser> guestList) { return -1; }

        public void Browse(List<Hotel> hotels) { }

        public void ViewBookings() { }

        public void CancelReservation() { }

        public void ViewDetails() { }

    }
}
