using System;
using System.Collections.Generic;
using hotel_management_final_project.Models;
namespace hotel_management_final_project.Repository
{
    public class BookingService
    {
        RoomService roomService = new RoomService();
        CustomerService customerService = new CustomerService();
        private List<Booking> Bookings = new List<Booking>();

        public Booking CreateBooking(Customer customer)
        {
            Booking booking = new Booking()
            {
                Id = Bookings.Count + 1,
                Customer = customer,
                CheckInDate = customer.CheckInDate,
                CheckOutDate = customer.CheckOutDate,
                CheckedIn = false,
                Closed = false
            };
            Bookings.Add(booking);
            return booking;
        }

        public bool CloseBooking(int customerId)
        {
            Customer customer1 = customerService.GetAll().Find(c => c.Id == customerId);
            Booking booking1 = GetAll().Find(c => c.Customer == customer1);
            if (booking1 != null)
            {
                booking1.Customer.ActualCheckOutDate = DateTime.Now;
                int actualcheckout = Convert.ToInt32(booking1.Customer.ActualCheckOutDate.Day);
                int enddate = Convert.ToInt32(booking1.Customer.CheckOutDate.Day);
                int startdate = Convert.ToInt32(booking1.Customer.CheckInDate.Day);
                int extradays = actualcheckout - enddate + 1;
                double penalty = extradays * booking1.Customer.Room.Price;
                double finalpricewithpenalty = (Math.Abs(enddate - startdate + 1) * booking1.Customer.Room.Price) + (extradays * booking1.Customer.Room.Price);
                if (actualcheckout - enddate > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Penalty for your late checkout is {penalty} ");
                    Console.WriteLine($"Final price for your stay is {finalpricewithpenalty:C2}");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Final price for Mr/Mrs {booking1.Customer.Name}'s stay is {(enddate - startdate + 1) * booking1.Customer.Room.Price }");
                }

                booking1.Closed = true;
                booking1.Visibility = false;
                booking1.ActualCheckOutDate = DateTime.Now;
                booking1.Penalty = (short)penalty;
                booking1.Room.EndDate = DateTime.Now;
                booking1.Room.Availability = true;
                booking1.Room.OccupyingCustomer = null;
                booking1.Customer.Room = null;
                booking1.Customer.ActualCheckOutDate = DateTime.Now;
                booking1.Customer.Penalty = (short)penalty;
                return true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Check-Out was unsuccessful. Please try again.");
                return false;
            }
        }

        public Booking Get(int id)
        {
            return Bookings.Find(c => c.Id == id);
        }

        public List<Booking> GetAll()
        {
            return Bookings;
        }

        public bool CheckIn(int id)
        {
            Customer customer1 = Database.Customers.Find(s => s.Id == id);
            Booking booking1 = Bookings.Find(c => c.Customer == customer1);
            if (booking1 != null)
            {
                booking1.CheckInDate = DateTime.Now;
                booking1.Customer.CheckInDate = DateTime.Now;
                booking1.Customer.CheckedIn = true;
                booking1.Room.StartDate = DateTime.Now;
                booking1.CheckedIn = true;
                booking1.Customer.CheckedIn = true;
                booking1.Room.Availability = false;
                return true;
            }
            else
            {
                Console.WriteLine("Enter a number that is in the list!");
                return false;
            }
        }

        public Booking Update(int bookingId, Booking model)
        {
            Booking booking2 = Database.Bookings.Find(c => c.Id == bookingId);
            booking2 = model;
            return model;

        }
    }

}
