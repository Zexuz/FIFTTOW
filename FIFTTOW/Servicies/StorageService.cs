using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FIFTTOW.Servicies
{
    public class StorageService<T> where T : class
    {
        public void Add(T obj)
        {
            var all = GetAll();
            all.Add(obj);
            File.WriteAllText(GetFileName(), JsonConvert.SerializeObject(all));
        }

        public List<T> GetAll()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(GetFileName()));
            }
            catch (FileNotFoundException e)
            {
                return new List<T>();
            }
        }

        private static string GetFileName()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path, $"{typeof(T)}.txt");
            return filename;
        }
    }
}