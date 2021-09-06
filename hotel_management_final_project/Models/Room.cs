using System;
namespace hotel_management_final_project.Models
{
    public class Room
    {
        private int _id;
        private string _type;
        private Customer _occupyingCustomer;
        private double _price;
        private bool _availability = true;
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _visibility = true;
        

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Type { get { return this._type; } set { this._type = value; } }
        public Customer OccupyingCustomer { get { return this._occupyingCustomer; } set { this._occupyingCustomer = value; } }
        public double Price { get { return this._price; } set { this._price = value; } }
        public bool Availability { get { return this._availability; } set { this._availability = value; } }
        public DateTime StartDate { get { return this._startDate; } set { this._startDate = value; } }
        public DateTime EndDate { get { return this._endDate; } set { this._endDate = value; } }
        public bool Visibility { get { return this._visibility; } set { this._visibility = value; } }
    }
}
