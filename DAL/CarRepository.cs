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
            return this.context.RetrieveData<Car>();
        }

        public Car GetCarByID(int id)
        {
            return this.GetCars().Find(car => car.Id == id);
        }

        private int getMajorId()
        {
            List<Car> cars = this.GetCars();
            int majorCar = -1;
            foreach(Car car in cars)
                if(car.Id > majorCar)
                    majorCar = car.Id;
            return majorCar;
        }

        public bool InsertCar(Car car)
        {
            List<Car> cars = this.GetCars();
            bool success = false;
            
            if(this.GetCarByID(car.Id) == null)
            {
                car.Id = getMajorId() + 1;
                cars.Add(car);
                success = this.context.SaveData<Car>(cars);
            }

            return success;
        }

        public bool DeleteCar(int id)
        {
            Car car = this.GetCarByID(id);
            bool success = false;

            if(car != null)
            {
                List<Car> cars = this.GetCars();
                int index = cars.FindIndex(_car => _car.Id == id);
                cars.RemoveAt(index);
                success = this.context.SaveData(cars);
            }
            return success;
        }

        public bool UpdateCar(Car car)
        {
            List<Car> cars = this.GetCars();
            bool success = false;

            if(this.GetCarByID(car.Id) != null)
            {
                int indexCar = cars.FindIndex(_car => _car.Id == car.Id);
                cars.Remove(cars[indexCar]);
                cars.Insert(indexCar, car);
                this.context.SaveData(cars);
                success = true;
            }

            return success;
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