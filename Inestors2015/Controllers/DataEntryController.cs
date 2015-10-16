using Inestors2015.Models;
using Investors2015.Models;
using Investors2015.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Investors2015.Controllers
{
    [Authorize]
    public class DataEntryController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext
        {
            get;
            set;
        }

        protected UserManager<ApplicationUser> UserManager
        {
            get;
            set;
        }

        public DataEntryIndexViewModel DataEntryIndexViewModel
        {
            get;
            set;
        }

        public DataEntryController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        public ActionResult Index()
        {
            if (this.DataEntryIndexViewModel == null)
            {
                this.DataEntryIndexViewModel = new DataEntryIndexViewModel();
                this.DataEntryIndexViewModel.DatabaseDropDownViewModel = new DatabaseDropDownViewModel(Datubaze.GetDatabasesFromTilde());
                this.DataEntryIndexViewModel.PartnerDropDownViewModel = new PartnerDropDownViewModel();
            }
            return (ActionResult)this.View((object)this.DataEntryIndexViewModel);
        }

        [HttpGet]
        public PartialViewResult GetDatabaseDropDownHtml()
        {
            List<Datubaze> listDatubazes = Datubaze.GetDatabasesFromTilde();
            DatabaseDropDownViewModel databaseDropDownViewModel = new DatabaseDropDownViewModel(listDatubazes);
            return this.PartialView("~/Views/Shared/_DatabaseDropDown.cshtml", databaseDropDownViewModel);
        }

        public JsonResult GetTest(string term, string datubaze)
        {
            List<PartnerCustomModel> list = new List<PartnerCustomModel>();
            try
            {
                list = PartnerCustomModel.GetSelectListPartners(datubaze, term);
            }
            catch (Exception ex)
            {
            }
            if (list == null)
                return (JsonResult)null;
            Dictionary<string, string> dictionary = Enumerable.ToDictionary<PartnerCustomModel, string, string>((IEnumerable<PartnerCustomModel>)list, (Func<PartnerCustomModel, string>)(o => o.PartnerId.ToString()), (Func<PartnerCustomModel, string>)(o => o.PartnerFullName));
            dictionary.Add("skaits", dictionary.Count.ToString());
            return this.Json((object)dictionary, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForTestingPurposes(PartnerDropDownViewModel pdn)
        {
            List<FinancialDocLineCM> financialDocLineCMs = FinancialDocCM.GetTestTemplates(pdn.Database, pdn.PartnerId);
            return this.PartialView("~/Views/DataEntry/_FinancialDocLineCM.cshtml", financialDocLineCMs);
        }

        [HttpPost]
        public ActionResult GetFinancialDocTemplates(PartnerDropDownViewModel pdn)
        {
            List<FinancialDocLineCM> financialDocLineCMs = FinancialDocCM.GetTestTemplates(pdn.Database, pdn.PartnerId);
            return this.PartialView("~/Views/DataEntry/_FinancialDocLineCM.cshtml", financialDocLineCMs);
        }

        [HttpPost]
        public ActionResult GetNumberOfPartners(PartnerDropDownViewModel pdn)
        {
            int partneruSkaits = 0;
            List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
            try
            {
                partneruSkaits = PartnerCustomModel.GetNumberOfPartners(pdn.SelectedDatabaseName);
            }
            catch (Exception)
            {
            }
            return this.PartialView("~/Views/Shared/_PartnerDropDown.cshtml", new PartnerDropDownViewModel(partnerCustomModels)
            {
                NumberOfPartnersAvailable = partneruSkaits,
                SelectedDatabaseName = pdn.SelectedDatabaseName
            });
        }

        [HttpPost]
        public ActionResult GetDatabasePartnersHtml(PartnerDropDownViewModel pdn)
        {
            List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
            try
            {
                partnerCustomModels = PartnerCustomModel.GetSelectListPartners(pdn.SelectedDatabaseName, pdn.SearchPartnerPhrase);
            }
            catch (Exception)
            {
            }
            ActionResult result;
            if (partnerCustomModels.Count == 0)
            {
                result = base.Content("(šai datubāzei nevarēja ielādēt partnerus)");
            }
            else
            {
                PartnerDropDownViewModel partnerDropDownViewModel = new PartnerDropDownViewModel(partnerCustomModels);
                result = this.PartialView("~/Views/Shared/_PartnerDropDown.cshtml", partnerDropDownViewModel);
            }
            return result;
        }

        [ActionName("GetDatabaseDropDownHtml"), HttpPost]
        public ActionResult AllDatabasesCombo_post(DatabaseDropDownViewModel dataEntryAllDatabases)
        {
            List<Datubaze> listDatubazes = Datubaze.GetDatabasesFromTilde();
            DatabaseDropDownViewModel databaseDropDownViewModel = new DatabaseDropDownViewModel(listDatubazes);
            return this.PartialView("~/Views/Shared/_DatabaseDropDown.cshtml", databaseDropDownViewModel);
        }
    }
}
