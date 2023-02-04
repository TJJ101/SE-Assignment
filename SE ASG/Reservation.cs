using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Reservation
    {
        private string reservationID;
        private string checkInDate;
        private string checkOutDate;
        private double revervationCost;
        private bool cancelledReservation;


        public void setCancelReservation()
        {
            // prompt for confirmation

            //  get the curent date
            // if check in date - current date >= 2: allow cancel. else dont allow

            cancelledReservation = true;

            // on successful transaction return credits back to customer 
            // display successful/unsuccessful cancel message
        }
    }
}
