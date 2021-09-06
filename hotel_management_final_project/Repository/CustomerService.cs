using System;
using System.Collections.Generic;
using hotel_management_final_project.Models;
namespace hotel_management_final_project.Repository

{
    public class CustomerService : IService<Customer>
    {
        RoomService roomService = new RoomService();

        public Customer Create(Customer model)
        {
            Database.Customers.Add(model);
            return model;
        }

        public bool Delete(int id)
        {
            Customer customer = Database.Customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return false;
            }
            Database.Customers.Remove(customer);
            return true;
        }

        public Customer Get(int id)
        {
            return Database.Customers.Find(c => c.Id == id);
        }

        public List<Customer> GetAll()
        {
            return Database.Customers;
        }

        public Customer Update(int id, Customer model)
        {
            Customer customer = Database.Customers.Find(c => c.Id == id);
            customer = model;
            return model;

        }



    }

}
