using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.Description.Length >= 2 && car.DailyPrice >0)
            {
                return new ErrorResult(Messages.CarNameInvalid + " " + Messages.CarPriceInvalid);
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarsAdded);
        }

        public IResult Delete(Car car)
        {
         _carDal.Delete(car);
            return new SuccessResult(Messages.CarsDeleted);
            
        }
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<Car>>(Messages.MainenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new  SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }
        public IResult Update(Car car)
        {
           _carDal.Update(car);
            return new SuccessResult(Messages.CarsUpdated);
           
        }
    }
}
