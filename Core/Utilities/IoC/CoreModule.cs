using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public class CoreModule : ICoreModule
    { //uygulama seviyesinde bağımlılıkları çözmek
        public void Load(IServiceCollection collection)
        {
            collection.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            collection.AddSingleton<ICacheManager,MemoryCacheManager>();
            collection.AddMemoryCache(); // .NET oto enjection redis yapılırsa ise bu gider üst taraf değişir
        }
    }
}
