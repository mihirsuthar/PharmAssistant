using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PharmAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmAssistant.Infrastructure
{
    public class DbSeedingData
    {
        protected void Seed(PharmAssistant.Models.PharmAssistantContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            AppUserManager userManager = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(context));

            string roleName = "Administrator";
            string userName = "Admin";
            string password = "Admin123";
            string email = "admin@gmail.com";

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new AppRole(roleName));
            }

            AppUser user = userManager.FindByName(userName);
            if (user == null)
            {
                userManager.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userManager.FindByName(userName);
            }

            if (!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }

            foreach (AppUser dbuser in userManager.Users)
            {
                dbuser.City = "Baroda";
            }

            context.Shelves.AddRange(new List<Shelf> { new Shelf { ShelfNumber = 1, ShelfName = "One"},
                                                        new Shelf { ShelfNumber = 2, ShelfName = "Two"},
                                                        new Shelf { ShelfNumber = 3, ShelfName = "Three"} });

            context.SaveChanges();

            context.Manufacturers.AddRange(new List<Manufacturer> {
                new Manufacturer{ ManufacturerName = "Cipla", Address = "Baroda", ContactNo = 8857484748, EmailId = "sales@cipla.com" },
                new Manufacturer{ ManufacturerName = "Torrent", Address = "Ahmedabad", ContactNo = 8859483948, EmailId = "sales@torrent.com" },
                new Manufacturer{ ManufacturerName = "Sun Pharma", Address = "Baroda", ContactNo = 9098493849, EmailId = "sales@sunpharma.com" },
            });

            context.SaveChanges();

            var Supplier1 = new Supplier { SupplierName = "Supplier1", Address = "Baroda", ContactNo = 4738372837, EmailId = "supplier1@gmail.com" };
            var Supplier2 = new Supplier { SupplierName = "Supplier2", Address = "Baroda", ContactNo = 7758438373, EmailId = "supplier2@gmail.com" };
            var Supplier3 = new Supplier { SupplierName = "Supplier3", Address = "Ahmedabad", ContactNo = 9012837165, EmailId = "supplier3@gmail.com" };

            //context.Suppliers.AddRange(new List<Supplier> { Supplier1, Supplier2, Supplier3 });
            //context.SaveChanges();

            var Category1 = new MedicineCategory { MedicineCategoryName = "Tablet" };
            var Category2 = new MedicineCategory { MedicineCategoryName = "Injectable" };
            var Category3 = new MedicineCategory { MedicineCategoryName = "Syrup" };

            Category1.Suppliers.Add(Supplier1);
            Category1.Suppliers.Add(Supplier3);

            Category2.Suppliers.Add(Supplier2);
            Category2.Suppliers.Add(Supplier3);

            Category3.Suppliers.Add(Supplier1);
            Category3.Suppliers.Add(Supplier2);

            context.MedicineCategories.AddRange(new List<MedicineCategory> { Category1, Category2, Category3 });

            context.SaveChanges();

            var Medicine1 = new Medicine { MedicineName = "Brufen", CostPrice = 12.50f, SellingPrice = 14.50f, Description = "Pain Killer", ShelfId = 1, CategoryId = 1, ManufacturerId = 1, StockCapacity = 200, ReorderLevel = 70, BufferLevel = 30 };
            var Medicine2 = new Medicine { MedicineName = "Crocin", CostPrice = 14.50f, SellingPrice = 16.75f, Description = "Fever Relief", ShelfId = 1, CategoryId = 1, ManufacturerId = 3, StockCapacity = 250, ReorderLevel = 90, BufferLevel = 40 };
            var Medicine3 = new Medicine { MedicineName = "Glycodin", CostPrice = 36.70f, SellingPrice = 38.50f, Description = "Cough Syrup", ShelfId = 3, CategoryId = 3, ManufacturerId = 2, StockCapacity = 175, ReorderLevel = 55, BufferLevel = 30 };
            var Medicine4 = new Medicine { MedicineName = "Benadryl", CostPrice = 40.36f, SellingPrice = 43.50f, Description = "Cough Syrup", ShelfId = 3, CategoryId = 3, ManufacturerId = 3, StockCapacity = 200, ReorderLevel = 70, BufferLevel = 30 };
            var Medicine5 = new Medicine { MedicineName = "Insulin Aspart", CostPrice = 32.50f, SellingPrice = 35.50f, Description = "Insulin", ShelfId = 2, CategoryId = 2, ManufacturerId = 1, StockCapacity = 220, ReorderLevel = 80, BufferLevel = 40 };
            var Medicine6 = new Medicine { MedicineName = "Streptokinase", CostPrice = 120.40f, SellingPrice = 125.50f, Description = "Blood Thinner", ShelfId = 2, CategoryId = 2, ManufacturerId = 2, StockCapacity = 290, ReorderLevel = 100, BufferLevel = 60 };

            Medicine1.Suppliers.Add(Supplier1);
            Medicine1.Suppliers.Add(Supplier3);

            Medicine2.Suppliers.Add(Supplier1);
            Medicine2.Suppliers.Add(Supplier2);

            Medicine3.Suppliers.Add(Supplier2);
            Medicine3.Suppliers.Add(Supplier3);

            Medicine4.Suppliers.Add(Supplier1);
            Medicine4.Suppliers.Add(Supplier2);

            Medicine5.Suppliers.Add(Supplier2);
            Medicine5.Suppliers.Add(Supplier3);

            Medicine6.Suppliers.Add(Supplier1);
            Medicine6.Suppliers.Add(Supplier3);

            context.Medicines.AddRange(new List<Medicine> { Medicine1, Medicine2, Medicine3, Medicine4, Medicine5, Medicine6 });

            context.SaveChanges();

            Customer customer1 = new Customer { CustomerName = "First Customer", Address = "Anand", ContactNumber = 8857483748, EmailId = "first@gmail.com" };
            Customer customer2 = new Customer { CustomerName = "Second Customer", Address = "Ahmedabad", ContactNumber = 9847382938, EmailId = "second@gmail.com" };
            Customer customer3 = new Customer { CustomerName = "Third Customer", Address = "Baroda", ContactNumber = 9944374837, EmailId = "third@gmail.com" };

            context.Customers.AddRange(new List<Customer> { customer1, customer2, customer3 });

            context.SaveChanges();

            MembershipType membershipType1 = new MembershipType { MembershipTypeName = "Silver", MembershipTypeDesc = "Silver Membership", UpperBillLimit = 1500, BonusPoints = 150 };
            MembershipType membershipType2 = new MembershipType { MembershipTypeName = "Gold", MembershipTypeDesc = "Gold Membership", UpperBillLimit = 2500, BonusPoints = 250 };
            MembershipType membershipType3 = new MembershipType { MembershipTypeName = "Platinum", MembershipTypeDesc = "Platinum Membership", UpperBillLimit = 3500, BonusPoints = 350 };

            context.MembershipTypes.AddRange(new List<MembershipType> { membershipType1, membershipType2, membershipType3 });

            context.SaveChanges();

        }
    }
}