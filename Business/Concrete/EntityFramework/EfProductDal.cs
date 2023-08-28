using Business.Abstract;
using Core.DataAcces.EntityFramework;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepository<Product, NorthwindContext>, IProductDal
    //entity Framework microsoftun ORM ile yanı veritabanındaki tabloyu sınıf gibi kullanır işleri burda yapar
    {
        public List<ProductDetailDto> GetProductDetails()
        {
           using(NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories //ürünler ile kategorileri join et
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
       }
    }
}
