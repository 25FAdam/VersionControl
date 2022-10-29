using Hatodik.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hatodik.Entities
{
    class CarFactory : IToyFactory
    {
        public Car CreateNew()
        {
            return new Car();
        }

        Toy IToyFactory.CreateNew()
        {
            throw new NotImplementedException();
        }
    }
}
