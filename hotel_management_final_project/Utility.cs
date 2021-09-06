using System;
using System.Linq;
using hotel_management_final_project.Models;
using hotel_management_final_project.Repository;
namespace hotel_management_final_project
{
    public class Utility
    {
        public static int TryParseForInt()
        {
            int result;
            int integer = 0;
            bool IsCorrect = Int32.TryParse(Console.ReadLine(), out result);
            while (!IsCorrect)
            {
                Console.WriteLine("Please type in a correct number!");
                IsCorrect = Int32.TryParse(Console.ReadLine(), out result);
            }
            integer = result;
            return integer;
        }

        public static Room SearchForRooms()
        {
            Console.WriteLine("Please enter a room number");
            int roomId = TryParseForInt();
            RoomService roomService = new RoomService();
            Room room = roomService.Get(roomId);
            return room;
        }

        public static bool CheckoutDateIsNotEarlier(DateTime checkin, DateTime checkout)
        {
            if (checkout.CompareTo(checkin) < 0)
            {
                Console.WriteLine("Check out date cannot be earlier than check in date!");
                return false;
            }
            else if (checkout.CompareTo(checkin) == 0)
            {
                Console.WriteLine("Check out date cannot be the same as the check in date!");
                return false;
            }
            else if (checkin.CompareTo(DateTime.Now)! < 0)
            {
                Console.WriteLine("Check-In date cannot be earlier than today!");
                return false;
            }
            else
            {
                return true;
            }
        }
       
    }
}
