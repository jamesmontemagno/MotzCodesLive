using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace SQLTest.Database
{
    public class ToDoDatabase
    {
        SQLiteConnection Connection { get; }
        public static string Root { get; set; } = string.Empty;

        public ToDoDatabase()
        {
            var location = "tododb.db3";
            location = System.IO.Path.Combine(Root, location);
            Connection = new SQLiteConnection(location);

            Connection.CreateTable<ToDoItem>();


        }


        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {

            return (from i in Connection.Table<T>()
                where i.Id == id
                select i).FirstOrDefault();

        }

        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {

            return (from i in Connection.Table<T>()
                select i);

        }

        public int SaveItem<T>(T item) where T : IBusinessEntity
        {

            if (item.Id != 0)
            {
                Connection.Update(item);
                return item.Id;
            }

            return Connection.Insert(item);

        }

        public void SaveItems<T>(IEnumerable<T> items) where T : IBusinessEntity
        {

            Connection.BeginTransaction();

            foreach (T item in items)
            {
                SaveItem(item);
            }

            Connection.Commit();

        }

        public int DeleteItem<T>(T item) where T : IBusinessEntity, new()
        {

            return Connection.Delete(item);

        }
    }
}

