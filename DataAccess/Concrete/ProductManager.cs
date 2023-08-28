using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; //soyut nesne ile iletişime geçiçez
        ICategoryService _categoryService;
        ILogger _logger;

        public ProductManager(IProductDal productDal, ILogger logger, ICategoryService categoryService) //generate constructor
        {
            _productDal = productDal;
            _categoryService = categoryService;
            _logger = logger;
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.ErrorDataList); //liste gelcek ki frontend bilecek
            }
            //iş kodu
            //return  new DataResult<List<Product>>(_productDal.GetAll(),true,Messages.ListedProduct ); //bu iyi hoş peki ya
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(), Messages.ListedProduct);
            //SuccesDataResult

            //vay anasını rastgele bir interface oluşumunda çalışan kod çağrılabilir
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), "test");
            //filtre mevzusu yazdık artık istediğimizi seçeriz
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), "test");
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productid)
        {
            return new SuccesDataResult<Product>(_productDal.Get(p => p.ProductId == productid), "test");
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccesDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), "test");
        }

        [ValidationAspect(typeof(ProductValidator))] //AOP attribute bu bu yöntem yaygın ama aop versiyonu da var
        //[SecuredOperation("product.add,admin")] //admin vs. JWT json web token bu olmadan yetki yok gözükür
        //başaramadım onu
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //validation yapalım doğrulama giriş örneği gibi şifre 8 krakter vs. 
            //taşında fluent ile yaptık

            /* ValidationTool.Validate(new ProductValidator(), product); //core içinde var
             //peki bu iş kodu değil O ZAMAN NİYE BURDA Kİ OLAMAZ YUKARI EKLEYECEĞİZ*/
            //e demedik ki sen burda product validator kullan ama merak etme interceptor selector var
            //aspectin varsaautafav business module de bu halloluyor yoksa validation çalışmaz

            //ÖNEMLİ İŞ KURALLARI CLEAN CODE bir kategoride en fazla 10 ürün
            //aynı isimde ürün eklenemez

            IResult result = BusinessRules.Run(CheckIfProductCategoryCount(product), DuplicateProductNameCheck(product)
                , CheckCategoryLimit());

            if(!result.Succes)
            {
                return new ErrorResult();
            }
            _logger.Log();
            _productDal.Add(product);
            return new Result(true, Messages.ProductAdded);
            //bu return new SuccesResult("başarılır ekleme");
        }
        [ValidationAspect(typeof(ProductValidator))] 
        [CacheRemoveAspect("IProductService.Get")]
        //remove mantığında veritabanı değişimi olduysa cache silinir ve sıfırdan çekilir
        public IResult Update(Product product)
        {
            //transaction muhabbeti yapılabilir örneğin 10 tl soldan al sağa ver sağa vermediysen soluda düzelt
            //transaction Scope komutu
            _productDal.Update(product);
            return new SuccessResult("Updated");
        }

        private IResult CheckIfProductCategoryCount(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;  //bu iştir ama içeri yazılmaz
            if (result >= 10)
            {
                return new ErrorResult(Messages.ErrorCountOfCategoryMessage);
            }

            return new SuccessResult();
        }

        private IResult DuplicateProductNameCheck(Product product)
        {
            var result = _productDal.GetAll(p => p.ProductName == product.ProductName);
            if (result.Count > 0)
            {
                return new ErrorResult(Messages.ProductNameExist);
            }
            else
                return new SuccessResult();
        }

        private IResult CheckCategoryLimit()
        {

            var result = _categoryService.GetAll();
            if(result.Data.Count>=15)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

    }
}
