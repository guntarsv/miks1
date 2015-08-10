using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Investors2015.Models;
using Investors2015.ViewModels;

namespace Investors2015.ViewModels
{
    public class PartnerDropDownViewModel
    {
        public SelectList SelectListPartners { get; set; }
        public string SelectedPartnerName { get; set; }
        public string SelectedPartnerId { get; set; }
        public string SelectedPartnerText { get; set; }
        public string Category { get; set; }
        //public string SelectedDatabaseName { get; set; }

        public string SelectedDatabaseName { get; set; }

        public string SearchPartnerPhrase { get; set; }
   
        public PartnerDropDownViewModel()
        {
            //PartnerCustomModel partnerCustomModel = new PartnerCustomModel{PartnerId = 1000000, Category = "unsepcified",PartnerName = "mx"};
            //List<PartnerCustomModel> partnerCustomModels = new List<PartnerCustomModel>();
            //partnerCustomModels.Add(partnerCustomModel);
            PartnerCustomModel pcm = new PartnerCustomModel();
            List<PartnerCustomModel> pcml = new List<PartnerCustomModel>();
            //pcml.Add(pcm);
            this.SelectListPartners = new SelectList(pcml, "PartnerId", "PartnerFullName", "Category",-1);

            //this.SelectListPartners = new SelectList(partnerCustomModels, "PartnerId", "PartnerFullName", "Category", 0);
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