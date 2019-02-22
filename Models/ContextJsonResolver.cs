using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;

namespace UsedCars.Models
{
    public class ContextJsonResolver : IContextResolver
    {
        private string sourceString = @"../UsedCars/DataSource/data.json";
        public List<T> RetrieveData<T>()
        {
            try
            {
                List<T> jsonObject = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(sourceString));
                return jsonObject;
            }
            catch(Exception ex)
            {
                throw new Exception("cannot retrieve data in source file: ", ex);
            }
        }

        public void SaveData<T>(List<T> items)
        {
            try
            {
                string serializeItems = JsonConvert.SerializeObject(items);
                File.WriteAllText(sourceString, serializeItems);
            }
            catch(Exception ex)
            {
                throw new Exception("cannot save data in source file: ", ex);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}