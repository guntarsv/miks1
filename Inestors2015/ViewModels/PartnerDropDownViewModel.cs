using Investors2015.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Investors2015.ViewModels
{
    public class PartnerDropDownViewModel
    {
        public int NumberOfPartnersAvailable
        {
            get;
            set;
        }

        public SelectList SelectListPartners
        {
            get;
            set;
        }

        public string SelectedPartnerName
        {
            get;
            set;
        }

        public string SelectedPartnerId
        {
            get;
            set;
        }

        public int PartnerId
        {
            get;
            set;
        }

        public string Database
        {
            get;
            set;
        }

        public string SelectedPartnerText
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

        public string SearchPartnerPhrase
        {
            get;
            set;
        }

        public PartnerDropDownViewModel()
        {
            PartnerCustomModel pcm = new PartnerCustomModel();
            List<PartnerCustomModel> pcml = new List<PartnerCustomModel>();
            this.SelectListPartners = new SelectList(pcml, "PartnerId", "PartnerFullName", "Category", -1);
        }

        public PartnerDropDownViewModel(List<PartnerCustomModel> partnerCustomModelList)
        {
            if (partnerCustomModelList.Count > 0)
            {
                this.SelectListPartners = new SelectList(partnerCustomModelList, "PartnerId", "PartnerFullName", "Category", 0);
                this.Category = "unspecified";
                this.SelectedDatabaseName = partnerCustomModelList[0].SelectedDatabaseName;
            }
        }
    }
}
