using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework.Concrete
{
    public class EfCarDal : EfEntityRepository<Car, ReCapContext>, ICarDal
    {
        public List<CarDetailsDTOs> GetCarDetails()
        {
            using (ReCapContext context=new ReCapContext())
            {
                var result = from p in context.Cars
                             join c in context.Colors on p.ColorId equals c.ColorId
                             join b in context.Brands on p.BrandId equals b.BrandId
                             select new CarDetailsDTOs() 
                             {
                                 CarName = p.CarName,
                                 ColorName = c.ColorName,
                                 BrandName = b.BrandName,
                                 DailyPrice = p.DailyPrice

                             };
                return result.ToList();
            }
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            using (ReCapContext context=new ReCapContext())
            {
                return context.Set<Car>().Where(p => p.BrandId == id).ToList();
            }
        }

        public List<Car> GetCarsByColorId(int id)
        {
            using (ReCapContext context = new ReCapContext())
            {
                return context.Set<Car>().Where(p => p.ColorId == id).ToList();
            }
        }
    }
}
