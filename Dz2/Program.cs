using System;
using System.Threading;

namespace Dz2
{
    public class Program
    {
        static object CreateVinyl(Type vinylType, int id)
        {
            var vinyl = Activator.CreateInstance(vinylType);
            vinylType.GetProperty("Id").SetValue(vinyl, id);
            vinylType.GetProperty("Title").SetValue(vinyl, "The Dark Side of the Moon");
            vinylType.GetProperty("Artist").SetValue(vinyl, "Pink Floyd");
            vinylType.GetProperty("Publisher").SetValue(vinyl, "Harvest Records");
            vinylType.GetProperty("TrackCount").SetValue(vinyl, 10);
            vinylType.GetProperty("Genre").SetValue(vinyl, "Progressive Rock");
            vinylType.GetProperty("ReleaseYear").SetValue(vinyl, 1973);
            vinylType.GetProperty("CostPrice").SetValue(vinyl, 15.99m);
            vinylType.GetProperty("SalePrice").SetValue(vinyl, 24.99m);
            vinylType.GetProperty("IsOnSale").SetValue(vinyl, true);
            vinylType.GetProperty("DiscountPercentage").SetValue(vinyl, 20);
            vinylType.GetProperty("SoldCount").SetValue(vinyl, 5000);
            vinylType.GetProperty("IsReserved").SetValue(vinyl, true);
            vinylType.GetProperty("ReservedByUserId").SetValue(vinyl, 42);
            return vinyl;
        }

        static void Main()
        {
            AppDomain domain = AppDomain.CreateDomain("ShopVinylsDomain");
            var service = (MarshalByRefObject)domain.CreateInstanceAndUnwrap(
                "ShopVinyls",
                "ShopVinyls.ServiceVinyl");

            dynamic vinylService = service;
            var vinylType = service.GetType().Assembly.GetType("ShopVinyls.Vinyl");

            var vinyl = CreateVinyl(vinylType, 12);

            Thread createVinyl = new Thread(() =>
            {
                try
                {
                    var vinyl2 = Activator.CreateInstance(vinylType);
                    vinylType.GetProperty("Id").SetValue(vinyl2, 12);
                    vinylType.GetProperty("Title").SetValue(vinyl2, "The Dark Side of the Moon");
                    vinylType.GetProperty("Artist").SetValue(vinyl2, "Pink Floyd");
                    vinylType.GetProperty("Publisher").SetValue(vinyl2, "Harvest Records");
                    vinylType.GetProperty("TrackCount").SetValue(vinyl2, 10);
                    vinylType.GetProperty("Genre").SetValue(vinyl2, "Progressive Rock");
                    vinylType.GetProperty("ReleaseYear").SetValue(vinyl2, 1973);
                    vinylType.GetProperty("CostPrice").SetValue(vinyl2, 15.99m);
                    vinylType.GetProperty("SalePrice").SetValue(vinyl2, 24.99m);
                    vinylType.GetProperty("IsOnSale").SetValue(vinyl2, true);
                    vinylType.GetProperty("DiscountPercentage").SetValue(vinyl2, 20);
                    vinylType.GetProperty("SoldCount").SetValue(vinyl2, 5000);
                    vinylType.GetProperty("IsReserved").SetValue(vinyl2, true);
                    vinylType.GetProperty("ReservedByUserId").SetValue(vinyl2, 42);

                    vinylService.AddVinyl(vinyl2);
                    Console.WriteLine("Vinyl added successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in AddVinyl: {ex.Message}");
                }
            });


            createVinyl.Start();
            createVinyl.Join();

            Thread updateVinyl = new Thread(() =>
            {
                try
                {
                    vinylService.UpdateVinyl(vinyl);
                    Console.WriteLine("Vinyl updated successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateVinyl: {ex.Message}");
                }
            });

            Thread deleteThread = new Thread(() =>
            {
                try
                {
                    vinylService.DeleteVinyl(11);
                    Console.WriteLine("Vinyl deleted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in DeleteVinyl: {ex.Message}");
                }
            });

            Thread readVinyls = new Thread(() =>
            {
                try
                {
                    var vinyls = vinylService.GetAllVinyls();
                    foreach (var v in vinyls)
                    {
                        Console.WriteLine(
                            $"ID: {v.Id}, Title: {v.Title}, Artist: {v.Artist}, Publisher: {v.Publisher}, Tracks: {v.TrackCount}, Genre: {v.Genre}, Year: {v.ReleaseYear}, CostPrice: {v.CostPrice}, SalePrice: {v.SalePrice}, IsOnSale: {v.IsOnSale}, Discount: {v.DiscountPercentage}%, SoldCount: {v.SoldCount}, Reserved: {v.IsReserved}, ReservedByUserId: {v.ReservedByUserId}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetAllVinyls: {ex.Message}");
                }
            });

            updateVinyl.Start();
            deleteThread.Start();
            readVinyls.Start();

            updateVinyl.Join();
            deleteThread.Join();
            readVinyls.Join();

            Console.ReadKey();
        }

    }
}
