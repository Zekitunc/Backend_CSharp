using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{

    //CORE içinde global olarak kullanılan ve diğer katmanlardan kesinlikle bağımsız olan bir katman
    //genel kodlar ve hangi kay için kullanıldığı
    public interface IEntityRepository<T> where T:class,IEntity,new()// Type olabilir fakat sadece entitylerin de gelebilir
        //generic constraint kısıtlama mesela yukarıda sadece T IEntity olduğunda çalışır
        //bir sınıf Ientitye bağlı ve yeni açılabilir olanları alır
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null); //bu bir filtre isteiği bununlar GetAll(p=>p.categoryId==2) gibi
        //filtre null ise gereksiz yani gerek olmadan hepsini alabilirsin demek delegedir expression
        T Get(Expression<Func<T, bool>> filter); //filtre var
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity); 

    }
}
