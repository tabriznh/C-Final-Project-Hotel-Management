using System;
namespace hotel_management_final_project.Models
{
    public class Customer
    {
        private int _id;
        private string _passport;
        private string _name;
        private string _surname;
        private string _gender;
        private Room _room;
        private string _email;
        //private bool _has_A_Booking = false;
        private bool _checkedIn = false;
        private bool _visible;
        private DateTime _checkInDate;
        private DateTime _checkOutDate;
        private DateTime _actualCheckOutDate;
        private short _penalty;
        private bool _visibility = true;


        public int Id { get { return this._id; } set { this._id = value; } }
        public string Passport { get { return this._passport; } set { this._passport = value; } }
        public string Name { get { return this._name; } set { this._name = value; } }
        public string Surname { get { return this._surname; } set { this._surname = value; } }
        public string Gender { get { return this._gender; } set { this._gender = value; } }
        public Room Room { get { return this._room; } set { this._room = value; } }
        public string Email { get { return this._email; } set { this._email = value; } }
        public DateTime CheckInDate { get { return this._checkInDate; } set { this._checkInDate = value; } }
        public DateTime CheckOutDate { get { return this._checkOutDate; } set { this._checkOutDate = value; } }
        public DateTime ActualCheckOutDate { get { return this._actualCheckOutDate; } set { this._actualCheckOutDate = value; } }
        public short Penalty { get { return this._penalty; } set { this._penalty = value; } }
        //public bool Has_A_Booking { get { return this._has_A_Booking; } set { this._has_A_Booking = value; } }
        public bool CheckedIn { get { return this._checkedIn; } set { this._checkedIn = value; } }
        public bool Visible { get { return this._visible; } set { this._visible = value; } }
        public bool Visibility { get { return this._visibility; } set { this._visibility = value; } }

    }
}
