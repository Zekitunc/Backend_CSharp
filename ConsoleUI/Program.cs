using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

internal class Program
{
    private static void Main(string[] args)
    {
        //data acces ile sql veya herhangi bir veri tabanından veri çekme yani SQL kodlarını yaz
        //business ise bizim iş için kullandığımız yer işsel komutlar yer alır mesela kayıt etmek vs.
        //entities veritabanımız ile yazılımımız arasında ilişki kurmamızı sağlayan kalıcı nesneler
        //UI olarak da konsol açtık test için şimdilik 
        //abstract soyut concrete somut olanlar Interface,base class,abstract class abstract içinde 
        //concrete gerçek işi yapan classlar Iduty abstract ve duty concrete içinde

        //entities aer katmanda kullanılır

        IProductService productManager = new ProductManager(new EfProductDal(), new FileLogger(),new CategoryManager( new EfCategoryDal())); //construc burda IProductDal alıyor 

        foreach(var p in productManager.GetProductDetails().Data) //message olarak eklendikten sonra ise burada Data ile çağrı
        {
            Console.WriteLine(p.ProductName + " " + p.CategoryName);
            
        }
          /*
        FileLogger a = new FileLogger();
        a.Log();
        DatabaseLogger b = new DatabaseLogger();
        b.Log(); 
          AOP örnekleme

        */
        

        /*
         *         OrderManager ordrManager = new OrderManager(new EfOrderDal());

         * foreach (var o in ordrManager.GetAll())
        {
            Console.WriteLine(o.ShipCity);
        }*/
        //yaz yaz kullan kardeşim
        //entity ve inmemory olayı sadece iskelet
    }
}