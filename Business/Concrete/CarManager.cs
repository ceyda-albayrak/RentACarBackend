using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
          
            _carDal.Add(car);
            return new SuccessResult(Messages.Added);
               
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll().ToList(), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetById(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.Id == id).ToList(), Messages.Listed);
        }

        public IDataResult<List<CarDetailsDTOs>> GetCarDetails()
        {
            _carDal.GetCarDetails();
            return new SuccessDataResult<List<CarDetailsDTOs>>(_carDal.GetCarDetails(), "Araba detayları:");
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetCarsByBrandId(id), "Arabalar markaya göre listelendi");
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetCarsByBrandId(id), "Arabalar renke göre listelendi");
        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }
    }
}
