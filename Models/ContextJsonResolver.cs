using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;

namespace UsedCars.Models
{
    public class ContextJsonResolver : IContextResolver
    {
        private string sourceString = @"../UsedCards/DataSource/data.json";
        public List<T> RetrieveData<T>()
        {
            try
            {
                List<T> jsonObject = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(sourceString));
                return jsonObject;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool SaveData<T>(List<T> items)
        {
            try
            {
                string serializeItems = JsonConvert.SerializeObject(items);
                File.WriteAllText(sourceString, serializeItems);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}