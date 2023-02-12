using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    // Start of code done by Tan Jun Jie S10194152D ------------------------------------------------
    public class Payment
    {
        private double amountPaid;
        private Reservation reservation;
        public double AmountPaid
        {
            get { return amountPaid; }
            set { amountPaid = value; }
        }
        public Reservation Reservation
        {
            get{ return reservation; }
            set{ reservation = value; }
        }
        public Payment() 
        {
            amountPaid = 0;
        }
        public Payment(Reservation r)
        {
            amountPaid = 0;
            reservation = r;
        }
        public void AddVoucher()
        {

        }
    }
    // End of code done by Tan Jun Jie S10194152D ------------------------------------------------
}
