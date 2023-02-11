using System;
using System.Collections.Generic;

namespace SE_ASG
{
    public interface IObserver
    {
        public void Update(int rating, string review, string hotelid);
    }
}
