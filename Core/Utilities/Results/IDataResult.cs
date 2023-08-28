using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult //farklı olarak sadece datası olan bir obje
    {
        T Data { get; }
    }
}
