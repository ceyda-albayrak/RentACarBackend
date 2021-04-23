using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = _rentalDal.GetAll(c=> c.CarId==rental.CarId && c.ReturnDate==0);

            if (result.Any())
            {
                Console.WriteLine("Araba teslim edilmedi");
                return new ErrorResult("Araba teslim edilmedi");
            }
            else
            {
                
                _rentalDal.Add(rental);
                Console.WriteLine("Araba kiralandı.");
                return new SuccessResult("Araba kiralandı");
            }
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll().ToList(),Messages.Listed);
        }

        public IDataResult<List<Rental>> GetById(int id)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(p => p.Id == id).ToList(), Messages.Listed);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.Updated);
        }
    }
}
