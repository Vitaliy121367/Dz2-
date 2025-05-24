using System;
using System.Threading;

namespace Dz2
{
    public class Program
    {
        static void Main()
        {
            AppDomain domain = AppDomain.CreateDomain("ShopVinylsDomain");
            var service = (MarshalByRefObject)domain.CreateInstanceAndUnwrap(
                "ShopVinyls",
                "ShopVinyls.ServiceVinyl");

            dynamic vinylService = service;

            Thread createVinyl = new Thread(() =>
            {
                var vinyl = new Vinyl
                {
                    Id = 12,
                    Title = "The Dark Side of the Moon",
                    Artist = "Pink Floyd",
                    Publisher = "Harvest Records",
                    TrackCount = 10,
                    Genre = "Progressive Rock",
                    ReleaseYear = 1973,
                    CostPrice = 15.99m,
                    SalePrice = 24.99m,
                    IsOnSale = true,
                    DiscountPercentage = 20,
                    SoldCount = 5000,
                    IsReserved = true,
                    ReservedByUserId = 42
                };
                vinylService.AddVinyl(vinyl);
            });
            Thread readVinyls = new Thread(() =>
            {
                var vinyls = vinylService.GetAllVinyls();
                foreach (var vinyl in vinyls)
                {
                    Console.WriteLine(
                            $"ID: {vinyl.Id}, Title: {vinyl.Title}, Artist: {vinyl.Artist}, Publisher: {vinyl.Publisher}, Tracks: {vinyl.TrackCount}, Genre: {vinyl.Genre}, Year: {vinyl.ReleaseYear}, CostPrice: {vinyl.CostPrice}, SalePrice: {vinyl.SalePrice}, IsOnSale: {vinyl.IsOnSale}, Discount: {vinyl.DiscountPercentage}%, SoldCount: {vinyl.SoldCount}, Reserved: {vinyl.IsReserved}, ReservedByUserId: {vinyl.ReservedByUserId}");
                }
            });

            Thread updateVinyl = new Thread(() =>
            {
                var vinyl = new Vinyl
                {
                    Id = 1,
                    Title = "The Dark Side of the Moon",
                    Artist = "Pink Floyd",
                    Publisher = "Harvest Records",
                    TrackCount = 10,
                    Genre = "Progressive Rock",
                    ReleaseYear = 1973,
                    CostPrice = 15.99m,
                    SalePrice = 24.99m,
                    IsOnSale = true,
                    DiscountPercentage = 20,
                    SoldCount = 5000,
                    IsReserved = true,
                    ReservedByUserId = 42
                };
                vinylService.UpdateVinyl(vinyl);
            });

            Thread deleteThread = new Thread(() =>
            {
                vinylService.DeleteVinyl(11);
            });

            createVinyl.Start();
            updateVinyl.Start();
            deleteThread.Start();
            readVinyls.Start();

            createVinyl.Join();
            updateVinyl.Join();
            deleteThread.Join();
            readVinyls.Join();

            Console.ReadKey();
        }
    }
}
