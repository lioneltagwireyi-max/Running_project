using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PhoneFitService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        int RegisterUser(UserAccount userAccount);

        [OperationContract]
        LoginInfo LoginUser(string email, string passwordHash);

        [OperationContract]
        List<PhoneCatalogue> GetActivePhones();

        [OperationContract]
        List<ManagerProductInfo> GetManagerProducts();

        [OperationContract]
        List<BrandInfo> GetActiveBrands();

        [OperationContract]
        List<ManagerBrandInfo> GetManagerBrands();

        [OperationContract]
        int AddBrand(string brandName, string brandDescription);

        [OperationContract]
        bool ChangeBrandStatus(int brandID, bool isActive);

        [OperationContract]
        ManagerBrandInfo GetManagerBrandByID(int brandID);

        [OperationContract]
        int UpdateBrand(int brandID, ManagerBrandInfo brandInfo);

        [OperationContract]
        List<ManagerVariantInfo> GetManagerVariants(int phoneModelID);

        [OperationContract]
        int AddVariant(int phoneModelID, ManagerVariantInfo variantInfo);

        [OperationContract]
        ManagerVariantInfo GetManagerVariantByID(int variantID);

        [OperationContract]
        int UpdateVariant(int variantID, ManagerVariantInfo variantInfo);

        [OperationContract]
        bool ChangeVariantStatus(int variantID, bool isActive);

        [OperationContract]
        int AddProduct(NewProductInfo product);

        [OperationContract]
        ManagerProductDetails GetManagerProductByID(int phoneModelID);

        [OperationContract]
        int UpdateProduct(int phoneModelID, ManagerProductDetails product);

        [OperationContract]
        bool ChangeProductStatus(int phoneModelID, bool isActive);

        [OperationContract]
        PhoneCatalogue GetPhoneByID(int phoneModelID);

        [OperationContract]
        List<PhoneVariant> GetVariantsByPhoneID(int phoneModelID);

        [OperationContract]
        PhoneSpecificationDto GetSpecificationByPhoneID(int phoneModelID);

        [OperationContract]
        bool AddToCart(int userID, int variantID, int quantity);

        [OperationContract]
        List<CartItemInfo> GetCartItems(int userID);

        [OperationContract]
        bool ChangeCartItemQuantity(int userID, int cartItemID, int quantityChange);

        [OperationContract]
        bool RemoveCartItem(int userID, int cartItemID);

        [OperationContract]
        bool ClearCart(int userID);

        [OperationContract]
        OrderResult PlaceOrder(int userID);

        [OperationContract]
        List<CustomerOrderInfo> GetCustomerOrders(int userID);

        [OperationContract]
        List<ManagerOrderInfo> GetManagerOrders();

        [OperationContract]
        InvoiceInfo GetManagerOrderDetails(int orderID);

        [OperationContract]
        InvoiceInfo GetInvoice(int orderID, int userID);

        [OperationContract]
        List<UserInfo> GetUsers();

        [OperationContract]
        List<PhoneCatalogue> GetActivePhonesByName();

        [OperationContract]
        List<PhoneCatalogue> GetActivePhonesPriceLowToHigh();

        [OperationContract]
        List<PhoneCatalogue> GetActivePhonesPriceHighToLow();

        [OperationContract]
        int GetTotalOrders();

        [OperationContract]
        decimal GetTotalRevenue();

        [OperationContract]
        int GetTotalUnitsSold();

        [OperationContract]
        int GetDifferentProductsSold();

        [OperationContract]
        int GetProductsOnHand();

        [OperationContract]
        int GetLowStockVariantCount();

        [OperationContract]
        List<ProductSalesInfo> GetProductSales();

        [OperationContract]
        List<UserRegistrationInfo> GetUserRegistrationsPerDay();

        [OperationContract]
        List<ProductSalesInfo> GetProductSalesByPhoneID(int phoneModelID);

        [OperationContract]
        List<UserRegistrationInfo> GetUserRegistrationsByDate(DateTime registrationDate);
    }

    [DataContract]
    public class LoginInfo
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string LoginStatus { get; set; }
    }

    [DataContract]
    public class CartItemInfo
    {
        [DataMember]
        public int CartItemID { get; set; }

        [DataMember]
        public int VariantID { get; set; }

        [DataMember]
        public int PhoneModelID { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public string VariantDescription { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public decimal LineTotal { get; set; }

        [DataMember]
        public int StockQuantity { get; set; }
    }

    // Catalogue class
    [DataContract]
    public class PhoneCatalogue
    {
        [DataMember]
        public int PhoneModelID { get; set; }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public decimal StartingPrice { get; set; }

        [DataMember]
        public int StockQuantity { get; set; }
    }

    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string UserEmail { get; set; }

        [DataMember]
        public string UserPhoneNumber { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public bool UserIsActive { get; set; }

        [DataMember]
        public DateTime UserAccountCreated { get; set; }
    }

    [DataContract]
    public class ManagerProductInfo
    {
        [DataMember]
        public int PhoneModelID { get; set; }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string OperatingSystem { get; set; }

        [DataMember]
        public int VariantCount { get; set; }

        [DataMember]
        public int TotalStock { get; set; }

        [DataMember]
        // int? - an integer that is allowed to have no value
        public int? ReleaseYear { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime DateAdded { get; set; }
    }

    [DataContract]
    public class BrandInfo
    {
        [DataMember]
        public int BrandID { get; set; }

        [DataMember]
        public string BrandName { get; set; }
    }

    [DataContract]
    public class NewProductInfo
    {
        // PhoneModel information

        [DataMember]
        public int BrandID { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string OperatingSystem { get; set; }

        [DataMember]
        public int? ReleaseYear { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ImagePath { get; set; }


        // PhoneSpecification information

        [DataMember]
        public string Processor { get; set; }

        [DataMember]
        public decimal? ScreenSize { get; set; }

        [DataMember]
        public string ScreenType { get; set; }

        [DataMember]
        public int? RefreshRate { get; set; }

        [DataMember]
        public int? BatteryCapacity { get; set; }

        [DataMember]
        public decimal? RearCameraMP { get; set; }

        [DataMember]
        public decimal? FrontCameraMP { get; set; }

        [DataMember]
        public bool Supports5G { get; set; }

        [DataMember]
        public bool DualSIM { get; set; }

        [DataMember]
        public bool ExpandableStorage { get; set; }

        [DataMember]
        public string WaterResistance { get; set; }


        // Initial PhoneVariant information

        [DataMember]
        public int RAMGB { get; set; }

        [DataMember]
        public int StorageGB { get; set; }

        [DataMember]
        public string Colour { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int StockQuantity { get; set; }

        [DataMember]
        public int LowStockLevel { get; set; }
    }

    [DataContract]
    public class ManagerProductDetails
    {
        // PhoneModel information

        [DataMember]
        public int PhoneModelID { get; set; }

        [DataMember]
        public int BrandID { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string OperatingSystem { get; set; }

        [DataMember]
        public int? ReleaseYear { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ImagePath { get; set; }


        // PhoneSpecification information

        [DataMember]
        public string Processor { get; set; }

        [DataMember]
        public decimal? ScreenSize { get; set; }

        [DataMember]
        public string ScreenType { get; set; }

        [DataMember]
        public int? RefreshRate { get; set; }

        [DataMember]
        public int? BatteryCapacity { get; set; }

        [DataMember]
        public decimal? RearCameraMP { get; set; }

        [DataMember]
        public decimal? FrontCameraMP { get; set; }

        [DataMember]
        public bool Supports5G { get; set; }

        [DataMember]
        public bool DualSIM { get; set; }

        [DataMember]
        public bool ExpandableStorage { get; set; }

        [DataMember]
        public string WaterResistance { get; set; }
    }

    [DataContract]
    public class ManagerBrandInfo
    {
        [DataMember]
        public int BrandID { get; set; }

        [DataMember]
        public string BrandName { get; set; }

        [DataMember]
        public string BrandDescription { get; set; }

        [DataMember]
        public bool BrandIsActive { get; set; }
    }

    [DataContract]
    public class ManagerVariantInfo
    {
        [DataMember]
        public int VariantID { get; set; }

        [DataMember]
        public int PhoneModelID { get; set; }

        [DataMember]
        public int RAMGB { get; set; }

        [DataMember]
        public int StorageGB { get; set; }

        [DataMember]
        public string Colour { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int StockQuantity { get; set; }

        [DataMember]
        public int LowStockLevel { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }

    [DataContract]
    public class OrderResult
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string OrderStatus { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }
    }

    [DataContract]
    public class InvoiceInfo
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerEmail { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public string OrderStatus { get; set; }

        [DataMember]
        public decimal Subtotal { get; set; }

        [DataMember]
        public decimal DiscountAmount { get; set; }

        [DataMember]
        public decimal ShippingAmount { get; set; }

        [DataMember]
        public decimal VATAmount { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }

        [DataMember]
        public List<InvoiceItemInfo> Items { get; set; }
    }

    [DataContract]
    public class InvoiceItemInfo
    {
        [DataMember]
        public int VariantID { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string VariantDescription { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public decimal LineTotal { get; set; }
    }

    [DataContract]
    public class CustomerOrderInfo
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }

        [DataMember]
        public string OrderStatus { get; set; }
    }

    [DataContract]
    public class ManagerOrderInfo
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerEmail { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public string OrderStatus { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }
    }

    // products sold
    [DataContract]
    public class ProductSalesInfo
    {
        [DataMember]
        public int PhoneModelID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int UnitsSold { get; set; }
    }

    // registrationa per day
    [DataContract]
    public class UserRegistrationInfo
    {
        [DataMember]
        public DateTime RegistrationDate { get; set; }

        [DataMember]
        public int UsersRegistered { get; set; }
    }

    [DataContract]
    public class PhoneSpecificationDto
    {
        [DataMember] public int SpecificationID { get; set; }
        [DataMember] public int PhoneModelID { get; set; }
        [DataMember] public string Processor { get; set; }
        [DataMember] public decimal ScreenSize { get; set; }
        [DataMember] public string ScreenType { get; set; }
        [DataMember] public int RefreshRate { get; set; }
        [DataMember] public int BatteryCapacity { get; set; }
        [DataMember] public decimal RearCameraMP { get; set; }
        [DataMember] public decimal FrontCameraMP { get; set; }
        [DataMember] public bool Supports5G { get; set; }
        [DataMember] public bool DualSIM { get; set; }
        [DataMember] public bool ExpandableStorage { get; set; }
        [DataMember] public string WaterResistance { get; set; }
    }

}
