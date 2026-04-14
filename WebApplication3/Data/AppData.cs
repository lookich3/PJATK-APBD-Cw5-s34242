using WebApplication3.Models;

namespace WebApplication3.Data;

public static class AppData
{
    public static List<Room> Rooms { get; set; } = new()
    {
        new Room
        {
            Id = 1,
            Name = "Sala A358",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Lab B226",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Sala C201",
            BuildingCode = "C",
            Floor = 3,
            Capacity = 15,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Sala B110",
            BuildingCode = "B",
            Floor = 1,
            Capacity = 30,
            HasProjector = true,
            IsActive = false
        },
        new Room
        {
            Id = 5,
            Name = "Sala A359",
            BuildingCode = "A",
            Floor = 2,
            Capacity = 18,
            HasProjector = false,
            IsActive = true
        }
    };

    public static List<Reservation> Reservations { get; set; } = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "Warsztaty z HTTP",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Oleksii Nyskoroda",
            Topic = "REST API",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 30),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Ewa Zielinska",
            Topic = "Szkolenie C#",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 5,
            OrganizerName = "Piotr Wisniewski",
            Topic = "Spotkanie organizacyjne",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "cancelled"
        }
    };
}