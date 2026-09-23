using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PhoneFitService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        PhonefitDataDataContext db = new PhonefitDataDataContext();

        public LoginInfo LoginUser(string email, string passwordHash)
        {
            // Authenticate the users credentials
            var user =
                (from account in db.UserAccounts
                 where account.UserEmail == email
                 && account.UserPasswordHash == passwordHash
                 select account).SingleOrDefault();

            if (user == null)
            {
                // Case - User not found
                return new LoginInfo
                {
                    UserID = 0,
                    RoleName = null,
                    LoginStatus = "Invalid"
                };
            }

            if (user.UserIsActive == false)
            {
                // Case - User account is inactive
                return new LoginInfo
                {
                    UserID = 0,
                    RoleName = null,
                    LoginStatus = "Inactive"
                };
            }

            // Get the users role
            var userRole =
                (from role in db.Roles
                 where role.RoleID == user.RoleID
                 select role).SingleOrDefault();

            if (userRole == null)
            {
                // Case - Role not found
                return new LoginInfo
                {
                    UserID = 0,
                    RoleName = null,
                    LoginStatus = "Invalid"
                };
            }

            return new LoginInfo
            {
                // Case - User successfully authenticated
                UserID = user.UserID,
                RoleName = userRole.RoleName,
                LoginStatus = "Success"
            };
        }

        public int RegisterUser(UserAccount userAccount)
        {
           var checkEmail =
                (from user in db.UserAccounts
                 where user.UserEmail == userAccount.UserEmail
                 select user).SingleOrDefault();

            if (checkEmail == null)
            {
                var customerRole =
                    (from role in db.Roles
                     where role.RoleName == "Customer"
                     select role).SingleOrDefault();

                if (customerRole == null)
                {
                    return 1;   // Customer role was not found
                }

                var newUser = new UserAccount
                {
                    RoleID = customerRole.RoleID,
                    UserEmail = userAccount.UserEmail,
                    UserPasswordHash = userAccount.UserPasswordHash,
                    UserFirstName = userAccount.UserFirstName,
                    UserSurname = userAccount.UserSurname,
                    UserPhoneNumber = userAccount.UserPhoneNumber,
                    UserIsActive = true,
                    UserAccountCreated = DateTime.Now
                };

                db.UserAccounts.InsertOnSubmit(newUser);

                try
                {
                    db.SubmitChanges();
                    return 0;   // Account registered successfully
                }
                catch (Exception)
                {
                    return 1; // Registration or database operation failed
                }
            }
            else
            {
                return 2; // Email already exists
            }
        }

        public List<PhoneCatalogue> GetActivePhones()
        {
            List<PhoneCatalogue> phoneList = new List<PhoneCatalogue>();

            var activePhones =
                (from phone in db.PhoneModels
                 where phone.IsActive == true
                 select phone).ToList();

            foreach(PhoneModel phone in activePhones)
            {
                var brand =
                    (from b in db.Brands
                     where b.BrandID == phone.BrandID
                     select b).SingleOrDefault();

                var variant =
                    (from v in db.PhoneVariants
                     where v.PhoneModelID == phone.PhoneModelID
                     && v.IsActive == true
                     orderby v.Price ascending
                     select v).FirstOrDefault();

                if(brand != null && variant != null)
                {
                    PhoneCatalogue cataloguePhone = new PhoneCatalogue
                    {
                        PhoneModelID = phone.PhoneModelID,
                        BrandName = brand.BrandName,
                        ModelName = phone.ModelName,
                        Description = phone.Description,
                        ImagePath = phone.ImagePath,
                        StartingPrice = variant.Price,
                        StockQuantity = variant.StockQuantity
                    };

                    phoneList.Add(cataloguePhone);
                }
            }

            return phoneList;
        }

        public PhoneCatalogue GetPhoneByID(int phoneModelID)
        {
            var phone =
                (from p in db.PhoneModels
                 where p.PhoneModelID == phoneModelID
                 && p.IsActive == true
                 select p).SingleOrDefault();

            if (phone == null)
            {
                return null;
            }

            var brand =
                (from b in db.Brands
                 where b.BrandID == phone.BrandID
                 select b).SingleOrDefault();

            var variant =
                (from v in db.PhoneVariants
                 where v.PhoneModelID == phone.PhoneModelID
                 && v.IsActive == true
                 orderby v.Price ascending
                 select v).FirstOrDefault();

            if(brand == null || variant == null)
            {
                return null;
            }

            PhoneCatalogue selectedPhone = new PhoneCatalogue
            {
                PhoneModelID = phone.PhoneModelID,
                BrandName = brand.BrandName,
                ModelName = phone.ModelName,
                Description = phone.Description,
                ImagePath = phone.ImagePath,
                StartingPrice = variant.Price,
                StockQuantity = variant.StockQuantity
            };

            return selectedPhone;
        }

        public List<PhoneVariant> GetVariantsByPhoneID(int phoneModelID)
        {
            var variants =
                (from variant in db.PhoneVariants
                 where variant.PhoneModelID == phoneModelID
                 && variant.IsActive == true
                 orderby variant.Price ascending
                 select variant).ToList();

            return variants;
        }

        public PhoneSpecificationDto GetSpecificationByPhoneID(int phoneModelID)
        {
            using (var db = new PhonefitDataDataContext())
            {
                return db.PhoneSpecifications
                         .Where(s => s.PhoneModelID == phoneModelID)
                         .Select(s => new PhoneSpecificationDto
                         {
                             SpecificationID = s.SpecificationID,
                             PhoneModelID = s.PhoneModelID,
                             Processor = s.Processor,
                             ScreenSize = (decimal)s.ScreenSize,
                             ScreenType = s.ScreenType,
                             RefreshRate = (int)s.RefreshRate,
                             BatteryCapacity = (int)s.BatteryCapacity,
                             RearCameraMP = (decimal) s.RearCameraMP,
                             FrontCameraMP =(decimal) s.FrontCameraMP,
                             Supports5G = s.Supports5G,
                             DualSIM = s.DualSIM,
                             ExpandableStorage = s.ExpandableStorage,
                             WaterResistance = s.WaterResistance
                         })
                         .FirstOrDefault();
            }
        }

        public bool AddToCart(int userID, int variantID, int quantity)
        {
            // A cart item must have a positive quantity.
            if (quantity <= 0)
            {
                return false;
            }

            // Finf the user who ia trying to add the product
            var user =
                (from account in db.UserAccounts
                 where account.UserID == userID
                 && account.UserIsActive == true
                 select account).SingleOrDefault();

            // User does not exist or account is inactive
            if (user == null)
            {
                return false;
            }

            // Find the variant selected by the customer
            var variant =
                (from phoneVariant in db.PhoneVariants
                 where phoneVariant.VariantID == variantID
                 && phoneVariant.IsActive == true
                 select phoneVariant).SingleOrDefault();

            // Variant does not exist or is inactive
            if (variant == null)
            {
                return false;
            }

            // Confirm that the phone model linked to the variant is active.
            var phoneModel =
                (from phone in db.PhoneModels
                 where phone.PhoneModelID == variant.PhoneModelID
                 && phone.IsActive == true
                 select phone).SingleOrDefault();

            if (phoneModel == null)
            {
                return false;
            }

            // The requested quantity must not exceed available stock.
            if (quantity > variant.StockQuantity)
            {
                return false;
            }

            // Find the user's active cart.
            var activeCart =
                (from cart in db.Carts
                 where cart.UserID == userID
                 && cart.IsActive == true
                 select cart).SingleOrDefault();

            // Create a new cart if the user does not have an active cart.
            if (activeCart == null)
            {
                activeCart = new Cart
                {
                    UserID = userID,
                    DateCreated = DateTime.Now,
                    IsActive = true
                };

                db.Carts.InsertOnSubmit(activeCart);
            }

            // Find out whether this variant is already in the active cart.
            var existingCartItem =
                (from item in db.CartItems
                 where item.Cart == activeCart
                 && item.VariantID == variantID
                 select item).SingleOrDefault();

            if (existingCartItem == null)
            {
                // Add the variant as a new cart item.
                CartItem newCartItem = new CartItem
                {
                    Cart = activeCart,
                    VariantID = variantID,
                    Quantity = quantity,
                    DateAdded = DateTime.Now
                };

                db.CartItems.InsertOnSubmit(newCartItem);
            }
            else
            {
                // Increase the quantity if the variant is already in the cart.
                int updatedQuantity = existingCartItem.Quantity + quantity;

                if (updatedQuantity > variant.StockQuantity)
                {
                    return false;
                }

                existingCartItem.Quantity = updatedQuantity;
            }

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public List<CartItemInfo> GetCartItems(int userID)
        {
            // empty list that will store the customers cart items
            List<CartItemInfo> cartItemList = new List<CartItemInfo>();

            // find the customers active cart
            var activeCart =
                (from cart in db.Carts
                 where cart.UserID == userID
                 && cart.IsActive == true
                 select cart).SingleOrDefault();

            if (activeCart == null)
            {
                return cartItemList;
            }

            // find all cartitems that belong to the active cart
            var cartItems = (
                from item in db.CartItems
                where item.CartID == activeCart.CartID
                select item).ToList();

            // Go through each cartItem
            foreach(CartItem item in cartItems)
            {
                // find the variant related to the item
                var variant = (
                    from phoneVariant in db.PhoneVariants
                    where phoneVariant.VariantID == item.VariantID
                    select phoneVariant).SingleOrDefault();

                // only continue if the variant exists
                if(variant != null)
                {
                    // find the phone model related to the variant
                    var phone = (
                        from phoneModel in db.PhoneModels
                        where phoneModel.PhoneModelID == variant.PhoneModelID
                        select phoneModel).SingleOrDefault();

                    if(phone != null)
                    {
                        // create the object that will be sent to the frontend
                        CartItemInfo cartItemInfo = new CartItemInfo
                        {
                            CartItemID = item.CartItemID,
                            VariantID = variant.VariantID,
                            PhoneModelID = phone.PhoneModelID,
                            ModelName = phone.ModelName,
                            ImagePath = phone.ImagePath,
                            VariantDescription = variant.RAMGB + " GB RAM · " + variant.StorageGB + " GB Storage · " + variant.Colour,
                            UnitPrice = variant.Price,
                            Quantity = item.Quantity,
                            LineTotal = variant.Price * item.Quantity,
                            StockQuantity = variant.StockQuantity
                        };

                        // add the cartiteminfo to the list
                        cartItemList.Add(cartItemInfo);
                    }
                }
            }
            
            // retuen all customer cart items to the frontend
            return cartItemList;
        }

        public bool ChangeCartItemQuantity(int userID, int cartItemID, int quantityChange)
        {
            // Quantity can only be increased by 1 or decreased by 1
            if (quantityChange != 1 && quantityChange != -1)
            {
                return false;
            }

            // Find the logged in customers active cart
            var activeCart = (
                from cart in db.Carts
                where cart.UserID == userID &&
                cart.IsActive == true
                select cart).SingleOrDefault();

            // Customer does not have an active cart
            if (activeCart == null)
            {
                return false;
            }

            // Find the selected item inside the customers active cart
            var cartItem = (
                from item in db.CartItems
                where item.CartID == activeCart.CartID &&
                item.CartItemID == cartItemID
                select item).SingleOrDefault();

            // Item does not exist inside the cart
            if(cartItem == null)
            {
                return false;
            }

            // Find the phone variant linked to the cart item
            var variant =
                (from phoneVariant in db.PhoneVariants
                 where phoneVariant.VariantID == cartItem.VariantID
                 && phoneVariant.IsActive == true
                 select phoneVariant).SingleOrDefault();

            // variant does not exist or is inactive
            if (variant == null)
            {
                return false;
            }

            // Calculate new quantity
            int newQuantity = cartItem.Quantity + quantityChange;

            // Remove the item if its quantity reaches zero.
            if (newQuantity <= 0)
            {
                db.CartItems.DeleteOnSubmit(cartItem);
            }
            else
            {
                // Do not allow the cart quantity to exceed stock.
                if (newQuantity > variant.StockQuantity)
                {
                    return false;
                }

                // Update the quantity
                cartItem.Quantity = newQuantity;
            }

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public bool RemoveCartItem(int userID, int cartItemID)
        {
            // find the logged in customers active cart
            var activeCart = (
                 from cart in db.Carts
                 where cart.UserID == userID &&
                 cart.IsActive == true
                 select cart).SingleOrDefault();

            // customer does not have an active cart
            if(activeCart == null)
            {
                return false;
            }

            // find the selected item inside the customers active cart
            var cartItem = (
                from item in db.CartItems
                where item.CartID == activeCart.CartID &&
                item.CartItemID == cartItemID
                select item).SingleOrDefault();

            // item not found in customer cart
            if(cartItem == null)
            {
                return false;
            }

            db.CartItems.DeleteOnSubmit(cartItem);

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public bool ClearCart(int userID)
        {
            // find the logged in customers active cart
            var activeCart =
                (from cart in db.Carts
                 where cart.UserID == userID
                 && cart.IsActive == true
                 select cart).SingleOrDefault();

            // customer has no active cart
            if (activeCart == null)
            {
                return false;
            }

            // find all the items inside the cart
            var cartItems =
                (from item in db.CartItems
                 where item.CartID == activeCart.CartID
                 select item).ToList();

            // cart is already empty
            if (cartItems.Count == 0)
            {
                return true;
            }

            db.CartItems.DeleteAllOnSubmit(cartItems);

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<UserInfo> GetUsers()
        {
            // list to store users
            List<UserInfo> userList = new List<UserInfo>();

            // get all registered users
            var users = (
                from user in db.UserAccounts
                orderby user.UserAccountCreated descending
                select user).ToList();

            foreach(UserAccount user in users)
            {
                // find the role
                var role = (
                    from r in db.Roles
                    where r.RoleID == user.RoleID
                    select r).SingleOrDefault();

                string roleName = "";
                if (role != null)
                {
                    roleName = role.RoleName;
                }

                UserInfo userInfo = new UserInfo
                {
                    UserID = user.UserID,
                    FullName = user.UserFirstName + " " + user.UserSurname,
                    UserEmail = user.UserEmail,
                    UserPhoneNumber = user.UserPhoneNumber,
                    RoleName = roleName,
                    UserIsActive = user.UserIsActive,
                    UserAccountCreated = user.UserAccountCreated
                };

                userList.Add(userInfo);
            }

            return userList;
        }

        public List<ManagerProductInfo> GetManagerProducts()
        {
            // empty list that will store all products to be displayed on the manager product management page
            List<ManagerProductInfo> productList = new List<ManagerProductInfo>();

            // get all the phone models
            var phones = (
                from phone in db.PhoneModels
                orderby phone.DateAdded descending
                select phone).ToList();

            // loop through each model
            foreach(PhoneModel phone in phones)
            {
                // find the brand linked to the phone
                var brand = (
                    from b in db.Brands
                    where b.BrandID == phone.BrandID
                    select b).SingleOrDefault();

                // find the variants linked to the phone
                var variants = (
                    from variant in db.PhoneVariants
                    where variant.PhoneModelID == phone.PhoneModelID
                    select variant).ToList();

                int variantCount = 0;
                int totalStock = 0;

                // calculate how many variants and total stock each phone has
                foreach(PhoneVariant phoneVariant in variants)
                {
                    variantCount++;
                    totalStock += phoneVariant.StockQuantity;
                }

                // create and add phone to list
                ManagerProductInfo product = new ManagerProductInfo
                {
                    PhoneModelID = phone.PhoneModelID,
                    BrandName = brand.BrandName,
                    ModelName = phone.ModelName,
                    OperatingSystem = phone.OperatingSystem,
                    ReleaseYear = phone.ReleaseYear,
                    VariantCount = variantCount,
                    TotalStock = totalStock,
                    IsActive = phone.IsActive,
                    DateAdded = phone.DateAdded
                };

                productList.Add(product);
            }

            return productList;

        }

        public bool ChangeProductStatus(int phoneModelID, bool isActive)
        {
            // find the phone model that the manager wants to update
            var phone = (
                from phoneModel in db.PhoneModels
                where phoneModel.PhoneModelID == phoneModelID
                select phoneModel).SingleOrDefault();

            // phone could not be found
            if(phone == null)
            {
                return false;
            }

            // change the active status of the phone
            phone.IsActive = isActive;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public List<BrandInfo> GetActiveBrands()
        {
            // empty list that will store active brands
            List<BrandInfo> brandList = new List<BrandInfo>();

            // find the active brands
            var brands = (
                from brand in db.Brands
                where brand.BrandIsActive == true
                orderby brand.BrandName ascending
                select brand).ToList();

            // add each active brand to the list
            foreach(Brand brand in brands)
            {
                BrandInfo brandInfo = new BrandInfo
                {
                    BrandID = brand.BrandID,
                    BrandName = brand.BrandName
                };

                brandList.Add(brandInfo);
            }

            return brandList;
        }

        public int AddProduct(NewProductInfo product)
        {
            // check that the product info was received
            if(product == null)
            {
                return 1;
            }

            // check that the selected brand exists and is active
            var brand = (
                from b in db.Brands
                where b.BrandID == product.BrandID &&
                b.BrandIsActive == true
                select b).SingleOrDefault();

            if(brand == null)
            {
                return 1;
            }

            // check whether the same phone model already exists under the same brand
            var existingPhone = (
                from phone in db.PhoneModels
                where phone.BrandID == product.BrandID &&
                phone.ModelName == product.ModelName
                select phone).SingleOrDefault();

            if(existingPhone != null)
            {
                return 2;
            }

            // create the new phone model
            PhoneModel newPhone = new PhoneModel
            {
                BrandID = product.BrandID,
                ModelName = product.ModelName,
                OperatingSystem = product.OperatingSystem,
                ReleaseYear = product.ReleaseYear,
                Description = product.Description,
                ImagePath = product.ImagePath,
                IsActive = true,
                DateAdded = DateTime.Now
            };

            // create the specs for the new phone
            PhoneSpecification newSpecs = new PhoneSpecification
            {
                Processor = product.Processor,
                ScreenSize = product.ScreenSize,
                ScreenType = product.ScreenType,
                RefreshRate = product.RefreshRate,
                BatteryCapacity = product.BatteryCapacity,
                RearCameraMP = product.RearCameraMP,
                FrontCameraMP = product.FrontCameraMP,
                Supports5G = product.Supports5G,
                DualSIM = product.DualSIM,
                ExpandableStorage = product.ExpandableStorage,
                WaterResistance = product.WaterResistance,

                // Link the specs to the new phone
                PhoneModel = newPhone
            };

            // create the first variant for the new phone
            PhoneVariant newVariant = new PhoneVariant
            {
                RAMGB = product.RAMGB,
                StorageGB = product.StorageGB,
                Colour = product.Colour,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                LowStockLevel = product.LowStockLevel,
                IsActive = true,

                // Link the variant to the new phone
                PhoneModel = newPhone
            };

            // prepare all 3 records to be added to the database
            db.PhoneModels.InsertOnSubmit(newPhone);
            db.PhoneSpecifications.InsertOnSubmit(newSpecs);
            db.PhoneVariants.InsertOnSubmit(newVariant);

            try
            {
                db.SubmitChanges();
                return 0;
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return 1;
            }

            //0 - Product added successfully
            //1 - Product could not be added
            //2 - Product already exists for that brand
        }

        public ManagerProductDetails GetManagerProductByID(int phoneModelID)
        {
            // find the phone selected by the manager
            var phone = (
                from phoneModel in db.PhoneModels
                where phoneModel.PhoneModelID == phoneModelID
                select phoneModel).SingleOrDefault();

            // phone not found
            if(phone == null)
            {
                return null;
            }

            // find the specs related to the phone
            var specs = (
                from spec in db.PhoneSpecifications
                where spec.PhoneModelID == phoneModelID
                select spec).SingleOrDefault();

            // specs not found
            if(specs == null)
            {
                return null;
            }

            // combine phoneModel and phoneSpecification info into one object
            ManagerProductDetails product = new ManagerProductDetails
            {
                PhoneModelID = phone.PhoneModelID,
                BrandID = phone.BrandID,
                ModelName = phone.ModelName,
                OperatingSystem = phone.OperatingSystem,
                ReleaseYear = phone.ReleaseYear,
                Description = phone.Description,
                ImagePath = phone.ImagePath,

                Processor = specs.Processor,
                ScreenSize = specs.ScreenSize,
                ScreenType = specs.ScreenType,
                RefreshRate = specs.RefreshRate,
                BatteryCapacity = specs.BatteryCapacity,
                RearCameraMP = specs.RearCameraMP,
                FrontCameraMP = specs.FrontCameraMP,
                Supports5G = specs.Supports5G,
                DualSIM = specs.DualSIM,
                ExpandableStorage = specs.ExpandableStorage,
                WaterResistance = specs.WaterResistance
            };

            return product;
        }

        public int UpdateProduct(int phoneModelID, ManagerProductDetails product)
        {
            if(product == null)
            {
                return 1; // update failed
            }

            // find the phone being edited
            var phone = (
                from phoneModel in db.PhoneModels
                where phoneModel.PhoneModelID == phoneModelID
                select phoneModel).SingleOrDefault();

            if(phone == null)
            {
                return 1;
            }

            // make sure the selected brand exists and is active
            var brand = (
                from b in db.Brands
                where b.BrandID == product.BrandID &&
                b.BrandIsActive == true
                select b).SingleOrDefault();

            if(brand == null)
            {
                return 1;
            }

            // check if another phone already uses the selected brand and model name
            var dupPhone = (
                from existingPhone in db.PhoneModels
                where existingPhone.BrandID == product.BrandID &&
                existingPhone.ModelName == product.ModelName &&
                existingPhone.PhoneModelID != phoneModelID
                select existingPhone).SingleOrDefault();

            if(dupPhone != null)
            {
                return 2; // duplicate phone found
            }

            // find the related specs
            var specs = (
                from spec in db.PhoneSpecifications
                where spec.PhoneModelID == phoneModelID
                select spec).SingleOrDefault();

            if(specs == null)
            {
                return 1;
            }

            // update the phone model information
            phone.BrandID = product.BrandID;
            phone.ModelName = product.ModelName;
            phone.OperatingSystem = product.OperatingSystem;
            phone.ReleaseYear = product.ReleaseYear;
            phone.Description = product.Description;
            phone.ImagePath = product.ImagePath;

            // update specification information
            specs.Processor = product.Processor;
            specs.ScreenSize = product.ScreenSize;
            specs.ScreenType = product.ScreenType;
            specs.RefreshRate = product.RefreshRate;
            specs.BatteryCapacity = product.BatteryCapacity;
            specs.RearCameraMP = product.RearCameraMP;
            specs.FrontCameraMP = product.FrontCameraMP;
            specs.Supports5G = product.Supports5G;
            specs.DualSIM = product.DualSIM;
            specs.ExpandableStorage = product.ExpandableStorage;
            specs.WaterResistance = product.WaterResistance;

            try
            {
                db.SubmitChanges();
                return 0; // updated successfully
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return 1;
            }
        }

        public List<ManagerBrandInfo> GetManagerBrands()
        {
            // empty list to store all brands
            List<ManagerBrandInfo> brandList = new List<ManagerBrandInfo>();

            // get all brands
            var brands = (
                from brand in db.Brands
                orderby brand.BrandName ascending
                select brand).ToList();

            foreach(Brand brand in brands)
            {
                ManagerBrandInfo brandInfo = new ManagerBrandInfo
                {
                    BrandID = brand.BrandID,
                    BrandName = brand.BrandName,
                    BrandDescription = brand.BrandDescription,
                    BrandIsActive = brand.BrandIsActive
                };

                brandList.Add(brandInfo);
            }

            return brandList;
        }

        public int AddBrand(string brandName, string brandDescription)
        {
            // check if the brand already exists
            var brand = (
                from b in db.Brands
                where b.BrandName == brandName
                select b).SingleOrDefault();

            if(brand != null)
            {
                return 2; // brand already esists
            }

            // create the new brand
            Brand newBrand = new Brand
            {
                BrandName = brandName,
                BrandDescription = brandDescription,
                BrandIsActive = true
            };

            db.Brands.InsertOnSubmit(newBrand);

            try
            {
                db.SubmitChanges();
                return 0; // brand added successfully
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return 1; // brand could not be added
            }
        }

        public bool ChangeBrandStatus(int brandID, bool isActive)
        {
            // find the selected brand
            var brand = (
                from b in db.Brands
                where b.BrandID == brandID
                select b).SingleOrDefault();

            if(brand == null)
            {
                return false;
            }

            // change the brand active status
            brand.BrandIsActive = isActive;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public ManagerBrandInfo GetManagerBrandByID(int brandID)
        {
            // find the selected brand
            var brand = (
                from b in db.Brands
                where b.BrandID == brandID
                select b).SingleOrDefault();

            if(brand == null)
            {
                return null;
            }

            ManagerBrandInfo brandInfo = new ManagerBrandInfo
            {
                BrandID = brand.BrandID,
                BrandName = brand.BrandName,
                BrandDescription = brand.BrandDescription,
                BrandIsActive = brand.BrandIsActive
            };

            return brandInfo;
        }

        public int UpdateBrand(int brandID, ManagerBrandInfo brandInfo)
        {
            if(brandInfo == null)
            {
                return 1; // brand could not be updated
            }

            // find the brand being edited
            var brand = (
                from b in db.Brands
                where b.BrandID == brandID
                select b).SingleOrDefault();

            if(brand == null)
            {
                return 1;
            }

            // check whether another already uses the new brand name
            var dupBrand = (
                from b in db.Brands
                where b.BrandName == brandInfo.BrandName &&
                b.BrandID != brandID
                select b).SingleOrDefault();

            if(dupBrand != null)
            {
                return 2; // duplicate brand found/another brand already uses that name
            }

            // update the selected brands information
            brand.BrandName = brandInfo.BrandName;
            brand.BrandDescription = brandInfo.BrandDescription;

            try
            {
                db.SubmitChanges();
                return 0; // brand updated successfully
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return 1;
            }
        }

        public List<ManagerVariantInfo> GetManagerVariants(int phoneModelID)
        {
            // empty list that will store all variants belonging to the selected phonemodel
            List<ManagerVariantInfo> variantList = new List<ManagerVariantInfo>();

            // get all variants belonging to the selected phone
            var variants = (
                from variant in db.PhoneVariants
                where variant.PhoneModelID == phoneModelID
                orderby variant.StorageGB ascending
                select variant).ToList();

            // convert each varaint into a ManagerVariantInfo object
            foreach(PhoneVariant variant in variants)
            {
                ManagerVariantInfo variantInfo = new ManagerVariantInfo
                {
                    VariantID = variant.VariantID,
                    PhoneModelID = variant.PhoneModelID,
                    RAMGB = variant.RAMGB,
                    StorageGB = variant.StorageGB,
                    Colour = variant.Colour,
                    Price = variant.Price,
                    StockQuantity = variant.StockQuantity,
                    LowStockLevel = variant.LowStockLevel,
                    IsActive = variant.IsActive
                };

                variantList.Add(variantInfo);
            }

            return variantList;
        }

        public int AddVariant(int phoneModelID, ManagerVariantInfo variantInfo)
        {
            if(variantInfo == null)
            {
                return 1; // variant could not be added
            }

            // find the phone the new variant will belong to
            var phone = (
                from p in db.PhoneModels
                where p.PhoneModelID == phoneModelID
                select p).SingleOrDefault();

            if(phone == null)
            {
                return 1;
            }

            // prevent the same variant from being added twice
            var existingVariant = (
                from variant in db.PhoneVariants
                where variant.PhoneModelID == phoneModelID &&
                variant.RAMGB == variantInfo.RAMGB &&
                variant.StorageGB == variantInfo.StorageGB &&
                variant.Colour == variantInfo.Colour
                select variant).SingleOrDefault();

            if(existingVariant != null)
            {
                return 2; // variant already exists
            }

            PhoneVariant newVariant = new PhoneVariant
            {
                PhoneModelID = phoneModelID,
                RAMGB = variantInfo.RAMGB,
                StorageGB = variantInfo.StorageGB,
                Colour = variantInfo.Colour,
                Price = variantInfo.Price,
                StockQuantity = variantInfo.StockQuantity,
                LowStockLevel = variantInfo.LowStockLevel,
                IsActive = true
            };

            db.PhoneVariants.InsertOnSubmit(newVariant);

            try
            {
                db.SubmitChanges();
                return 0; // variant added successfully
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return 1;
            }
        }

        public ManagerVariantInfo GetManagerVariantByID(int variantID)
        {
            var variant = (
                from v in db.PhoneVariants
                where v.VariantID == variantID
                select v).SingleOrDefault();

            if(variant == null)
            {
                return null;
            }

            ManagerVariantInfo variantInfo = new ManagerVariantInfo
            {
                VariantID = variant.VariantID,
                PhoneModelID = variant.PhoneModelID,
                RAMGB = variant.RAMGB,
                StorageGB = variant.StorageGB,
                Colour = variant.Colour,
                Price = variant.Price,
                StockQuantity = variant.StockQuantity,
                LowStockLevel = variant.LowStockLevel,
                IsActive = variant.IsActive
            };

            return variantInfo;
        }

        public int UpdateVariant(int variantID, ManagerVariantInfo variantInfo)
        {
            if(variantInfo == null)
            {
                return 1;
            }

            // find the variant being edited
            var variant = (
                from v in db.PhoneVariants
                where v.VariantID == variantID
                select v).SingleOrDefault();

            if(variant == null)
            {
                return 1; // variant could not be updated
            }

            // check for duplicate variants
            var dupVariant = (
                from v in db.PhoneVariants
                where v.PhoneModelID == variant.PhoneModelID &&
                v.RAMGB == variantInfo.RAMGB &&
                v.StorageGB == variantInfo.StorageGB &&
                v.Colour == variantInfo.Colour &&
                v.VariantID != variantID
                select v).SingleOrDefault();

            if(dupVariant != null)
            {
                return 2; // duplicate variant found
            }

            // update the variant info
            variant.RAMGB = variantInfo.RAMGB;
            variant.StorageGB = variantInfo.StorageGB;
            variant.Colour = variantInfo.Colour;
            variant.Price = variantInfo.Price;
            variant.StockQuantity = variantInfo.StockQuantity;
            variant.LowStockLevel = variantInfo.LowStockLevel;

            try
            {
                db.SubmitChanges();
                return 0; // variant updated successfully
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return 1;
            }
        }

        public bool ChangeVariantStatus(int variantID, bool isActive)
        {
            // find the selected variant
            var variant = (
                from v in db.PhoneVariants
                where v.VariantID == variantID
                select v).SingleOrDefault();

            if(variant == null)
            {
                return false;
            }

            // change the active status
            variant.IsActive = isActive;

            try
            {
                db.SubmitChanges();
                return true;
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }

        public OrderResult PlaceOrder(int userID)
        {
            // ensure the customer exists and is active
            var user = (
                from account in db.UserAccounts
                where account.UserID == userID &&
                account.UserIsActive == true
                select account).SingleOrDefault();

            if(user == null)
            {
                return new OrderResult
                {
                    OrderID = 0,
                    OrderStatus = "Failed",
                    TotalAmount = 0
                };
            }

            // find the customers active cart
            var activeCart = (
                from cart in db.Carts
                where cart.UserID == userID &&
                cart.IsActive == true
                select cart).SingleOrDefault();

            if(activeCart == null)
            {
                return new OrderResult
                {
                    OrderID = 0,
                    OrderStatus = "EmptyCart",
                    TotalAmount = 0
                };
            }

            // get all items inside the active cart
            var cartItems = (
                from item in db.CartItems
                where item.CartID == activeCart.CartID
                select item).ToList();

            if(cartItems.Count == 0)
            {
                return new OrderResult
                {
                    OrderID = 0,
                    OrderStatus = "EmptyCart",
                    TotalAmount = 0
                };
            }

            decimal subTotal = 0;

            // check that every variant exists, is active and has enough stock
            foreach(CartItem item in cartItems)
            {
                var variant = (
                    from v in db.PhoneVariants
                    where v.VariantID == item.VariantID &&
                    v.IsActive == true
                    select v).SingleOrDefault();

                if(variant == null)
                {
                    return new OrderResult
                    {
                        OrderID = 0,
                        OrderStatus = "Failed",
                        TotalAmount = 0
                    };
                }

                // ensure the phone model is active as well
                var phone = (
                    from phoneModel in db.PhoneModels
                    where phoneModel.PhoneModelID == variant.PhoneModelID &&
                    phoneModel.IsActive == true
                    select phoneModel).SingleOrDefault();

                if(phone == null)
                {
                    return new OrderResult
                    {
                        OrderID = 0,
                        OrderStatus = "Failed",
                        TotalAmount = 0
                    };
                }

                // recheck the stock at the moment the order is placed
                if(item.Quantity > variant.StockQuantity)
                {
                    return new OrderResult
                    {
                        OrderID = 0,
                        OrderStatus = "InsufficientStock",
                        TotalAmount = 0
                    };
                }

                // calculate subtotal using the current price stored in the database
                subTotal += variant.Price * item.Quantity;
            }

            // Transaction processing rule 1:
            // 5% discount for orders >= R20 000
            decimal discount = 0;
            if(subTotal >= 20000)
            {
                discount = subTotal * 0.05m;
            }

            // Transaction processing rule 2:
            // R150 shipping for orders < R10 000 and free if >= R10 000
            decimal shipping = 150;
            if(subTotal >= 10000)
            {
                shipping = 0;
            }

            // calculate the final amount
            decimal total = subTotal - discount + shipping;

            // Transaction processing rule 3:
            // VAT already included with final amount
            decimal vat = total * 15 / 115;

            // create the new customer order
            CustomerOrder newOrder = new CustomerOrder
            {
                UserID = userID,
                OrderDate = DateTime.Now,
                Subtotal = subTotal,
                DiscountAmount = discount,
                ShippingAmount = shipping,
                VATAmount = vat,
                TotalAmount = total,
                OrderStatus = "Received"
            };

            db.CustomerOrders.InsertOnSubmit(newOrder);

            // handle the orderitems, stock reductions and stockmovement records
            foreach(CartItem item in cartItems)
            {
                var variant = (
                    from phoneVariant in db.PhoneVariants
                    where phoneVariant.VariantID == item.VariantID
                    select phoneVariant).SingleOrDefault();

                // store the price at the time of purchase
                OrderItem newOrderItem = new OrderItem
                {
                    CustomerOrder = newOrder,
                    VariantID = variant.VariantID,
                    Quantity = item.Quantity,
                    UnitPrice = variant.Price
                };

                db.OrderItems.InsertOnSubmit(newOrderItem);

                // update the available stock
                variant.StockQuantity -= item.Quantity;

                // record the stockMovement caused by this sale
                StockMovement movement = new StockMovement
                {
                    VariantID = variant.VariantID,
                    ChangedByUserID = userID,
                    MovementType = "Sale",
                    QuantityChange = -item.Quantity,
                    MovementDate = DateTime.Now,
                    Notes = "Stock reduced after customer order."
                };

                db.StockMovements.InsertOnSubmit(movement);
            }

            // close the customers cart after successfully recording
            activeCart.IsActive = false;

            try
            {
                db.SubmitChanges();
                return new OrderResult
                {
                    OrderID = newOrder.OrderID,
                    OrderStatus = "Success",
                    TotalAmount = total
                };
            }
            catch(Exception e)
            {
                e.GetBaseException();
                return new OrderResult
                {
                    OrderID = 0,
                    OrderStatus = "Failed",
                    TotalAmount = 0
                };
            }
        }

        public InvoiceInfo GetInvoice(int orderID, int userID)
        {
            // find the requested  order and check that it belongs to the logged in customer
            var order = (
                from customerOrder in db.CustomerOrders
                where customerOrder.OrderID == orderID &&
                customerOrder.UserID == userID
                select customerOrder).SingleOrDefault();

            // order does not exist or does not belong this customer
            if(order == null)
            {
                return null;
            }

            // find the customer who placed the order
            var customer = (
                from account in db.UserAccounts
                where account.UserID == order.UserID
                select account).SingleOrDefault();

            if(customer == null)
            {
                return null;
            }

            // create the invoice item list
            List<InvoiceItemInfo> invoiceItems = new List<InvoiceItemInfo>();

            // get all the items belonging to this order
            var orderItems = (
                from item in db.OrderItems
                where item.OrderID == orderID
                select item).ToList();

            foreach(OrderItem item in orderItems)
            {
                // find the varaint that was purchased
                var variant = (
                    from v in db.PhoneVariants
                    where v.VariantID == item.VariantID
                    select v).SingleOrDefault();

                if(variant != null)
                {
                    // find the phone model linked to the variant
                    var phone = (
                        from p in db.PhoneModels
                        where p.PhoneModelID == variant.PhoneModelID
                        select p).SingleOrDefault();

                    if(phone != null)
                    {
                        InvoiceItemInfo invoiceItem = new InvoiceItemInfo
                        {
                            VariantID = variant.VariantID,
                            ModelName = phone.ModelName,
                            VariantDescription =
                            variant.RAMGB + " GB RAM · " +
                            variant.StorageGB + " GB Storage · " +
                            variant.Colour,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            LineTotal = item.UnitPrice * item.Quantity
                        };

                        invoiceItems.Add(invoiceItem);
                    }
                }
            }

            // create the complete invoice
            InvoiceInfo invoice = new InvoiceInfo
            {
                OrderID = order.OrderID,
                CustomerName = customer.UserFirstName + " " + customer.UserSurname,
                CustomerEmail = customer.UserEmail,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                Subtotal = order.Subtotal,
                DiscountAmount = order.DiscountAmount,
                ShippingAmount = order.ShippingAmount,
                VATAmount = order.VATAmount,
                TotalAmount = order.TotalAmount,
                Items = invoiceItems
            };

            return invoice;
        }
        
        public List<CustomerOrderInfo> GetCustomerOrders(int userID)
        {
            // empty list that will store the customers orders
            List<CustomerOrderInfo> orderList = new List<CustomerOrderInfo>();

            // find the orders
            var orders = (
                from order in db.CustomerOrders
                where order.UserID == userID
                orderby order.OrderDate descending
                select order).ToList();

            // convert each customerorder into customerorderinfo
            foreach(CustomerOrder order in orders)
            {
                CustomerOrderInfo orderInfo = new CustomerOrderInfo
                {
                    OrderID = order.OrderID,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = order.OrderStatus
                };

                orderList.Add(orderInfo);
            }

            return orderList;
        }

        public List<ManagerOrderInfo> GetManagerOrders()
        {
            // list to store all orders
            List<ManagerOrderInfo> orderList = new List<ManagerOrderInfo>();

            // get all orders
            var orders = (
                from order in db.CustomerOrders
                orderby order.OrderDate descending
                select order).ToList();

            foreach(CustomerOrder order in orders)
            {
                // find the customer linked to the order
                var customer = (
                    from account in db.UserAccounts
                    where account.UserID == order.UserID
                    select account).SingleOrDefault();

                if(customer != null)
                {
                    ManagerOrderInfo orderInfo = new ManagerOrderInfo
                    {
                        OrderID = order.OrderID,
                        CustomerName = customer.UserFirstName + " " + customer.UserSurname,
                        CustomerEmail = customer.UserEmail,
                        OrderDate = order.OrderDate,
                        OrderStatus = order.OrderStatus,
                        TotalAmount = order.TotalAmount
                    };

                    orderList.Add(orderInfo);
                }
            }

            return orderList;
        }

        public InvoiceInfo GetManagerOrderDetails(int orderID)
        {
            // find the selected customer
            var order = (
                from customerOrder in db.CustomerOrders
                where customerOrder.OrderID == orderID
                select customerOrder).SingleOrDefault();

            if(order == null)
            {
                return null;
            }

            // find the customer who placed the order
            var customer = (
                from account in db.UserAccounts
                where account.UserID == order.UserID
                select account).SingleOrDefault();

            if(customer == null)
            {
                return null;
            }

            // list to store all the items
            List<InvoiceItemInfo> invoiceItems = new List<InvoiceItemInfo>();

            // get all items in order
            var orderItems = (
                from item in db.OrderItems
                where item.OrderID == orderID
                select item).ToList();

            foreach(OrderItem item in orderItems)
            {
                // find the variant
                var variant = (
                    from v in db.PhoneVariants
                    where v.VariantID == item.VariantID
                    select v).SingleOrDefault();

                if(variant != null)
                {
                    // find the phone model
                    var phone = (
                        from phoneModel in db.PhoneModels
                        where phoneModel.PhoneModelID == variant.PhoneModelID
                        select phoneModel).SingleOrDefault();

                    if(phone != null)
                    {
                        InvoiceItemInfo invoiceItem = new InvoiceItemInfo
                        {
                            VariantID = variant.VariantID,
                            ModelName = phone.ModelName,
                            VariantDescription =
                            variant.RAMGB + " GB RAM · " +
                            variant.StorageGB + " GB Storage · " +
                            variant.Colour,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            LineTotal = item.UnitPrice * item.Quantity
                        };

                        invoiceItems.Add(invoiceItem);
                    }
                }
            }

            // create the transaction details object
            InvoiceInfo invoice = new InvoiceInfo
            {
                OrderID = order.OrderID,
                CustomerName = customer.UserFirstName + " " + customer.UserSurname,
                CustomerEmail = customer.UserEmail,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                Subtotal = order.Subtotal,
                DiscountAmount = order.DiscountAmount,
                ShippingAmount = order.ShippingAmount,
                VATAmount = order.VATAmount,
                TotalAmount = order.TotalAmount,
                Items = invoiceItems
            };

            return invoice;
        }

        public List<PhoneCatalogue> GetActivePhonesByName()
        {
            List<PhoneCatalogue> phoneList = new List<PhoneCatalogue>();

            var activePhones =
                (from phone in db.PhoneModels
                 where phone.IsActive == true
                 orderby phone.ModelName ascending
                 select phone).ToList();

            foreach (PhoneModel phone in activePhones)
            {
                var brand =
                    (from b in db.Brands
                     where b.BrandID == phone.BrandID
                     select b).SingleOrDefault();

                var variant =
                    (from v in db.PhoneVariants
                     where v.PhoneModelID == phone.PhoneModelID
                     && v.IsActive == true
                     orderby v.Price ascending
                     select v).FirstOrDefault();

                if (brand != null && variant != null)
                {
                    PhoneCatalogue cataloguePhone = new PhoneCatalogue
                        {
                            PhoneModelID = phone.PhoneModelID,
                            BrandName = brand.BrandName,
                            ModelName = phone.ModelName,
                            Description = phone.Description,
                            ImagePath = phone.ImagePath,
                            StartingPrice = variant.Price,
                            StockQuantity = variant.StockQuantity
                        };

                    phoneList.Add(cataloguePhone);
                }
            }

            return phoneList;
        }

        public List<PhoneCatalogue> GetActivePhonesPriceLowToHigh()
        {
            List<PhoneCatalogue> phones = GetActivePhones();

            var sortedPhones =
                (from phone in phones
                 orderby phone.StartingPrice ascending
                 select phone).ToList();

            return sortedPhones;
        }

        public List<PhoneCatalogue> GetActivePhonesPriceHighToLow()
        {
            List<PhoneCatalogue> phones = GetActivePhones();

            var sortedPhones =
                (from phone in phones
                 orderby phone.StartingPrice descending
                 select phone).ToList();

            return sortedPhones;
        }

        public int GetTotalOrders()
        {
            // get all customer orders
            var orders = (
                from order in db.CustomerOrders
                select order).ToList();

            return orders.Count;
        }

        public decimal GetTotalRevenue()
        {
            // get all orders
            var orders = (
                from order in db.CustomerOrders
                select order).ToList();

            decimal totalRevenue = 0;
            
            // add the final total of every order
            foreach(CustomerOrder order in orders)
            {
                totalRevenue += order.TotalAmount;
            }

            return totalRevenue;
        }

        public int GetTotalUnitsSold()
        {
            // get all purchased items
            var orderItens = (
                from item in db.OrderItems
                select item).ToList();

            int totalUnits = 0;

            // add the purchased quantity from every order item
            foreach(OrderItem item in orderItens)
            {
                totalUnits += item.Quantity;
            }

            return totalUnits;
        }

        public int GetDifferentProductsSold()
        {
            // list to store phone models sold
            List<int> soldPhoneIDs = new List<int>();

            // get all order items
            var orderItems = (
                from item in db.OrderItems
                select item).ToList();

            foreach(OrderItem item in orderItems)
            {
                // find the variant that was sold
                var variant = (
                    from v in db.PhoneVariants
                    where v.VariantID == item.VariantID
                    select v).SingleOrDefault();

                if(variant != null)
                {
                    // add the phoneModel once
                    soldPhoneIDs.Add(variant.PhoneModelID);
                }
            }

            return soldPhoneIDs.Count;
        }

        public int GetProductsOnHand()
        {
            // get all variants
            var variants = (
                from variant in db.PhoneVariants
                select variant).ToList();

            int productsOnHand = 0;

            // add the current stock of every variant
            foreach(PhoneVariant variant in variants)
            {
                productsOnHand += variant.StockQuantity;
            }

            return productsOnHand;
        }

        public int GetLowStockVariantCount()
        {
            // find varaints who have hit theit low stock threshhold
            var lowStockVariants = (
                from variant in db.PhoneVariants
                where variant.StockQuantity <= variant.LowStockLevel
                select variant).ToList();

            return lowStockVariants.Count;
        }

        public List<ProductSalesInfo> GetProductSales()
        {
            // list that will store the sales report
            List<ProductSalesInfo> salesList = new List<ProductSalesInfo>();

            // get all phone models
            var phones = (
                from phone in db.PhoneModels
                select phone).ToList();

            foreach(PhoneModel phone in phones)
            {
                int unitsSold = 0;

                // find all variants belonging to the phone
                var variants = (
                    from variant in db.PhoneVariants
                    where variant.PhoneModelID == phone.PhoneModelID
                    select variant).ToList();

                foreach(PhoneVariant variant in variants)
                {
                    // find all order items containing this variants
                    var orderItems = (
                        from item in db.OrderItems
                        where item.VariantID == variant.VariantID
                        select item).ToList();

                    foreach(OrderItem item in orderItems)
                    {
                        unitsSold += item.Quantity;
                    }
                }

                // only including products that have been sold
                if(unitsSold > 0)
                {
                    // find the brand
                    var brand = (
                        from b in db.Brands
                        where b.BrandID == phone.BrandID
                        select b).SingleOrDefault();

                    string productName = phone.ModelName;
                    
                    if(brand != null)
                    {
                        productName = brand.BrandName + " " + phone.ModelName;
                    }

                    ProductSalesInfo salesInfo = new ProductSalesInfo
                    {
                        PhoneModelID = phone.PhoneModelID,
                        ProductName = productName,
                        UnitsSold = unitsSold,
                    };

                    salesList.Add(salesInfo);
                }
            }

            // display highest selling products first
            var sortedSales = (
                from sale in salesList
                orderby sale.UnitsSold descending
                select sale).ToList();

            return sortedSales;
        }

        public List<UserRegistrationInfo> GetUserRegistrationsPerDay()
        {
            List<UserRegistrationInfo> registrationList = new List<UserRegistrationInfo>();

            // get all registered users oldest first
            var users =
                (from user in db.UserAccounts
                 orderby user.UserAccountCreated ascending
                 select user).ToList();

            foreach (UserAccount user in users)
            {
                DateTime registrationDate = user.UserAccountCreated.Date;

                UserRegistrationInfo existingDate = null;

                // Check whether this date is already in the report
                foreach (UserRegistrationInfo registration in registrationList)
                {
                    if (registration.RegistrationDate == registrationDate)
                    {
                        existingDate = registration;
                        break;
                    }
                }

                if (existingDate == null)
                {
                    // First user registered on this date.
                    UserRegistrationInfo newRegistration = new UserRegistrationInfo
                        {
                            RegistrationDate = registrationDate,
                            UsersRegistered = 1,
                        };

                    registrationList.Add(newRegistration);
                }
                else
                {
                    // Another user registered on the same date
                    existingDate.UsersRegistered++;
                }
            }

            return registrationList;
        }

        public List<ProductSalesInfo> GetProductSalesByPhoneID(int phoneModelID)
        {
            List<ProductSalesInfo> salesList = new List<ProductSalesInfo>();

            // find the selected phone
            var phone =
                (from p in db.PhoneModels
                 where p.PhoneModelID == phoneModelID
                 select p).SingleOrDefault();

            if (phone == null)
            {
                return salesList;
            }

            int unitsSold = 0;

            // find all variants belonging to the phone
            var variants =
                (from variant in db.PhoneVariants
                 where variant.PhoneModelID == phoneModelID
                 select variant).ToList();

            foreach (PhoneVariant variant in variants)
            {
                // find all OrderItems containing this variant
                var orderItems =
                    (from item in db.OrderItems
                     where item.VariantID == variant.VariantID
                     select item).ToList();

                foreach (OrderItem item in orderItems)
                {
                    unitsSold += item.Quantity;
                }
            }

            if (unitsSold > 0)
            {
                var brand =
                    (from b in db.Brands
                     where b.BrandID == phone.BrandID
                     select b).SingleOrDefault();

                string productName = phone.ModelName;

                if (brand != null)
                {
                    productName = brand.BrandName + " " + phone.ModelName;
                }

                ProductSalesInfo salesInfo = new ProductSalesInfo
                    {
                        PhoneModelID = phone.PhoneModelID,
                        ProductName = productName,
                        UnitsSold = unitsSold
                    };

                salesList.Add(salesInfo);
            }

            return salesList;
        }

        public List<UserRegistrationInfo> GetUserRegistrationsByDate(DateTime registrationDate)
        {
            List<UserRegistrationInfo> registrationList = new List<UserRegistrationInfo>();

            // get all registered users
            var users =
                (from user in db.UserAccounts
                 select user).ToList();

            int usersRegistered = 0;

            foreach (UserAccount user in users)
            {
                if (user.UserAccountCreated.Date == registrationDate.Date)
                {
                    usersRegistered++;
                }
            }

            if (usersRegistered > 0)
            {
                UserRegistrationInfo registrationInfo = new UserRegistrationInfo
                    {
                        RegistrationDate = registrationDate.Date,
                        UsersRegistered = usersRegistered
                    };

                registrationList.Add(registrationInfo);
            }

            return registrationList;
        }
    }
}
