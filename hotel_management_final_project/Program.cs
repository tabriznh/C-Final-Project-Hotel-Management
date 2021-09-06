using System;
using System.Collections.Generic;
using System.Linq;
using hotel_management_final_project.Models;
using hotel_management_final_project.Repository;

namespace hotel_management_final_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            Database.DefaultUsers();
            Database.DefaultRooms();
            List<Room> availrooms;
            AdminService adminService = new AdminService();
            CustomerService customerService = new CustomerService();
            RoomService roomService = new RoomService();
            BookingService bookingService = new BookingService();
            byte choice = 255;
            Admin logedInAdmin = new Admin();
            byte loginAttempt = 0;
            Console.WriteLine("Welcome!");
            Console.WriteLine();
            Console.WriteLine("************************************************");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please enter your username");
                string username = Console.ReadLine();
                Console.WriteLine("Please enter your password");
                string password = Console.ReadLine();
                logedInAdmin = adminService.GetAll().Find(c => c.Username == username && c.Password == password);
                if (logedInAdmin == null)
                {
                    loginAttempt++;
                }
                else
                {
                    break;
                }
            } while (loginAttempt < 3);

            if (logedInAdmin != null)
            {
                Console.WriteLine();
                Console.WriteLine($"Hello {logedInAdmin.Name} {logedInAdmin.Surname}. Select which operation you would like to do");
                do
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("1. Show list of available rooms");
                    Console.WriteLine("2. Make a booking");
                    Console.WriteLine("3. Customer booking details");
                    Console.WriteLine("4. Check-in");
                    Console.WriteLine("5. Check-out");
                    Console.WriteLine("6. Update a booking");
                    Console.WriteLine("7. Update Customer Details");
                    Console.WriteLine("8. Modify a room");
                    Console.WriteLine("9. Booking report");
                    Console.WriteLine("0. Exit");
                    choice = (byte)Utility.TryParseForInt();
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Please select the dates for bookings");
                            Console.WriteLine();
                            Console.WriteLine("Please select the check in Day");
                            int checkinDay = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Month");
                            int checkinMonth = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Year");
                            int checkinYear = Utility.TryParseForInt();
                            DateTime checkindate1 = new DateTime(checkinYear, checkinMonth, checkinDay);
                            Console.WriteLine("Please select the check out date");
                            int checkoutDay = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Month");
                            int checkoutMonth = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Year");
                            int checkoutYear = Utility.TryParseForInt();
                            DateTime checkoutdate1 = new DateTime(checkoutYear, checkoutMonth, checkoutDay);
                            if (Utility.CheckoutDateIsNotEarlier(checkindate1, checkoutdate1) == true)
                            {
                                availrooms = roomService.AvailableRooms(checkindate1, checkoutdate1);
                                
                                if (availrooms.Count > 0)
                                {
                                    foreach (var item in availrooms)
                                    {
                                        Console.WriteLine($"Room Number: {item.Id}, Room Type: {item.Type}, Room Price: {item.Price:C2}");
                                    }
                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine("Please select the dates for booking");
                            Console.WriteLine();
                            Console.WriteLine("Please select the check in Day");
                            int checkinDay1 = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Month");
                            int checkinMonth1 = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Year");
                            int checkinYear1 = Utility.TryParseForInt();
                            DateTime checkindate2 = new DateTime(checkinYear1, checkinMonth1, checkinDay1);
                            Console.WriteLine("Please select the check out date");
                            int checkoutDay1 = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Month");
                            int checkoutMonth1 = Utility.TryParseForInt();
                            Console.WriteLine("Please select the check in Year");
                            int checkoutYear1 = Utility.TryParseForInt();
                            DateTime checkoutdate2 = new DateTime(checkoutYear1, checkoutMonth1, checkoutDay1);
                            if (Utility.CheckoutDateIsNotEarlier(checkindate2, checkoutdate2) == true)
                            {
                                List<Room> availrooms1 = roomService.AvailableRooms(checkindate2, checkoutdate2);

                                if (availrooms1.Count > 0)
                                {
                                    Console.WriteLine("Please enter the passport number of a customer");
                                    string custPassport = Console.ReadLine();
                                    Console.WriteLine("Please enter the name of a customer");
                                    string custName = Console.ReadLine();
                                    Console.WriteLine("Please enter the surname of a customer");
                                    string custSurname = Console.ReadLine();
                                    Console.WriteLine("Please enter the gender of a customer");
                                    string custGender = Console.ReadLine();
                                    Console.WriteLine("Please enter the email of a customer");
                                    string custEmail = Console.ReadLine();
                                    Console.WriteLine("Please enter a room type (Single, Double, Twin, Deluxe, Deluxe King, Executive)");
                                    string roomtype = Console.ReadLine();
                                    Room newroom = roomService.GetAll().Find(c => string.Equals(c.Type, roomtype, StringComparison.OrdinalIgnoreCase));
                                    if (newroom != null)
                                    {
                                        Customer customer = new Customer()
                                        {
                                            Id = customerService.GetAll().Count + 1,
                                            Passport = custPassport,
                                            Name = custName,
                                            Surname = custSurname,
                                            Gender = custGender,
                                            Email = custEmail,
                                            CheckInDate = checkindate2,
                                            CheckOutDate = checkoutdate2,
                                            Room = newroom
                                        };
                                        customerService.Create(customer);
                                        newroom.StartDate = checkindate2;
                                        newroom.EndDate = checkoutdate2;
                                        newroom.OccupyingCustomer = customer;
                                        Booking newbooking = bookingService.CreateBooking(customer);
                                        newbooking.Id = bookingService.GetAll().Count + 1;
                                        newbooking.Customer = customer;
                                        newbooking.CheckInDate = customer.CheckInDate;
                                        newbooking.CheckOutDate = customer.CheckOutDate;
                                        newbooking.Room = newroom;
                                        roomService.ReserveARoom(newroom.Id);
                                        Console.WriteLine();
                                        Console.WriteLine();
                                        Console.WriteLine("Congratulations! Your booking was successful!");
                                        Console.WriteLine();
                                        Console.WriteLine($"Room number {newroom.Id} has been booked for {custName} {custSurname}, for the dates {checkindate2.ToString("dd/MMMM/yyyy")} - {checkoutdate2.ToString("dd/MMMM/yyyy")}. Room Type: {newroom.Type}, The price per night for the booking is {newroom.Price:C2}");
                                        Console.WriteLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Enter a correct room type!");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Unfortunately there are no rooms available");
                            }
                            break;
                        case 3:
                            if (bookingService.GetAll().Count > 0)
                            {
                                Console.WriteLine("Please type a passport number to search the booking");
                                string passport = Console.ReadLine().Replace(" ", "");
                                List<Booking> foundbookings = bookingService.GetAll().FindAll(c => string.Equals(c.Customer.Passport, passport, StringComparison.OrdinalIgnoreCase) && c.Closed == false && c.Visibility == true);
                                if (foundbookings.Count > 0)
                                {
                                    foreach (var item in foundbookings)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine($"Name: {item.Customer.Name}, Surname: {item.Customer.Surname}, Room Number: {item.Customer.Room.Id}, Room Type: {item.Customer.Room.Type}, Price per night: {item.Customer.Room.Price:C2}, Check-In Date: {item.Customer.CheckInDate.ToString("dd/MMMM/yyyy")}, Check-Out Date: {item.Customer.CheckOutDate.ToString("dd/MMMM/yyyy")}, Is checked in: {item.Customer.CheckedIn} ");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Unfortunately no one found according to your search");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Booking list is empty!");
                            }
                            break;
                        case 4:
                            List<Booking> foundbookings1 = bookingService.GetAll().FindAll(c => c.CheckedIn == false && c.Visibility == true);
                            if (bookingService.GetAll().Count > 0 && foundbookings1.Count > 0)
                            {
                                Console.WriteLine("Please select which customer you would like to check in");
                                Console.WriteLine();
                                foreach (var item in foundbookings1)
                                {
                                    Console.WriteLine($"{item.Customer.Id}. Name: {item.Customer.Name}, Surname: {item.Customer.Surname}, Room Type: {item.Customer.Room.Type}, Price: {item.Customer.Room.Price:C2}, Check-In Date: {item.Customer.CheckInDate.ToString("dd/MMMM/yyyy")}, Check-Out Date: {item.Customer.CheckOutDate.ToString("dd/MMMM/yyyy")}, Is checked in: {item.Customer.CheckedIn} ");
                                    int choice1 = Utility.TryParseForInt();
                                    if (bookingService.CheckIn(choice1) == true)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Check-In was successful!");
                                        Console.WriteLine();
                                        Console.WriteLine("Here are the details of the booking");
                                        Console.WriteLine();
                                        Customer customer1 = customerService.GetAll().Find(s => s.Id == choice1);
                                        Booking booking1 = bookingService.GetAll().Find(c => c.Customer == customer1);
                                        Console.WriteLine($"Name: {booking1.Customer.Name}, Surname: {booking1.Customer.Surname}, Room Number: {booking1.Room.Id}, Room Type: {booking1.Room.Type}, Price per night: {booking1.Room.Price:C2}, Arrival Dates: {booking1.Customer.CheckInDate} - {booking1.Customer.CheckOutDate.ToString("dd.MM.yyyy")}");
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Check-In was unsuccessful! Please try again");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Bookings list is empty!");
                            }

                            break;
                        case 5:
                            if (bookingService.GetAll().Count > 0)
                            {
                                List<Booking> checkedInCustomers = bookingService.GetAll().FindAll(c => c.CheckedIn == true && c.Visibility == true && c.Closed == false);
                                if (checkedInCustomers.Count > 0)
                                {
                                    Console.WriteLine("Please select which customer you would like to check out");
                                    Console.WriteLine();
                                    foreach (var item in checkedInCustomers)
                                    {
                                        Console.WriteLine($"{item.Customer.Id}. Name: {item.Customer.Name}, Surname: {item.Customer.Surname}, Room Type: {item.Customer.Room.Type}, Price per night: {item.Customer.Room.Price:C2}, Check-In Date: {item.Customer.CheckInDate.ToString("dd/MMMM/yyyy")}, Check-Out Date: {item.Customer.CheckOutDate.ToString("dd/MMMM/yyyy")}, Is checked in: {item.Customer.CheckedIn} ");
                                    }
                                    int choice2 = Utility.TryParseForInt();

                                    Customer customer2 = customerService.GetAll().Find(c => c.Id == choice2);
                                    bookingService.CloseBooking(customer2.Id);
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("No one has checked in yet!");
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Booking list is empty");
                            }
                            break;
                        case 6:
                            if (bookingService.GetAll().Count > 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Please select which booking you would like to update");
                                List<Booking> activebookings = bookingService.GetAll().FindAll(c => c.Visibility == true && c.Closed == false);
                                foreach (var booking in activebookings)
                                {
                                    Console.WriteLine($"{booking.Customer.Id}. Name: {booking.Customer.Name}, Surname: {booking.Customer.Surname}, Room Number: {booking.Room.Id}, Room Type: {booking.Room.Type}, Check-In Date: {booking.CheckInDate.ToString("dd/MMMM/yyyy")}, Check-out Date: {booking.CheckOutDate.ToString("dd/MMMM/yyyy")}");
                                }
                                int choice3 = Utility.TryParseForInt();
                                Customer customer5 = customerService.Get(choice3);
                                Booking booking1 = bookingService.GetAll().Find(c => c.Customer == customer5);
                                if (booking1 != null)
                                {
                                    Booking updbooking = bookingService.Get(booking1.Id);
                                    Console.WriteLine($"Please select what would you like to update about this booking");
                                    Console.WriteLine("1. Room Type");
                                    Console.WriteLine("2. Check-In Date");
                                    Console.WriteLine("3. Check-Out Date");
                                    Console.WriteLine("4. Cancel booking");
                                    int choice4 = Utility.TryParseForInt();
                                    switch (choice4)
                                    {
                                        case 1:
                                            Console.WriteLine();
                                            Console.WriteLine("Please enter a room type (Single, Double, Twin, Deluxe, Deluxe King, Executive)");
                                            string roomtype = Console.ReadLine();
                                            Room newroom1 = roomService.GetAll().Find(c => string.Equals(c.Type, roomtype, StringComparison.OrdinalIgnoreCase));
                                            if (newroom1 != null)
                                            {
                                                updbooking.Room = newroom1;
                                                updbooking.Customer.Room = newroom1;
                                                newroom1.OccupyingCustomer = updbooking.Customer;
                                                newroom1.StartDate = updbooking.Customer.CheckInDate;
                                                newroom1.EndDate = updbooking.Customer.CheckOutDate;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enter a correct room type");
                                            }
                                            Console.WriteLine("Room type has been successfully changed");
                                            Console.WriteLine();
                                            Console.WriteLine($"The new room number is {newroom1.Id}");
                                            break;
                                        case 2:
                                            Console.WriteLine();
                                            Console.WriteLine("Please select a new check in Day");
                                            int updcheckinDay = Utility.TryParseForInt();
                                            Console.WriteLine("Please select a new check in Month");
                                            int updcheckinMonth = Utility.TryParseForInt();
                                            Console.WriteLine("Please select a new check in Year");
                                            int updcheckinYear = Utility.TryParseForInt();
                                            DateTime updcheckindate = new DateTime(updcheckinYear, updcheckinMonth, updcheckinDay);
                                            updbooking.CheckInDate = updcheckindate;
                                            updbooking.Customer.CheckInDate = updcheckindate;
                                            updbooking.Room.StartDate = updcheckindate;
                                            Console.WriteLine("Check-In date has been successfully changed");
                                            Console.WriteLine($"New Check-In date for Mr/Mrs {updbooking.Customer.Name} for the room {updbooking.Room.Id} is {updcheckindate.ToString("dd.MMMM.yyyy")}");
                                            break;
                                        case 3:
                                            Console.WriteLine();
                                            Console.WriteLine("Please select a new check out Day");
                                            int updcheckoutDay = Utility.TryParseForInt();
                                            Console.WriteLine("Please select a new check out Month");
                                            int updcheckoutMonth = Utility.TryParseForInt();
                                            Console.WriteLine("Please select a new check out Year");
                                            int updcheckoutYear = Utility.TryParseForInt();
                                            DateTime updcheckoutdate = new DateTime(updcheckoutYear, updcheckoutMonth, updcheckoutDay);
                                            updbooking.CheckOutDate = updcheckoutdate;
                                            updbooking.Customer.CheckOutDate = updcheckoutdate;
                                            updbooking.Room.EndDate = updcheckoutdate;
                                            Console.WriteLine("Check-Out date has been successfully changed");
                                            Console.WriteLine($"New Check-Out date for Mr/Mrs {updbooking.Customer.Name} for the room {updbooking.Room.Id} is {updcheckoutdate.ToString("dd.MMMM.yyyy")}");
                                            break;
                                        case 4:
                                            updbooking.Visibility = false;
                                            updbooking.CheckInDate = default;
                                            updbooking.CheckOutDate = default;
                                            updbooking.Room.StartDate = default;
                                            updbooking.Room.EndDate = default;
                                            updbooking.Closed = true;
                                            Console.WriteLine("The booking has been successfully cancelled");
                                            break;
                                        default:
                                            Console.WriteLine();
                                            Console.WriteLine("You can only enter a number that's in the list!");
                                            break;

                                    }
                                }

                                else
                                {
                                    Console.WriteLine("Booking was not found");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Booking list is empty");
                            }
                            break;
                        case 7:
                            if (customerService.GetAll().Count > 0)
                            {

                                List<Customer> activecustomers = customerService.GetAll().FindAll(c => c.Visibility == true && c.CheckedIn == true);
                                if (activecustomers.Count > 0)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Please select which customer you would like to update");
                                    foreach (var customer in activecustomers)
                                    {
                                        Console.WriteLine($"{customer.Id}. Name: {customer.Name}, Surname: {customer.Surname}, Room Number: {customer.Room.Id}, Room Type: {customer.Room.Type}, Check-In date: {customer.CheckInDate.ToString("dd.MMMM.yyyy")}, Check-Out Date: {customer.CheckOutDate.ToString("dd.MMMM.yyyy")}");
                                    }
                                    int choice6 = Utility.TryParseForInt();
                                    Customer updcustomer = customerService.Get(choice6);
                                    if (updcustomer != null)
                                    {
                                        Console.WriteLine($"Please select what would you like to update about this this customer");
                                        Console.WriteLine("1. Room");
                                        Console.WriteLine("2. Check-In Date");
                                        Console.WriteLine("3. Check-Out Date");
                                        Console.WriteLine("4. Delete customer");
                                        int choice7 = Utility.TryParseForInt();
                                        switch (choice7)
                                        {
                                            case 1:
                                                Console.WriteLine();
                                                Console.WriteLine("Please enter a room type (Single, Double, Twin, Deluxe, Deluxe King, Executive)");
                                                string roomtype = Console.ReadLine();
                                                Room newroom2 = roomService.GetAll().Find(c => string.Equals(c.Type, roomtype, StringComparison.OrdinalIgnoreCase));
                                                if (newroom2 != null)
                                                {
                                                    updcustomer.Room = newroom2;
                                                    updcustomer.Room = newroom2;
                                                    newroom2.OccupyingCustomer = updcustomer;
                                                    newroom2.StartDate = updcustomer.CheckInDate;
                                                    newroom2.EndDate = updcustomer.CheckOutDate;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Enter a correct room type");
                                                }
                                                Console.WriteLine("Room type has been successfully changed");
                                                Console.WriteLine();
                                                Console.WriteLine($"The new room number is {newroom2.Id}");
                                                break;
                                            case 2:
                                                Console.WriteLine();
                                                Console.WriteLine("Please select a new check in Day");
                                                int custupdcheckinDay = Utility.TryParseForInt();
                                                Console.WriteLine("Please select a new check in Month");
                                                int custupdcheckinMonth = Utility.TryParseForInt();
                                                Console.WriteLine("Please select a new check in Year");
                                                int custupdcheckinYear = Utility.TryParseForInt();
                                                DateTime custupdcheckindate = new DateTime(custupdcheckinYear, custupdcheckinMonth, custupdcheckinDay);
                                                updcustomer.CheckInDate = custupdcheckindate;
                                                updcustomer.CheckInDate = custupdcheckindate;
                                                updcustomer.Room.StartDate = custupdcheckindate;
                                                Console.WriteLine("Check-In date has been successfully changed");
                                                Console.WriteLine($"New Check-In date for Mr/Mrs {updcustomer.Name} for the room {updcustomer.Room.Id} is {custupdcheckindate.ToString("dd.MMMM.yyyy")}");
                                                break;
                                            case 3:
                                                Console.WriteLine();
                                                Console.WriteLine("Please select a new check out Day");
                                                int custupdcheckoutDay = Utility.TryParseForInt();
                                                Console.WriteLine("Please select a new check out Month");
                                                int custupdcheckoutMonth = Utility.TryParseForInt();
                                                Console.WriteLine("Please select a new check out Year");
                                                int custupdcheckoutYear = Utility.TryParseForInt();
                                                DateTime custupdcheckoutdate = new DateTime(custupdcheckoutYear, custupdcheckoutMonth, custupdcheckoutDay);
                                                updcustomer.CheckOutDate = custupdcheckoutdate;
                                                updcustomer.CheckOutDate = custupdcheckoutdate;
                                                updcustomer.Room.EndDate = custupdcheckoutdate;
                                                Console.WriteLine("Check-Out date has been successfully changed");
                                                Console.WriteLine($"New Check-Out date for Mr/Mrs {updcustomer.Name} for the room {updcustomer.Room.Id} is {custupdcheckoutdate.ToString("dd.MMMM.yyyy")}");
                                                break;
                                            case 4:
                                                updcustomer.Visibility = false;
                                                updcustomer.CheckInDate = default;
                                                updcustomer.CheckOutDate = default;
                                                updcustomer.Room.StartDate = default;
                                                updcustomer.Room.EndDate = default;
                                                Console.WriteLine("The booking has been successfully cancelled");
                                                break;
                                            default:
                                                Console.WriteLine();
                                                Console.WriteLine("You can only enter a number that's in the list!");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("No customer found");


                                    }

                                }
                                else
                                {
                                    Console.WriteLine("There are no checked-in customers. To change the booking details go to option 6.");
                                }

                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Customer list is empty");
                            }

                            break;
                        case 8:
                            Console.WriteLine();
                            Console.WriteLine("Please select which operation you would like to do:");
                            Console.WriteLine("1. Add a new room into database");
                            Console.WriteLine("2. Update room details");
                            Console.WriteLine("3. Delete a room");
                            int choice8 = Utility.TryParseForInt();
                            switch (choice8)
                            {
                                case 1:
                                    Console.WriteLine();
                                    Console.WriteLine("Please enter the number of a new room");
                                    int newid = Utility.TryParseForInt();
                                    Console.WriteLine("Please enter the type of a new room (Single, Double, Twin, Deluxe, Deluxe King, Executive)");
                                    string newtype = Console.ReadLine();
                                    Console.WriteLine("Please enter the price of a new room");
                                    int newprice = Utility.TryParseForInt();
                                    Room newroom = new Room() { Id = newid, Type = newtype, Price = newprice };
                                    roomService.Create(newroom);
                                    break;
                                case 2:
                                    List<Room> availablerooms = roomService.GetAll().FindAll(c => c.Availability == true);
                                    if (availablerooms.Count > 0)
                                    {
                                        Console.WriteLine("Please select a room you would like to update");
                                        foreach (var room in availablerooms)
                                        {
                                            Console.WriteLine($"Room Number: {room.Id}, Room Type: {room.Type}, Room Price: {room.Price:C2}");
                                        }
                                        int choice9 = Utility.TryParseForInt();
                                        Console.WriteLine();
                                        Console.WriteLine("Please enter the new type (Single, Double, Twin, Deluxe, Deluxe King, Executive)");
                                        string newtype1 = Console.ReadLine();
                                        Console.WriteLine("Please enter the new price");
                                        int newprice1 = Utility.TryParseForInt();
                                        Room newroom1 = new Room() { Type = newtype1, Price = newprice1 };
                                        roomService.Update(choice9, newroom1);
                                        if (roomService.Update(choice9, newroom1) != null)
                                        {
                                            Console.WriteLine($"The room number {newroom1.Id} has been successfully updated");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to update a room. Please try again.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("All rooms are occupied now. You cannot update the room while there is a customer inside.");
                                    }
                                    break;
                                case 3:
                                    List<Room> availablerooms2 = roomService.GetAll().FindAll(c => c.Availability == true);
                                    if (availablerooms2.Count > 0)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Please enter a room number you would like to remove");
                                        int choicee = Utility.TryParseForInt();
                                        roomService.Delete(choicee);
                                        if (roomService.Delete(choicee) == true)
                                        {
                                            Console.WriteLine("The room has been successfully removed");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to remove the room. Please try again.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("All rooms are occupied now. You cannot delete the room while there is a customer inside.");
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 9:
                            Console.WriteLine();
                            Console.WriteLine("Please select which report you would like to get)");
                            Console.WriteLine();
                            Console.WriteLine("1. Customer report");
                            Console.WriteLine("2. Report by date");
                            Console.WriteLine("3. Rooms report");
                            int choicee1 = Utility.TryParseForInt();
                            switch (choicee1)
                            {
                                case 1:
                                    Console.WriteLine("Please type a passport number to search the booking");
                                    string passport1 = Console.ReadLine().Replace(" ", "");
                                    List<Customer> foundcustomers = customerService.GetAll().FindAll(c => string.Equals(c.Passport, passport1, StringComparison.OrdinalIgnoreCase));
                                    if (foundcustomers.Count > 0)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Here is full history of the customer");
                                        foreach (var customer in foundcustomers)
                                        {
                                            Console.WriteLine($"Name: {customer.Name}, Surname: {customer.Surname}, Room Number: {customer.Room.Id}, Dates of Arrival: {customer.CheckInDate} - {customer.CheckOutDate}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("No history found according to your search");
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("Please select the dates for bookings");
                                    Console.WriteLine();
                                    Console.WriteLine("Please select the check in Day");
                                    int searchstartdateday = Utility.TryParseForInt();
                                    Console.WriteLine("Please select the check in Month");
                                    int searhstartdatemonth = Utility.TryParseForInt();
                                    Console.WriteLine("Please select the check in Year");
                                    int searchstartdateyear = Utility.TryParseForInt();
                                    DateTime searchstartdate = new DateTime(searchstartdateyear, searhstartdatemonth, searchstartdateday);
                                    Console.WriteLine("Please select the check out date");
                                    int searchenddateday = Utility.TryParseForInt();
                                    Console.WriteLine("Please select the check in Month");
                                    int searchenddatemonth = Utility.TryParseForInt();
                                    Console.WriteLine("Please select the check in Year");
                                    int serachenddateyear = Utility.TryParseForInt();
                                    DateTime searchenddate = new DateTime(serachenddateyear, searchenddatemonth, searchenddateday);
                                    if (Utility.CheckoutDateIsNotEarlier(searchstartdate, searchenddate) == true)
                                    {
                                        List<Booking> bookingreport = bookingService.GetAll().FindAll(c => searchstartdate.CompareTo(c.CheckInDate)! < 0 && searchenddate.CompareTo(c.CheckOutDate)! > 0);
                                        if (bookingreport.Count > 0)
                                        {
                                            foreach (var booking in bookingreport)
                                            {
                                                Console.WriteLine($"{booking.Id}. Room Number: {booking.Room.Id}, Customer: {booking.Customer.Name} {booking.Customer.Surname}, Check-In Date: {booking.CheckInDate}, Check-Out Date: {booking.CheckOutDate}.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Nothing found according to your search.");
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine();
                                    Console.WriteLine("Please enter a room number");
                                    int roomnum = Utility.TryParseForInt();
                                    Room room12 = roomService.Get(roomnum);
                                    if (room12 != null)
                                    {
                                        List<Booking> bookingbyroom = bookingService.GetAll().FindAll(c => c.Room == room12);
                                        if (bookingbyroom.Count > 0)
                                        {
                                            Console.WriteLine($"Here is the full history of the room number {room12.Id}");
                                            foreach (var booking in bookingbyroom)
                                            {
                                                Console.WriteLine($"Customer: {booking.Customer.Name} {booking.Customer.Surname}, Check-In Date: {booking.CheckInDate}, Check-Out Date: {booking.CheckOutDate}.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"The room number {room12.Id} has never been booked before.");
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Nothing found according to your search. Please try again.");
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            Console.WriteLine("You can only enter a number that's in the list!");
                            break;
                    }
                } while (choice != 0);
            }
        }
    }
}
