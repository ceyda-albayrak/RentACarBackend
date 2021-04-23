using Business.Concrete;
using System;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Concrete;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarManager carManager = new CarManager(new EfCarDal());
            //carManager.Add(new Car(){BrandId =1,ColorId = 1,CarName = "",DailyPrice = 1000,Description = "SATILIK",ModelYear = 1000});
            //carManager.Delete(car:new Car(){Id = 1});
            //carManager.Update(new Car(){Id = 2,DailyPrice = 30});
            //ColorManager colorManager = new ColorManager(new EfColorDal());
            //colorManager.Add(new Color(){ColorName = "Gri"});
            //colorManager.Update(new Color(){ColorId =1 ,ColorName = "Kırmızıı"});
            //colorManager.Delete(new Color(){ColorId = 11});
            //BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand() {BrandName  = "Volvo"});
            //CarTest();
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            //rentalManager.Add(new Rental() {CarId = 4, CustomerId = 1, RentDate = 2020});
            rentalManager.Update(new Rental() {Id = 3,CarId = 2, CustomerId = 1, RentDate = 2020, ReturnDate = 2020});


        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            if (result.Success == true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine("Bilgiler:" + car.CarName +"-"+ car.ColorName +"-"+ car.BrandName +"-"+ car.DailyPrice+Messages.Listed);
                }
            }
            else
            {
                Console.WriteLine("Hatalı");
            }
        }
    }
}
