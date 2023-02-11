using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public interface IUser
    {
        int Login(string email, string pass, List<IUser> userList);
        void Browse(List<Hotel> hotels);
        void ViewBookings();
        void ViewDetails();

    }
}
