using System.Collections.Generic;
using System;

namespace UsedCars.Models
{
    public interface IContextResolver : IDisposable
    {
        List<T> RetrieveData<T>();

        void SaveData<T>(List<T> items);
    }
}