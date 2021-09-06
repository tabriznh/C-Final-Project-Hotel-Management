using System;
using System.Collections.Generic;
using hotel_management_final_project.Models;
namespace hotel_management_final_project.Repository
{
    public class AdminService : IService<Admin>
    {
        public Admin Create(Admin model)
        {
            Database.Admins.Add(model);
            return model;
        }

        public bool Delete(int id)
        {
            Admin admin = Database.Admins.Find(c => c.Id == id);
            if (admin == null)
            {
                return false;
            }
            Database.Admins.Remove(admin);
            return true;
        }

        public Admin Get(int id)
        {
            return Database.Admins.Find(c => c.Id == id);
        }

        public List<Admin> GetAll()
        {
            return Database.Admins;
        }

        public Admin Update(int id, Admin model)
        {
            Admin admin = Database.Admins.Find(c => c.Id == id);
            admin = model;
            return model;
        }
    }
}
