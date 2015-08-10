using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Investors2015.Models;

namespace Investors2015.Models
{

    public class PartnerCustomModel
    {
        [HiddenInput(DisplayValue = false)]
        [ScaffoldColumn(false)]
        public int PartnerId { get; set; }
        public byte PhysicalPerson { get; set; }
        public string PartnerName { get; set; }
        public string PartnerTitle { get; set; }
        public string PhysicalPersonFirstName { get; set; }
        public string PhysicalPersonIdentityNo { get; set; }
        public string Category { get; set; }
        public string SelectedDatabaseName { get; set; }

        public string PartnerFullName
        {
            get
            {
                string partnerFullName;
                switch (PhysicalPerson)
                {
                    case 0:
                        partnerFullName = PartnerName + (PartnerTitle == null ? "" : ", " + PartnerTitle);
                        break;
                    default:
                        partnerFullName = PartnerName +
                                          (PhysicalPersonFirstName == null ? "" : " " + PhysicalPersonFirstName) +
                                          (PhysicalPersonIdentityNo == null ? "" : " (" + PhysicalPersonIdentityNo + ")");

                        break;
                }
                return partnerFullName;
            }

        }

        //public static List<PartnerCustomModel> GetSelectListPartners(string datubaze)
        //{
        //    List<PartnerCustomModel> partnerCustomModels;
        //    using (TildesJumisFinancialDBContext dbContext = new TildesJumisFinancialDBContext())
        //    {
        //        dbContext.ChangeDatabase(initialCatalog: datubaze,
        //            userId: "admin",
        //            password: "is ri itldes ajuna aprole", integratedSecuity: false);

        //        List<Partner> partners = dbContext.Partners.Select(x => x).OrderBy(d => d.PartnerName).ToList();
        //        if (partners.Any())
        //        {
        //            Mapper.CreateMap<Partner, PartnerCustomModel>();
        //            partnerCustomModels =
        //                Mapper.Map<List<Partner>, List<PartnerCustomModel>>(partners);
        //            partnerCustomModels.ForEach(x => { x.SelectedDatabaseName = datubaze; });
        //            return partnerCustomModels;
        //        }
        //    }
        //    return null;
        //}

        public static List<PartnerCustomModel> GetSelectListPartners(string datubaze, string searchTerm)
        {
            List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
            using (TildesJumisFinancialDBContext dbContext = new TildesJumisFinancialDBContext())
            {
                dbContext.ChangeDatabase(initialCatalog: datubaze,
                    userId: "admin",
                    password: "is ri itldes ajuna aprole", integratedSecuity: false);
                List<Partner> partners;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    partners = dbContext.Partners.Select(x => x).OrderBy(d => d.PartnerName).ToList();
                }
                else
                {
                    partners = dbContext.Partners.Where(x => (x.PartnerName.Contains(searchTerm) 
                        || x.PhysicalPersonFirstName.Contains(searchTerm)
                        || x.PartnerTitle.Contains(searchTerm) )).OrderBy(d => d.PartnerName).ToList();

                }

                if (!partners.Any()) return null;
                
                
                    Mapper.CreateMap<Partner, PartnerCustomModel>();
                    partnerCustomModels =
                        Mapper.Map<List<Partner>, List<PartnerCustomModel>>(partners);
                    partnerCustomModels.ForEach(x => { x.SelectedDatabaseName = datubaze; });
                    
                    //if (!string.IsNullOrEmpty(searchTerm))
                    //{
                    //    partnerCustomModels = partnerCustomModels.Where(x => x.PartnerFullName.Contains(searchTerm)).ToList();
                    //}
                    return partnerCustomModels;
                
            }
            
        }



    }


}