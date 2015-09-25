using System;
using MVVMFramework.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using MVVMFramework.Models;

namespace MVVMFramework.Services
{
    public class OfflineDataStore : IDataStore
    {
        public OfflineDataStore()
        {
        }

        #region IDataStore implementation

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
          //do stuff!!!!
            var people = new List<Person>
            {
                    new Person
                    {
                        FirstName = "James",
                        LastName = "Montemagno"
                    },
                    new Person
                    {
                        FirstName = "Craig",
                        LastName = "Dunn"
                    },
                    new Person
                    {
                        FirstName ="Da Cheese",
                        LastName="Da Baron"
                    }
            };

            return people;
        }

        #endregion
    }
}

