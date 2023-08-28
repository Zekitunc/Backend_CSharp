using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolver.Autofac
{
    public class AutofacBusinessModule :Module //autofac module
    {
        //burda config ayarları yapıcaz bir projede bunlar sadece birer kez yapılır
        //dependencies ayarları oto tip belirleme ve hangi DAL yolunun kullanımı gibi şeyler 
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder); //program çalıştığında çalıştır
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance(); //bu da autofac versiyonu addsingleton 
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance(); //ef ver ne isterse ıproductdal biz newlemeyiz

            builder.RegisterType<FileLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();    

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            //ALT KOD APİ İÇİNDİR
            var assembly  = System.Reflection.Assembly.GetExecutingAssembly();  //burda ise otomatik typeof kontrolü yapılır

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector  = new AspectInterceptorSelector()
                })
                .SingleInstance();
            //e bunu şimdi APIye bunu kullan diycez webapi program cs
        }
    }
}
