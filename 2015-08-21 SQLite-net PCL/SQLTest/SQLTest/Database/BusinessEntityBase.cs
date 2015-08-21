using System;
using SQLite;

namespace SQLTest.Database
{
    public class BusinessEntityBase : IBusinessEntity
    {
        public BusinessEntityBase()
        {
        }

        #region IBusinessEntity implementation

        [PrimaryKey, AutoIncrement] 
        public int Id
        {
            get; set;
        }

        #endregion
    }
}

