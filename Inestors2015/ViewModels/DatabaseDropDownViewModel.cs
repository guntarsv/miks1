using Investors2015.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Investors2015.ViewModels
{
    public class DatabaseDropDownViewModel
    {
        public SelectList SelectListDatabases
        {
            get;
            set;
        }

        public string SelectedDatabaseName
        {
            get;
            set;
        }

        public string SelectedDatabaseId
        {
            get;
            set;
        }

        public string SelectedDatabaseText
        {
            get;
            set;
        }

        public DatabaseDropDownViewModel()
        {
        }

        public DatabaseDropDownViewModel(List<Datubaze> datubazeObjectsList)
        {
            this.SelectListDatabases = new SelectList(datubazeObjectsList, "Id", "Name", "Category", 2);
        }
    }
}
