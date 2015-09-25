using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MVVMFramework.Models;

namespace MVVMFramework.Interfaces
{
    public interface IDataStore
    {
        Task<IEnumerable<Person>> GetPeopleAsync();
    }
}

