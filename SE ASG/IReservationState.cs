using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    // Start of code done by Tan Jun Jie S10194152D ------------------------------------------------
    public interface IReservationState
    {
        void CancelReservation(Reservation r);
        void MakePayment(Reservation r);
        void CheckIn(Reservation r);
        void CheckOut(Reservation r);
    }

    // End of code done by Tan Jun Jie S10194152D ------------------------------------------------
}
