using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_ASG
{
    public class Hotel : ISubject
    {
        // missing average rating

        // added hotel id from class diagram
        public string hotelID { get; set; }
        public string hotelType { get; set; }
        private int rating;
        private string review = "NIL";
        private List<IObserver> observers = new List<IObserver>(); //Creates a list of observers

        // can remove available room from class diagram
        public int avaliableRooms { get; set; }
        public bool allowVoucher { get; set; }
        public string hotelAddress { get; set; }

        public List<Room> rooms;

        public Hotel()
        {
            rooms = new List<Room>();
        }

        public Hotel(string id, string type, bool allowV, string addr)
        {
            hotelID = id;
            hotelType = type;
            allowVoucher = allowV;
            hotelAddress = addr;
            rooms = new List<Room>();
        }

        public void AddRoom(Room room)
        {
            if (!rooms.Contains(room))
            {
                rooms.Add(room);
                room.Hotel = this;
            }

            // calcualte number of available rooms
            foreach(Room r in rooms)
            {
                if(r.availability == true)
                {
                    avaliableRooms += 1;
                }
            }
        }

        // Need to add this operation into the class diagram
        public bool Details()
        {
            avaliableRooms = 0;
            foreach (Room r in rooms)
            {
                if (r.availability) { avaliableRooms++; } 
            }

            if (avaliableRooms > 0)
            {
                Console.WriteLine("\nHotel type: " + hotelType + "\nAvailable Rooms: " + avaliableRooms + "\nAllows Vouchers: " + Convert.ToString(allowVoucher) + "\nAddress: " + hotelAddress + "");
                return true;
            }
            else 
            {
                Console.WriteLine("\nThere are currently no rooms available at this hotel");
                return false; 
            }
        }

        // add this into class diagram
        public void DisplayRooms()
        {
            Console.WriteLine("\n---------------------- Rooms ----------------------");
            string text;
            foreach (Room r in rooms)
            {
                if(r.availability) { text = "Yes"; } else { text = "No"; }
                Console.WriteLine(r.roomNumber + ") | Type: " + r.roomType + "  | Cost: $" + r.roomCost + " | Availability: " + text);
            }
        }

        // add this into class diagram
        public Room getRoom(string number)
        {
            try
            {
                int num = Convert.ToInt32(number);
                foreach (Room r in rooms)
                {
                    if (num == r.roomNumber)
                    {
                        return r;
                    }
                }
            }
            catch 
            {
                //Console.WriteLine("Invalid Number");
                //String cant convert to number
            }
            return null;
        }

        // Start of S10194048D Daryl Chong Teck Yuan's code------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Adds a new observer to the list of observers in runtime
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        //Removes an observer from the list of observers in runtime, not being utilised but leaving it here just in case.
        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        //Updates information of subject class to observers, which in this case is the admin class.
        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(rating, review, hotelID);
            }
        }

        //Method to update ratings to observers.
        public void UpdateRatings(int rating)
        {
            this.rating = rating;
            Notify();
        }

        //Method to update both rating and review to observers
        public void UpdateReview(string review, int rating)
        {
            this.review = review;
            this.rating = rating;
            Notify();
        }
        // End of S10194048D Daryl Chong Teck Yuan's code------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
