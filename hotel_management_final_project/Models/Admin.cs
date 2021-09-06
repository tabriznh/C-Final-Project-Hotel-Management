using System;
namespace hotel_management_final_project.Models
{
    public class Admin
    {
        private int _id;
        private string _username;
        private string _name;
        private string _surname;
        private string _password;
        private string _email;
        private bool _visibility = true;

        public int Id { get { return this._id; } set { this._id = value; } }
        public string Username { get { return this._username; } set { this._username = value; } }
        public string Name { get { return this._name; } set { this._name = value; } }
        public string Surname { get { return this._surname; } set { this._surname = value; } }
        public string Password { get { return this._password; } set { this._password = value; } }
        public string Email { get { return this._email; } set { this._email = value; } }
        public bool Visibility { get { return this._visibility; } set { this._visibility = value; } }
    }
}
