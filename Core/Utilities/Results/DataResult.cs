using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result,IDataResult<T> //T dikkatli
    {
        public DataResult(T data ,bool succes,string message) : base(succes,message)
        {
            this.Data = data;
        }

        public DataResult(T data,bool succes): base(true)
        {
            Data = data;
        }

        public T Data { get;}
    }

    
}
