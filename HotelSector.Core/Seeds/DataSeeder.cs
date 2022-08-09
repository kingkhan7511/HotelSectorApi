using HotelSector.Core.Entities.Room;
using HotelSector.Core.Entities.User;
using HotelSector.Core.EntityFrameworkCore.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace HotelSector.Core.Seeds
{
    public class DataSeeder
    {
        public static void Initialize(HotelSectorDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = new List<UserEntity>();
                if (context.Users.FirstOrDefault(x => x.Email == "email2wajid@gmail.com") == null)
                {
                    users.Add(new UserEntity
                    {
                        FirstName = "Wajid",
                        LastName = "Nazar",
                        Email = "email2wajid@gmail.com",
                        PhoneNumber = "+971566290465",
                        CreatedBy = 1
                    });
                };
                if (context.Users.FirstOrDefault(x => x.Email == "john@gmail.com") == null)
                {
                    users.Add(new UserEntity
                    {
                        FirstName = "John",
                        LastName = "Lewis",
                        Email = "john@gmail.com",
                        CreatedBy = 1
                    });
                };
                if (users.Count > 0)
                {
                    context.Users.AddRange(users);
                    context.SaveChanges();
                }
            }
            if (!context.Room.Any())
            {
                var roomSeedingList = new List<RoomEntity>();
                if (context.Room.FirstOrDefault(x => x.RoomNo == "RN1") == null)
                {
                    roomSeedingList.Add(new RoomEntity
                    {
                        RoomNo = "RN1", 
                        IsAvailable = true, 
                        RentPerHour = 100, 
                        RoomFloor = 1,  
                        CreatedBy = 1
                    });
                };
                if (context.Room.FirstOrDefault(x => x.RoomNo == "RN2") == null)
                {
                    roomSeedingList.Add(new RoomEntity
                    {
                        RoomNo = "RN2",
                        IsAvailable = true,
                        RentPerHour = 80,
                        RoomFloor = 2,
                        CreatedBy = 1
                    });
                };
                if (roomSeedingList.Count > 0)
                {
                    context.Room.AddRange(roomSeedingList);
                    context.SaveChanges();
                }
            }

        }
    }
}
