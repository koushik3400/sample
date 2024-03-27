using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stock.api.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Stock.api.Model.Inventory;

namespace Stock.api.Controllers
{
    [Route("api/Inventory")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class InventoryController : ControllerBase
    {
        private IConfiguration _config;
        private readonly InventoryDbContext _context;
        public InventoryController(InventoryDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        #region Category
        [Authorize]
        [HttpGet("GetDashboard")]
        public ApiResponse GetDashboard()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var list = _context.getAdminDash().FirstOrDefault();
                _res.Result = true;
                _res.Data = list;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("GetAllCategory")]
        public ApiResponse GetAllCategory()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var list = _context.Categorys.ToList().OrderByDescending(m => m.categoryId).ToList();
                _res.Result = true;
                _res.Data = list;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("AddCategory")]
        public ApiResponse AddCategory([FromBody] Category obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var isExist = _context.Categorys.SingleOrDefault(m => m.categoryName == obj.categoryName);
                if (isExist == null)
                {
                    _context.Categorys.Add(obj);
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Saved Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Category Name Already Present";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("UpdateCategory")]
        public ApiResponse UpdateCategory([FromBody] Category obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var response = _context.Categorys.SingleOrDefault(m => m.categoryId == obj.categoryId);
                if (response != null)
                {
                    response.categoryName = obj.categoryName;
                    response.categoryImage = obj.categoryImage;
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Updated Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Record Not Found";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("DeleteCategoryById")]
        public ApiResponse DeleteCategoryById(int id)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var record = _context.Categorys.SingleOrDefault(m => m.categoryId == id);
                _context.Categorys.Remove(record);
                _context.SaveChanges();
                _res.Result = true;
                _res.Message = "Deleted Successfully";
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }
        #endregion

        #region Customer
        [HttpGet("GetAllCustomer")]
        public ApiResponse GetAllCustomer()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var list = _context.Customers.ToList().OrderByDescending(m => m.customerId).ToList();
                _res.Result = true;
                _res.Data = list;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("AddCustomer")]
        public ApiResponse AddCustomer([FromBody] Stock_Customer obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var isExist = _context.Customers.SingleOrDefault(m => m.mobileNo == obj.mobileNo);
                if (isExist == null)
                {
                    _context.Customers.Add(obj);
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Saved Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "mobile No Already Present";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("UpdateCustomer")]
        public ApiResponse UpdateCustomer([FromBody] Stock_Customer obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var response = _context.Customers.SingleOrDefault(m => m.customerId == obj.customerId);
                if (response != null)
                {
                    response.aadharCard = obj.aadharCard;
                    response.address = obj.address;
                    response.name = obj.name;
                    response.mobileNo = obj.mobileNo;
                    response.email = obj.email;
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Updated Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Record Not Found";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("DeleteCustomerById")]
        public ApiResponse DeleteCustomerById(int id)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var record = _context.Customers.SingleOrDefault(m => m.customerId == id);
                _context.Customers.Remove(record);
                _context.SaveChanges();
                _res.Result = true;
                _res.Message = "Deleted Successfully";
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }
        #endregion

        #region User
        [HttpGet("GetAllUser")]
        public ApiResponse GetAllUser()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var list = _context.Users.ToList().OrderByDescending(m => m.userId).ToList();
                _res.Result = true;
                _res.Data = list;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("AddUser")]
        public ApiResponse AddUser([FromBody] Stock_users obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var isExist = _context.Users.SingleOrDefault(m => m.contactNo == obj.contactNo);
                if (isExist == null)
                {
                    _context.Users.Add(obj);
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Saved Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "mobile No Already Present";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("UpdateUser")]
        public ApiResponse UpdateUser([FromBody] Stock_users obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var response = _context.Users.SingleOrDefault(m => m.userId == obj.userId);
                if (response != null)
                {
                    response.userName = obj.userName;
                    response.employeeName = obj.employeeName;
                    response.contactNo = obj.contactNo;
                    response.password = obj.password;
                    response.role = obj.role;
                    response.canEdit = obj.canEdit;
                    response.canCreate = obj.canCreate;
                    response.canDelete = obj.canDelete;
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Updated Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Record Not Found";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }
        private string GenerateAccessToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:newKey"]));
            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
             _config["Jwt:Issuer"],
             null,
             expires: DateTime.Now.AddMinutes(3),
             signingCredentials: signinCredentials
             );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        [HttpPost("Login")]
        public ApiResponse Login([FromBody] userLogin obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var user = _context.Users.SingleOrDefault(m => m.userName == obj.userName && m.password == obj.password);
                if (user != null)
                {
                    Stock_usersReturn _user = new Stock_usersReturn();

                    var accessToken = GenerateAccessToken();
                    var refreshToken = GenerateRefreshToken();
                    user.refreshTokenExpiryTime = DateTime.Now.AddMinutes(3);
                    user.refreshToken = refreshToken;
                    _context.SaveChanges();
                    _user.refreshTokenExpiryTime = DateTime.Now.AddMinutes(3);
                    _user.refreshToken = refreshToken;
                    _user.token = accessToken;
                    _user.userName = user.userName;
                    _user.userId = user.userId;
                    _user.canCreate = user.canCreate;
                    _user.canDelete = user.canDelete;
                    _user.canEdit = user.canEdit;

                    _res.Data = _user;
                    _res.Result = true;
                    _res.Message = "Login Successful";
                    return _res;
                }
                else
                {

                    _res.Result = false;
                    _res.Message = "Wrong User Name or Password";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(userLogin tokenApiModel)
        {
            Stock_usersReturn _obj = new Stock_usersReturn();
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            var user = _context.Users.SingleOrDefault(u => u.userName == tokenApiModel.userName);
            if (user is null)
            {
                return BadRequest("Email Not Matched");
            }
            if (user.refreshToken != tokenApiModel.refreshToken)
            {
                return BadRequest("RefreshToken Not Matched");
            }
            if (user.refreshTokenExpiryTime > DateTime.Now)
            {
                return BadRequest("Token Not Expired Yet");
            }

            string refreshToken = tokenApiModel.refreshToken;
            //var principal =  GetPrincipalFromExpiredToken(accessToken);  
            var newAccessToken = GenerateAccessToken();
            var newRefreshToken = GenerateRefreshToken();
            _obj.refreshToken = newRefreshToken;
            _obj.token = newAccessToken;
            user.refreshToken = refreshToken;
            _context.SaveChanges();
            return Ok(new ApiResponse
            {
                Result = true,
                Message = "Refresh Token Generated Successfully",
                Data = _obj
            });
        }

        [HttpGet("DeleteUserById")]
        public ApiResponse DeleteUserById(int id)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var record = _context.Users.SingleOrDefault(m => m.userId == id);
                _context.Users.Remove(record);
                _context.SaveChanges();
                _res.Result = true;
                _res.Message = "Deleted Successfully";
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }
        #endregion

        #region Supplier
        [HttpGet("GetAllSupplier")]
        public ApiResponse GetAllSupplier()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var list = _context.Suppliers.ToList().OrderByDescending(m => m.supplierId).ToList();
                _res.Result = true;
                _res.Data = list;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("AddSupplier")]
        public ApiResponse AddSupplier([FromBody] Supplier obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var isExist = _context.Suppliers.SingleOrDefault(m => m.mobileNo == obj.mobileNo);
                if (isExist == null)
                {
                    _context.Suppliers.Add(obj);
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Saved Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "mobile No Already Present";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("UpdateSupplier")]
        public ApiResponse UpdateSupplier([FromBody] Supplier obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var response = _context.Suppliers.SingleOrDefault(m => m.supplierId == obj.supplierId);
                if (response != null)
                {
                    response.supplierName = obj.supplierName;
                    response.address = obj.address;
                    response.mobileNo = obj.mobileNo;
                    response.gstNo = obj.gstNo;
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Updated Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Record Not Found";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("DeleteSupplierById")]
        public ApiResponse DeleteSupplierById(int id)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var record = _context.Suppliers.SingleOrDefault(m => m.supplierId == id);
                _context.Suppliers.Remove(record);
                _context.SaveChanges();
                _res.Result = true;
                _res.Message = "Deleted Successfully";
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        #endregion

        #region Product
        [HttpGet("GetProductByCategoryId")]
        public ApiResponse GetProductByCategoryId(int categoryId)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var list = _context.Products.SingleOrDefault(m => m.categoryId == categoryId);
                if (list != null)
                {
                    _res.Result = true;
                    _res.Data = list;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "No Record Found with Category id";
                }
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("GetAllProduct")]
        public ApiResponse GetAllProduct()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = (from pro in _context.Products
                           join cat in _context.Categorys on pro.categoryId equals cat.categoryId
                           select new
                           {
                               isExpiryProduct = pro.isExpiryProduct,
                               expiryDate = pro.expiryDate,
                               price = pro.price,
                               description = pro.description,
                               longName = pro.longName,
                               shortName = pro.shortName,
                               productId = pro.productId,
                               categoryName = cat.categoryName,
                               imageName = pro.imageName
                           }).OrderByDescending(m => m.productId).ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("AddProduct")]
        public ApiResponse AddProduct([FromBody] Product obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var isExist = _context.Products.SingleOrDefault(m => m.shortName == obj.shortName);
                if (isExist == null)
                {
                    obj.createdDate = DateTime.Now;
                    _context.Products.Add(obj);
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Saved Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Product Name Already Present";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("UpdateProduct")]
        public ApiResponse UpdateProduct([FromBody] Product obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var response = _context.Products.SingleOrDefault(m => m.productId == obj.productId);
                if (response != null)
                {
                    response.shortName = obj.shortName;
                    response.categoryId = obj.categoryId;
                    response.longName = obj.longName;
                    response.price = obj.price;
                    response.isExpiryProduct = obj.isExpiryProduct;
                    response.description = obj.description;
                    response.expiryDate = obj.expiryDate;
                    _context.SaveChanges();
                    _res.Result = true;
                    _res.Message = "Updated Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Record Not Found";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("DeleteProductById")]
        public ApiResponse DeleteProductById(int id)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var record = _context.Products.SingleOrDefault(m => m.productId == id);
                _context.Products.Remove(record);
                _context.SaveChanges();
                _res.Result = true;
                _res.Message = "Deleted Successfully";
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }
        #endregion

        #region Purchase
        [HttpGet("GetPurchaseSupplierId")]
        public ApiResponse GetPurchaseSupplierId(int supplierId)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var list = _context.Purchases.SingleOrDefault(m => m.supplierId == supplierId);
                if (list != null)
                {
                    _res.Result = true;
                    _res.Data = list;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "No Record Found with Category id";
                }
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("GetAllPurchaseList")]
        public ApiResponse GetAllPurchaseList()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = (from pro in _context.Purchases
                           join cat in _context.Suppliers on pro.supplierId equals cat.supplierId

                           select new
                           {
                               purchaseId = pro.purchaseId,
                               totalBillAmount = pro.totalBillAmount,
                               totalQuantity = pro.totalQuantity,
                               purchaseDate = pro.purchaseDate,
                               supplierName = cat.supplierName,
                           }).OrderByDescending(m => m.purchaseId).ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("openPurchaseByPurchaseId")]
        public ApiResponse openPurchaseByPurchaseId(int purchaseId)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                PurchaseViewModel sale = new PurchaseViewModel();
                var saleInfo = _context.Purchases.SingleOrDefault(m => m.purchaseId == purchaseId);
                if (saleInfo != null)
                {
                    sale.purchaseDate = saleInfo.purchaseDate;
                    sale.supplierId = saleInfo.supplierId;
                    sale.totalBillAmount = saleInfo.totalBillAmount;
                    sale.totalQuantity = saleInfo.totalQuantity;
                    var saleProducts = _context.PurchaseProducts.Where(m => m.purchaseId == purchaseId).ToList();
                    sale.PurchaseProducts = saleProducts;
                    _res.Result = true;
                    _res.Data = sale;
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "No Records Found with Sale Id";
                    return _res;
                }


            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }


        [HttpPost("AddPurchase")]
        public ApiResponse AddPurchase([FromBody] PurchaseViewModel obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                Purchase _sale = new Purchase()
                {
                    purchaseDate = obj.purchaseDate,
                    supplierId = obj.supplierId,
                    totalBillAmount = obj.totalBillAmount,
                    totalQuantity = obj.totalQuantity
                };
                _context.Purchases.Add(_sale);
                _context.SaveChanges();
                foreach (var item in obj.PurchaseProducts)
                {
                    item.purchaseId = _sale.purchaseId;
                    _context.PurchaseProducts.Add(item);
                    _context.SaveChanges();

                    var isStock = _context.StockMasters.SingleOrDefault(m => m.productId == item.productId);
                    if (isStock == null)
                    {
                        StockMaster _stock = new StockMaster()
                        {
                            createdDate = DateTime.Now,
                            lastUpdatedDate = DateTime.Now,
                            productId = item.productId,
                            quantity = item.quantity
                        };
                        _context.StockMasters.Add(_stock);
                        _context.SaveChanges();
                    }
                    else
                    {
                        isStock.quantity = isStock.quantity + item.quantity;
                        _context.SaveChanges();
                    }

                }

                _res.Result = true;
                _res.Message = "Purchase Entry Created n Stock Updated Successfully";
                return _res;

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("UpdatePurchase")]
        public ApiResponse UpdatePurchase([FromBody] PurchaseViewModel obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var response = _context.Purchases.SingleOrDefault(m => m.purchaseId == obj.purchaseId);
                if (response != null)
                {

                    response.purchaseDate = obj.purchaseDate;
                    response.supplierId = obj.supplierId;
                    response.totalBillAmount = obj.totalBillAmount;
                    response.totalQuantity = obj.totalQuantity;
                    _context.SaveChanges();
                    foreach (var item in obj.PurchaseProducts)
                    {
                        if (item.purchaseProductId == 0)
                        {
                            item.purchaseId = obj.purchaseId;
                            _context.PurchaseProducts.Add(item);
                            _context.SaveChanges();
                            var isStock = _context.StockMasters.SingleOrDefault(m => m.productId == item.productId);
                            if (isStock == null)
                            {
                                StockMaster _stock = new StockMaster()
                                {
                                    createdDate = DateTime.Now,
                                    lastUpdatedDate = DateTime.Now,
                                    productId = item.productId,
                                    quantity = item.quantity
                                };
                                _context.StockMasters.Add(_stock);
                                _context.SaveChanges();
                            }
                            else
                            {
                                isStock.quantity = isStock.quantity + item.quantity;
                                _context.SaveChanges();
                            }
                        }
                    }
                    _res.Result = true;
                    _res.Message = "Updated Successfully";
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "Record Not Found";
                    return _res;
                }

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("DeletePurchaseById")]
        public ApiResponse DeletePurchaseById(int id)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var recordall = _context.PurchaseProducts.Where(m => m.purchaseId == id).ToList();
                _context.PurchaseProducts.RemoveRange(recordall);
                _context.SaveChanges();
                var record = _context.Purchases.SingleOrDefault(m => m.purchaseId == id);
                _context.Purchases.Remove(record);
                _context.SaveChanges();
                foreach (var item in recordall)
                {
                    var isStock = _context.StockMasters.SingleOrDefault(m => m.productId == item.productId);
                    isStock.quantity = isStock.quantity - item.quantity;
                    _context.SaveChanges();
                }


                _res.Result = true;
                _res.Message = "Deleted Successfully";
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }
        #endregion

        #region StockMaster
        [HttpGet("checkProductStock")]
        public ApiResponse checkProductStock(int productId)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var stock = _context.StockMasters.SingleOrDefault(m => m.productId == productId);
                if (stock != null)
                {
                    if (stock.quantity != 0)
                    {
                        _res.Result = true;
                        _res.Data = stock;
                        _res.Message = "Stock Available";
                    }
                    else
                    {
                        _res.Result = false;
                        _res.Message = "No Stock Available";
                    }
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "No Stock Available";
                }

                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("GetStock")]
        public ApiResponse GetStock()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = (from pro in _context.StockMasters
                           join cat in _context.Products on pro.productId equals cat.productId

                           select new
                           {
                               stockId = pro.stockId,
                               createdDate = pro.createdDate,
                               lastUpdatedDate = pro.lastUpdatedDate,
                               quantity = pro.quantity,
                               shortName = cat.shortName,
                           }).OrderByDescending(m => m.stockId).ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }



        #endregion

        #region Sale API

        [HttpGet("GetAllSaleList")]
        public ApiResponse GetAllSaleList()
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var all = (from sale in _context.Sales
                           join product in _context.Customers on sale.customerId equals product.customerId
                           select new
                           {
                               saleId = sale.saleId,
                               saleDate = sale.saleDate,
                               totalQuantity = sale.totalQuantity,
                               paymentMode = sale.paymentMode,
                               totalBillAmount = sale.totalBillAmount,
                               payementDetails = sale.payementDetails,
                               name = product.name,
                           }).OrderByDescending(m => m.saleId).ToList();
                _res.Result = true;
                _res.Data = all;
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpGet("openSaleBySaleId")]
        public ApiResponse openSaleBySaleId(int saleId)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                SaleViewMOdel sale = new SaleViewMOdel();
                var saleInfo = _context.Sales.SingleOrDefault(m => m.saleId == saleId);
                if (saleInfo != null)
                {
                    var cus = _context.Customers.SingleOrDefault(m => m.customerId == saleInfo.customerId);
                    sale.aadharCard = cus.aadharCard;
                    sale.address = cus.address;
                    sale.email = cus.email;
                    sale.mobileNo = cus.mobileNo;
                    sale.name = cus.name;
                    sale.payementDetails = saleInfo.payementDetails;
                    sale.paymentMode = saleInfo.paymentMode;
                    sale.saleDate = saleInfo.saleDate;
                    sale.totalBillAmount = saleInfo.totalBillAmount;
                    sale.totalQuantity = saleInfo.totalQuantity;
                    var saleProducts = _context.SaleProducts.Where(m => m.saleId == saleId).ToList();
                    sale.SaleProducts = saleProducts;
                    _res.Result = true;
                    _res.Data = sale;
                    return _res;
                }
                else
                {
                    _res.Result = false;
                    _res.Message = "No Records Found with Sale Id";
                    return _res;
                }


            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        [HttpPost("CreateSale")]
        public ApiResponse CreateSale([FromBody] SaleViewMOdel obj)
        {
            ApiResponse _res = new ApiResponse();
            if (!ModelState.IsValid)
            {
                _res.Result = false;
                _res.Message = "Validation Error";
                return _res;
            }
            try
            {
                var isCust = _context.Customers.SingleOrDefault(m => m.mobileNo == obj.mobileNo);
                var custId = 0;
                if (isCust == null)
                {
                    Stock_Customer _cust = new Stock_Customer()
                    {
                        aadharCard = obj.aadharCard,
                        name = obj.name,
                        address = obj.address,
                        email = obj.email,
                        mobileNo = obj.mobileNo
                    };
                    _context.Customers.Add(_cust);
                    _context.SaveChanges();
                    custId = _cust.customerId;
                }
                else
                {
                    custId = isCust.customerId;
                }
                Sales _sale = new Sales()
                {
                    customerId = custId,
                    payementDetails = obj.payementDetails,
                    paymentMode = obj.paymentMode,
                    saleDate = obj.saleDate,
                    totalBillAmount = obj.totalBillAmount,
                    totalQuantity = obj.totalQuantity
                };
                _context.Sales.Add(_sale);
                _context.SaveChanges();
                foreach (var item in obj.SaleProducts)
                {
                    item.saleId = _sale.saleId;
                    _context.SaleProducts.Add(item);


                    var isStock = _context.StockMasters.SingleOrDefault(m => m.productId == item.productId);
                    if (isStock != null)
                    {
                        isStock.quantity = isStock.quantity - item.quantity;
                        _context.SaveChanges();
                    }
                }
                _res.Result = true;
                _res.Message = "Sale Entry Created Successfully";
                return _res;

            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }
        [HttpGet("RemoveSaleBySaleId")]
        public ApiResponse RemoveSaleBySaleId(int id)
        {
            ApiResponse _res = new ApiResponse();
            try
            {
                var recordall = _context.SaleProducts.Where(m => m.saleId == id).ToList();
                _context.SaleProducts.RemoveRange(recordall);
                _context.SaveChanges();
                var record = _context.Sales.SingleOrDefault(m => m.saleId == id);
                _context.Sales.Remove(record);

                _context.SaveChanges();
                foreach (var item in recordall)
                {
                    var isStock = _context.StockMasters.SingleOrDefault(m => m.productId == item.productId);
                    isStock.quantity = isStock.quantity + item.quantity;
                    _context.SaveChanges();
                }
                _res.Result = true;
                _res.Message = "Deleted Successfully";
                return _res;
            }
            catch (Exception ex)
            {
                _res.Result = false;
                _res.Message = ex.InnerException.Message;
                return _res;
            }
        }

        //[HttpPost("updateSale")]
        //public ApiResponse updateSale([FromBody] SaleViewMOdel obj)
        //{
        //    ApiResponse _res = new ApiResponse();
        //    if (!ModelState.IsValid)
        //    {
        //        _res.Result = false;
        //        _res.Message = "Validation Error";
        //        return _res;
        //    }
        //    try
        //    {
        //        Sales _sale = new Sales()
        //        {
        //            customerId = obj.customerId,
        //            payementDetails = obj.payementDetails,
        //            paymentMode = obj.paymentMode,
        //            saleDate = obj.saleDate,
        //            totalBillAmount = obj.totalBillAmount,
        //            totalQuantity = obj.totalQuantity
        //        };
        //        _context.Sales.Add(_sale);
        //        _context.SaveChanges();
        //        foreach (var item in obj.SaleProducts)
        //        {
        //            item.saleId = _sale.saleId;
        //            _context.SaleProducts.Add(item);
        //            _context.SaveChanges();
        //        }
        //        _res.Result = true;
        //        _res.Message = "Sale Entry Created Successfully";
        //        return _res;

        //    }
        //    catch (Exception ex)
        //    {
        //        _res.Result = false;
        //        _res.Message = ex.InnerException.Message;
        //        return _res;
        //    }
        //}
        #endregion
    }
}
