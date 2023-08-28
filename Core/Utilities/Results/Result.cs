using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool succes, string message):this(succes) //this demek classdır Result yani yani bu Result(succes) demek 
             //bu sayede constructorlar birlikte çalışır
        {    
            this.message = message;
        }

        public Result(bool succes)
        {
            Succes= succes;
        }

        public bool Succes { get; }

        public string message { get; }
    }
}
