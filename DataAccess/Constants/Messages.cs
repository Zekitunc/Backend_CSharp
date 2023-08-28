using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi.";
        public static string ProductNameInvalid = "Ürün Eklenemedi";
        public static string ListedProduct = "Ürünler Başarıyla Listelendi.";
        public static string ErrorDataList = "Liste Getirilemedi.";
        public static string UnitPriceInvalid = "Birim fiyatı yanlış.";
        internal static string ErrorCountOfCategoryMessage = "bir kategoride en fazla 10 ürün bulunabilir.";
        internal static string ProductNameExist = "Bu isimde bir ürün zaten var";
        internal static string? AuthorizationDenied = "yetki geçersiz";
    }
}
