using System;
using System.Collections.Generic;
using UsedCars.Models;

namespace UsedCars.DAL
{
    public interface ICarRepository : IDisposable
    {
         List<Car> GetCars();
         Car GetCarByID(int id);
         int InsertCar(Car car);
         int DeleteCar(int id);
         int UpdateCar(Car car);
    }
}