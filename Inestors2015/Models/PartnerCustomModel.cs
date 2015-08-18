using AutoMapper;
using Inestors2015.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Investors2015.Models
{
    public class PartnerCustomModel
    {
        [ScaffoldColumn(false), HiddenInput(DisplayValue = false)]
        public int PartnerId
        {
            get;
            set;
        }

        public byte PhysicalPerson
        {
            get;
            set;
        }

        public string PartnerName
        {
            get;
            set;
        }

        public string PartnerTitle
        {
            get;
            set;
        }

        public string PhysicalPersonFirstName
        {
            get;
            set;
        }

        public string PhysicalPersonIdentityNo
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string SelectedDatabaseName
        {
            get;
            set;
        }

        public string PartnerFullName
        {
            get
            {
                byte physicalPerson = this.PhysicalPerson;
                string partnerFullName;
                if (physicalPerson != 0)
                {
                    partnerFullName = this.PartnerName + ((this.PhysicalPersonFirstName == null) ? "" : (" " + this.PhysicalPersonFirstName)) + ((this.PhysicalPersonIdentityNo == null) ? "" : (" (" + this.PhysicalPersonIdentityNo + ")"));
                }
                else
                {
                    partnerFullName = this.PartnerName + ((this.PartnerTitle == null) ? "" : (", " + this.PartnerTitle));
                }
                return partnerFullName;
            }
        }

        public static int GetNumberOfPartners(string datubaze)
        {
            int result;
            using (TildesJumisFinancialDBContext dbContext = new TildesJumisFinancialDBContext())
            {
                dbContext.ChangeDatabase(datubaze, "", "admin", "is ri itldes ajuna aprole", false, "");
                List<Partner> partners = (from x in dbContext.Partners
                                          select x into d
                                          orderby d.PartnerName
                                          select d).ToList<Partner>();
                if (!partners.Any<Partner>())
                {
                    result = 0;
                }
                else
                {
                    result = partners.Count;
                }
            }
            return result;
        }

        public static List<PartnerCustomModel> GetSelectListPartners(string datubaze, string searchTerm)
        {
            List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
            List<PartnerCustomModel> result;
            using (TildesJumisFinancialDBContext dbContext = new TildesJumisFinancialDBContext())
            {
                dbContext.ChangeDatabase(datubaze, "", "admin", "is ri itldes ajuna aprole", false, "");
                List<Partner> partners;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    partners = (from x in dbContext.Partners
                                select x into d
                                orderby d.PartnerName
                                select d).ToList<Partner>();
                }
                else
                {
                    partners = (from x in dbContext.Partners
                                where x.PartnerName.Contains(searchTerm) || x.PhysicalPersonFirstName.Contains(searchTerm) || x.PartnerTitle.Contains(searchTerm)
                                select x into d
                                orderby d.PartnerName
                                select d).ToList<Partner>();
                }
                if (!partners.Any<Partner>())
                {
                    result = null;
                }
                else
                {
                    Mapper.CreateMap<Partner, PartnerCustomModel>();
                    partnerCustomModels = Mapper.Map<List<Partner>, List<PartnerCustomModel>>(partners);
                    partnerCustomModels.ForEach(delegate(PartnerCustomModel x)
                    {
                        x.SelectedDatabaseName = datubaze;
                    });
                    result = partnerCustomModels;
                }
            }
            return result;
        }
    }
}
