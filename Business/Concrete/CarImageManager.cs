using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRole.Rol(CheckCarImageCount(carImage.CarId), CheckIfCarImageNull(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            var imageResult = FileHelper.Upload(file);
            if(!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }

            carImage.ImagePath = imageResult.Message;
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(CarImage carImage)
        {
            var image = _carImageDal.Get(p => p.Id == carImage.Id);
            if(image==null)
            {
                return new ErrorResult("Image not found.");
            }

            FileHelper.Delete(image.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll().ToList(), Messages.Listed);
        }

        public IDataResult<List<CarImage>> GetById(int id)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(p => p.CarId == id).ToList(),
                Messages.Listed);
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            var result = _carImageDal.Get(p => p.Id == carImage.Id);
            if (result == null)
            {
                return new ErrorResult("Image not found!");
            }

            var uploadImage = FileHelper.Update(file, result.ImagePath);
            if(!uploadImage.Success)
            {
                return new ErrorResult(uploadImage.Message);
            }

            carImage.ImagePath = uploadImage.Message;
            carImage.Date=DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckCarImageCount(int carId)
        {
            var result = _carImageDal.GetAll(p => p.CarId ==carId).Count;
            if (result >= 5)
            {
                return new ErrorResult("Arabanın en fazla 5 resmi olabilir.");
            }

            return new SuccessResult();
        }
        private IDataResult<List<CarImage>> CheckIfCarImageNull(int id)
        {
            try
            {
                string path = @"\images\logo.jpg";
                var result = _carImageDal.GetAll(c => c.CarId == id).Any();
                if (!result)
                {
                    List<CarImage> carimage = new List<CarImage>();
                    carimage.Add(new CarImage { CarId = id, ImagePath = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<CarImage>>(carimage);
                }
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<CarImage>>(exception.Message);
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(p => p.CarId == id).ToList());
        }
    }
}
