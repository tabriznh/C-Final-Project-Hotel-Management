using System;
namespace hotel_management_final_project.Models
{
    public class Booking
    {
        private int _id;
        private Customer _customer;
        private Room _room;
        private bool _checkedin = false;
        private bool _closed = false;
        private DateTime _checkInDate;
        private DateTime _checkOutDate;
        private DateTime _actualCheckoutDate;
        private short _penalty;
        private bool _visibility = true;
            

        public int Id { get { return this._id; } set { this._id = value; } }
        public Customer Customer { get { return this._customer; } set { this._customer = value; } }
        public Room Room { get { return this._room; } set { this._room = value; } }
        public DateTime CheckInDate { get { return this._checkInDate; } set { this._checkInDate = value; } }
        public DateTime CheckOutDate { get { return this._checkOutDate; } set { this._checkOutDate = value; } }
        public DateTime ActualCheckOutDate { get { return this._actualCheckoutDate; } set { this._actualCheckoutDate = value; } }
        public short Penalty { get { return this._penalty; } set { this._penalty = value; } }
        public bool Closed { get { return this._closed; } set { this._closed = value; } }
        public bool CheckedIn { get { return this._checkedin; } set { this._checkedin = value; } }
        public bool Visibility { get { return this._visibility; } set { this._visibility = value; } }
    }
}
