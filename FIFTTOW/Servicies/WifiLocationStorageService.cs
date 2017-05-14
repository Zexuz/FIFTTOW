using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIFTTOW.Exceptions;
using FIFTTOW.Interfaces;
using FIFTTOW.Models;
using Newtonsoft.Json;

namespace FIFTTOW.Servicies
{
    public class WifiLocationStorageService : IStorageService<WifiLocation>
    {
        public void Add(WifiLocation obj)
        {
            var all = GetAll();

            var itemAlredyInFile = all.Any(wifi => wifi.SSID== obj.SSID);
            if (itemAlredyInFile) throw new ItemAlredySavedException("The wifilocation is alredy saved and can not be saved again. This also happens if the SSID is the same a alredy stored SSID. Sorry for this.");

            all.Add(obj);
            File.WriteAllText(GetFileName(), JsonConvert.SerializeObject(all));
        }

        public List<WifiLocation> GetAll()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<WifiLocation>>(File.ReadAllText(GetFileName()));
            }
            catch (FileNotFoundException e)
            {
                return new List<WifiLocation>();
            }
        }

        /// <summary>
        /// This method replases every item stored.
        /// </summary>
        /// <param name="items"></param>
        public void Save(IEnumerable<WifiLocation> items)
        {
            File.WriteAllText(GetFileName(), string.Empty); //delete all items in file.
            File.WriteAllText(GetFileName(), JsonConvert.SerializeObject(items));
        }


        private static string GetFileName()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path, $"{typeof(WifiLocation)}.txt");
            return filename;
        }
    }
}