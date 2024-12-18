using Lab2.DataAccess;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1 - Filter houses by year");
        Console.WriteLine("2 - Find address by city");
        Console.WriteLine("3 - Add new house and address");
        Console.WriteLine("4 - Delete house and address");
        Console.WriteLine("5 - Update house and address");
        Console.WriteLine("6 - Add garage");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                FilterHousesByYearBuilt();
                break;
            case "2":
                FindAddressByCity();
                break;
            case "3":
                AddNewHouseAndAddress();
                break;
            case "4":
                DeleteHouseAndAddress();
                break;
            case "5":
                UpdateHouseAndAddress();
                break;
            case "6":
                AddGarage();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    static void FilterHousesByYearBuilt()
    {
        using (var context = new HouseDbContext())
        {
            var filteredHouses = context.Houses
                                        .Where(h => h.YearBuilt > 2000)
                                        .ToList();

            foreach (var house in filteredHouses)
            {
                Console.WriteLine($"House ID: {house.Id}, Owner: {house.Owner}, Year Built: {house.YearBuilt}");
            }
        }
    }

    static void FindAddressByCity()
    {
        using (var context = new HouseDbContext())
        {
            var address = context.Addresses.FirstOrDefault(a => a.City == "Chicago");

            if (address != null)
            {
                Console.WriteLine($"Address ID: {address.Id}, Street: {address.Street}, City: {address.City}, Postal Code: {address.PostalCode}, Country: {address.Country}");
            }
            else
            {
                Console.WriteLine("No address found in Chicago.");
            }
        }
    }

    static void AddNewHouseAndAddress()
    {
        using (var context = new HouseDbContext())
        {
            var newHouse = new House
            {
                YearBuilt = 2024,
                Owner = "Alex Ivanov",
                Area = 340.0,
                Floors = 4
            };

            context.Houses.Add(newHouse);
            context.SaveChanges();

            var newAddress = new Address
            {
                Street = "Rigas st",
                City = "Daugavpils",
                PostalCode = "Lv-****",
                Country = "LV",
                Notes = "New house",
                HouseId = newHouse.Id
            };

            context.Addresses.Add(newAddress);
            context.SaveChanges();

            Console.WriteLine("New house and address added successfully.");
        }
    }

    static void DeleteHouseAndAddress()
    {
        using (var context = new HouseDbContext())
        {
            var addressToDelete = context.Addresses.FirstOrDefault(a => a.HouseId == 22);

            if (addressToDelete != null)
            {
                context.Addresses.Remove(addressToDelete);
                Console.WriteLine("Address deleted successfully.");
            }

            var houseToDelete = context.Houses.FirstOrDefault(h => h.Id == 22);

            if (houseToDelete != null)
            {
                context.Houses.Remove(houseToDelete);
                Console.WriteLine("House deleted successfully.");
            }

            context.SaveChanges();
        }
    }

    static void UpdateHouseAndAddress()
    {
        using (var context = new HouseDbContext())
        {
            var houseToUpdate = context.Houses.FirstOrDefault(h => h.Id == 3);

            if (houseToUpdate != null)
            {
                houseToUpdate.Owner = "Denis Petrov";
                context.SaveChanges();
                Console.WriteLine("House updated successfully.");
            }

            var addressToUpdate = context.Addresses.FirstOrDefault(a => a.HouseId == 3);

            if (addressToUpdate != null)
            {
                addressToUpdate.Notes = "Family home";
                context.SaveChanges();
                Console.WriteLine("Address updated successfully.");
            }
        }
    }

    static void AddGarage()
    {
        using (var context = new HouseDbContext())
        {
            var house = context.Houses.FirstOrDefault(h => h.Owner == "Denis Petrov");

            if (house != null)
            {
                var newGarage = new Garage
                {
                    Type = "Underground",
                    Size = 50.0,
                    HouseId = house.Id
                };

                context.Garages.Add(newGarage);
                context.SaveChanges();

                Console.WriteLine("Garage added successfullyv.");
            }
            else
            {
                Console.WriteLine("House not found.");
            }
        }
    }
}
