using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Investors2015.Models;
using Investors2015.ViewModels;

namespace Investors2015.ViewModels
{
    public class DataEntryIndexViewModel
    {
        public DatabaseDropDownViewModel DatabaseDropDownViewModel { get; set; }
        //public PartnerCustomModel PartnerCustomModel { get; set; }

        //public string SelectedDatabase { get; set; }

        //public string SelectedPartner { get; set; }

        public PartnerDropDownViewModel PartnerDropDownViewModel { get; set; }

    }
}