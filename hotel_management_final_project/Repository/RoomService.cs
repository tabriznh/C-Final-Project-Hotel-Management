using System;
using System.Collections.Generic;
using hotel_management_final_project.Models;
using System.Linq;
namespace hotel_management_final_project.Repository
{
    public class RoomService : IService<Room>
    {
        public Room Create(Room model)
        {
            Database.Rooms.Add(model);
            return model;
        }

        public bool Delete(int id)
        {
            Room room1 = Database.Rooms.Find(c => c.Id == id);
            if (room1 != null)
            {
                room1.Visibility = false;
                return true;
            }
            
            return false;
        }

        public List<Room> AvailableRooms(DateTime searchCheckindate, DateTime searchCheckoutdate)
        {
            List<Room> Available = new List<Room>();
            List<Room> availableRooms1 = Database.ReservedRooms.FindAll(c => ((searchCheckindate.CompareTo(c.StartDate) < 0 && searchCheckoutdate.CompareTo(c.StartDate) < 0) || (searchCheckindate.CompareTo(c.EndDate) > 0 && searchCheckoutdate.CompareTo(c.EndDate) > 0))  && (c.Visibility == true));
            foreach (var item in Database.Rooms.FindAll(c => c.Visibility == true))
            {
                Available.Add(item);
            }
            foreach (var item in availableRooms1)
            {
                Available.Add(item);
            }
            return Available;
        }

        public bool ReserveARoom(int id)
        {
            Room room1 = GetAll().Find(c => c.Id == id);
            if (room1 != null)
            {
                Database.Rooms.Remove(room1);
                Database.ReservedRooms.Add(room1);
                return true;
            }
            else
            {
                Console.WriteLine("Enter a correct room number");
                return false;
            }
        }

        public bool MakeRoomAvailableAgain(int id)
        {
            Room room1 = Database.ReservedRooms.Find(c => c.Id == id);
            if (room1 != null)
            {
                Database.ReservedRooms.Remove(room1);
                Database.Rooms.Add(room1);
                return true;
            }
            else
            {
                Console.WriteLine("Enter a correct room number");
                return false;
            }
        }

        public Room Get(int id)
        {
            return Database.Rooms.Find(c => c.Id == id);
        }

        public List<Room> GetAll()
        {
            return Database.Rooms;
        }

        public Room Update(int id, Room model)
        {
            Room room2 = Database.Rooms.Find(c => c.Id == id);
            room2 = model;
            return model;
        }
    }
}
