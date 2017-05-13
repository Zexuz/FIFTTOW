using System;
using SQLite;

namespace FIFTTOW.Data
{
    public class Repository<T> where T:class, new()
    {
        public string DbName => "FIFFTOW.db";

        public async void CreateTableAsync()
        {
            await GetAsyncConnection().CreateTableAsync<T>().ContinueWith((a) =>
            {
                System.Diagnostics.Debug.WriteLine("Created database");
            });
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var folderPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var conn = new SQLiteAsyncConnection (System.IO.Path.Combine (folderPath, DbName));
            return conn;
        }

    }


    public class WifiLocation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}