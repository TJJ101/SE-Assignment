using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public interface IReservationState
    {
        void CancelReservation(Reservation r);
        void MakePayment(Reservation r);
        void CheckIn(Reservation r);
        void CheckOut(Reservation r);
    }
}
