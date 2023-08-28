using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("bu bir product validator sınıfı değil");
            }

            _validatorType = validatorType;
        }

        //çalışmadan önce bu çalışsın bunun işte tipini kendi seçsin ve hoopp tool ile çalıştırdık
        protected override void OnBefore(IInvocation invocation) //override ettik
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //çalışma anında oluşması gerek veri create instance ile üretilir
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            // ana sınıfın inherit clası var generic argumenti al diyor bak ilkine e bizimki de zaten product generic son git kontrol et

            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); //tool kullanıldı
            }
        }
    }
}
