using Newtonsoft.Json;
using PharmAssistant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PharmAssistant.Controllers
{
    public class MembershipsController : Controller
    {
        // GET: List Membership Accounts
        public ActionResult Index()
        {
            using (PharmAssistantContext db = new PharmAssistantContext())
            {
                var membershipAcounts = from c in db.Customers
                                        join ma in db.MembershipAccounts on c.MembershipId equals ma.MembershipId
                                        join mtype in db.MembershipTypes on ma.MembershipTypeId equals mtype.MembershipTypeId
                                        select new
                                        {
                                            CustomerId = c.CustomerId,
                                            CustomerName = c.CustomerName,
                                            MembershipId = c.MembershipId,
                                            MembershipType = mtype.MembershipTypeName,
                                            JoiningDate = ma.JoiningDate,
                                            TotalPurchase = ma.TotalPurchaseAmount,
                                            BonusPoints = ma.BonusPoints
                                        };
            }
            return View();
        }

        #region Membership Accounts Management

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult NewMembershipAccount(int CustomerId, int MembershipTypeId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    MembershipAccount membershipAccount = new MembershipAccount { CustomerId = CustomerId, MembershipTypeId = MembershipTypeId, JoiningDate = DateTime.Now };
                    db.MembershipAccounts.Add(membershipAccount);
                    db.SaveChanges();

                    Customer customer = db.Customers.Where(c => c.CustomerId == CustomerId).FirstOrDefault();
                    customer.MembershipId = db.MembershipAccounts.Where(ma => ma.CustomerId == CustomerId).FirstOrDefault().MembershipId;

                    db.Customers.Attach(customer);
                    db.Entry(customer).State = EntityState.Modified;

                    db.SaveChanges();
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public JsonResult FillNewMembershipAccountForm()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var customers = db.Customers.Select(c => new { CustomerId = c.CustomerId, CustomerName = c.CustomerName }).ToList();
                    var membershipTypes = db.MembershipTypes.Select(mt => new { MembershipTypeId = mt.MembershipTypeId, MembershipTypeName = mt.MembershipTypeName }).ToList();

                    return Json(new { Customers = customers, MembershipTypes = membershipTypes }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { Customers = new { }, MembershipTypes = new { } });
            }
        }

        public ActionResult GetMembershipById(int membershipId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return Json(db.MembershipAccounts.Where(m => m.MembershipId == membershipId).FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        public ActionResult GetMemberships()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var membershipAcounts = (from c in db.Customers
                                            join ma in db.MembershipAccounts on c.CustomerId equals ma.CustomerId
                                            join mtype in db.MembershipTypes on ma.MembershipTypeId equals mtype.MembershipTypeId
                                            select new
                                            {
                                                CustomerId = c.CustomerId,
                                                CustomerName = c.CustomerName,
                                                MembershipId = ma.MembershipId,
                                                MembershipTypeId = mtype.MembershipTypeId,
                                                MembershipType = mtype.MembershipTypeName,
                                                JoiningDate = ma.JoiningDate,
                                                TotalPurchase = ma.TotalPurchaseAmount,
                                                BonusPoints = ma.BonusPoints
                                            }).ToList();

                    return Json(membershipAcounts, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult UpdateMembershipAccount(int MembershipId, int MembershipTypeId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    var membershipAccount = db.MembershipAccounts.Where(a => a.MembershipId == MembershipId).FirstOrDefault();
                    membershipAccount.MembershipTypeId = MembershipTypeId;

                    db.MembershipAccounts.Attach(membershipAccount);
                    db.Entry(membershipAccount).Property("MembershipTypeId").IsModified = true;
                    //db.Entry(membershipAccount).Property("TotalPurchaseAmount").IsModified = true;
                    //db.Entry(membershipAccount).Property("BonusPoints").IsModified = true;
                    db.SaveChanges();

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult DeleteMembersip(int MembershipId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.MembershipAccounts.Remove(db.MembershipAccounts.Where(ma => ma.MembershipId == MembershipId).FirstOrDefault());                    
                    db.SaveChanges();

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        #endregion

        #region Membership Types Management

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult AddMembershipType(MembershipType membershipType)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.MembershipTypes.Add(membershipType);
                    db.SaveChanges();
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult GetMembershipTypeById(int membershipTypeId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return Json(db.MembershipTypes.Where(m => m.MembershipTypeId == membershipTypeId).FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        public ActionResult GetMembershipTypes()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return Json(db.MembershipTypes.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult UpdateMembershipType(MembershipType membershipType)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.MembershipTypes.Attach(membershipType);
                    db.Entry(membershipType).Property("MembershipTypeName").IsModified = true;
                    db.Entry(membershipType).Property("MembershipTypeDesc").IsModified = true;
                    db.Entry(membershipType).Property("UpperBillLimit").IsModified = true;
                    db.Entry(membershipType).Property("BonusPoints").IsModified = true;
                    db.SaveChanges();

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public HttpStatusCodeResult DeleteMembershipType(int membershipTypeId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.MembershipTypes.Remove(db.MembershipTypes.Where(mt => mt.MembershipTypeId == membershipTypeId).FirstOrDefault());
                    db.SaveChanges();

                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        #endregion

        #region Discount Policy Management - Disabled

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public HttpStatusCodeResult NewDiscountPolicy(DiscountPolicy discountPolicy)
        //{
        //    try
        //    {
        //        using (PharmAssistantContext db = new PharmAssistantContext())
        //        {
        //            db.DiscountPolicies.Add(discountPolicy);
        //            db.SaveChanges();
        //        }

        //        return new HttpStatusCodeResult(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}

        //public ActionResult GetDiscountPolicyById(int policyId)
        //{
        //    try
        //    {
        //        using (PharmAssistantContext db = new PharmAssistantContext())
        //        {
        //            return Json(db.DiscountPolicies.Where(d => d.PolicyId == policyId).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        //    }
        //}

        //public ActionResult GetDiscountPolicies()
        //{
        //    try
        //    {
        //        using (PharmAssistantContext db = new PharmAssistantContext())
        //        {
        //            var discountPolicies =  from d in db.DiscountPolicies join
        //                                    m in db.MembershipTypes 
        //                                    on d.MembershipTypeId equals m.MembershipTypeId
        //                                   select new
        //                                   {
        //                                       PolicyId = d.PolicyId,
        //                                       MembershipTypeId = m.MembershipTypeId,
        //                                       MembershipTypeName = m.MembershipTypeName,
        //                                       UpperBillLimit = d.UpperBillLimit,
        //                                       BonusPoints = d.BonusPoints
        //                                   };

        //            return Json(discountPolicies.ToList(), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        //    }
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public HttpStatusCodeResult UpdateDiscountPolicy(DiscountPolicy discountPolicy)
        //{
        //    try
        //    {
        //        using (PharmAssistantContext db = new PharmAssistantContext())
        //        {
        //            db.DiscountPolicies.Attach(discountPolicy);
        //            db.Entry(discountPolicy).Property("UpperBillLimit").IsModified = true;
        //            db.Entry(discountPolicy).Property("BonusPoints").IsModified = true;
        //            db.SaveChanges();

        //            return new HttpStatusCodeResult(HttpStatusCode.OK);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public HttpStatusCodeResult DeleteDiscountPolicy(DiscountPolicy discountPolicy)
        //{
        //    try
        //    {
        //        using (PharmAssistantContext db = new PharmAssistantContext())
        //        {
        //            db.DiscountPolicies.Remove(db.DiscountPolicies.Where(d => d.PolicyId == discountPolicy.PolicyId).FirstOrDefault());
        //            db.SaveChanges();

        //            return new HttpStatusCodeResult(HttpStatusCode.OK);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}

        //public ActionResult GetDiscountRedeemHistory(int membershipId)
        //{
        //    try
        //    {
        //        using (PharmAssistantContext db = new PharmAssistantContext())
        //        {
        //            return Json(db.MembershipDiscounts.Where(md => md.MembershipId == membershipId).ToList(), JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        //    }
        //}
        
        #endregion

    }
}