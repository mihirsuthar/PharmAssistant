using PharmAssistant.Models;
using System;
using System.Collections.Generic;
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
        [ValidateAntiForgeryToken]
        public HttpStatusCodeResult NewMembershipAccount(MembershipAccount membershipAccount)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.MembershipAccounts.Add(membershipAccount);
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
                    return Json(db.MembershipAccounts.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpStatusCodeResult UpdateMembership(MembershipAccount membershipAccount)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.MembershipAccounts.Attach(membershipAccount);
                    db.Entry(membershipAccount).Property("MembershipTypeId").IsModified = true;
                    db.Entry(membershipAccount).Property("TotalPurchaseAmount").IsModified = true;
                    db.Entry(membershipAccount).Property("BonusPoints").IsModified = true;
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
        [ValidateAntiForgeryToken]
        public HttpStatusCodeResult DeleteMembersip(MembershipAccount membershipAccount)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.MembershipAccounts.Remove(membershipAccount);                    
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

        #region Discount Policy Management

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpStatusCodeResult NewDiscountPolicy(DiscountPolicy discountPolicy)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.DiscountPolicies.Add(discountPolicy);
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

        public ActionResult GetDiscountPolicyById(int policyId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return Json(db.DiscountPolicies.Where(d => d.PolicyId == policyId).FirstOrDefault(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        public ActionResult GetDiscountPolicies()
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return Json(db.DiscountPolicies.Include("MembershipType").ToList(), JsonRequestBehavior.AllowGet);
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
        public HttpStatusCodeResult UpdateDiscountPolicy(DiscountPolicy discountPolicy)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.DiscountPolicies.Attach(discountPolicy);
                    db.Entry(discountPolicy).Property("UpperBillLimit").IsModified = true;
                    db.Entry(discountPolicy).Property("BonusPoints").IsModified = true;
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
        public HttpStatusCodeResult DeleteDiscountPolicy(DiscountPolicy discountPolicy)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    db.DiscountPolicies.Remove(discountPolicy);
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

        public ActionResult GetDiscountRedeemHistory(int membershipId)
        {
            try
            {
                using (PharmAssistantContext db = new PharmAssistantContext())
                {
                    return Json(db.MembershipDiscounts.Where(md => md.MembershipId == membershipId).ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }
        
        #endregion

    }
}