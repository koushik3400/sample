using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.api.Model
{
    public class Inventory
    {
        [Table("stock_category")]
        public class Category
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int categoryId { get; set; }
            [Required]
            public string categoryName { get; set; }
            public string categoryImage { get; set; }

        }
        [Table("stock_customer")]
        public class Stock_Customer
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int customerId { get; set; }
            [Required]
            public string name { get; set; }
            [Required]
            public string mobileNo { get; set; }
            public string email { get; set; }
            [Required]
            public string aadharCard { get; set; }
            public string address { get; set; }

        }

        [Table("stock_supplier")]
        public class Supplier
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int supplierId { get; set; }
            [Required]
            public string supplierName { get; set; }
            [Required]
            public string mobileNo { get; set; }
            public string address { get; set; }
            public string gstNo { get; set; }

        }

        [Table("stock_users")]
        public class Stock_users
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int userId { get; set; }
            [Required]
            public string userName { get; set; }
            [Required]
            public string employeeName { get; set; }
            [Required]
            public string contactNo { get; set; }
            [Required]
            public string password { get; set; }
            [Required]
            public string role { get; set; }
            public Nullable<bool> canEdit { get; set; }
            public Nullable<bool> canCreate { get; set; }
            public Nullable<bool> canDelete { get; set; }
            public Nullable<DateTime> refreshTokenExpiryTime { get; set; }
            public string refreshToken { get; set; }
        }

        public class Stock_usersReturn
        {
            public int userId { get; set; }
            public string token { get; set; }
            public string userName { get; set; }
            public string employeeName { get; set; }
            public string contactNo { get; set; }
            public string password { get; set; }
            public string role { get; set; }
            public Nullable<bool> canEdit { get; set; }
            public Nullable<bool> canCreate { get; set; }
            public Nullable<bool> canDelete { get; set; }
            public Nullable<DateTime> refreshTokenExpiryTime { get; set; }
            public string refreshToken { get; set; }
        }

        [Table("stock_product")]
        public class Product
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int productId { get; set; }
            [Required]
            public int categoryId { get; set; }
            [Required]
            public string shortName { get; set; }
            [Required]
            public string longName { get; set; }
            public string description { get; set; }
            public Nullable<System.DateTime> createdDate { get; set; }
            [Required]
            public double price { get; set; }
            public string imageName { get; set; }
            public Nullable<bool> isExpiryProduct { get; set; }
            public Nullable<System.DateTime> expiryDate { get; set; }
        }

        [Table("stock_purchase")]
        public class Purchase
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int purchaseId { get; set; }
            [Required]
            public int supplierId { get; set; }
            [Required]
            public System.DateTime purchaseDate { get; set; }
            [Required]
            public int totalQuantity { get; set; }
            [Required]
            public double totalBillAmount { get; set; }
        }


        public class PurchaseViewModel
        {
            public int purchaseId { get; set; }
            [Required]
            public int supplierId { get; set; }
            [Required]
            public System.DateTime purchaseDate { get; set; }
            [Required]
            public int totalQuantity { get; set; }
            [Required]
            public double totalBillAmount { get; set; }
            public virtual List<PurchaseProducts> PurchaseProducts { get; set; }

        }

        [Table("stock_purchase_products")]
        public class PurchaseProducts
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int purchaseProductId { get; set; }
            public int purchaseId { get; set; }
            [Required]
            public int productId { get; set; }
            [Required]
            public int quantity { get; set; }
        }

        [Table("stock_sale_products")]
        public class SaleProducts
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int saleProductId { get; set; }
            public int saleId { get; set; }
            [Required]
            public int productId { get; set; }
            [Required]
            public int quantity { get; set; }
            public string serialNo { get; set; }
        }

        [Table("stock_sales")]
        public class Sales
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int saleId { get; set; }
            public int customerId { get; set; }
            [Required]
            public System.DateTime saleDate { get; set; }
            [Required]
            public int totalQuantity { get; set; }
            [Required]
            public double totalBillAmount { get; set; }
            [Required]
            public string paymentMode { get; set; }
            public string payementDetails { get; set; }
        }
        public class MessageDto
        {
            public string user { get; set; }
            public string msgText { get; set; }
        }
        public class ApiResponse
        {
            public string Message { get; set; }
            public bool Result { get; set; }
            public object Data { get; set; }
            public ApiResponse(object data = null, bool result = false, string message = "")
            {
                Result = result;
                Data = data;
                Message = message;
            }
        }

        public class SaleViewMOdel
        {
            [Required]
            public string name { get; set; }
            [Required]
            public string mobileNo { get; set; }
            public string email { get; set; }
            public string aadharCard { get; set; }
            public string address { get; set; }
            public int saleId { get; set; }
            [Required]
            public System.DateTime saleDate { get; set; }
            [Required]
            public int totalQuantity { get; set; }
            [Required]
            public double totalBillAmount { get; set; }
            [Required]
            public string paymentMode { get; set; }
            public string payementDetails { get; set; }
            public virtual List<SaleProducts> SaleProducts { get; set; }

        }


        [Table("stock_stockMaster")]
        public class StockMaster
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int stockId { get; set; }
            public int productId { get; set; }
            public int quantity { get; set; }
            public Nullable<System.DateTime> createdDate { get; set; }
            public Nullable<System.DateTime> lastUpdatedDate { get; set; }

        }
        public class userLogin
        {

            public string userName { get; set; }
            public string password { get; set; }
            public string refreshToken { get; set; }
        }

        public class StockDash
        {
            [Key]
            public int totCategory { get; set; }
            public int totProducts { get; set; }
            public int totSales { get; set; }
            public int totPurchase { get; set; }
            public int totTodaysSales { get; set; }
            public int totTodaysPurchase { get; set; }
            public int totCustomer { get; set; }
            public int totProductWithEmptyStock { get; set; }
        }
    }
}
