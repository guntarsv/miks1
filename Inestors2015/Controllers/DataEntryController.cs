using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AutoMapper;
using Investors2015.Models;
using Investors2015.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Investors2015.Controllers
{
    [Authorize]
    public class DataEntryController : Controller
    {
        #region šie pielikti tikai dēļ vajadzības atšifrēt User
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        #endregion

        public DataEntryIndexViewModel DataEntryIndexViewModel { get; set; }

        #region arī šis konstruktors tikai dēļ vajadzības atšifrēt user
        public DataEntryController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }
        #endregion

        // GET: DataEntry
        public ActionResult Index()
        {

            //miksmiks šito vajag userim
            //string user = UserManager.FindById(User.Identity.GetUserId()).UserName;

            //System.Web.HttpContext.Current.Response.Write(
            //    "<SCRIPT LANGUAGE='JavaScript'>alert('" + user + "')</SCRIPT>");

            if (DataEntryIndexViewModel == null)
            {
                DataEntryIndexViewModel = new DataEntryIndexViewModel();
                
                #region mikmiks

                List<Datubaze> listDatubazes = Datubaze.GetDatabasesFromTilde(); // ielasām datubāzes
                DatabaseDropDownViewModel databaseDropDownViewModel = new DatabaseDropDownViewModel(listDatubazes); //formatējam datubāzes dropdown mērķiem
                DataEntryIndexViewModel.DatabaseDropDownViewModel = databaseDropDownViewModel;
                #endregion
                //DataEntryIndexViewModel.DatabaseDropDownViewModel = new DatabaseDropDownViewModel();
                DataEntryIndexViewModel.PartnerDropDownViewModel = new PartnerDropDownViewModel();

                //DataEntryIndexViewModel.PartnerDropDownViewModel = PartnerCustomModel.GetSelectListPartners("kta2014");

                //   System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('start')</SCRIPT>");
                //  DataEntryIndexViewModel.PartnerViewModelList = PartnerCustomModel.GetPartnerCustomModelListAsync("attido2014").Result;
                //   System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>alert('end')</SCRIPT>");

            }
            return View(DataEntryIndexViewModel);
        }




        [HttpGet]
        public PartialViewResult GetDatabaseDropDownHtml()
        {
            //var something = Task<List<Datubaze>>.Factory.StartNew(() => Datubaze.GetDatabasesFromTilde());
            //something.Wait();
            //List<Datubaze> listDatubazes = something.Result;

            List<Datubaze> listDatubazes = Datubaze.GetDatabasesFromTilde();

            DatabaseDropDownViewModel databaseDropDownViewModel = new DatabaseDropDownViewModel(listDatubazes);
            return PartialView("~/Views/Shared/_DatabaseDropDown.cshtml", databaseDropDownViewModel);
        }

        //[HttpGet]
        //public PartialViewResult GetDatabasePartnersHtml()
        //{
        //    string datubaze = "kta2014";
        //    List<PartnerCustomModel> partnerCustomModels = PartnerCustomModel.GetSelectListPartners(datubaze);
        //    PartnerDropDownViewModel partnerDropDownViewModel = new PartnerDropDownViewModel(partnerCustomModels);
        //    return PartialView("~/Views/Shared/_PartnerDropDownHtml.cshtml", partnerDropDownViewModel);
        //}


        //[HttpPost]
        //public ActionResult GetDatabasePartnersTest()
        //{
        //    List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
        //    try
        //    {
        //        partnerCustomModels = PartnerCustomModel.GetSelectListPartners("sol2015");
        //    }
        //    catch (Exception) { }
        //    return View();
        //}

        public JsonResult GetTest(string term, string datubaze)
        {
            List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
            try
            {
                partnerCustomModels = PartnerCustomModel.GetSelectListPartners(datubaze, term);
            }
            catch (Exception) { }

            if (partnerCustomModels==null) return null;
            
            //List<string> partnerStringList = partnerCustomModels.Select(x => x.PartnerFullName).ToList();

            //var partnerPairs1 =
            //    partnerCustomModels.Select(o => new {o.PartnerId, o.PartnerFullName}).AsEnumerable()
            //        .Select(p => new KeyValuePair<int, string>(p.PartnerId, p.PartnerFullName)).ToList();
            //    ;  //  Select( s => new KeyValuePair<string,string> {  });

            Dictionary<string,string> partnerDictionary = partnerCustomModels.ToDictionary(o => o.PartnerId.ToString(), o => o.PartnerFullName);

            // pieliekam klāt skaitu
            partnerDictionary.Add("skaits", partnerDictionary.Count.ToString());

            //var i = new Dictionary<string, object>
            //{
            //    { "mainData", partnerStringList  },
            //    { "lastElement", "buuuuba"}
            //};

            JsonResult t = Json(partnerDictionary, JsonRequestBehavior.AllowGet);
            return t;
           // return Json(partnerDictionary, JsonRequestBehavior.AllowGet);   

            //return Json(i, JsonRequestBehavior.AllowGet);   
            //return Json(partnerStringList, JsonRequestBehavior.AllowGet);    
        }


        [HttpPost]
        public ActionResult GetDatabasePartnersHtml(PartnerDropDownViewModel pdn)//  FormCollection fc)
        {

 
            List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
            try
            {
                partnerCustomModels = PartnerCustomModel.GetSelectListPartners(pdn.SelectedDatabaseName, pdn.SearchPartnerPhrase);
            }
            catch (Exception) { }

            if (partnerCustomModels.Count == 0) return Content("(šai datubāzei nevarēja ielādēt partnerus)");
            PartnerDropDownViewModel partnerDropDownViewModel = new PartnerDropDownViewModel(partnerCustomModels);
            return PartialView("~/Views/Shared/_PartnerDropDown.cshtml", partnerDropDownViewModel);
        }

        //[HttpPost]
        //public ActionResult GetDatabasePartnersHtml_SearchTerm()
        //{

        //    //PartnerDropDownViewModel partnerDropDownViewModel1 = new PartnerDropDownViewModel();
        //    //UpdateModel(partnerDropDownViewModel1);
        //    //List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
        //    //if (ModelState.IsValid)
        //    //{
        //        //try
        //        //{
        //            //List<PartnerCustomModel> partnerCustomModels = PartnerCustomModel
        //            //    .GetSelectListPartners(partnerDropDownViewModel.SelectedDatabaseName, partnerDropDownViewModel.SearchPartnerPhrase;

        //   List<PartnerCustomModel> partnerCustomModels = PartnerCustomModel.GetSelectListPartners("sol2015");
        //   PartnerDropDownViewModel partnerDropDownViewModel = new PartnerDropDownViewModel(partnerCustomModels);
                    
        //            //UpdateModel(partnerDropDownViewModel);
        //    return PartialView("~/Views/Shared/_PartnerDropDown.cshtml", partnerDropDownViewModel); // partnerDropDownViewModel);

        //    //        return PartialView("~/Views/Shared/_PartnerDropDown2.cshtml");
        //    //return null;

        //}






        [HttpPost]
        [ActionName("GetDatabaseDropDownHtml")]
        public ActionResult AllDatabasesCombo_post(DatabaseDropDownViewModel dataEntryAllDatabases)
        {
            List<Datubaze> listDatubazes = Datubaze.GetDatabasesFromTilde();
            DatabaseDropDownViewModel databaseDropDownViewModel = new DatabaseDropDownViewModel(listDatubazes);

            //DatabaseDropDownViewModel.SelectedDatabaseName = "auto2014";
            //DatabaseDropDownViewModel.SelectedDatabaseId = "5";
            return PartialView("~/Views/Shared/_DatabaseDropDown.cshtml", databaseDropDownViewModel);
        }


        #region partner




        //public List<PartnerCustomModel> GetSelectListPartners(string datubaze)
        //{
        //    List<PartnerCustomModel> partnerViewModels;
        //    using (TildesJumisFinancialDBContext dbContext = new TildesJumisFinancialDBContext())
        //    {
        //        dbContext.ChangeDatabase(initialCatalog: datubaze, 
        //            userId: "admin", 
        //            password: "is ri itldes ajuna aprole", integratedSecuity: false);
        //        List<Partner> partners = dbContext.Partners.Select(x => x).ToList();

        //        Mapper.CreateMap<Partner,PartnerCustomModel>();
        //        partnerViewModels =
        //            Mapper.Map<List<Partner>, List<PartnerCustomModel>>(partners);
        //    }

        //    return partnerViewModels;
        //}



        #endregion
    }
}
