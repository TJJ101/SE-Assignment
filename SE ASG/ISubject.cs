using System;
using System.Collections.Generic;

namespace SE_ASG
{
    public interface ISubject
    {
        public void Attach(IObserver observer);
        public void Detach(IObserver observer);
        public void Notify();
    }
}
