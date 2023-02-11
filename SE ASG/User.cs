using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public interface IUser
    {
        Guest Login(List<Guest> guestList);
        void Browse(List<Hotel> hotels);
        void ViewBookings();
        void ViewDetails();

    }
}
