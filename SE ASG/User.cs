using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public interface IUser
    {
        // might need to return a guest object. Added a list parameter to logins
        Guest Login(List<Guest> guestList);

        // I think might also need browse feature (for search and shit)
        void Browse(List<Hotel> hotels);

        // Need to add this to class diagram
        void ViewBookings();

        // Need to add this to class diagram
        void CancelReservation();

        // add this to class diageam
        void MakePayment();
    }
}
