using System;

namespace SQLTest.Database
{
    public class ToDoItem : BusinessEntityBase
    {
        public ToDoItem()
        {
           
        }
        public string Name {get;set;} = string.Empty;
        public DateTime Created {get;set;} = DateTime.UtcNow;
        public bool Done {get;set;} = false;


        [SQLite.Ignore]
        public string CreatedDisplay
        {
            get { return Created.ToLocalTime().ToString("f"); }
        }
    }
}

