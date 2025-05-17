using System;

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

            var vinyls = vinylService.GetAllVinyls();
            foreach (var vinyl in vinyls)
            {
                Console.WriteLine(
                        $"ID: {vinyl.Id}, Title: {vinyl.Title}, Artist: {vinyl.Artist}, Publisher: {vinyl.Publisher}, Tracks: {vinyl.TrackCount}, Genre: {vinyl.Genre}, Year: {vinyl.ReleaseYear}, CostPrice: {vinyl.CostPrice}, SalePrice: {vinyl.SalePrice}, IsOnSale: {vinyl.IsOnSale}, Discount: {vinyl.DiscountPercentage}%, SoldCount: {vinyl.SoldCount}, Reserved: {vinyl.IsReserved}, ReservedByUserId: {vinyl.ReservedByUserId}");
            }

            Console.ReadKey();
        }
    }
}
