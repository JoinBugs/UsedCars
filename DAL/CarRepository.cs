using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using UsedCars.DAL;
using UsedCars.Models;

namespace UsedCars.DAL
{
    public class CarRepository : ICarRepository, IDisposable
    {
        private IContextResolver context;

        public CarRepository(IContextResolver context)
        {
            this.context = context;
        }

        public List<Car> GetCars()
        {
            List<Car> cars = null;
            try
            {
                cars = this.context.RetrieveData<Car>();
            }
            catch(Exception)
            {
                throw;
            }
            return cars;
        }

        public Car GetCarByID(int id)
        {
            Car car = null;
            try
            {
                car = this.GetCars().Find(_car => _car.Id == id);
            }
            catch(Exception)
            {
                throw;
            }
            return car;
        }

        private int getMajorId()
        {
            int majorCar = -1;
            try
            {
                List<Car> cars = this.GetCars();
                foreach(Car car in cars)
                    if(car.Id > majorCar)
                        majorCar = car.Id;
            }
            catch(Exception)
            {
                throw;
            }

            return majorCar;
        }

        public int InsertCar(Car car)
        {
            int idNewCar = -1;
            try
            {
                List<Car> cars = this.GetCars();
                
                if(this.GetCarByID(car.Id) == null)
                {
                    idNewCar = getMajorId() + 1;
                    car.Id = idNewCar;
                    cars.Add(car);
                    this.context.SaveData<Car>(cars);
                }
            }
            catch(Exception)
            {
                throw;
            }

            return idNewCar;
        }

        public int DeleteCar(int id)
        {
            int index = -1;
            try
            {
                Car car = this.GetCarByID(id);

                if(car != null)
                {
                    List<Car> cars = this.GetCars();
                    index = cars.FindIndex(_car => _car.Id == id);
                    cars.RemoveAt(index);
                    this.context.SaveData(cars);
                }
            }
            catch(Exception)
            {
                throw;
            }
            return index;
        }

        public int UpdateCar(Car car)
        {
            int indexCar = -1;
            try
            {
                List<Car> cars = this.GetCars();

                if(this.GetCarByID(car.Id) != null)
                {
                    indexCar = cars.FindIndex(_car => _car.Id == car.Id);
                    cars.Remove(cars[indexCar]);
                    cars.Insert(indexCar, car);
                    this.context.SaveData(cars);
                }
            }
            catch(Exception)
            {
                throw;
            }
            return indexCar;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}