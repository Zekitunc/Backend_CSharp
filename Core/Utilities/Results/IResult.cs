using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{

    //utilities araçlar demek ve şimdi result dönmek için gerekli olan ilki Void olsun
    public interface IResult
    {
        bool Succes { get; } //oldu mu sadece get construct ile set ederiz
        string message { get; }
        
    }
}
